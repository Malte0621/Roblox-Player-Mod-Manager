namespace RobloxPlayerModManager
{
    partial class Launcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.launchPlayer = new System.Windows.Forms.Button();
            this.manageMods = new System.Windows.Forms.Button();
            this.branchSelect = new System.Windows.Forms.ComboBox();
            this.branchLabel = new System.Windows.Forms.Label();
            this.forceRebuild = new System.Windows.Forms.CheckBox();
            this.openPlayerDirectory = new System.Windows.Forms.CheckBox();
            this.targetVersionLabel = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.targetVersion = new System.Windows.Forms.ComboBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.dontUpdate1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.revertButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // launchPlayer
            // 
            this.launchPlayer.AccessibleName = "Launch Roblox Player";
            this.launchPlayer.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.launchPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.launchPlayer.Cursor = System.Windows.Forms.Cursors.Default;
            this.launchPlayer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchPlayer.Location = new System.Drawing.Point(19, 232);
            this.launchPlayer.Margin = new System.Windows.Forms.Padding(9, 5, 4, 5);
            this.launchPlayer.Name = "launchPlayer";
            this.launchPlayer.Size = new System.Drawing.Size(213, 35);
            this.launchPlayer.TabIndex = 6;
            this.launchPlayer.Text = "Launch Player";
            this.launchPlayer.UseVisualStyleBackColor = true;
            this.launchPlayer.Click += new System.EventHandler(this.launchPlayer_Click);
            // 
            // manageMods
            // 
            this.manageMods.AccessibleName = "Open Mod Folder";
            this.manageMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.manageMods.Cursor = System.Windows.Forms.Cursors.Default;
            this.manageMods.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manageMods.Location = new System.Drawing.Point(19, 276);
            this.manageMods.Margin = new System.Windows.Forms.Padding(9, 5, 4, 5);
            this.manageMods.Name = "manageMods";
            this.manageMods.Size = new System.Drawing.Size(213, 35);
            this.manageMods.TabIndex = 9;
            this.manageMods.Text = "Open Mod Folder";
            this.manageMods.UseVisualStyleBackColor = true;
            this.manageMods.Click += new System.EventHandler(this.manageMods_Click);
            // 
            // branchSelect
            // 
            this.branchSelect.AccessibleName = "Branch Selector";
            this.branchSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.branchSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.branchSelect.FormattingEnabled = true;
            this.branchSelect.Items.AddRange(new object[] {
            "roblox",
            "sitetest1.robloxlabs",
            "sitetest2.robloxlabs",
            "sitetest3.robloxlabs"});
            this.branchSelect.Location = new System.Drawing.Point(264, 196);
            this.branchSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.branchSelect.Name = "branchSelect";
            this.branchSelect.Size = new System.Drawing.Size(226, 28);
            this.branchSelect.TabIndex = 10;
            this.branchSelect.SelectedIndexChanged += new System.EventHandler(this.branchSelect_SelectedIndexChanged);
            // 
            // branchLabel
            // 
            this.branchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.branchLabel.AutoSize = true;
            this.branchLabel.BackColor = System.Drawing.Color.Transparent;
            this.branchLabel.CausesValidation = false;
            this.branchLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.branchLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.branchLabel.Location = new System.Drawing.Point(259, 166);
            this.branchLabel.Margin = new System.Windows.Forms.Padding(0);
            this.branchLabel.Name = "branchLabel";
            this.branchLabel.Size = new System.Drawing.Size(126, 25);
            this.branchLabel.TabIndex = 11;
            this.branchLabel.Text = "Player Branch: ";
            this.branchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // forceRebuild
            // 
            this.forceRebuild.AccessibleName = "Force Client Rebuild";
            this.forceRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.forceRebuild.AutoSize = true;
            this.forceRebuild.Location = new System.Drawing.Point(262, 331);
            this.forceRebuild.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.forceRebuild.Name = "forceRebuild";
            this.forceRebuild.Size = new System.Drawing.Size(176, 24);
            this.forceRebuild.TabIndex = 12;
            this.forceRebuild.Text = "Force Reinstallation";
            this.forceRebuild.UseVisualStyleBackColor = true;
            // 
            // openPlayerDirectory
            // 
            this.openPlayerDirectory.AccessibleName = "Just Open Player Path";
            this.openPlayerDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openPlayerDirectory.AutoSize = true;
            this.openPlayerDirectory.Location = new System.Drawing.Point(261, 359);
            this.openPlayerDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.openPlayerDirectory.Name = "openPlayerDirectory";
            this.openPlayerDirectory.Size = new System.Drawing.Size(222, 24);
            this.openPlayerDirectory.TabIndex = 14;
            this.openPlayerDirectory.Text = "Just Open Player Directory";
            this.openPlayerDirectory.UseVisualStyleBackColor = true;
            // 
            // targetVersionLabel
            // 
            this.targetVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.targetVersionLabel.AutoSize = true;
            this.targetVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.targetVersionLabel.CausesValidation = false;
            this.targetVersionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.targetVersionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.targetVersionLabel.Location = new System.Drawing.Point(259, 230);
            this.targetVersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.targetVersionLabel.Name = "targetVersionLabel";
            this.targetVersionLabel.Size = new System.Drawing.Size(127, 25);
            this.targetVersionLabel.TabIndex = 17;
            this.targetVersionLabel.Text = "Target Version:";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Segoe UI Light", 20F);
            this.title.Location = new System.Drawing.Point(204, 41);
            this.title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(260, 108);
            this.title.TabIndex = 20;
            this.title.Text = "Roblox Player\r\nMod Manager";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // targetVersion
            // 
            this.targetVersion.AccessibleName = "Target Version";
            this.targetVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.targetVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetVersion.FormattingEnabled = true;
            this.targetVersion.Items.AddRange(new object[] {
            "(Use Latest)"});
            this.targetVersion.Location = new System.Drawing.Point(263, 256);
            this.targetVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.targetVersion.Name = "targetVersion";
            this.targetVersion.Size = new System.Drawing.Size(226, 28);
            this.targetVersion.TabIndex = 18;
            this.targetVersion.SelectedIndexChanged += new System.EventHandler(this.targetVersion_SelectedIndexChanged);
            // 
            // logo
            // 
            this.logo.BackgroundImage = global::RobloxPlayerModManager.Properties.Resources.Logo;
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logo.Location = new System.Drawing.Point(56, 28);
            this.logo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(135, 135);
            this.logo.TabIndex = 22;
            this.logo.TabStop = false;
            // 
            // dontUpdate1
            // 
            this.dontUpdate1.AccessibleName = "Dont Update";
            this.dontUpdate1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dontUpdate1.AutoSize = true;
            this.dontUpdate1.Location = new System.Drawing.Point(261, 300);
            this.dontUpdate1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dontUpdate1.Name = "dontUpdate1";
            this.dontUpdate1.Size = new System.Drawing.Size(127, 24);
            this.dontUpdate1.TabIndex = 23;
            this.dontUpdate1.Text = "Dont Update";
            this.dontUpdate1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "By: Malte0621";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "(Prototype)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 22);
            this.label3.TabIndex = 27;
            this.label3.Text = "[For testing purposes only.]";
            // 
            // revertButton
            // 
            this.revertButton.AccessibleName = "Open Mod Folder";
            this.revertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.revertButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.revertButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revertButton.Location = new System.Drawing.Point(16, 321);
            this.revertButton.Margin = new System.Windows.Forms.Padding(9, 5, 4, 5);
            this.revertButton.Name = "revertButton";
            this.revertButton.Size = new System.Drawing.Size(216, 35);
            this.revertButton.TabIndex = 28;
            this.revertButton.Text = "Revert To Normal Roblox";
            this.revertButton.UseVisualStyleBackColor = true;
            this.revertButton.Click += new System.EventHandler(this.revertButton_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(502, 384);
            this.Controls.Add(this.revertButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dontUpdate1);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.title);
            this.Controls.Add(this.targetVersion);
            this.Controls.Add(this.targetVersionLabel);
            this.Controls.Add(this.openPlayerDirectory);
            this.Controls.Add(this.forceRebuild);
            this.Controls.Add(this.branchLabel);
            this.Controls.Add(this.branchSelect);
            this.Controls.Add(this.manageMods);
            this.Controls.Add(this.launchPlayer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::RobloxPlayerModManager.Properties.Resources.Icon;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roblox Player Mod Manager";
            this.Load += new System.EventHandler(this.Launcher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button launchPlayer;
        private System.Windows.Forms.Button manageMods;
        private System.Windows.Forms.ComboBox branchSelect;
        private System.Windows.Forms.Label branchLabel;
        private System.Windows.Forms.CheckBox forceRebuild;
        private System.Windows.Forms.CheckBox openPlayerDirectory;
        private System.Windows.Forms.Label targetVersionLabel;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.ComboBox targetVersion;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.CheckBox dontUpdate1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button revertButton;
    }
}

