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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnVPNClientLaunch = new System.Windows.Forms.Button();
            this.BtnVPNSwitch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnTraderRootSave = new System.Windows.Forms.Button();
            this.btnChangeC9TraderRoot = new System.Windows.Forms.Button();
            this.btnDefaultC9TraderRoot = new System.Windows.Forms.Button();
            this.lblC9TraderRoot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDefaultVPN = new System.Windows.Forms.Button();
            this.btnChangeVPNPath = new System.Windows.Forms.Button();
            this.lblVPNClientTarget = new System.Windows.Forms.TextBox();
            this.btnVPNSaveSettings = new System.Windows.Forms.Button();
            this.labelx = new System.Windows.Forms.Label();
            this.btnChangeVPNClient = new System.Windows.Forms.Button();
            this.btnCloseApp = new System.Windows.Forms.Button();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.chkBoxSetViewPassword = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxSetUsrPassword = new System.Windows.Forms.TextBox();
            this.opnFDVPNClientSelector = new System.Windows.Forms.OpenFileDialog();
            this.fldBrwsDiagSharedFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnServerDefault = new System.Windows.Forms.Button();
            this.BtnLaunchApp = new System.Windows.Forms.Button();
            this.btnCloseAppSelUser = new System.Windows.Forms.Button();
            this.txtBoxServerName = new System.Windows.Forms.TextBox();
            this.chkBoxNoUpd = new System.Windows.Forms.CheckBox();
            this.chkBoxMultiApp = new System.Windows.Forms.CheckBox();
            this.MSI_Toggle = new System.Windows.Forms.Button();
            this.cmbBoxUsers = new System.Windows.Forms.ComboBox();
            this.btnRefreshVersions = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxVersionsList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.Links = new System.Windows.Forms.GroupBox();
            this.btnPortalLink = new System.Windows.Forms.Button();
            this.lblPortal = new System.Windows.Forms.Label();
            this.reeEEE = new System.Windows.Forms.PictureBox();
            this.btnDesktopApp = new System.Windows.Forms.Button();
            this.btnWebApp = new System.Windows.Forms.Button();
            this.btnScrumBoard = new System.Windows.Forms.Button();
            this.btnTestCycles = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnStartLocalServer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.Links.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reeEEE)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnVPNClientLaunch);
            this.groupBox1.Controls.Add(this.BtnVPNSwitch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 175);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VPN Manager";
            // 
            // btnVPNClientLaunch
            // 
            this.btnVPNClientLaunch.Enabled = false;
            this.btnVPNClientLaunch.Location = new System.Drawing.Point(141, 101);
            this.btnVPNClientLaunch.Name = "btnVPNClientLaunch";
            this.btnVPNClientLaunch.Size = new System.Drawing.Size(273, 49);
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
            this.BtnVPNSwitch.Location = new System.Drawing.Point(336, 40);
            this.BtnVPNSwitch.Name = "BtnVPNSwitch";
            this.BtnVPNSwitch.Size = new System.Drawing.Size(107, 40);
            this.BtnVPNSwitch.TabIndex = 1;
            this.BtnVPNSwitch.Text = "OFF";
            this.BtnVPNSwitch.UseVisualStyleBackColor = false;
            this.BtnVPNSwitch.Click += new System.EventHandler(this.BtnVPNSwitch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current VPN Services Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(587, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(645, 547);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnTraderRootSave);
            this.groupBox5.Controls.Add(this.btnChangeC9TraderRoot);
            this.groupBox5.Controls.Add(this.btnDefaultC9TraderRoot);
            this.groupBox5.Controls.Add(this.lblC9TraderRoot);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(7, 229);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(626, 197);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "App Manager";
            // 
            // btnTraderRootSave
            // 
            this.btnTraderRootSave.Enabled = false;
            this.btnTraderRootSave.Location = new System.Drawing.Point(238, 138);
            this.btnTraderRootSave.Name = "btnTraderRootSave";
            this.btnTraderRootSave.Size = new System.Drawing.Size(134, 46);
            this.btnTraderRootSave.TabIndex = 9;
            this.btnTraderRootSave.Text = "Save";
            this.btnTraderRootSave.UseVisualStyleBackColor = true;
            this.btnTraderRootSave.Click += new System.EventHandler(this.btnTraderRootSave_Click);
            // 
            // btnChangeC9TraderRoot
            // 
            this.btnChangeC9TraderRoot.Location = new System.Drawing.Point(297, 43);
            this.btnChangeC9TraderRoot.Name = "btnChangeC9TraderRoot";
            this.btnChangeC9TraderRoot.Size = new System.Drawing.Size(107, 40);
            this.btnChangeC9TraderRoot.TabIndex = 3;
            this.btnChangeC9TraderRoot.Text = "Change";
            this.btnChangeC9TraderRoot.UseVisualStyleBackColor = true;
            this.btnChangeC9TraderRoot.Click += new System.EventHandler(this.btnChangeC9TraderRoot_Click);
            // 
            // btnDefaultC9TraderRoot
            // 
            this.btnDefaultC9TraderRoot.Enabled = false;
            this.btnDefaultC9TraderRoot.Location = new System.Drawing.Point(410, 43);
            this.btnDefaultC9TraderRoot.Name = "btnDefaultC9TraderRoot";
            this.btnDefaultC9TraderRoot.Size = new System.Drawing.Size(107, 40);
            this.btnDefaultC9TraderRoot.TabIndex = 2;
            this.btnDefaultC9TraderRoot.Text = "Default";
            this.btnDefaultC9TraderRoot.UseVisualStyleBackColor = true;
            this.btnDefaultC9TraderRoot.Click += new System.EventHandler(this.btnDefaultC9TraderRoot_Click);
            // 
            // lblC9TraderRoot
            // 
            this.lblC9TraderRoot.Location = new System.Drawing.Point(11, 89);
            this.lblC9TraderRoot.Name = "lblC9TraderRoot";
            this.lblC9TraderRoot.Size = new System.Drawing.Size(588, 31);
            this.lblC9TraderRoot.TabIndex = 1;
            this.lblC9TraderRoot.Text = "Trader Root Path";
            this.lblC9TraderRoot.Click += new System.EventHandler(this.lblC9TraderRoot_Click);
            this.lblC9TraderRoot.TextChanged += new System.EventHandler(this.lblC9TraderRoot_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "App Root Folder (C9Trader):";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDefaultVPN);
            this.groupBox3.Controls.Add(this.btnChangeVPNPath);
            this.groupBox3.Controls.Add(this.lblVPNClientTarget);
            this.groupBox3.Controls.Add(this.btnVPNSaveSettings);
            this.groupBox3.Controls.Add(this.labelx);
            this.groupBox3.Controls.Add(this.btnChangeVPNClient);
            this.groupBox3.Location = new System.Drawing.Point(7, 30);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 193);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "VPN Manager";
            // 
            // btnDefaultVPN
            // 
            this.btnDefaultVPN.Enabled = false;
            this.btnDefaultVPN.Location = new System.Drawing.Point(238, 36);
            this.btnDefaultVPN.Name = "btnDefaultVPN";
            this.btnDefaultVPN.Size = new System.Drawing.Size(107, 40);
            this.btnDefaultVPN.TabIndex = 8;
            this.btnDefaultVPN.Text = "Default";
            this.btnDefaultVPN.UseVisualStyleBackColor = true;
            this.btnDefaultVPN.Click += new System.EventHandler(this.btnDefaultVPN_Click);
            // 
            // btnChangeVPNPath
            // 
            this.btnChangeVPNPath.Location = new System.Drawing.Point(125, 36);
            this.btnChangeVPNPath.Name = "btnChangeVPNPath";
            this.btnChangeVPNPath.Size = new System.Drawing.Size(107, 40);
            this.btnChangeVPNPath.TabIndex = 7;
            this.btnChangeVPNPath.Text = "Change";
            this.btnChangeVPNPath.UseVisualStyleBackColor = true;
            this.btnChangeVPNPath.Click += new System.EventHandler(this.btnChangeVPNPath_Click);
            // 
            // lblVPNClientTarget
            // 
            this.lblVPNClientTarget.Location = new System.Drawing.Point(15, 82);
            this.lblVPNClientTarget.Name = "lblVPNClientTarget";
            this.lblVPNClientTarget.Size = new System.Drawing.Size(588, 31);
            this.lblVPNClientTarget.TabIndex = 6;
            this.lblVPNClientTarget.Text = "xxxxxxxx";
            this.lblVPNClientTarget.TextChanged += new System.EventHandler(this.lblVPNClientTarget_TextChanged);
            // 
            // btnVPNSaveSettings
            // 
            this.btnVPNSaveSettings.Enabled = false;
            this.btnVPNSaveSettings.Location = new System.Drawing.Point(238, 132);
            this.btnVPNSaveSettings.Name = "btnVPNSaveSettings";
            this.btnVPNSaveSettings.Size = new System.Drawing.Size(134, 46);
            this.btnVPNSaveSettings.TabIndex = 1;
            this.btnVPNSaveSettings.Text = "Save";
            this.btnVPNSaveSettings.UseVisualStyleBackColor = true;
            this.btnVPNSaveSettings.Click += new System.EventHandler(this.btnVPNSaveSettings_Click);
            // 
            // labelx
            // 
            this.labelx.AutoSize = true;
            this.labelx.Location = new System.Drawing.Point(6, 39);
            this.labelx.Name = "labelx";
            this.labelx.Size = new System.Drawing.Size(122, 25);
            this.labelx.TabIndex = 4;
            this.labelx.Text = "VPN Client:";
            // 
            // btnChangeVPNClient
            // 
            this.btnChangeVPNClient.Location = new System.Drawing.Point(1275, 39);
            this.btnChangeVPNClient.Name = "btnChangeVPNClient";
            this.btnChangeVPNClient.Size = new System.Drawing.Size(107, 40);
            this.btnChangeVPNClient.TabIndex = 1;
            this.btnChangeVPNClient.Text = "Change";
            this.btnChangeVPNClient.UseVisualStyleBackColor = true;
            this.btnChangeVPNClient.Click += new System.EventHandler(this.btnChangeVPNClient_Click);
            // 
            // btnCloseApp
            // 
            this.btnCloseApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCloseApp.Location = new System.Drawing.Point(401, 321);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Size = new System.Drawing.Size(158, 44);
            this.btnCloseApp.TabIndex = 15;
            this.btnCloseApp.Text = "Close All";
            this.btnCloseApp.UseVisualStyleBackColor = false;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Enabled = false;
            this.btnRemoveUser.Location = new System.Drawing.Point(351, 130);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(42, 36);
            this.btnRemoveUser.TabIndex = 10;
            this.btnRemoveUser.Text = "-";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Enabled = false;
            this.btnAddUser.Location = new System.Drawing.Point(288, 130);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(40, 36);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "+";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // chkBoxSetViewPassword
            // 
            this.chkBoxSetViewPassword.AutoSize = true;
            this.chkBoxSetViewPassword.Location = new System.Drawing.Point(327, 210);
            this.chkBoxSetViewPassword.Name = "chkBoxSetViewPassword";
            this.chkBoxSetViewPassword.Size = new System.Drawing.Size(190, 29);
            this.chkBoxSetViewPassword.TabIndex = 8;
            this.chkBoxSetViewPassword.Text = "View Password";
            this.chkBoxSetViewPassword.UseVisualStyleBackColor = true;
            this.chkBoxSetViewPassword.CheckedChanged += new System.EventHandler(this.chkBoxSetViewPassword_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Password";
            // 
            // txtBoxSetUsrPassword
            // 
            this.txtBoxSetUsrPassword.Location = new System.Drawing.Point(6, 208);
            this.txtBoxSetUsrPassword.Name = "txtBoxSetUsrPassword";
            this.txtBoxSetUsrPassword.Size = new System.Drawing.Size(269, 31);
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
            this.groupBox4.Controls.Add(this.btnServerDefault);
            this.groupBox4.Controls.Add(this.btnCloseApp);
            this.groupBox4.Controls.Add(this.BtnLaunchApp);
            this.groupBox4.Controls.Add(this.btnCloseAppSelUser);
            this.groupBox4.Controls.Add(this.txtBoxServerName);
            this.groupBox4.Controls.Add(this.chkBoxNoUpd);
            this.groupBox4.Controls.Add(this.chkBoxMultiApp);
            this.groupBox4.Controls.Add(this.MSI_Toggle);
            this.groupBox4.Controls.Add(this.chkBoxSetViewPassword);
            this.groupBox4.Controls.Add(this.btnRemoveUser);
            this.groupBox4.Controls.Add(this.btnAddUser);
            this.groupBox4.Controls.Add(this.txtBoxSetUsrPassword);
            this.groupBox4.Controls.Add(this.cmbBoxUsers);
            this.groupBox4.Controls.Add(this.btnRefreshVersions);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cmbBoxVersionsList);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblServer);
            this.groupBox4.Location = new System.Drawing.Point(15, 189);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(565, 371);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "App Manager";
            // 
            // btnServerDefault
            // 
            this.btnServerDefault.Location = new System.Drawing.Point(351, 269);
            this.btnServerDefault.Name = "btnServerDefault";
            this.btnServerDefault.Size = new System.Drawing.Size(128, 46);
            this.btnServerDefault.TabIndex = 18;
            this.btnServerDefault.Text = "Default";
            this.btnServerDefault.UseVisualStyleBackColor = true;
            this.btnServerDefault.Click += new System.EventHandler(this.btnServerDefault_Click);
            // 
            // BtnLaunchApp
            // 
            this.BtnLaunchApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BtnLaunchApp.Location = new System.Drawing.Point(6, 321);
            this.BtnLaunchApp.Name = "BtnLaunchApp";
            this.BtnLaunchApp.Size = new System.Drawing.Size(165, 44);
            this.BtnLaunchApp.TabIndex = 11;
            this.BtnLaunchApp.Text = "Launch";
            this.BtnLaunchApp.UseVisualStyleBackColor = false;
            this.BtnLaunchApp.Click += new System.EventHandler(this.BtnLaunchApp_Click);
            // 
            // btnCloseAppSelUser
            // 
            this.btnCloseAppSelUser.Location = new System.Drawing.Point(203, 321);
            this.btnCloseAppSelUser.Name = "btnCloseAppSelUser";
            this.btnCloseAppSelUser.Size = new System.Drawing.Size(163, 44);
            this.btnCloseAppSelUser.TabIndex = 15;
            this.btnCloseAppSelUser.Text = "Close User";
            this.btnCloseAppSelUser.UseVisualStyleBackColor = true;
            this.btnCloseAppSelUser.Click += new System.EventHandler(this.btnCloseAppSelUser_Click);
            // 
            // txtBoxServerName
            // 
            this.txtBoxServerName.Location = new System.Drawing.Point(6, 277);
            this.txtBoxServerName.Name = "txtBoxServerName";
            this.txtBoxServerName.Size = new System.Drawing.Size(269, 31);
            this.txtBoxServerName.TabIndex = 17;
            // 
            // chkBoxNoUpd
            // 
            this.chkBoxNoUpd.AutoSize = true;
            this.chkBoxNoUpd.Location = new System.Drawing.Point(495, 135);
            this.chkBoxNoUpd.Name = "chkBoxNoUpd";
            this.chkBoxNoUpd.Size = new System.Drawing.Size(62, 29);
            this.chkBoxNoUpd.TabIndex = 14;
            this.chkBoxNoUpd.Text = "-x";
            this.chkBoxNoUpd.UseVisualStyleBackColor = true;
            // 
            // chkBoxMultiApp
            // 
            this.chkBoxMultiApp.AutoSize = true;
            this.chkBoxMultiApp.Location = new System.Drawing.Point(426, 135);
            this.chkBoxMultiApp.Name = "chkBoxMultiApp";
            this.chkBoxMultiApp.Size = new System.Drawing.Size(63, 29);
            this.chkBoxMultiApp.TabIndex = 13;
            this.chkBoxMultiApp.Text = "-a";
            this.chkBoxMultiApp.UseVisualStyleBackColor = true;
            this.chkBoxMultiApp.CheckedChanged += new System.EventHandler(this.chkBoxMultiApp_CheckedChanged);
            // 
            // MSI_Toggle
            // 
            this.MSI_Toggle.Location = new System.Drawing.Point(415, 60);
            this.MSI_Toggle.Name = "MSI_Toggle";
            this.MSI_Toggle.Size = new System.Drawing.Size(142, 45);
            this.MSI_Toggle.TabIndex = 12;
            this.MSI_Toggle.Text = "Squirrel";
            this.MSI_Toggle.UseVisualStyleBackColor = true;
            this.MSI_Toggle.Click += new System.EventHandler(this.BuildToggle_Click);
            // 
            // cmbBoxUsers
            // 
            this.cmbBoxUsers.FormattingEnabled = true;
            this.cmbBoxUsers.Location = new System.Drawing.Point(6, 133);
            this.cmbBoxUsers.Name = "cmbBoxUsers";
            this.cmbBoxUsers.Size = new System.Drawing.Size(269, 33);
            this.cmbBoxUsers.TabIndex = 3;
            this.cmbBoxUsers.TextChanged += new System.EventHandler(this.cmbBoxUsers_TextChanged);
            // 
            // btnRefreshVersions
            // 
            this.btnRefreshVersions.Location = new System.Drawing.Point(281, 60);
            this.btnRefreshVersions.Name = "btnRefreshVersions";
            this.btnRefreshVersions.Size = new System.Drawing.Size(128, 45);
            this.btnRefreshVersions.TabIndex = 2;
            this.btnRefreshVersions.Text = "Refresh";
            this.btnRefreshVersions.UseVisualStyleBackColor = true;
            this.btnRefreshVersions.Click += new System.EventHandler(this.btnRefreshVersions_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version";
            // 
            // cmbBoxVersionsList
            // 
            this.cmbBoxVersionsList.FormattingEnabled = true;
            this.cmbBoxVersionsList.Location = new System.Drawing.Point(6, 60);
            this.cmbBoxVersionsList.Name = "cmbBoxVersionsList";
            this.cmbBoxVersionsList.Size = new System.Drawing.Size(269, 33);
            this.cmbBoxVersionsList.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "User";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(6, 249);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(75, 25);
            this.lblServer.TabIndex = 16;
            this.lblServer.Text = "Server";
            // 
            // Links
            // 
            this.Links.Controls.Add(this.btnPortalLink);
            this.Links.Controls.Add(this.lblPortal);
            this.Links.Controls.Add(this.reeEEE);
            this.Links.Controls.Add(this.btnDesktopApp);
            this.Links.Controls.Add(this.btnWebApp);
            this.Links.Controls.Add(this.btnScrumBoard);
            this.Links.Controls.Add(this.btnTestCycles);
            this.Links.Controls.Add(this.label8);
            this.Links.Controls.Add(this.label4);
            this.Links.Location = new System.Drawing.Point(1238, 12);
            this.Links.Name = "Links";
            this.Links.Size = new System.Drawing.Size(737, 547);
            this.Links.TabIndex = 3;
            this.Links.TabStop = false;
            this.Links.Text = "Links";
            this.Links.Enter += new System.EventHandler(this.Links_Enter);
            // 
            // btnPortalLink
            // 
            this.btnPortalLink.Location = new System.Drawing.Point(12, 282);
            this.btnPortalLink.Name = "btnPortalLink";
            this.btnPortalLink.Size = new System.Drawing.Size(156, 47);
            this.btnPortalLink.TabIndex = 9;
            this.btnPortalLink.Text = "Web App";
            this.btnPortalLink.UseVisualStyleBackColor = true;
            this.btnPortalLink.Click += new System.EventHandler(this.btnPortalLink_Click);
            // 
            // lblPortal
            // 
            this.lblPortal.AutoSize = true;
            this.lblPortal.Location = new System.Drawing.Point(16, 247);
            this.lblPortal.Name = "lblPortal";
            this.lblPortal.Size = new System.Drawing.Size(101, 25);
            this.lblPortal.TabIndex = 8;
            this.lblPortal.Text = "C9 Portal";
            // 
            // reeEEE
            // 
            this.reeEEE.Image = ((System.Drawing.Image)(resources.GetObject("reeEEE.Image")));
            this.reeEEE.Location = new System.Drawing.Point(336, 474);
            this.reeEEE.Name = "reeEEE";
            this.reeEEE.Size = new System.Drawing.Size(57, 53);
            this.reeEEE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.reeEEE.TabIndex = 7;
            this.reeEEE.TabStop = false;
            this.reeEEE.Click += new System.EventHandler(this.reeEEE_Click);
            // 
            // btnDesktopApp
            // 
            this.btnDesktopApp.Location = new System.Drawing.Point(175, 176);
            this.btnDesktopApp.Name = "btnDesktopApp";
            this.btnDesktopApp.Size = new System.Drawing.Size(151, 47);
            this.btnDesktopApp.TabIndex = 6;
            this.btnDesktopApp.Text = "Desktop App";
            this.btnDesktopApp.UseVisualStyleBackColor = true;
            this.btnDesktopApp.Click += new System.EventHandler(this.btnDesktopApp_Click);
            // 
            // btnWebApp
            // 
            this.btnWebApp.Location = new System.Drawing.Point(12, 176);
            this.btnWebApp.Name = "btnWebApp";
            this.btnWebApp.Size = new System.Drawing.Size(156, 47);
            this.btnWebApp.TabIndex = 5;
            this.btnWebApp.Text = "Web App";
            this.btnWebApp.UseVisualStyleBackColor = true;
            this.btnWebApp.Click += new System.EventHandler(this.btnWebApp_Click);
            // 
            // btnScrumBoard
            // 
            this.btnScrumBoard.Location = new System.Drawing.Point(169, 65);
            this.btnScrumBoard.Name = "btnScrumBoard";
            this.btnScrumBoard.Size = new System.Drawing.Size(156, 47);
            this.btnScrumBoard.TabIndex = 4;
            this.btnScrumBoard.Text = "Scrum Board";
            this.btnScrumBoard.UseVisualStyleBackColor = true;
            this.btnScrumBoard.Click += new System.EventHandler(this.btnScrumBoard_Click);
            // 
            // btnTestCycles
            // 
            this.btnTestCycles.Location = new System.Drawing.Point(7, 65);
            this.btnTestCycles.Name = "btnTestCycles";
            this.btnTestCycles.Size = new System.Drawing.Size(156, 47);
            this.btnTestCycles.TabIndex = 3;
            this.btnTestCycles.Text = "Test Cycles";
            this.btnTestCycles.UseVisualStyleBackColor = true;
            this.btnTestCycles.Click += new System.EventHandler(this.btnTestCycles_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "C9 Slack";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "JIRA Test Cycles";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnStartLocalServer);
            this.groupBox6.Location = new System.Drawing.Point(6, 432);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(627, 95);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Local Server Manager";
            // 
            // btnStartLocalServer
            // 
            this.btnStartLocalServer.Location = new System.Drawing.Point(16, 37);
            this.btnStartLocalServer.Name = "btnStartLocalServer";
            this.btnStartLocalServer.Size = new System.Drawing.Size(216, 45);
            this.btnStartLocalServer.TabIndex = 0;
            this.btnStartLocalServer.Text = "Start Local Server";
            this.btnStartLocalServer.UseVisualStyleBackColor = true;
            this.btnStartLocalServer.Click += new System.EventHandler(this.btnStartLocalServer_Click);
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(2112, 572);
            this.Controls.Add(this.Links);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
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
            ((System.ComponentModel.ISupportInitialize)(this.reeEEE)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnVPNSwitch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnVPNSaveSettings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelx;
        private System.Windows.Forms.Button btnChangeVPNClient;
        private System.Windows.Forms.OpenFileDialog opnFDVPNClientSelector;
        private System.Windows.Forms.FolderBrowserDialog fldBrwsDiagSharedFolder;
        private System.Windows.Forms.TextBox lblVPNClientTarget;
        private System.Windows.Forms.Button btnVPNClientLaunch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRefreshVersions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxVersionsList;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox lblC9TraderRoot;
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
        private System.Windows.Forms.Button BtnLaunchApp;
        private System.Windows.Forms.Button MSI_Toggle;
        private System.Windows.Forms.GroupBox Links;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTestCycles;
        private System.Windows.Forms.Button btnDesktopApp;
        private System.Windows.Forms.Button btnWebApp;
        private System.Windows.Forms.Button btnScrumBoard;
        private System.Windows.Forms.CheckBox chkBoxNoUpd;
        private System.Windows.Forms.CheckBox chkBoxMultiApp;
        private System.Windows.Forms.Button btnCloseApp;
        private System.Windows.Forms.PictureBox reeEEE;
        private System.Windows.Forms.Button btnChangeVPNPath;
        private System.Windows.Forms.Button btnCloseAppSelUser;
        private System.Windows.Forms.Button btnDefaultVPN;
        private System.Windows.Forms.TextBox txtBoxServerName;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button btnServerDefault;
        private System.Windows.Forms.Button btnTraderRootSave;
        private System.Windows.Forms.Button btnPortalLink;
        private System.Windows.Forms.Label lblPortal;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnStartLocalServer;
    }
}

