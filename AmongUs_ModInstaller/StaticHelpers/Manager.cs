using AmongUs_ModInstaller.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace AmongUs_ModInstaller
{
    static class Manager
    {
        public static ModInstallation InstallMod(Properties.Settings settings, ModInfo modInfo, MainForm mainForm)
        {
            DialogForm df = new DialogForm("Installing...", "Downloading and installing mods...");
            df.Size = new System.Drawing.Size(250, 72);
            df.Show(mainForm);

            ModInstallation modInstallation = new ModInstallation(modInfo, CreateModFolder(settings));

            CopyBaseGame(modInstallation, settings);

            string tempZipFile = DownloadZip(modInfo);

            ExtractZip(modInstallation, tempZipFile);
            File.Delete(tempZipFile);

            df.Close();

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

        private static string DownloadZip(ModInfo modInfo)
        {
            
            string tempFile = Path.GetTempFileName();
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Automated Among Us Mod Installer (AAMI) for GitHub repositories");

                //Download JSON for release from GitHub first
                string json = wc.DownloadString(modInfo.permanentAPIURL);
                string zipDownloadURL = JObject.Parse(json)["assets"][0]["browser_download_url"].ToString();

                //Now download zip that's specified for release to temporary file
                wc.DownloadFile(zipDownloadURL, tempFile);
            }

            return tempFile;
        }

        private static void ExtractZip(ModInstallation modInstallation, String tempFile)
        {
            ZipFile.ExtractToDirectory(tempFile, modInstallation.absolutePath, true);
        }

        public static void UninstallMod(Properties.Settings settings, ModInstallation modInstallation)
        {
            Directory.Delete(modInstallation.absolutePath, true);
        }

        public static void LaunchGame(Properties.Settings settings, ModInstallation modInstallation, MainForm mainForm)
        {
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
    }
}
