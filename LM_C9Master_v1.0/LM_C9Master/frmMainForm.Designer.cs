namespace LM_C9Master
{
    partial class frmMainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenSharedFolder = new System.Windows.Forms.Button();
            this.btnVPNClientLaunch = new System.Windows.Forms.Button();
            this.BtnVPNSwitch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnChangeC9TraderRoot = new System.Windows.Forms.Button();
            this.btnDefaultC9TraderRoot = new System.Windows.Forms.Button();
            this.lblC9TraderRoot = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSharedFolderTarget = new System.Windows.Forms.Label();
            this.lblVPNClientTarget = new System.Windows.Forms.Label();
            this.labely = new System.Windows.Forms.Label();
            this.labelx = new System.Windows.Forms.Label();
            this.btnDefaultSharedFolder = new System.Windows.Forms.Button();
            this.btnChangeSharedFolder = new System.Windows.Forms.Button();
            this.btnChangeVPNClient = new System.Windows.Forms.Button();
            this.btnDefaultVPNClient = new System.Windows.Forms.Button();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.chkBoxSetViewPassword = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxSetUsrPassword = new System.Windows.Forms.TextBox();
            this.opnFDVPNClientSelector = new System.Windows.Forms.OpenFileDialog();
            this.fldBrwsDiagSharedFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBoxUsers = new System.Windows.Forms.ComboBox();
            this.btnRefreshVersions = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxVersionsList = new System.Windows.Forms.ComboBox();
            this.Links = new System.Windows.Forms.GroupBox();
            this.btnDesktopApp = new System.Windows.Forms.Button();
            this.btnWebApp = new System.Windows.Forms.Button();
            this.btnScrumBoard = new System.Windows.Forms.Button();
            this.btnTestCycles = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.Links.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenSharedFolder);
            this.groupBox1.Controls.Add(this.btnVPNClientLaunch);
            this.groupBox1.Controls.Add(this.BtnVPNSwitch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VPN Manager";
            // 
            // btnOpenSharedFolder
            // 
            this.btnOpenSharedFolder.Enabled = false;
            this.btnOpenSharedFolder.Location = new System.Drawing.Point(261, 103);
            this.btnOpenSharedFolder.Name = "btnOpenSharedFolder";
            this.btnOpenSharedFolder.Size = new System.Drawing.Size(250, 47);
            this.btnOpenSharedFolder.TabIndex = 3;
            this.btnOpenSharedFolder.Text = "Open Shared Folder";
            this.btnOpenSharedFolder.UseVisualStyleBackColor = true;
            this.btnOpenSharedFolder.Click += new System.EventHandler(this.btnOpenSharedFolder_Click);
            // 
            // btnVPNClientLaunch
            // 
            this.btnVPNClientLaunch.Enabled = false;
            this.btnVPNClientLaunch.Location = new System.Drawing.Point(6, 103);
            this.btnVPNClientLaunch.Name = "btnVPNClientLaunch";
            this.btnVPNClientLaunch.Size = new System.Drawing.Size(250, 47);
            this.btnVPNClientLaunch.TabIndex = 2;
            this.btnVPNClientLaunch.Text = "Launch Client";
            this.btnVPNClientLaunch.UseVisualStyleBackColor = true;
            this.btnVPNClientLaunch.Click += new System.EventHandler(this.btnVPNClientLaunch_Click);
            // 
            // BtnVPNSwitch
            // 
            this.BtnVPNSwitch.BackColor = System.Drawing.Color.LightCoral;
            this.BtnVPNSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVPNSwitch.ForeColor = System.Drawing.Color.DarkRed;
            this.BtnVPNSwitch.Location = new System.Drawing.Point(308, 38);
            this.BtnVPNSwitch.Name = "BtnVPNSwitch";
            this.BtnVPNSwitch.Size = new System.Drawing.Size(98, 38);
            this.BtnVPNSwitch.TabIndex = 1;
            this.BtnVPNSwitch.Text = "OFF";
            this.BtnVPNSwitch.UseVisualStyleBackColor = false;
            this.BtnVPNSwitch.Click += new System.EventHandler(this.BtnVPNSwitch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current VPN Services Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.btnSaveSettings);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(538, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(591, 525);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnChangeC9TraderRoot);
            this.groupBox5.Controls.Add(this.btnDefaultC9TraderRoot);
            this.groupBox5.Controls.Add(this.lblC9TraderRoot);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(6, 240);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(574, 202);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "App Manager";
            // 
            // btnChangeC9TraderRoot
            // 
            this.btnChangeC9TraderRoot.Location = new System.Drawing.Point(11, 69);
            this.btnChangeC9TraderRoot.Name = "btnChangeC9TraderRoot";
            this.btnChangeC9TraderRoot.Size = new System.Drawing.Size(98, 38);
            this.btnChangeC9TraderRoot.TabIndex = 3;
            this.btnChangeC9TraderRoot.Text = "Change";
            this.btnChangeC9TraderRoot.UseVisualStyleBackColor = true;
            this.btnChangeC9TraderRoot.Click += new System.EventHandler(this.btnChangeC9TraderRoot_Click);
            // 
            // btnDefaultC9TraderRoot
            // 
            this.btnDefaultC9TraderRoot.Enabled = false;
            this.btnDefaultC9TraderRoot.Location = new System.Drawing.Point(115, 69);
            this.btnDefaultC9TraderRoot.Name = "btnDefaultC9TraderRoot";
            this.btnDefaultC9TraderRoot.Size = new System.Drawing.Size(98, 38);
            this.btnDefaultC9TraderRoot.TabIndex = 2;
            this.btnDefaultC9TraderRoot.Text = "Default";
            this.btnDefaultC9TraderRoot.UseVisualStyleBackColor = true;
            this.btnDefaultC9TraderRoot.Click += new System.EventHandler(this.btnDefaultC9TraderRoot_Click);
            // 
            // lblC9TraderRoot
            // 
            this.lblC9TraderRoot.AutoSize = true;
            this.lblC9TraderRoot.Location = new System.Drawing.Point(14, 112);
            this.lblC9TraderRoot.Name = "lblC9TraderRoot";
            this.lblC9TraderRoot.Size = new System.Drawing.Size(64, 25);
            this.lblC9TraderRoot.TabIndex = 1;
            this.lblC9TraderRoot.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "App Root Folder (C9Trader):";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Enabled = false;
            this.btnSaveSettings.Location = new System.Drawing.Point(6, 475);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(123, 44);
            this.btnSaveSettings.TabIndex = 1;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSharedFolderTarget);
            this.groupBox3.Controls.Add(this.lblVPNClientTarget);
            this.groupBox3.Controls.Add(this.labely);
            this.groupBox3.Controls.Add(this.labelx);
            this.groupBox3.Controls.Add(this.btnDefaultSharedFolder);
            this.groupBox3.Controls.Add(this.btnChangeSharedFolder);
            this.groupBox3.Controls.Add(this.btnChangeVPNClient);
            this.groupBox3.Controls.Add(this.btnDefaultVPNClient);
            this.groupBox3.Location = new System.Drawing.Point(6, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(574, 206);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "VPN Manager";
            // 
            // lblSharedFolderTarget
            // 
            this.lblSharedFolderTarget.AutoSize = true;
            this.lblSharedFolderTarget.Location = new System.Drawing.Point(14, 151);
            this.lblSharedFolderTarget.Name = "lblSharedFolderTarget";
            this.lblSharedFolderTarget.Size = new System.Drawing.Size(0, 25);
            this.lblSharedFolderTarget.TabIndex = 7;
            // 
            // lblVPNClientTarget
            // 
            this.lblVPNClientTarget.AutoSize = true;
            this.lblVPNClientTarget.Location = new System.Drawing.Point(14, 69);
            this.lblVPNClientTarget.Name = "lblVPNClientTarget";
            this.lblVPNClientTarget.Size = new System.Drawing.Size(0, 25);
            this.lblVPNClientTarget.TabIndex = 6;
            // 
            // labely
            // 
            this.labely.AutoSize = true;
            this.labely.Location = new System.Drawing.Point(6, 109);
            this.labely.Name = "labely";
            this.labely.Size = new System.Drawing.Size(142, 25);
            this.labely.TabIndex = 5;
            this.labely.Text = "Shared Folder:";
            // 
            // labelx
            // 
            this.labelx.AutoSize = true;
            this.labelx.Location = new System.Drawing.Point(6, 34);
            this.labelx.Name = "labelx";
            this.labelx.Size = new System.Drawing.Size(114, 25);
            this.labelx.TabIndex = 4;
            this.labelx.Text = "VPN Client:";
            // 
            // btnDefaultSharedFolder
            // 
            this.btnDefaultSharedFolder.Enabled = false;
            this.btnDefaultSharedFolder.Location = new System.Drawing.Point(115, 137);
            this.btnDefaultSharedFolder.Name = "btnDefaultSharedFolder";
            this.btnDefaultSharedFolder.Size = new System.Drawing.Size(98, 38);
            this.btnDefaultSharedFolder.TabIndex = 3;
            this.btnDefaultSharedFolder.Text = "Default";
            this.btnDefaultSharedFolder.UseVisualStyleBackColor = true;
            this.btnDefaultSharedFolder.Click += new System.EventHandler(this.btnDefaultSharedFolder_Click);
            // 
            // btnChangeSharedFolder
            // 
            this.btnChangeSharedFolder.Location = new System.Drawing.Point(11, 138);
            this.btnChangeSharedFolder.Name = "btnChangeSharedFolder";
            this.btnChangeSharedFolder.Size = new System.Drawing.Size(98, 38);
            this.btnChangeSharedFolder.TabIndex = 2;
            this.btnChangeSharedFolder.Text = "Change";
            this.btnChangeSharedFolder.UseVisualStyleBackColor = true;
            this.btnChangeSharedFolder.Click += new System.EventHandler(this.btnChangeSharedFolder_Click);
            // 
            // btnChangeVPNClient
            // 
            this.btnChangeVPNClient.Location = new System.Drawing.Point(11, 62);
            this.btnChangeVPNClient.Name = "btnChangeVPNClient";
            this.btnChangeVPNClient.Size = new System.Drawing.Size(98, 38);
            this.btnChangeVPNClient.TabIndex = 1;
            this.btnChangeVPNClient.Text = "Change";
            this.btnChangeVPNClient.UseVisualStyleBackColor = true;
            this.btnChangeVPNClient.Click += new System.EventHandler(this.btnChangeVPNClient_Click);
            // 
            // btnDefaultVPNClient
            // 
            this.btnDefaultVPNClient.Enabled = false;
            this.btnDefaultVPNClient.Location = new System.Drawing.Point(115, 62);
            this.btnDefaultVPNClient.Name = "btnDefaultVPNClient";
            this.btnDefaultVPNClient.Size = new System.Drawing.Size(98, 38);
            this.btnDefaultVPNClient.TabIndex = 0;
            this.btnDefaultVPNClient.Text = "Default";
            this.btnDefaultVPNClient.UseVisualStyleBackColor = true;
            this.btnDefaultVPNClient.Click += new System.EventHandler(this.btnDefaultVPNClient_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Enabled = false;
            this.btnRemoveUser.Location = new System.Drawing.Point(319, 150);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(37, 38);
            this.btnRemoveUser.TabIndex = 10;
            this.btnRemoveUser.Text = "-";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Enabled = false;
            this.btnAddUser.Location = new System.Drawing.Point(269, 150);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(37, 38);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "+";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // chkBoxSetViewPassword
            // 
            this.chkBoxSetViewPassword.AutoSize = true;
            this.chkBoxSetViewPassword.Location = new System.Drawing.Point(259, 222);
            this.chkBoxSetViewPassword.Name = "chkBoxSetViewPassword";
            this.chkBoxSetViewPassword.Size = new System.Drawing.Size(172, 29);
            this.chkBoxSetViewPassword.TabIndex = 8;
            this.chkBoxSetViewPassword.Text = "View Password";
            this.chkBoxSetViewPassword.UseVisualStyleBackColor = true;
            this.chkBoxSetViewPassword.CheckedChanged += new System.EventHandler(this.chkBoxSetViewPassword_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Password";
            // 
            // txtBoxSetUsrPassword
            // 
            this.txtBoxSetUsrPassword.Location = new System.Drawing.Point(6, 220);
            this.txtBoxSetUsrPassword.Name = "txtBoxSetUsrPassword";
            this.txtBoxSetUsrPassword.Size = new System.Drawing.Size(224, 29);
            this.txtBoxSetUsrPassword.TabIndex = 6;
            this.txtBoxSetUsrPassword.UseSystemPasswordChar = true;
            this.txtBoxSetUsrPassword.TextChanged += new System.EventHandler(this.txtBoxSetUsrPassword_TextChanged);
            // 
            // opnFDVPNClientSelector
            // 
            this.opnFDVPNClientSelector.Filter = "vpnui.exe|*.exe";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkBoxSetViewPassword);
            this.groupBox4.Controls.Add(this.btnRemoveUser);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.btnAddUser);
            this.groupBox4.Controls.Add(this.txtBoxSetUsrPassword);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.cmbBoxUsers);
            this.groupBox4.Controls.Add(this.btnRefreshVersions);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cmbBoxVersionsList);
            this.groupBox4.Location = new System.Drawing.Point(14, 181);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(518, 356);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "App Manager";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "User";
            // 
            // cmbBoxUsers
            // 
            this.cmbBoxUsers.FormattingEnabled = true;
            this.cmbBoxUsers.Location = new System.Drawing.Point(6, 150);
            this.cmbBoxUsers.Name = "cmbBoxUsers";
            this.cmbBoxUsers.Size = new System.Drawing.Size(247, 32);
            this.cmbBoxUsers.TabIndex = 3;
            this.cmbBoxUsers.TextChanged += new System.EventHandler(this.cmbBoxUsers_TextChanged);
            // 
            // btnRefreshVersions
            // 
            this.btnRefreshVersions.Location = new System.Drawing.Point(259, 60);
            this.btnRefreshVersions.Name = "btnRefreshVersions";
            this.btnRefreshVersions.Size = new System.Drawing.Size(117, 42);
            this.btnRefreshVersions.TabIndex = 2;
            this.btnRefreshVersions.Text = "Refresh";
            this.btnRefreshVersions.UseVisualStyleBackColor = true;
            this.btnRefreshVersions.Click += new System.EventHandler(this.btnRefreshVersions_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version";
            // 
            // cmbBoxVersionsList
            // 
            this.cmbBoxVersionsList.FormattingEnabled = true;
            this.cmbBoxVersionsList.Location = new System.Drawing.Point(6, 62);
            this.cmbBoxVersionsList.Name = "cmbBoxVersionsList";
            this.cmbBoxVersionsList.Size = new System.Drawing.Size(247, 32);
            this.cmbBoxVersionsList.TabIndex = 0;
            // 
            // Links
            // 
            this.Links.Controls.Add(this.btnDesktopApp);
            this.Links.Controls.Add(this.btnWebApp);
            this.Links.Controls.Add(this.btnScrumBoard);
            this.Links.Controls.Add(this.btnTestCycles);
            this.Links.Controls.Add(this.label8);
            this.Links.Controls.Add(this.label7);
            this.Links.Controls.Add(this.label4);
            this.Links.Location = new System.Drawing.Point(1135, 12);
            this.Links.Name = "Links";
            this.Links.Size = new System.Drawing.Size(676, 525);
            this.Links.TabIndex = 3;
            this.Links.TabStop = false;
            this.Links.Text = "Links";
            this.Links.Enter += new System.EventHandler(this.Links_Enter);
            // 
            // btnDesktopApp
            // 
            this.btnDesktopApp.Location = new System.Drawing.Point(160, 169);
            this.btnDesktopApp.Name = "btnDesktopApp";
            this.btnDesktopApp.Size = new System.Drawing.Size(138, 45);
            this.btnDesktopApp.TabIndex = 6;
            this.btnDesktopApp.Text = "Desktop App";
            this.btnDesktopApp.UseVisualStyleBackColor = true;
            this.btnDesktopApp.Click += new System.EventHandler(this.btnDesktopApp_Click);
            // 
            // btnWebApp
            // 
            this.btnWebApp.Location = new System.Drawing.Point(11, 169);
            this.btnWebApp.Name = "btnWebApp";
            this.btnWebApp.Size = new System.Drawing.Size(143, 45);
            this.btnWebApp.TabIndex = 5;
            this.btnWebApp.Text = "Web App";
            this.btnWebApp.UseVisualStyleBackColor = true;
            this.btnWebApp.Click += new System.EventHandler(this.btnWebApp_Click);
            // 
            // btnScrumBoard
            // 
            this.btnScrumBoard.Location = new System.Drawing.Point(155, 62);
            this.btnScrumBoard.Name = "btnScrumBoard";
            this.btnScrumBoard.Size = new System.Drawing.Size(143, 45);
            this.btnScrumBoard.TabIndex = 4;
            this.btnScrumBoard.Text = "Scrum Board";
            this.btnScrumBoard.UseVisualStyleBackColor = true;
            this.btnScrumBoard.Click += new System.EventHandler(this.btnScrumBoard_Click);
            // 
            // btnTestCycles
            // 
            this.btnTestCycles.Location = new System.Drawing.Point(6, 62);
            this.btnTestCycles.Name = "btnTestCycles";
            this.btnTestCycles.Size = new System.Drawing.Size(143, 45);
            this.btnTestCycles.TabIndex = 3;
            this.btnTestCycles.Text = "Test Cycles";
            this.btnTestCycles.UseVisualStyleBackColor = true;
            this.btnTestCycles.Click += new System.EventHandler(this.btnTestCycles_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "C9 Slack";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(234, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 25);
            this.label7.TabIndex = 1;
            this.label7.Text = "JIRA Scrum Board";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "JIRA Test Cycles";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1936, 549);
            this.Controls.Add(this.Links);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmMainForm";
            this.Text = "LM_C9Master_Alpha_1.9";
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.Links.ResumeLayout(false);
            this.Links.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnVPNSwitch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labely;
        private System.Windows.Forms.Label labelx;
        private System.Windows.Forms.Button btnDefaultSharedFolder;
        private System.Windows.Forms.Button btnChangeSharedFolder;
        private System.Windows.Forms.Button btnChangeVPNClient;
        private System.Windows.Forms.Button btnDefaultVPNClient;
        private System.Windows.Forms.OpenFileDialog opnFDVPNClientSelector;
        private System.Windows.Forms.FolderBrowserDialog fldBrwsDiagSharedFolder;
        private System.Windows.Forms.Label lblVPNClientTarget;
        private System.Windows.Forms.Label lblSharedFolderTarget;
        private System.Windows.Forms.Button btnOpenSharedFolder;
        private System.Windows.Forms.Button btnVPNClientLaunch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRefreshVersions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxVersionsList;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblC9TraderRoot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChangeC9TraderRoot;
        private System.Windows.Forms.Button btnDefaultC9TraderRoot;
        private System.Windows.Forms.CheckBox chkBoxSetViewPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxSetUsrPassword;
        private System.Windows.Forms.Button btnRemoveUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbBoxUsers;
        private System.Windows.Forms.GroupBox Links;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTestCycles;
        private System.Windows.Forms.Button btnDesktopApp;
        private System.Windows.Forms.Button btnWebApp;
        private System.Windows.Forms.Button btnScrumBoard;
    }
}

