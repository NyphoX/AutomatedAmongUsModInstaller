using AmongUs_ModInstaller.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AmongUs_ModInstaller
{
    static class Manager
    {
        private static bool isOffline = false;

        public static void SetOfflineMode()
        {
            isOffline = true;
        }

        public static ModInstallation InstallMod(Properties.Settings settings, ModInfo modInfo, MainForm mainForm, List<ModInstallation> modInstallations)
        {
            if (isOffline)
                return null;

            DialogForm df = new DialogForm("Installing...", "Downloading and installing mod...");
            df.Size = new System.Drawing.Size(250, 72);
            df.Show(mainForm);

            //Download GitHub release file to get tag (version that is not "latest") that is saved for this installation (update-functionality)
            string jsonRelease = "";
            string tag = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");

                //Download JSON for release from GitHub first
                jsonRelease = wc.DownloadString(modInfo.permanentAPIURL);
                tag = JObject.Parse(jsonRelease)["tag_name"].ToString();
            }

            ModInstallation modInstallation = new ModInstallation(modInfo, CreateModFolder(settings), tag);

            CopyBaseGame(modInstallation, settings);

            string tempZipFile = DownloadZip(modInfo, jsonRelease);
            if (tempZipFile.Equals(""))
            {
                Directory.Delete(modInstallation.absolutePath, true);
                df.Close();

                MessageBox.Show("The mod archive could not be located on the configured GitHub repository. " +
                    "Please check manually, if you can find an archive on the release page of the selected mod.\n\n" +
                    "If this issue persists and is not due to being offline or experiencing similar problems, please open an issue on AAMI's GitHub or the GitHub of the mod in question.",
                    "Installation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            ExtractZip(modInstallation, tempZipFile);
            File.Delete(tempZipFile);

            df.Close();

            modInstallations.Add(modInstallation);
            settings.ModInstallations = JsonConvert.SerializeObject(modInstallations);
            settings.Save();

            return modInstallation;
        }

        private static string CreateModFolder(Properties.Settings settings)
        {
            return Directory.CreateDirectory(Path.Combine(settings.AAMIModdingFullPath, Path.GetRandomFileName())).FullName;
        }

        private static void CopyBaseGame(ModInstallation modInstallation, Properties.Settings settings)
        {
            string sourcePath = settings.AmongUsGameFullPath;
            string destPath = modInstallation.absolutePath;

            //Create all directories first
            foreach (string directoryPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(directoryPath.Replace(sourcePath, destPath));

            //Now copy files
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
        }

        private static string DownloadZip(ModInfo modInfo, string jsonRelease)
        {
            string zipDownloadURL = "";
            bool isModArchiveLocationFound = false;

            JToken assets = JObject.Parse(jsonRelease)["assets"];

            if (!assets.HasValues)
                return "";

            //Try to directly access archive by assetId, check if it is an archive (zip/rar)
            if (modInfo.assetId < assets.Count())
            {
                zipDownloadURL = assets[modInfo.assetId]["browser_download_url"].ToString();
                isModArchiveLocationFound = zipDownloadURL.EndsWith(".zip") || zipDownloadURL.EndsWith(".rar");
            }

            //If assetId is incorrect or not an archive, try to find the mod (assume archive with largest size is the mod) instead of error behavior
            if (!isModArchiveLocationFound)
            {
                string currentDownloadURL;
                int largestSize, currentSize;
                largestSize = -1;

                foreach (JToken asset in assets)
                {
                    currentDownloadURL = asset["browser_download_url"].ToString();
                    currentSize = int.Parse(asset["size"].ToString());

                    if (largestSize >= currentSize || !(currentDownloadURL.EndsWith(".zip") || currentDownloadURL.EndsWith(".rar")))
                        continue;

                    zipDownloadURL = currentDownloadURL;
                    largestSize = currentSize;
                    isModArchiveLocationFound = true;
                }
            }

            if (!isModArchiveLocationFound)
                return "";

            string tempFile = Path.GetTempFileName();
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");
                wc.DownloadFile(zipDownloadURL, tempFile);
            }

            return tempFile;
        }

        private static void ExtractZip(ModInstallation modInstallation, String tempFile)
        {
            ZipFile.ExtractToDirectory(tempFile, modInstallation.absolutePath, true);
        }

        public static void UninstallMod(Properties.Settings settings, ModInstallation modInstallation, List<ModInstallation> modInstallations)
        {
            Directory.Delete(modInstallation.absolutePath, true);

            modInstallations.Remove(modInstallation);
            settings.ModInstallations = JsonConvert.SerializeObject(modInstallations);
            settings.Save();
        }

        public static void LaunchGame(Properties.Settings settings, ModInstallation modInstallation, MainForm mainForm, List<ModInstallation> modInstallations, List<ModInfo> modInfos)
        {
            if (!isOffline)
            {
                modInstallation = CheckModInstallationForUpdate(settings, modInstallation, mainForm, modInstallations, modInfos);
                if (modInstallation == null)
                    return;
            }

            Process.Start(Path.Combine(modInstallation.absolutePath, settings.AmongUsGameExeName));

            DialogForm df = new DialogForm("Starting game...", "The modded game has been started, please be patient.\n\n" + 
                "This may take anywhere from a few seconds up to a \nfew minutes, depending on the installed mod and \nyour hardware.\n\n" + 
                "This window will close itself after 10 seconds.");
            
            df.Show(mainForm);
            df.Size = new System.Drawing.Size(330, 165);
            df.Update();//Windows should display the text in the new form. BEFORE putting everything to sleep...
            System.Threading.Thread.Sleep(10000);
            df.Close();
        }

        private static ModInstallation CheckModInstallationForUpdate(Properties.Settings settings, ModInstallation modInstallation, MainForm mainForm, List<ModInstallation> modInstallations, List<ModInfo> modInfos)
        {
            //Check if there was a change in tag_name (an update) and offer user to automatically update
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");

                string jsonRelease = wc.DownloadString(modInstallation.modInfo.permanentAPIURL);
                string newTag = JObject.Parse(jsonRelease)["tag_name"].ToString();

                if (newTag.Equals(modInstallation.installedTag))
                    return modInstallation;

                //Ask user if he wants to update
                if (MessageBox.Show("There was a new version (" + newTag + ") found for the selected mod.\nDo you want AAMI to automatically update the mod for you?\n\nYour mod settings will be saved.", "New mod version found", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    return modInstallation;

                //If mod info is removed from available list (client is not up to date!), don't update, show message and start non-updated game normally
                ModInfo newModInfo = GetNewerModInfo(modInfos, modInstallation.modInfo);
                if (newModInfo == null)
                {
                    MessageBox.Show("Could not update game", "AAMI could not update the mod you selected, as it is no longer available for your version of the AAMI client.\n\nYou should update your AAMI installation.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return modInstallation;
                }

                List<Tuple<string, string>> savedConfigFiles = SaveModConfigurationFiles(modInstallation);
                UninstallMod(settings, modInstallation, modInstallations);

                modInstallation = InstallMod(settings, newModInfo, mainForm, modInstallations);
                if (modInstallation == null)
                    return null;

                RestoreModConfigurationFiles(modInstallation, savedConfigFiles);

                mainForm.UpdateInstalledListBox();

                return modInstallation;
            }
        }

        private static ModInfo GetNewerModInfo(List<ModInfo> modInfos, ModInfo modInfo)
        {
            foreach (ModInfo newModInfo in modInfos)
                if (newModInfo.Equals(modInfo))
                    return newModInfo;

            return null;
        }

        private static List<Tuple<string, string>> SaveModConfigurationFiles(ModInstallation modInstallation)
        {
            List<Tuple<string, string>> savedConfigFiles = new List<Tuple<string, string>>();
            foreach (string configFileSubPath in modInstallation.modInfo.configFiles)
            {
                string configFilePath = Path.Combine(modInstallation.absolutePath, configFileSubPath);
                if (!File.Exists(configFilePath))
                    continue;

                string tempFile = Path.GetTempFileName();
                File.Copy(configFilePath, tempFile, true);
                savedConfigFiles.Add(new Tuple<string, string>(configFileSubPath, tempFile));
            }

            return savedConfigFiles;
        }

        private static void RestoreModConfigurationFiles(ModInstallation modInstallation, List<Tuple<string, string>> savedConfigFiles)
        {
            foreach (Tuple<string, string> tuple in savedConfigFiles)
            {
                string configFileSubPath = tuple.Item1;
                string tempFile = tuple.Item2;
                string combinedPath = Path.Combine(modInstallation.absolutePath, configFileSubPath);
                Directory.CreateDirectory(Path.GetDirectoryName(combinedPath));
                File.Copy(tempFile, Path.Combine(combinedPath), true);
                File.Delete(tempFile);
            }
        }
    }
}
