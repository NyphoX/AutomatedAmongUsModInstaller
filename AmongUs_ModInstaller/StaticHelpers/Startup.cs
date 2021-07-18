using AmongUs_ModInstaller.Forms;
using Gameloop.Vdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace AmongUs_ModInstaller
{
    static class Startup
    {
        private static Properties.Settings defaultSettings = Properties.Settings.Default;
        private static List<ModInfo> modInfos;
        private static List<ModInstallation> modInstallations;

        private static bool isFreshInstall = false;
        private static bool isOffline = true;

        public static Properties.Settings GetSettings()
        {
            return defaultSettings;
        }

        public static List<ModInfo> GetModInfos()
        {
            return modInfos;
        }

        public static List<ModInstallation> GetModInstallations()
        {
            return modInstallations;
        }

        public static bool IsOffline()
        {
            return isOffline;
        }

        private static void GetOSArchitecture()
        {
            if (defaultSettings.IsCPUArchitectureSet)
                return;

            //Check if system is 64 bit for registry lookup
            defaultSettings.IsCPUArchitecture64Bit = Environment.Is64BitOperatingSystem;
            defaultSettings.IsCPUArchitectureSet = true;
            defaultSettings.Save();
        }

        private static void GetSteamSetupFromRegistry()
        {
            if (!defaultSettings.IsCPUArchitectureSet)
                return;

            if (defaultSettings.IsGamePathSet)
                return;

            //Get Steam installation folder from registry depending on OS architecture
            string registryPath = (defaultSettings.IsCPUArchitecture64Bit ? defaultSettings.SteamRegistryPathLocalMachine64Bit : defaultSettings.SteamRegistryPathLocalMachine32Bit);
            object registryValue = Microsoft.Win32.Registry.GetValue(registryPath, defaultSettings.SteamRegistryKeyNameInstallPath, null);

            if (registryValue == null)
                return;

            //Get all steam library folders (can be multiple) from libraryfolders.vdf in Steam's main install location
            string steamPath = (string)registryValue;
            List<string> libraryPaths = new List<string>();
            libraryPaths.Add(steamPath);

            string steamLibraryFoldersFile = Path.Combine(steamPath, defaultSettings.SteamLibraryFoldersFileSublocation);

            Gameloop.Vdf.Linq.VProperty libraries = VdfConvert.Deserialize(System.IO.File.ReadAllText(steamLibraryFoldersFile));
            foreach (Gameloop.Vdf.Linq.VProperty vProperty in libraries.Value)
                if (Int32.TryParse(vProperty.Key, out _))
                    libraryPaths.Add(vProperty.Value.ToString());

            //Scan each of the library locations for the game manifest file of Among Us (Steam game directory where Among Us is installed)
            foreach (string path in libraryPaths)
            {
                string gameAppManifest = System.IO.Path.Combine(path, defaultSettings.AmongUsAppManifestSublocation);
                if (!System.IO.File.Exists(gameAppManifest))
                    continue;

                defaultSettings.AmongUsGameFullPath = Path.Combine(path, defaultSettings.SteamCommonFolderSubpath, defaultSettings.AmongUsGameFolderName);
                defaultSettings.IsGamePathSet = true;
                defaultSettings.Save();
                break;
            }
        }

        private static void PrepareModdingDirectory()
        {
            if (!defaultSettings.IsGamePathSet)
                return;

            if (defaultSettings.IsAAMIModdingFullPathSet)
                return;

            isFreshInstall = true;

            defaultSettings.AAMIModdingFullPath = Path.Combine(Directory.GetParent(defaultSettings.AmongUsGameFullPath).FullName, defaultSettings.AAMIModdingFolderName);
            defaultSettings.IsAAMIModdingFullPathSet = true;
            defaultSettings.Save();

            //Create folder if it doesn't exist. If it exists and it's a fresh install, delete old folders from previous version (can't use them anyway)
            if (!Directory.Exists(defaultSettings.AAMIModdingFullPath))
                Directory.CreateDirectory(defaultSettings.AAMIModdingFullPath);
            else if (isFreshInstall)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(defaultSettings.AAMIModdingFullPath);

                foreach (FileInfo file in dirInfo.GetFiles())
                    file.Delete();

                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                    dir.Delete(true);
            }
        }

        private static void LoadModInstallations()
        {
            modInstallations = JsonConvert.DeserializeObject<List<ModInstallation>>(defaultSettings.ModInstallations);

            if (modInstallations == null)
                modInstallations = new List<ModInstallation>();
        }

        private static bool UpdateAAMI(MainForm mainForm)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version AAMIversion = new Version(FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion);

            try
            {
                using (WebClient wc = new WebClient())
                {
                    //Check if there is an update available and get information from JSON
                    wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");
                    string jsonRelease = wc.DownloadString(defaultSettings.JSONLatestAAMIRelease);
                    JToken jToken = JObject.Parse(jsonRelease);
                    Version newAAMIVersion = new Version(jToken["tag_name"].ToString()[1..]);

                    if (AAMIversion.CompareTo(newAAMIVersion) >= 0)
                        return true;

                    string zipDownloadURL = jToken["assets"][defaultSettings.IsCPUArchitecture64Bit ? 0 : 1]["browser_download_url"].ToString();

                    //Ask user if he wants to update
                    if (MessageBox.Show("A new version of AAMI is available for download. " +
                        "These updates include possible bugfixes for mod installation, quality of life improvements and support for a multitide of new mods.\n\n" +
                        "Do you want to automatically update to the newest release?",
                        "Update available for AAMI client", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        return true;

                    //Show info window that we are doing something in the background...
                    DialogForm df = new DialogForm("Updating AAMI...", "Downloading and installing new version of AAMI...", false);
                    df.Show(mainForm);

                    //Download new version
                    string tempFile = Path.GetTempFileName();
                    wc.DownloadFile(zipDownloadURL, tempFile);

                    //Rename the currently executing assembly
                    string currentExeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    string currentExePath = Directory.GetFiles(currentExeDir, "*.exe", SearchOption.TopDirectoryOnly)[0];
                    string oldExePath = currentExePath + ".old";
                    File.Delete(oldExePath);
                    File.Move(currentExePath, oldExePath);

                    //Extract zip and rename new exe to whatever the user named his file
                    ZipFile.ExtractToDirectory(tempFile, currentExeDir, true);
                    File.Delete(tempFile);
                    File.Move(Path.Combine(currentExeDir, defaultSettings.IsCPUArchitecture64Bit ? "AAMI_x64.exe" : "AAMI_x86.exe"), currentExePath);

                    //Start the new client
                    Process.Start(new ProcessStartInfo("cmd.exe", "/C \"" + currentExePath + "\"")
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    });
                }
                Environment.Exit(0);
            }
            catch (Exception)
            {
                if (MessageBox.Show("AAMI was unable to download the current mod list from GitHub. This may be due to your internet connection or a problem with GitHub itself.\n\n" +
                    "You may use AAMI in \"offline mode\" to start the game with or without mods, but will be unable to update or install new mods. Do you want to continue in \"offline mode\"?",
                    "Unable to download mod list", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    Environment.Exit(0);

                return false;
            }

            return false;
        }

        private static void DownloadJSONModList()
        {
            modInfos = new List<ModInfo>();

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");
                string json = wc.DownloadString(defaultSettings.JSONModlistURL);
                modInfos = JsonConvert.DeserializeObject<List<ModInfo>>(json);
            }
        }

        private static void RemoveUnsupportedModsFromModList()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version AAMIversion = new Version(FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion);

            List<ModInfo> modsToRemove = new List<ModInfo>();
            foreach (ModInfo modInfo in modInfos)
            {
                Version modVersion = new Version(modInfo.AAMIversion);
                if (AAMIversion.CompareTo(modVersion) < 0)
                    modsToRemove.Add(modInfo);
            }

            foreach (ModInfo modInfo in modsToRemove)
                modInfos.Remove(modInfo);
        }

        private static bool SteamRegistryForLaunchCheckExists()
        {
            //Check Steam's "ActiveProcess"-key which seems independent from OS architecture
            object pid = Microsoft.Win32.Registry.GetValue(defaultSettings.SteamRegistryPathCurrentUserActiveProcess, defaultSettings.SteamRegistryKeyNamePID, null);
            object activeUser= Microsoft.Win32.Registry.GetValue(defaultSettings.SteamRegistryPathCurrentUserActiveProcess, defaultSettings.SteamRegistryKeyNamePID, null);
            object steamExe = Microsoft.Win32.Registry.GetValue(defaultSettings.SteamRegistryPathCurrentUser, defaultSettings.SteamRegistryKeyNameSteamExe, null);

            if (pid == null || activeUser == null || steamExe == null)
                return false;

            return true;
        }

        public static void LoadSettings(MainForm mainForm)
        {
            //First check if this is an upgrade from old AAMI, then we can go on and actually check the settings.
            if (defaultSettings.AAMISettingsUpgradeRequired)
            {
                defaultSettings.Upgrade();
                defaultSettings.AAMISettingsUpgradeRequired = false;
                defaultSettings.Save();
            }

            GetOSArchitecture();
            GetSteamSetupFromRegistry();

            if (!defaultSettings.IsGamePathSet || !SteamRegistryForLaunchCheckExists())
                throw new Exception("Problems detected with registry or with AAMI settings.");

            PrepareModdingDirectory();
            LoadModInstallations();

            isOffline = !UpdateAAMI(mainForm);
            if (!isOffline)
            {
                DownloadJSONModList();
                RemoveUnsupportedModsFromModList();
            }
        }
    }
}
