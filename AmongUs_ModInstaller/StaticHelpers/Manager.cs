using AmongUs_ModInstaller.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace AmongUs_ModInstaller
{
    static class Manager
    {
        public static ModInstallation InstallMod(Properties.Settings settings, ModInfo modInfo, MainForm mainForm, List<ModInstallation> modInstallations)
        {
            DialogForm df = new DialogForm("Installing...", "Downloading and installing mod...");
            df.Size = new System.Drawing.Size(250, 72);
            df.Show(mainForm);

            //Download GitHub release file, to get tag (version that is not "latest") that is saved for this installation (update-functionality)
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
            
            string tempFile = Path.GetTempFileName();
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");

                //Download zip that's specified for release to temporary file
                string zipDownloadURL = JObject.Parse(jsonRelease)["assets"][modInfo.assetId]["browser_download_url"].ToString();
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

        public static void LaunchGame(Properties.Settings settings, ModInstallation modInstallation, MainForm mainForm, List<ModInstallation> modInstallations)
        {
            modInstallation = CheckModInstallationForUpdate(settings, modInstallation, mainForm, modInstallations);

            Process.Start(Path.Combine(modInstallation.absolutePath, settings.AmongUsGameExeName));

            DialogForm df = new DialogForm("Starting game...", "The modded game has been started, please be patient.\n\n" + 
                "This may take anywhere from a few seconds up to a \nfew minutes, depending on the installed mod and \nyour hardware.\n\n" + 
                "This window will close itself after 15 seconds.");
            
            df.Show(mainForm);
            df.Size = new System.Drawing.Size(330, 165);
            df.Update();//Hack, so Windows actually displays the text in the new form before putting everything to sleep...
            System.Threading.Thread.Sleep(15000);
            df.Close();
        }

        private static ModInstallation CheckModInstallationForUpdate(Properties.Settings settings, ModInstallation modInstallation, MainForm mainForm, List<ModInstallation> modInstallations)
        {
            //Check if there was a change in tag_name (an update) and offer to user to automatically update
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");

                string jsonRelease = wc.DownloadString(modInstallation.modInfo.permanentAPIURL);
                string tag = JObject.Parse(jsonRelease)["tag_name"].ToString();

                if (tag.Equals(modInstallation.installedTag))
                    return modInstallation;

                //Ask user if he wants to update
                if (MessageBox.Show("There was a new version found for the selected mod.\nDo you want AAMI to automatically update the mod for you?", "New mod version found", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    return modInstallation;

                UninstallMod(settings, modInstallation, modInstallations);
                modInstallation = InstallMod(settings, modInstallation.modInfo, mainForm, modInstallations);

                mainForm.UpdateInstalledListBox();

                return modInstallation;
            }
        }
    }
}
