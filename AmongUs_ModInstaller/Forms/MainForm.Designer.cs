
namespace AmongUs_ModInstaller
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxGamePath = new System.Windows.Forms.TextBox();
            this.listBoxAvailableMods = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxInstalledMods = new System.Windows.Forms.ListBox();
            this.btnInstallMod = new System.Windows.Forms.Button();
            this.btnUninstallMod = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnOpenGamePath = new System.Windows.Forms.Button();
            this.btnOpenModPath = new System.Windows.Forms.Button();
            this.txtBoxModPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPlayVanilla = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 101;
            this.label1.Text = "Game Path:";
            // 
            // txtBoxGamePath
            // 
            this.txtBoxGamePath.Enabled = false;
            this.txtBoxGamePath.Location = new System.Drawing.Point(86, 6);
            this.txtBoxGamePath.Name = "txtBoxGamePath";
            this.txtBoxGamePath.ReadOnly = true;
            this.txtBoxGamePath.Size = new System.Drawing.Size(405, 23);
            this.txtBoxGamePath.TabIndex = 1;
            // 
            // listBoxAvailableMods
            // 
            this.listBoxAvailableMods.FormattingEnabled = true;
            this.listBoxAvailableMods.ItemHeight = 15;
            this.listBoxAvailableMods.Location = new System.Drawing.Point(12, 157);
            this.listBoxAvailableMods.Name = "listBoxAvailableMods";
            this.listBoxAvailableMods.Size = new System.Drawing.Size(256, 274);
            this.listBoxAvailableMods.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 100;
            this.label2.Text = "Available Mods:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 15);
            this.label3.TabIndex = 101;
            this.label3.Text = "Installed Mods:";
            // 
            // listBoxInstalledMods
            // 
            this.listBoxInstalledMods.FormattingEnabled = true;
            this.listBoxInstalledMods.ItemHeight = 15;
            this.listBoxInstalledMods.Location = new System.Drawing.Point(299, 157);
            this.listBoxInstalledMods.Name = "listBoxInstalledMods";
            this.listBoxInstalledMods.Size = new System.Drawing.Size(256, 274);
            this.listBoxInstalledMods.TabIndex = 5;
            // 
            // btnInstallMod
            // 
            this.btnInstallMod.Location = new System.Drawing.Point(12, 80);
            this.btnInstallMod.Name = "btnInstallMod";
            this.btnInstallMod.Size = new System.Drawing.Size(256, 48);
            this.btnInstallMod.TabIndex = 2;
            this.btnInstallMod.Text = "Install Mod";
            this.btnInstallMod.UseVisualStyleBackColor = true;
            this.btnInstallMod.Click += new System.EventHandler(this.btnInstallMod_Click);
            // 
            // btnUninstallMod
            // 
            this.btnUninstallMod.Location = new System.Drawing.Point(299, 80);
            this.btnUninstallMod.Name = "btnUninstallMod";
            this.btnUninstallMod.Size = new System.Drawing.Size(256, 48);
            this.btnUninstallMod.TabIndex = 3;
            this.btnUninstallMod.Text = "Uninstall Mod";
            this.btnUninstallMod.UseVisualStyleBackColor = true;
            this.btnUninstallMod.Click += new System.EventHandler(this.btnUninstallMod_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(299, 437);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(256, 48);
            this.btnPlay.TabIndex = 7;
            this.btnPlay.Text = "Play Mod";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnOpenGamePath
            // 
            this.btnOpenGamePath.Location = new System.Drawing.Point(497, 6);
            this.btnOpenGamePath.Name = "btnOpenGamePath";
            this.btnOpenGamePath.Size = new System.Drawing.Size(58, 23);
            this.btnOpenGamePath.TabIndex = 0;
            this.btnOpenGamePath.Text = "Open";
            this.btnOpenGamePath.UseVisualStyleBackColor = true;
            this.btnOpenGamePath.Click += new System.EventHandler(this.btnOpenGamePath_Click);
            // 
            // btnOpenModPath
            // 
            this.btnOpenModPath.Location = new System.Drawing.Point(497, 35);
            this.btnOpenModPath.Name = "btnOpenModPath";
            this.btnOpenModPath.Size = new System.Drawing.Size(58, 23);
            this.btnOpenModPath.TabIndex = 1;
            this.btnOpenModPath.Text = "Open";
            this.btnOpenModPath.UseVisualStyleBackColor = true;
            this.btnOpenModPath.Click += new System.EventHandler(this.btnOpenModPath_Click);
            // 
            // txtBoxModPath
            // 
            this.txtBoxModPath.Enabled = false;
            this.txtBoxModPath.Location = new System.Drawing.Point(86, 35);
            this.txtBoxModPath.Name = "txtBoxModPath";
            this.txtBoxModPath.ReadOnly = true;
            this.txtBoxModPath.Size = new System.Drawing.Size(405, 23);
            this.txtBoxModPath.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 104;
            this.label4.Text = "Mod Path:";
            // 
            // btnPlayVanilla
            // 
            this.btnPlayVanilla.Location = new System.Drawing.Point(12, 437);
            this.btnPlayVanilla.Name = "btnPlayVanilla";
            this.btnPlayVanilla.Size = new System.Drawing.Size(256, 48);
            this.btnPlayVanilla.TabIndex = 6;
            this.btnPlayVanilla.Text = "Play Vanilla";
            this.btnPlayVanilla.UseVisualStyleBackColor = true;
            this.btnPlayVanilla.Click += new System.EventHandler(this.btnPlayVanilla_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 495);
            this.Controls.Add(this.btnPlayVanilla);
            this.Controls.Add(this.btnOpenModPath);
            this.Controls.Add(this.txtBoxModPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOpenGamePath);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnUninstallMod);
            this.Controls.Add(this.btnInstallMod);
            this.Controls.Add(this.listBoxInstalledMods);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxAvailableMods);
            this.Controls.Add(this.txtBoxGamePath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AAMI - Automated Among Us Mod Installer";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxGamePath;
        private System.Windows.Forms.ListBox listBoxAvailableMods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxInstalledMods;
        private System.Windows.Forms.Button btnInstallMod;
        private System.Windows.Forms.Button btnUninstallMod;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnOpenGamePath;
        private System.Windows.Forms.Button btnOpenModPath;
        private System.Windows.Forms.TextBox od;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxModPath;
        private System.Windows.Forms.Button btnPlayVanilla;
    }
}

