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
            this.lblVPNClientTarget = new System.Windows.Forms.Label();
            this.labely = new System.Windows.Forms.Label();
            this.labelx = new System.Windows.Forms.Label();
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
            this.MSI_Toggle = new System.Windows.Forms.Button();
            this.BtnLaunchApp = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBoxUsers = new System.Windows.Forms.ComboBox();
            this.btnRefreshVersions = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxVersionsList = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.btnVPNClientLaunch.Location = new System.Drawing.Point(6, 107);
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
            this.label1.Location = new System.Drawing.Point(42, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current VPN Services Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.btnSaveSettings);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(587, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1513, 548);
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
            this.groupBox5.Size = new System.Drawing.Size(1501, 250);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "App Manager";
            // 
            // btnChangeC9TraderRoot
            // 
            this.btnChangeC9TraderRoot.Location = new System.Drawing.Point(1275, 66);
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
            this.btnDefaultC9TraderRoot.Location = new System.Drawing.Point(1388, 66);
            this.btnDefaultC9TraderRoot.Name = "btnDefaultC9TraderRoot";
            this.btnDefaultC9TraderRoot.Size = new System.Drawing.Size(107, 40);
            this.btnDefaultC9TraderRoot.TabIndex = 2;
            this.btnDefaultC9TraderRoot.Text = "Default";
            this.btnDefaultC9TraderRoot.UseVisualStyleBackColor = true;
            this.btnDefaultC9TraderRoot.Click += new System.EventHandler(this.btnDefaultC9TraderRoot_Click);
            // 
            // lblC9TraderRoot
            // 
            this.lblC9TraderRoot.AutoSize = true;
            this.lblC9TraderRoot.Location = new System.Drawing.Point(15, 81);
            this.lblC9TraderRoot.Name = "lblC9TraderRoot";
            this.lblC9TraderRoot.Size = new System.Drawing.Size(176, 25);
            this.lblC9TraderRoot.TabIndex = 1;
            this.lblC9TraderRoot.Text = "Trader Root Path";
            this.lblC9TraderRoot.Click += new System.EventHandler(this.lblC9TraderRoot_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "App Root Folder (C9Trader):";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Enabled = false;
            this.btnSaveSettings.Location = new System.Drawing.Point(1373, 496);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(134, 46);
            this.btnSaveSettings.TabIndex = 1;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblVPNClientTarget);
            this.groupBox3.Controls.Add(this.labely);
            this.groupBox3.Controls.Add(this.labelx);
            this.groupBox3.Controls.Add(this.btnChangeVPNClient);
            this.groupBox3.Controls.Add(this.btnDefaultVPNClient);
            this.groupBox3.Location = new System.Drawing.Point(6, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1501, 199);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "VPN Manager";
            // 
            // lblVPNClientTarget
            // 
            this.lblVPNClientTarget.AutoSize = true;
            this.lblVPNClientTarget.Location = new System.Drawing.Point(15, 72);
            this.lblVPNClientTarget.Name = "lblVPNClientTarget";
            this.lblVPNClientTarget.Size = new System.Drawing.Size(0, 25);
            this.lblVPNClientTarget.TabIndex = 6;
            // 
            // labely
            // 
            this.labely.Location = new System.Drawing.Point(356, 117);
            this.labely.Name = "labely";
            this.labely.Size = new System.Drawing.Size(100, 23);
            this.labely.TabIndex = 7;
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
            // btnDefaultVPNClient
            // 
            this.btnDefaultVPNClient.Enabled = false;
            this.btnDefaultVPNClient.Location = new System.Drawing.Point(1388, 39);
            this.btnDefaultVPNClient.Name = "btnDefaultVPNClient";
            this.btnDefaultVPNClient.Size = new System.Drawing.Size(107, 40);
            this.btnDefaultVPNClient.TabIndex = 0;
            this.btnDefaultVPNClient.Text = "Default";
            this.btnDefaultVPNClient.UseVisualStyleBackColor = true;
            this.btnDefaultVPNClient.Click += new System.EventHandler(this.btnDefaultVPNClient_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Enabled = false;
            this.btnRemoveUser.Location = new System.Drawing.Point(348, 156);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(40, 40);
            this.btnRemoveUser.TabIndex = 10;
            this.btnRemoveUser.Text = "-";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Enabled = false;
            this.btnAddUser.Location = new System.Drawing.Point(293, 156);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(40, 40);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "+";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // chkBoxSetViewPassword
            // 
            this.chkBoxSetViewPassword.AutoSize = true;
            this.chkBoxSetViewPassword.Location = new System.Drawing.Point(282, 231);
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
            this.label5.Location = new System.Drawing.Point(7, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Password";
            // 
            // txtBoxSetUsrPassword
            // 
            this.txtBoxSetUsrPassword.Location = new System.Drawing.Point(7, 229);
            this.txtBoxSetUsrPassword.Name = "txtBoxSetUsrPassword";
            this.txtBoxSetUsrPassword.Size = new System.Drawing.Size(244, 31);
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
            this.groupBox4.Controls.Add(this.MSI_Toggle);
            this.groupBox4.Controls.Add(this.BtnLaunchApp);
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
            this.groupBox4.Location = new System.Drawing.Point(15, 189);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(565, 371);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "App Manager";
            // 
            // MSI_Toggle
            // 
            this.MSI_Toggle.Location = new System.Drawing.Point(416, 63);
            this.MSI_Toggle.Name = "MSI_Toggle";
            this.MSI_Toggle.Size = new System.Drawing.Size(143, 44);
            this.MSI_Toggle.TabIndex = 12;
            this.MSI_Toggle.Text = "Squirrel";
            this.MSI_Toggle.UseVisualStyleBackColor = true;
            this.MSI_Toggle.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BtnLaunchApp
            // 
            this.BtnLaunchApp.Location = new System.Drawing.Point(155, 294);
            this.BtnLaunchApp.Name = "BtnLaunchApp";
            this.BtnLaunchApp.Size = new System.Drawing.Size(233, 45);
            this.BtnLaunchApp.TabIndex = 11;
            this.BtnLaunchApp.Text = "Launch Application";
            this.BtnLaunchApp.UseVisualStyleBackColor = true;
            this.BtnLaunchApp.Click += new System.EventHandler(this.BtnLaunchApp_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "User";
            // 
            // cmbBoxUsers
            // 
            this.cmbBoxUsers.FormattingEnabled = true;
            this.cmbBoxUsers.Location = new System.Drawing.Point(7, 156);
            this.cmbBoxUsers.Name = "cmbBoxUsers";
            this.cmbBoxUsers.Size = new System.Drawing.Size(269, 33);
            this.cmbBoxUsers.TabIndex = 3;
            this.cmbBoxUsers.TextChanged += new System.EventHandler(this.cmbBoxUsers_TextChanged);
            // 
            // btnRefreshVersions
            // 
            this.btnRefreshVersions.Location = new System.Drawing.Point(282, 63);
            this.btnRefreshVersions.Name = "btnRefreshVersions";
            this.btnRefreshVersions.Size = new System.Drawing.Size(128, 44);
            this.btnRefreshVersions.TabIndex = 2;
            this.btnRefreshVersions.Text = "Refresh";
            this.btnRefreshVersions.UseVisualStyleBackColor = true;
            this.btnRefreshVersions.Click += new System.EventHandler(this.btnRefreshVersions_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version";
            // 
            // cmbBoxVersionsList
            // 
            this.cmbBoxVersionsList.FormattingEnabled = true;
            this.cmbBoxVersionsList.Location = new System.Drawing.Point(7, 65);
            this.cmbBoxVersionsList.Name = "cmbBoxVersionsList";
            this.cmbBoxVersionsList.Size = new System.Drawing.Size(269, 33);
            this.cmbBoxVersionsList.TabIndex = 0;
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(2112, 572);
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
        private System.Windows.Forms.Button btnChangeVPNClient;
        private System.Windows.Forms.Button btnDefaultVPNClient;
        private System.Windows.Forms.OpenFileDialog opnFDVPNClientSelector;
        private System.Windows.Forms.FolderBrowserDialog fldBrwsDiagSharedFolder;
        private System.Windows.Forms.Label lblVPNClientTarget;
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
        private System.Windows.Forms.Button BtnLaunchApp;
        private System.Windows.Forms.Button MSI_Toggle;
    }
}

