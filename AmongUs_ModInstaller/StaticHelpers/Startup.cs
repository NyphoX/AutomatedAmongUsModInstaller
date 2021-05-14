using Gameloop.Vdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            string registryPath = (defaultSettings.IsCPUArchitecture64Bit ? defaultSettings.SteamRegistryPath64Bit : defaultSettings.SteamRegistryPath32Bit);
            object registryValue = Microsoft.Win32.Registry.GetValue(registryPath, defaultSettings.SteamRegistryKeyName, null);

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

        private static bool DownloadJSONModList()
        {
            modInfos = new List<ModInfo>();

            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");
                    string json = wc.DownloadString(defaultSettings.JSONModlistURL);
                    modInfos = JsonConvert.DeserializeObject<List<ModInfo>>(json);
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show("AAMI was unable to download the current mod list from GitHub. This may be due to your internet connection or a problem with GitHub itself.\n\n" +
                    "You may use AAMI in \"offline mode\" to start the game with or without mods, but will be unable to update or install new mods. Do you want to continue in \"offline mode\"?",
                    "Unable to download mod list", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    Environment.Exit(0);

                return false;
            }

            return true;
        }

        private static void RemoveUnsupportedModsFromModList()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version AAMIversion = new Version(FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion);

            bool newerClientRequired = false;
            Version minimumVersion = new Version("0.0.0");
            List<ModInfo> modsToRemove = new List<ModInfo>();
            foreach (ModInfo modInfo in modInfos)
            {
                Version modVersion = new Version(modInfo.AAMIversion);
                if (AAMIversion.CompareTo(modVersion) < 0)
                {
                    newerClientRequired = true;
                    minimumVersion = (minimumVersion.CompareTo(modVersion) < 0) ? modVersion : minimumVersion;
                    modsToRemove.Add(modInfo);
                }
            }

            foreach (ModInfo modInfo in modsToRemove)
                modInfos.Remove(modInfo);

            if (newerClientRequired)
            {
                if (MessageBox.Show("There are (new) mods, that can now be (better) managed by the AAMI client.\n\n" +
                    "In order for AAMI to manage mods for you without any problems, you must first update to the newest AAMI client. The minimum version to support all mods is AAMI v" + minimumVersion + "\n\n" + 
                    "Do you want to go to the GitHub-release website now?", 
                    "New AAMI client available", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", "https://github.com/NyphoX/AutomatedAmongUsModInstaller/releases/latest");
                    Environment.Exit(0);
                }
            }
        }

        public static void LoadSettings()
        {
            GetOSArchitecture();
            GetSteamSetupFromRegistry();

            if (!defaultSettings.IsGamePathSet)
                Environment.Exit(0);

            PrepareModdingDirectory();
            LoadModInstallations();

            isOffline = !DownloadJSONModList();
            if (!isOffline)
                RemoveUnsupportedModsFromModList();
        }
    }
}
