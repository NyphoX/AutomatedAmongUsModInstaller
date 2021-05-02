using Gameloop.Vdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            string steamLibraryFoldersFile = System.IO.Path.Combine(steamPath, defaultSettings.SteamLibraryFoldersFileSublocation);

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

                defaultSettings.AmongUsGameFullPath = System.IO.Path.Combine(path, defaultSettings.SteamCommonFolderSubpath, defaultSettings.AmongUsGameFolderName);
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

            defaultSettings.AAMIModdingFullPath = System.IO.Path.Combine(System.IO.Directory.GetParent(defaultSettings.AmongUsGameFullPath).FullName, defaultSettings.AAMIModdingFolderName);
            defaultSettings.IsAAMIModdingFullPathSet = true;
            defaultSettings.Save();

            if (!System.IO.Directory.Exists(defaultSettings.AAMIModdingFullPath))
                System.IO.Directory.CreateDirectory(defaultSettings.AAMIModdingFullPath);
        }

        private static void LoadModInstallations()
        {
            modInstallations = JsonConvert.DeserializeObject<List<ModInstallation>>(defaultSettings.ModInstallations);

            if (modInstallations == null)
                modInstallations = new List<ModInstallation>();
        }

        private static void DownloadJSONModList()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");
                string json = wc.DownloadString(defaultSettings.JSONModlistURL);
                modInfos = JsonConvert.DeserializeObject<List<ModInfo>>(json);

                if (modInfos == null)
                    modInfos = new List<ModInfo>();
            }
        }

        private static void RemoveUnsupportedModsFromModList()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version AAMIversion = new Version(FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion);

            bool newerClientRequired = false;
            List<ModInfo> modsToRemove = new List<ModInfo>();
            foreach (ModInfo modInfo in modInfos)
            {
                Version modVersion = new Version(modInfo.AAMIversion);
                if (AAMIversion.CompareTo(modVersion) < 0)
                {
                    newerClientRequired = true;
                    modsToRemove.Add(modInfo);
                }
            }

            foreach (ModInfo modInfo in modsToRemove)
                modInfos.Remove(modInfo);

            if (newerClientRequired)
            {
                if (MessageBox.Show("There are new mods, that can now be managed by AAMI.\n\n" +
                    "In order for AAMI to manage these new mods for you, you must first update to the newest AAMI client.\n\n" + 
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

            DownloadJSONModList();
            RemoveUnsupportedModsFromModList();
        }
    }
}
