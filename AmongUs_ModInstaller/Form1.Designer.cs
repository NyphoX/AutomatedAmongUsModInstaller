
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxGamePath = new System.Windows.Forms.TextBox();
            this.txtBoxExtraRolesName = new System.Windows.Forms.TextBox();
            this.txtBoxExtraRolesInstalledVersion = new System.Windows.Forms.TextBox();
            this.txtBoxExtraRolesNewestVersion = new System.Windows.Forms.TextBox();
            this.btnExtraRolesInstall = new System.Windows.Forms.Button();
            this.btnExtraRolesRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Path:";
            // 
            // txtBoxGamePath
            // 
            this.txtBoxGamePath.Enabled = false;
            this.txtBoxGamePath.Location = new System.Drawing.Point(86, 6);
            this.txtBoxGamePath.Name = "txtBoxGamePath";
            this.txtBoxGamePath.ReadOnly = true;
            this.txtBoxGamePath.Size = new System.Drawing.Size(472, 23);
            this.txtBoxGamePath.TabIndex = 1;
            // 
            // txtBoxExtraRolesName
            // 
            this.txtBoxExtraRolesName.Enabled = false;
            this.txtBoxExtraRolesName.Location = new System.Drawing.Point(12, 92);
            this.txtBoxExtraRolesName.Name = "txtBoxExtraRolesName";
            this.txtBoxExtraRolesName.ReadOnly = true;
            this.txtBoxExtraRolesName.Size = new System.Drawing.Size(198, 23);
            this.txtBoxExtraRolesName.TabIndex = 2;
            // 
            // txtBoxExtraRolesInstalledVersion
            // 
            this.txtBoxExtraRolesInstalledVersion.Enabled = false;
            this.txtBoxExtraRolesInstalledVersion.Location = new System.Drawing.Point(216, 92);
            this.txtBoxExtraRolesInstalledVersion.Name = "txtBoxExtraRolesInstalledVersion";
            this.txtBoxExtraRolesInstalledVersion.ReadOnly = true;
            this.txtBoxExtraRolesInstalledVersion.Size = new System.Drawing.Size(95, 23);
            this.txtBoxExtraRolesInstalledVersion.TabIndex = 3;
            // 
            // txtBoxExtraRolesNewestVersion
            // 
            this.txtBoxExtraRolesNewestVersion.Enabled = false;
            this.txtBoxExtraRolesNewestVersion.Location = new System.Drawing.Point(317, 92);
            this.txtBoxExtraRolesNewestVersion.Name = "txtBoxExtraRolesNewestVersion";
            this.txtBoxExtraRolesNewestVersion.ReadOnly = true;
            this.txtBoxExtraRolesNewestVersion.Size = new System.Drawing.Size(95, 23);
            this.txtBoxExtraRolesNewestVersion.TabIndex = 4;
            // 
            // btnExtraRolesInstall
            // 
            this.btnExtraRolesInstall.Location = new System.Drawing.Point(418, 92);
            this.btnExtraRolesInstall.Name = "btnExtraRolesInstall";
            this.btnExtraRolesInstall.Size = new System.Drawing.Size(67, 23);
            this.btnExtraRolesInstall.TabIndex = 5;
            this.btnExtraRolesInstall.Text = "Install";
            this.btnExtraRolesInstall.UseVisualStyleBackColor = true;
            this.btnExtraRolesInstall.Click += new System.EventHandler(this.btnExtraRolesInstall_Click);
            // 
            // btnExtraRolesRemove
            // 
            this.btnExtraRolesRemove.Location = new System.Drawing.Point(491, 92);
            this.btnExtraRolesRemove.Name = "btnExtraRolesRemove";
            this.btnExtraRolesRemove.Size = new System.Drawing.Size(67, 23);
            this.btnExtraRolesRemove.TabIndex = 6;
            this.btnExtraRolesRemove.Text = "Remove";
            this.btnExtraRolesRemove.UseVisualStyleBackColor = true;
            this.btnExtraRolesRemove.Click += new System.EventHandler(this.btnExtraRolesRemove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mod Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Installed Version:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Newest Version:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExtraRolesRemove);
            this.Controls.Add(this.btnExtraRolesInstall);
            this.Controls.Add(this.txtBoxExtraRolesNewestVersion);
            this.Controls.Add(this.txtBoxExtraRolesInstalledVersion);
            this.Controls.Add(this.txtBoxExtraRolesName);
            this.Controls.Add(this.txtBoxGamePath);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxGamePath;
        private System.Windows.Forms.TextBox txtBoxExtraRolesName;
        private System.Windows.Forms.TextBox txtBoxExtraRolesInstalledVersion;
        private System.Windows.Forms.TextBox txtBoxExtraRolesNewestVersion;
        private System.Windows.Forms.Button btnExtraRolesInstall;
        private System.Windows.Forms.Button btnExtraRolesRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

