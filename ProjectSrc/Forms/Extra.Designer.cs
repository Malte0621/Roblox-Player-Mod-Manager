namespace RobloxPlayerModManager.Forms
{
    partial class Extra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Extra));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.extraPatchesPage = new System.Windows.Forms.TabPage();
            this.graphicsApisComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.disableDpiCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fpsNumberTextBox = new System.Windows.Forms.TextBox();
            this.serversPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.placeIdTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.serverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.players = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.setServerJoin = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControl1.SuspendLayout();
            this.extraPatchesPage.SuspendLayout();
            this.serversPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.extraPatchesPage);
            this.tabControl1.Controls.Add(this.serversPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 411);
            this.tabControl1.TabIndex = 4;
            // 
            // extraPatchesPage
            // 
            this.extraPatchesPage.Controls.Add(this.graphicsApisComboBox);
            this.extraPatchesPage.Controls.Add(this.label6);
            this.extraPatchesPage.Controls.Add(this.label5);
            this.extraPatchesPage.Controls.Add(this.label4);
            this.extraPatchesPage.Controls.Add(this.disableDpiCheckBox);
            this.extraPatchesPage.Controls.Add(this.label3);
            this.extraPatchesPage.Controls.Add(this.ApplyButton);
            this.extraPatchesPage.Controls.Add(this.label2);
            this.extraPatchesPage.Controls.Add(this.fpsNumberTextBox);
            this.extraPatchesPage.Location = new System.Drawing.Point(4, 22);
            this.extraPatchesPage.Name = "extraPatchesPage";
            this.extraPatchesPage.Padding = new System.Windows.Forms.Padding(3);
            this.extraPatchesPage.Size = new System.Drawing.Size(776, 385);
            this.extraPatchesPage.TabIndex = 1;
            this.extraPatchesPage.Text = "Extra Patches";
            this.extraPatchesPage.UseVisualStyleBackColor = true;
            // 
            // graphicsApisComboBox
            // 
            this.graphicsApisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphicsApisComboBox.FormattingEnabled = true;
            this.graphicsApisComboBox.Items.AddRange(new object[] {
            "DirectX11",
            "DirectX10",
            "OpenGL (⚠️)",
            "Vulkan"});
            this.graphicsApisComboBox.Location = new System.Drawing.Point(85, 49);
            this.graphicsApisComboBox.Name = "graphicsApisComboBox";
            this.graphicsApisComboBox.Size = new System.Drawing.Size(98, 21);
            this.graphicsApisComboBox.TabIndex = 9;
            this.graphicsApisComboBox.SelectedIndexChanged += new System.EventHandler(this.graphicsApisComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(542, 26);
            this.label6.TabIndex = 8;
            this.label6.Text = "Changes which graphics API Roblox uses for rendering. Some of these APIs might ma" +
    "ke your Roblox run faster so\r\ntry them at your own leisure and figure which one " +
    "is best for you";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Graphics API";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(452, 39);
            this.label4.TabIndex = 5;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // disableDpiCheckBox
            // 
            this.disableDpiCheckBox.AutoSize = true;
            this.disableDpiCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.disableDpiCheckBox.Location = new System.Drawing.Point(10, 94);
            this.disableDpiCheckBox.Name = "disableDpiCheckBox";
            this.disableDpiCheckBox.Size = new System.Drawing.Size(170, 17);
            this.disableDpiCheckBox.TabIndex = 4;
            this.disableDpiCheckBox.Text = "Disable Automatic DPI Scaling";
            this.disableDpiCheckBox.UseVisualStyleBackColor = true;
            this.disableDpiCheckBox.CheckedChanged += new System.EventHandler(this.disableDpiCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(572, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // ApplyButton
            // 
            this.ApplyButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ApplyButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ApplyButton.Location = new System.Drawing.Point(3, 357);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(770, 25);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max FPS";
            // 
            // fpsNumberTextBox
            // 
            this.fpsNumberTextBox.Location = new System.Drawing.Point(66, 13);
            this.fpsNumberTextBox.Name = "fpsNumberTextBox";
            this.fpsNumberTextBox.Size = new System.Drawing.Size(117, 20);
            this.fpsNumberTextBox.TabIndex = 0;
            this.fpsNumberTextBox.Leave += new System.EventHandler(this.fpsNumberTextBox_TextChanged);
            // 
            // serversPage
            // 
            this.serversPage.Controls.Add(this.label1);
            this.serversPage.Controls.Add(this.placeIdTextBox);
            this.serversPage.Controls.Add(this.button1);
            this.serversPage.Controls.Add(this.dataGridView1);
            this.serversPage.Location = new System.Drawing.Point(4, 22);
            this.serversPage.Name = "serversPage";
            this.serversPage.Padding = new System.Windows.Forms.Padding(3);
            this.serversPage.Size = new System.Drawing.Size(776, 385);
            this.serversPage.TabIndex = 0;
            this.serversPage.Text = "Servers";
            this.serversPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "PlaceId";
            // 
            // placeIdTextBox
            // 
            this.placeIdTextBox.Location = new System.Drawing.Point(337, 26);
            this.placeIdTextBox.Name = "placeIdTextBox";
            this.placeIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.placeIdTextBox.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(256, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Find Servers";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverId,
            this.ping,
            this.players,
            this.setServerJoin});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(7, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(760, 311);
            this.dataGridView1.TabIndex = 4;
            // 
            // serverId
            // 
            this.serverId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.serverId.HeaderText = "Server ID";
            this.serverId.MinimumWidth = 8;
            this.serverId.Name = "serverId";
            this.serverId.ReadOnly = true;
            this.serverId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.serverId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ping
            // 
            this.ping.HeaderText = "Ping";
            this.ping.Name = "ping";
            this.ping.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // players
            // 
            this.players.HeaderText = "Players";
            this.players.Name = "players";
            this.players.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // setServerJoin
            // 
            this.setServerJoin.HeaderText = "Join Server";
            this.setServerJoin.Name = "setServerJoin";
            this.setServerJoin.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.setServerJoin.Text = "Join Server";
            // 
            // Extra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Extra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extra";
            this.Load += new System.EventHandler(this.extraSettingsPage_Load);
            this.tabControl1.ResumeLayout(false);
            this.extraPatchesPage.ResumeLayout(false);
            this.extraPatchesPage.PerformLayout();
            this.serversPage.ResumeLayout(false);
            this.serversPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage serversPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox placeIdTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ping;
        private System.Windows.Forms.DataGridViewTextBoxColumn players;
        private System.Windows.Forms.DataGridViewButtonColumn setServerJoin;
        private System.Windows.Forms.TabPage extraPatchesPage;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fpsNumberTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox disableDpiCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox graphicsApisComboBox;
    }
}