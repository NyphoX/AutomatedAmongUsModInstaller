using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AmongUs_ModInstaller
{
    public partial class MainForm : Form
    {
        private Properties.Settings settings;
        private List<ModInfo> modInfos;
        private List<ModInstallation> modInstallations;

        public MainForm()
        {
            InitializeComponent();
        }

        public void UpdateInstalledListBox()
        {
            listBoxInstalledMods.Items.Clear();
            listBoxInstalledMods.Items.AddRange(modInstallations.ToArray());

            int cnt = modInstallations.Count;

            if (cnt != 0)
                listBoxInstalledMods.SelectedItem = listBoxInstalledMods.Items[0];

            listBoxInstalledMods.Update();
        }

        private void UpdateAvailableListBox()
        {
            listBoxAvailableMods.Items.Clear();
            listBoxAvailableMods.Items.AddRange(modInfos.ToArray());

            int cnt = modInfos.Count;

            if (cnt != 0)
                listBoxAvailableMods.SelectedItem = listBoxAvailableMods.Items[0];
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private void MainForm_Shown(object sender, EventArgs e)
        {
            HideCaret(txtBoxGamePath.Handle);
            HideCaret(txtBoxModPath.Handle);

            Startup.LoadSettings();

            settings = Startup.GetSettings();
            modInfos = Startup.GetModInfos();
            modInstallations = Startup.GetModInstallations();

            txtBoxGamePath.Text = settings.AmongUsGameFullPath;
            txtBoxModPath.Text = settings.AAMIModdingFullPath;

            UpdateInstalledListBox();
            UpdateAvailableListBox();
        }

        private void btnInstallMod_Click(object sender, EventArgs e)
        {
            //Get selection
            if (!(listBoxAvailableMods.SelectedItem is ModInfo selection))
                return;

            //Check if it's already installed
            foreach (ModInstallation modInstallation in modInstallations)
                if (modInstallation.modInfo.Equals(selection))
                {
                    MessageBox.Show("This mod is already installed.\nSelect it under \"Installed Mods\" and click \"Play Mod\".\n\n" + 
                        "If this is the \"latest\" version of a mod and there was an update or if Among Us itself was updated, uninstall the mod and install it again.", 
                        "Mod already installed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            Manager.InstallMod(settings, selection, this, modInstallations);

            UpdateInstalledListBox();
        }

        private void btnUninstallMod_Click(object sender, EventArgs e)
        {
            //Get selection
            if (!(listBoxInstalledMods.SelectedItem is ModInstallation selection))
                return;

            //Make sure it's installed
            bool skip = true;
            foreach (ModInstallation modInstallation in modInstallations)
                skip &= !modInstallation.modInfo.Equals(selection.modInfo);

            if (skip)
                return;//TODO: check if this can ever happen

            Manager.UninstallMod(settings, selection, modInstallations);

            UpdateInstalledListBox();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //Get selection
            if (!(listBoxInstalledMods.SelectedItem is ModInstallation selection))
                return;

            //Make sure it's installed
            bool skip = true;
            foreach (ModInstallation modInstallation in modInstallations)
                skip &= !modInstallation.modInfo.Equals(selection.modInfo);

            if (skip)
                return;//TODO: check if this can ever happen

            Manager.LaunchGame(settings, selection, this, modInstallations);
        }

        private void btnOpenGamePath_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", settings.AmongUsGameFullPath);
        }

        private void btnOpenModPath_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", settings.AAMIModdingFullPath);
        }

        private void btnPlayVanilla_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(settings.AmongUsGameFullPath, settings.AmongUsGameExeName));
        }
    }
}
