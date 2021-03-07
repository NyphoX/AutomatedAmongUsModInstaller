using Gameloop.Vdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace AmongUs_ModInstaller
{
    public partial class MainForm : Form
    {
        Properties.Settings defaultSettings = Properties.Settings.Default;
        Dictionary<string, ModInfo> mods = new Dictionary<string, ModInfo>();

        //TODO:
        // Reset for saved settings (basically everything that's stored in settings)
        // button to start among us (with installed mods)
        // button to start among us WITHOUT mods (automatically remove all mods and start)
        // red/green/yellow indicators for not installed/installed/working

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckSettings();
            LoadData();
            InitializeGUI();
        }

        private void CheckSettings()
        {
            //Check if system is 64 bit for registry lookup
            bool is64Bit;
            if (!(bool)defaultSettings["IsCPUArchitectureSet"])
            {
                is64Bit = Environment.Is64BitOperatingSystem;
                defaultSettings["IsCPUArchitecture64Bit"] = is64Bit;
                defaultSettings["IsCPUArchitectureSet"] = true;
                defaultSettings.Save();
            }
            else
                is64Bit = (bool)defaultSettings["IsCPUArchitecture64Bit"];

            if (!(bool)defaultSettings["IsGamePathSet"])
            {
                //Get Steam installation folder from registry (or from user, if registry is not set up)
                string gamePath = "";
                string registryPath = (is64Bit ? (string)defaultSettings["SteamRegistryPath64Bit"] : (string)defaultSettings["SteamRegistryPath32Bit"]);
                string registryKey = (string)defaultSettings["SteamRegistryKeyName"];
                object registryValue = Microsoft.Win32.Registry.GetValue(registryPath, registryKey, null);
                if (registryValue != null)
                {
                    string steamPath = (string)registryValue;

                    //Get all steam library folders (can be multiple)
                    List<string> libraryPaths = new List<string>();
                    libraryPaths.Add(steamPath);
                    string steamLibraryFoldersFile = System.IO.Path.Combine(steamPath, (string)defaultSettings["SteamLibraryFoldersFileSublocation"]);
                    Gameloop.Vdf.Linq.VProperty libraries = VdfConvert.Deserialize(System.IO.File.ReadAllText(steamLibraryFoldersFile));
                    foreach (Gameloop.Vdf.Linq.VProperty vProperty in libraries.Value)
                    {
                        if (Int32.TryParse(vProperty.Key, out _))
                        {
                            libraryPaths.Add(vProperty.Value.ToString());
                        }
                    }

                    //Scan each of the library locations for the game manifest file of Among Us
                    foreach (string path in libraryPaths)
                    {
                        string gameAppManifest = System.IO.Path.Combine(path, (string)defaultSettings["AmongUsAppManifestSublocation"]);
                        if (!System.IO.File.Exists(gameAppManifest))
                            continue;

                        gamePath = System.IO.Path.Combine(path, (string)defaultSettings["GameSublocation"]);
                        defaultSettings["GamePath"] = gamePath;
                        defaultSettings["IsGamePathSet"] = true;
                        defaultSettings.Save();
                        break;
                    }
                }

                //Ask user if our logic failed (last resort)
                if (gamePath == "")
                {
                    gamePath = GetGameLocationFromUser();
                    if (gamePath != "")
                    {
                        defaultSettings["GamePath"] = gamePath;
                        defaultSettings["IsGamePathSet"] = true;
                        defaultSettings.Save();
                    }
                }
            }
        }

        private string GetGameLocationFromUser()
        {
            //TODO: ask for manual selection of among us.exe in filechoosedialog
            //TODO: do sanity checks, Environment.Exit(0) if the input doesn't work
            Environment.Exit(0);

            return "";
        }

        private void LoadData()
        {
            mods.Add((string)defaultSettings["MOD_ExtraRoles_Name"], null);

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us mod installer for github repositories");
                List<string> keys = new List<string>(mods.Keys);
                foreach (string key in keys)
                {
                    string json = wc.DownloadString((string)defaultSettings["MOD_" + key + "_APIURL"]);
                    var data = (JObject)JsonConvert.DeserializeObject(json);
                    string newestVersion = data["tag_name"].Value<string>();
                    string zipLocation = data["assets"][0]["browser_download_url"].Value<string>();

                    if ((StringCollection)defaultSettings["MOD_" + key + "_FileList"] == null)
                    {
                        defaultSettings["MOD_" + key + "_FileList"] = new StringCollection();
                        defaultSettings["MOD_" + key + "_FolderList"] = new StringCollection();
                        defaultSettings.Save();
                    }

                    ModInfo modInfo = new ModInfo((string)defaultSettings["MOD_" + key + "_Name"], (bool)defaultSettings["MOD_" + key + "_IsInstalled"], (string)defaultSettings["MOD_" + key + "_APIURL"],
                        (string)defaultSettings["MOD_" + key + "_InstalledVersion"], (StringCollection)defaultSettings["MOD_" + key + "_FileList"], (StringCollection)defaultSettings["MOD_" + key + "_FolderList"]);

                    modInfo.SetNewestVersion(newestVersion);
                    modInfo.SetZipLocation(zipLocation);
                    modInfo.SetControls(txtBoxExtraRolesName, txtBoxExtraRolesInstalledVersion, txtBoxExtraRolesNewestVersion, btnExtraRolesInstall, btnExtraRolesRemove);

                    mods[key] = modInfo;
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private void InitializeGUI()
        {
            txtBoxGamePath.Text = (string)defaultSettings["GamePath"];
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            HideCaret(txtBoxGamePath.Handle);
        }

        private void btnExtraRolesInstall_Click(object sender, EventArgs e)
        {
            btnExtraRolesInstall.Enabled = false;

            ModInfo mod = mods[(string)defaultSettings["MOD_ExtraRoles_Name"]];
            string filename = System.IO.Path.GetFileName(mod.GetZipLocation());

            //Download zip
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(mod.GetZipLocation(), filename);
            }

            //Extract to fresh temp folder
            System.IO.Directory.CreateDirectory("temp");
            ZipFile.ExtractToDirectory(filename, "temp", true);

            //Update filelist
            System.IO.Directory.SetCurrentDirectory("temp");
            string[] modFolders = System.IO.Directory.GetDirectories(".", "", System.IO.SearchOption.AllDirectories);
            string[] modFiles = System.IO.Directory.GetFiles(".", "", System.IO.SearchOption.AllDirectories);
            StringCollection fileCollection = new StringCollection();
            StringCollection folderCollection = new StringCollection();
            folderCollection.AddRange(modFolders);
            fileCollection.AddRange(modFiles);
            mod.SetFolderList(folderCollection);
            mod.SetFileList(fileCollection);

            //Copy files into game folder
            string gamePath = (string)defaultSettings["GamePath"];
            foreach (string dir in modFolders)
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(gamePath, dir));
            foreach (string file in modFiles)
                System.IO.File.Copy(file, System.IO.Path.Combine(gamePath, file));
            System.IO.Directory.SetCurrentDirectory("..");
            System.IO.Directory.Delete("temp", true);
            mod.SetInstalledVersion(mod.GetNewestVersion());
            mod.SetIsInstalled(true);

            //Set stored settings
            defaultSettings["MOD_ExtraRoles_FileList"] = fileCollection;
            defaultSettings["MOD_ExtraRoles_FolderList"] = folderCollection;
            defaultSettings["MOD_ExtraRoles_InstalledVersion"] = mod.GetNewestVersion();
            defaultSettings["MOD_ExtraRoles_IsInstalled"] = true;

            btnExtraRolesRemove.Enabled = true;
        }

        private void btnExtraRolesRemove_Click(object sender, EventArgs e)
        {
            btnExtraRolesRemove.Enabled = false;

            ModInfo mod = mods[(string)defaultSettings["MOD_ExtraRoles_Name"]];

            //Remove files from game folder
            string currentDir = System.IO.Directory.GetCurrentDirectory();
            string gamePath = (string)defaultSettings["GamePath"];
            System.IO.Directory.SetCurrentDirectory(gamePath);
            for (int i = mod.GetFileList().Count - 1; i >= 0; i--)
                System.IO.File.Delete(mod.GetFileList()[i]);
            for (int i = mod.GetFolderList().Count - 1; i >= 0; i--)
                System.IO.Directory.Delete(mod.GetFolderList()[i], true);
            System.IO.Directory.SetCurrentDirectory(currentDir);
            mod.SetFileList(new StringCollection());
            mod.SetFolderList(new StringCollection());
            mod.SetInstalledVersion("(none)");
            mod.SetIsInstalled(false);

            //Set stored settings
            defaultSettings["MOD_ExtraRoles_FileList"] = new StringCollection();
            defaultSettings["MOD_ExtraRoles_FolderList"] = new StringCollection();
            defaultSettings["MOD_ExtraRoles_InstalledVersion"] = "(none)";
            defaultSettings["MOD_ExtraRoles_IsInstalled"] = false;

            btnExtraRolesInstall.Enabled = true;
        }
    }
}
