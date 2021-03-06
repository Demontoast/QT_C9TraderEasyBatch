﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Media;
using System.Runtime.InteropServices;
using System.Net;
using Microsoft.VisualBasic;

//need to review the whole account retreival/saving system, the accounts get overwritten when saved.

namespace LM_C9Master
{
    public partial class frmMainForm : Form
    {

        // Initialization
        public frmMainForm()
        {
            InitializeComponent();
        }

        // App Accounts contain a username and a password
        // @strUserName: String containing user's account name
        // @strPassword: String containing a user's password
        public class AppAccount
        {
            public string strUserName = "";
            public string strPassword = "";
            public string strFirm = "";
            public string strGroup = "";
            public string strFeatures = "";

            public String getUserName()
            {
                return strUserName;
            }

            public string getPassword()
            {
                return strPassword;
            }

            public string getFirm()
            {
                return strFirm;
            }

            public string getGroup()
            {
                return strGroup;
            }

            public string getFeatures()
            {
                return strFeatures;
            }
        }

        // ProcessUser is a tuple of username and the process associated with it
        // @userProcess the associated process started with a username
        // @userName the user name associated with a started process
        public class ProcessUser
        {
            public Process userProcess { get; set; }
            public String userName = "";

            public String getUserName()
            {
                return userName;
            }

            public Process getUserProcess()
            {
                return userProcess;
            }

        }

        // Data lists containing app accounts and active processes used in the executable
        List<AppAccount> AccountsFromSettings = new List<AppAccount>();
        List<ProcessUser> ActiveProcesses = new List<ProcessUser>();
        List<String> serverList = new List<String>();
        bool searchFlag = false;
        String[] traderRoots = new String[2];
        frmUserInfoForm userInfoForm;
        frmMainForm mainForm;
        userOrderingForm userOrderForm;
        ARComparer arCompareForm;
        String currVersion;

        // Main method, loads all forms and settings
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            mainForm = this;

            traderRoots[0] = "";
            traderRoots[1] = "";
            currVersion = mainForm.Text;

            this.Width = 1015;
            this.Height = 525;
            this.SetDesktopLocation(130, 75);

            foreach (Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Contains("C9Shell"))
                {
                    ProcessUser x = new ProcessUser();
                    x.userProcess = p;
                    ActiveProcesses.Add(x);
                }
            }

            LoadSettings("VPNCLIENT");
            LoadSettings("C9TRADERROOT");
            LoadSettings("SERVER");

            LoadSettings("ACCESSORIES");
            LoadSettings("APPACCOUNTS");
            LoadSettings("VERSIONMANAGER");
            LoadSettings("TCPVIEW");
            LoadSettings("SQDB");
            LoadSettings("TRSCPSERV");
            LoadSettings("FEATURES");

            btnDefaultC9TraderRoot.Enabled = false;
            btnTraderRootSave.Enabled = false;
            btnDefaultVPN.Enabled = false;
            btnVPNSaveSettings.Enabled = false;
            btnDefaultAccessoryEdits.Enabled = false;
            btnDefaultTrscpServ.Enabled = false;
            btnSaveTrscpServ.Enabled = false;
            btnSaveFirm.Enabled = false;
            btnDefaultFirm.Enabled = false;
            btnSaveGroup.Enabled = false;
            btnDefaultGroup.Enabled = false;

            //VPN Manager Startup Setup

            bool acumbrellaFound = true;
            ServiceController svcAcumbrella;
            try
            {
                svcAcumbrella = new ServiceController("acumbrellaagent");
                if (svcAcumbrella.Status == ServiceControllerStatus.Stopped || svcAcumbrella.Status == ServiceControllerStatus.Running)
                    acumbrellaFound = true;
            }
            catch
            {
                acumbrellaFound = false;
            }

            bool vpnAgentFound = true;
            ServiceController svcVPNAgent;
            try
            {
                svcVPNAgent = new ServiceController("vpnagent");
            }
            catch
            {
                vpnAgentFound = false;
            }
            
            if (!vpnAgentFound)
            {
                BtnVPNSwitch.Text = "OFF";
                BtnVPNSwitch.BackColor = Color.LightCoral;
                BtnVPNSwitch.ForeColor = Color.DarkRed;
            }
            else
            {
                svcVPNAgent = new ServiceController("vpnagent");
                if (!acumbrellaFound)
                {
                    if (svcVPNAgent.Status == ServiceControllerStatus.Stopped)
                    {
                        BtnVPNSwitch.Text = "OFF";
                        BtnVPNSwitch.BackColor = Color.LightCoral;
                        BtnVPNSwitch.ForeColor = Color.DarkRed;
                    }
                    else if (svcVPNAgent.Status == ServiceControllerStatus.Running)
                    {
                        BtnVPNSwitch.Text = "ON";
                        BtnVPNSwitch.BackColor = Color.LightGreen;
                        BtnVPNSwitch.ForeColor = Color.DarkGreen;
                    }
                }
                else
                {
                    svcAcumbrella = new ServiceController("acumbrellaagent");
                    if (svcAcumbrella.Status == ServiceControllerStatus.Stopped)
                    {
                        BtnVPNSwitch.Text = "OFF";
                        BtnVPNSwitch.BackColor = Color.LightCoral;
                        BtnVPNSwitch.ForeColor = Color.DarkRed;
                    }
                    else if (svcAcumbrella.Status == ServiceControllerStatus.Running)
                    {
                        BtnVPNSwitch.Text = "ON";
                        BtnVPNSwitch.BackColor = Color.LightGreen;
                        BtnVPNSwitch.ForeColor = Color.DarkGreen;
                    }
                }
            }

            ///
            //App Manager Startup Setup
            RefreshVersions();
            ///

                
        }

        // Loads all settings from the LM_C9Settings.set file
        // @string strWhat2Load: denotes the specific section of the .set file to read to load
        public void LoadSettings(string strWhat2Load)
        {
            string[] LineSplit= null;
            string Line = "";
            switch (strWhat2Load)
            {

                case "VPNCLIENT":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while ((Line = SR.ReadLine()) != "<VPNClient>")
                        {
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND <VPNClient>");
                                break;
                            }
                        }
                        while ((Line = SR.ReadLine())!= "</VPNClient>")
                        {
                            try
                            {
                                LineSplit = Line.Split('=');
                                if (LineSplit[0] == "VPNClientLocation")
                                {
                                    lblVPNClientTarget.Text = LineSplit[1];
                                    break;
                                }
                            }
                            catch
                            {

                            }
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND </VPNClient>");
                                break;
                            }
                        }
                    }
                    break;
                case "C9TRADERROOT":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while ((Line = SR.ReadLine()) != "<AppManager>")
                        {
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND <AppManager>");
                                break;
                            }
                        }
                        while ((Line = SR.ReadLine()) != "</AppManager>")
                        {
                            try
                            {
                                LineSplit = Line.Split('=');
                                if (LineSplit[0] == "C9TraderRootLocation")
                                {
                                    lblC9TraderRoot.Text = LineSplit[1];
                                    traderRoots[0] = LineSplit[1];
                                }
                                if (LineSplit[0] == "C9TraderMSILocation")
                                {
                                    traderRoots[1] = LineSplit[1];
                                }
                                if (MSI_Toggle.Text.Equals("Squirrel") || MSI_Toggle.Text.Equals("Production"))
                                    lblC9TraderRoot.Text = traderRoots[0];
                                else
                                    lblC9TraderRoot.Text = traderRoots[1];
                            }
                            catch
                            {

                            }
                            
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND </AppManager>");
                                break;
                            }
                        }
                    }
                    break;
                case "APPACCOUNTS": //works

                    cmbBoxUsers.Items.Clear();
                    AccountsFromSettings.Clear();
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while ((Line = SR.ReadLine()) != "<UserCollection>")
                        {
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND <UserCollection>");
                                break;
                            }
                        }
                        if ((Line = SR.ReadLine()) != "</UserCollection>")
                        {
                            while (Line != "</UserCollection>")
                            {
                                AppAccount TmpAccount = new AppAccount();
                                LineSplit = Line.Split(':');
                                try
                                {
                                    TmpAccount.strUserName = LineSplit[0];
                                    try
                                    {
                                        TmpAccount.strPassword = LineSplit[1];
                                    }
                                    catch
                                    {

                                    }
                                }
                                catch
                                {

                                }
                                
                                try
                                {
                                    TmpAccount.strFirm = LineSplit[2];
                                    try
                                    {
                                        TmpAccount.strGroup = LineSplit[3];
                                    }
                                    catch
                                    {
                                    }
                                }
                                catch
                                { }
                                
                                if(TmpAccount!=null)
                                    AccountsFromSettings.Add(TmpAccount);

                                if (Line.Equals(null))
                                {
                                    MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND </UserCollection>");
                                    break;
                                }
                                Line = SR.ReadLine();
                                
                            }


                        }
                        else
                        {
                            AppAccount tmpaccount = new AppAccount();
                            tmpaccount.strUserName = "NoSavedAccounts";
                            tmpaccount.strPassword = "";
                            AccountsFromSettings.Add(tmpaccount);
                        }
                        foreach (AppAccount AC in AccountsFromSettings)
                        {
                            cmbBoxUsers.Items.Add(AC.strUserName);

                        }

                        cmbBoxUsers.SelectedIndex = 0;
                        AutoSetAccSettings(cmbBoxUsers.Text); //works
                        //btnAddUser.Enabled = false;
                        //btnRemoveUser.Enabled = false;
                    }
                    
                    break;
                case "SERVER":
                    txtBoxServerName.Items.Clear();
                    serverList.Clear();
                    String defServ = "";
                    bool flag = false;
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while ((Line = SR.ReadLine()) != "<DesignatedServer>")
                        {
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND <DesignatedServer>");
                                break;
                            }
                        }
                        while ((Line = SR.ReadLine()) != "</DesignatedServer>")
                        {
                                if (Line.Contains("Server="))
                                {
                                    defServ = Line.Remove(0, 7);
                                    if (!serverList.Contains(defServ))
                                        serverList.Add(defServ);
                                    flag = true;
                                    continue;
                                }
                                serverList.Add(Line);
                                flag = true;
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND </DesignatedServer>");
                                break;
                            }
                        }
                        
                        if (!flag)
                        {
                            defServ = "NoDefaultServer";
                            serverList.Add("NoDefaultServer");
                        }
                        foreach (String s in serverList)
                        {
                            txtBoxServerName.Items.Add(s);
                        }
                    }
                    txtBoxServerName.SelectedItem = defServ;
                    break;
                case "FEATURES":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while (!(Line = SR.ReadLine()).Equals("<UserInfo>"))
                        {
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: <UserInfo> not found!");
                                break;
                            }
                        }
                        while (!(Line = SR.ReadLine()).Equals("</UserInfo>"))
                        {
                            if (Line.Equals(null))
                            {
                                MessageBox.Show("WARNING: </UserInfo> not found, UserInfo is corrupted!");
                                break;
                            }
                            try
                            {
                                LineSplit = Line.Split(':');

                                foreach(AppAccount AC in AccountsFromSettings)
                                {
                                    if (AC.strUserName.Equals(LineSplit[0]))
                                    {
                                        AC.strFeatures = LineSplit[1];
                                        break;
                                    }   
                                }
                            }
                            catch
                            { }

                        }
                    }
                    break;
                case "ACCESSORIES":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "<Accessories>")
                        {

                            if (Line == (null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND <Accessories>");
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                        while (Line != "</Accessories>")
                        {  
                            try
                            {
                                LineSplit = Line.Split('=');
                                TabPage newTab = new TabPage(LineSplit[0]);
                                TextBox newTabFPBox = new TextBox();
                                newTabFPBox.SetBounds(0, 0, 300, 50);
                                newTabFPBox.Text = LineSplit[1];
                                newTabFPBox.Enabled = false;
                                newTab.Controls.Add(newTabFPBox);
                                accessoryTabs.TabPages.Add(newTab);
                            }
                            catch
                            {

                            }
                            if (Line == (null))
                            {
                                MessageBox.Show("WARNING: SETTINGS FILE CORRUPTED CANNOT FIND </Accessories>");
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                        break;
                    }
            }
        }

        // Sets the password text box to the password known to be associated with an account
        // @string usrname: Parameter denoting which user's password to be supplied
        public void AutoSetAccSettings(string usrname)
        {
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName == usrname)
                {
                    txtBoxSetUsrPassword.Text = AC.strPassword;
                    txtBoxFirm.Text = AC.strFirm;
                    txtBoxGroup.Text = AC.strGroup;
                }
            }
        }

        // @Leo TBH I'm not entirely sure where this button can even be found
        private void btnChangeVPNClient_Click(object sender, EventArgs e)
        {
            DialogResult VPNSelectorResult = new DialogResult();
            VPNSelectorResult = opnFDVPNClientSelector.ShowDialog();
            if (VPNSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = opnFDVPNClientSelector.FileName;
                if (strResultChangeCheck != lblVPNClientTarget.Text)
                {
                    lblVPNClientTarget.Text = strResultChangeCheck;
                    btnDefaultVPN.Enabled = true;
                    btnVPNSaveSettings.Enabled = true;
                }

            }
        }

        // Action Listener for the Change button used to change the C9 Trader root directory
        private void btnChangeC9TraderRoot_Click(object sender, EventArgs e)
        {
            DialogResult C9TraderRootSelectorResult = new DialogResult();
            C9TraderRootSelectorResult = fldBrwsDiagSharedFolder.ShowDialog();
            if (C9TraderRootSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = fldBrwsDiagSharedFolder.SelectedPath;
                if (strResultChangeCheck != lblC9TraderRoot.Text)
                {
                    lblC9TraderRoot.Clear();
                    lblC9TraderRoot.Text = strResultChangeCheck;
                    btnDefaultC9TraderRoot.Enabled = true;
                    btnTraderRootSave.Enabled = true;
                }
            }
        }

        // Action Listener for the Default button used to revert the C9 Trader root directory to the default
        // designated in the settings file
        private void btnDefaultC9TraderRoot_Click(object sender, EventArgs e)
        {
            LoadSettings("C9TRADERROOT");
            btnDefaultC9TraderRoot.Enabled = false;
            btnTraderRootSave.Enabled = false;
            lblC9TraderRoot.Select();
        }

        private void btnVPNSaveSettings_Click(object sender, EventArgs e)
        {
            List<string> strCurrentSettings = new List<string>();

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                string Line = SR.ReadLine();
                while (Line != null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                }
            }
            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
            {
                if (strCurrentSettings != null)
                {
                    foreach (string s in strCurrentSettings)
                    {
                        if (s.Contains("VPNClientLocation="))
                        {
                            String append = s;
                            append = "VPNClientLocation=" + lblVPNClientTarget.Text;
                            SW.WriteLine(append);
                        }
                        else
                            SW.WriteLine(s);
                    }
                }
            }

            btnVPNSaveSettings.Enabled = false;
            btnDefaultVPN.Enabled = false;


        }

        // Action Listener for the VPN Services button, turns VPN services on / off
        private void BtnVPNSwitch_Click(object sender, EventArgs e)
        {
            bool acumbrellaFound = true;
            bool svcVPNAgentFound = true;

            ServiceController svcAcumbrella;
            ServiceController svcVPNAgent;

            try
            {
                svcAcumbrella = new ServiceController("acumbrellaagent");
            }
            catch
            {
                acumbrellaFound = false;
            }

            try
            {
                svcVPNAgent = new ServiceController("vpnagent");
            }
            catch
            {
                svcVPNAgentFound = false;
            }

            
            if (BtnVPNSwitch.Text == "OFF")
            {
                    if (acumbrellaFound)
                    {
                        svcAcumbrella = new ServiceController("acumbrellaagent");

                    if (svcAcumbrella.Status == ServiceControllerStatus.Stopped)
                        {
                            svcAcumbrella.Start();
                        }
                    }
                    
                    
                    if (svcVPNAgentFound)
                    {
                        svcVPNAgent = new ServiceController("vpnagent");

                    if (svcVPNAgent.Status == ServiceControllerStatus.Stopped)
                        {
                            svcVPNAgent.Start();
                        }

                        BtnVPNSwitch.Enabled = false;
                        Thread.Sleep(1500);
                        BtnVPNSwitch.Text = "ON";
                        BtnVPNSwitch.BackColor = Color.LightGreen;
                        BtnVPNSwitch.ForeColor = Color.DarkGreen;
                        BtnVPNSwitch.Enabled = true;
                }
                    else
                    {
                        MessageBox.Show("VPN services are not working properly..." + Environment.NewLine + "Please verify their status in Windows Task Manager", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        BtnVPNSwitch.Enabled = false;
                    }

                    
            }
            else
            {
                    if (acumbrellaFound)
                    {
                        svcAcumbrella = new ServiceController("acumbrellaagent");
                        if (svcAcumbrella.Status == ServiceControllerStatus.Running)
                        {
                            svcAcumbrella.Stop();
                        }
                    }
                    
                    if (svcVPNAgentFound)
                    {
                        svcVPNAgent = new ServiceController("vpnagent");
                        if (svcVPNAgent.Status == ServiceControllerStatus.Running)
                        {
                            svcVPNAgent.Stop();
                        }

                        BtnVPNSwitch.Enabled = false;
                        Thread.Sleep(1500);
                        BtnVPNSwitch.Text = "OFF";
                        BtnVPNSwitch.BackColor = Color.LightCoral;
                        BtnVPNSwitch.ForeColor = Color.DarkRed;
                        BtnVPNSwitch.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("VPN services are not working properly..." + Environment.NewLine + "Please verify their status in Windows Task Manager", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        BtnVPNSwitch.Enabled = false;
                    } 
            }
        }

        // Action Listener for the Launch Client button that will start the VPN client executable
        private void btnVPNClientLaunch_Click(object sender, EventArgs e)
        {

            ProcessStartInfo ProcVPNCli = new ProcessStartInfo();
            ProcVPNCli.FileName = lblVPNClientTarget.Text;


            //@Will had to slightly modify this because the app was giving me the vpnui.exe not found error.
            //The reason for this is that ProcVPNCli.FileName contains the FULL PATH of the executable, so it can never match up to "vpnui.exe" only.
            //So, I split out the full path of the textbox and compared the last item from the split to "vpnui.exe". 
            //This step can stay here for now, but i have a few ideas on how to optimize the code a little and have it run in the form loading function,
            //instead of having it run every single time the user clicks this button. 


            String[] fileNameSplitter = lblVPNClientTarget.Text.Split('\\');
            if (fileNameSplitter[fileNameSplitter.Length - 1] != "vpnui.exe")

            {
                MessageBox.Show("Error: vpnui.exe not selected.");
                btnDefaultVPN_Click(sender, e);
            }
            else
            {
                Process.Start(ProcVPNCli);
            }
        }

        // Function that refreshes the UI whenever changes are made to the version directories or build version
        public void RefreshVersions()
        {
            // Tries the existing filepath for the C9Trader, otherwise looks in the expected folder
            try
            {
                Process.Start(lblC9TraderRoot.ToString());
            }
            catch (System.ComponentModel.Win32Exception)
            {
                string startPath;
                // Determines if user has settings for Squirrel or MSI build
                if (MSI_Toggle.Text == "Squirrel" || MSI_Toggle.Text == "Production")
                {
                    LoadSettings("C9TRADERROOT");
                    try
                    {
                        string[] strSubDirList = Directory.GetDirectories(lblC9TraderRoot.Text);
                        foreach (string s in strSubDirList)
                        {
                            string[] strPathSplit = s.Split('\\');
                            string[] strAppSplit = strPathSplit[strPathSplit.Length - 1].Split('-');
                            if (strAppSplit[0] == "app" && !cmbBoxVersionsList.Items.Contains(strAppSplit[1]))
                            {
                                cmbBoxVersionsList.SelectedIndex = cmbBoxVersionsList.Items.Add(strAppSplit[1]);
                            }
                        }
                    }
                    catch
                    { }
                }
                else if (MSI_Toggle.Text == "MSI")
                {
                    if (!traderRoots[1].Equals(null) && !traderRoots[1].Equals(""))
                        startPath = traderRoots[1];
                    else
                        startPath = @"C:\Program Files (x86)\Cloud9 Technologies LLC\C9Trader";
                    lblC9TraderRoot.Text = startPath;
                }
            }
        }

        // Action Listener for the Refresh button that calls the RefreshVersions function manually
        private void btnRefreshVersions_Click(object sender, EventArgs e)
        {
            RefreshVersions();
        }

        // Action Listener for the View Password Check Box enabling the user to see their password rather than
        // hidden characters
        private void chkBoxSetViewPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxSetUsrPassword.UseSystemPasswordChar = !chkBoxSetViewPassword.Checked;
        }

        // Action Listener for the user password text box that tracks any changes made in user password allowing
        // a new user to be added or an old user to be removed
        private void txtBoxSetUsrPassword_TextChanged(object sender, EventArgs e)
        {
            btnAddUser.Enabled = false;
            btnRemoveUser.Enabled = false;
            if (txtBoxSetUsrPassword.Text != "" && (cmbBoxUsers.Text != "" && cmbBoxUsers.Text != "NoSavedAccounts") && !searchFlag)
            {
                btnAddUser.Enabled = true;
                btnRemoveUser.Enabled = true;
            }
        }

        // Action Listener for the + button that adds a new user to the settings file using the username and
        // and password supplied in the appropriate fields
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            //EXECUTE CHECKS ON ACCOUNTSFROMSETTINGS LIST (THE USER DOESNT EXISTS ALREADY, ETC.) CAN EASILY BE DONE WITH FLAGS

            bool flgMustAddNewUser = true;
            if (AccountsFromSettings[0].strUserName == "NoSavedAccounts")
                AccountsFromSettings[0].strUserName = "";

            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName == cmbBoxUsers.Text)
                {
                    flgMustAddNewUser = false;
                    break;
                }
            }
            if (flgMustAddNewUser)
            {
                AppAccount tmpAcc = new AppAccount();
                tmpAcc.strUserName = cmbBoxUsers.Text;
                tmpAcc.strPassword = txtBoxSetUsrPassword.Text;
                tmpAcc.strFirm = txtBoxFirm.Text;
                tmpAcc.strGroup = txtBoxGroup.Text;
                AccountsFromSettings.Add(tmpAcc);
                SaveAccountsToSettings();
                cmbBoxUsers.Text = "";
                txtBoxSetUsrPassword.Text = "";
                cmbBoxUsers.SelectedText = tmpAcc.strUserName;
                MessageBox.Show("User " + tmpAcc.strUserName + ":" + tmpAcc.strPassword + " has been added!");
                //cmbBoxUsers.SelectedText = tmpAcc.strUserName;
                //cmbBoxUsers.Text = tmpAcc.strUserName;
                //txtBoxSetUsrPassword.Text = tmpAcc.strPassword;
            }
            else
                MessageBox.Show("User " + cmbBoxUsers.Text + " already exists.");

        }

        // Function used to write new or modified accounts to the LM_C9Settings.set file
        public void SaveAccountsToSettings()
        {

            string Line = null;
            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                using (StreamWriter SW = new StreamWriter("LM_C9MSettings.new"))
                {
                    while ((Line = SR.ReadLine()) != "<UserCollection>")
                        SW.WriteLine(Line);
                    SW.WriteLine("<UserCollection>");
                    foreach (AppAccount AC in AccountsFromSettings)
                    {
                        if (!AC.strUserName.Equals("") && !AC.strUserName.Equals("NoSavedAccounts"))
                            SW.WriteLine(AC.strUserName + ":" + AC.strPassword + ":" + AC.strFirm + ":" + AC.strGroup);
                    }
                    SW.WriteLine("</UserCollection>");
                }


            }
            File.Delete("LM_C9MSettings.set");
            File.Copy("LM_C9MSettings.new", "LM_C9MSettings.set");
            File.Delete("LM_C9MSettings.new");
            LoadSettings("APPACCOUNTS");
        }

        // Action Listener for the user combo box that automatically supplies a saved password if the user
        // selected is changed
        private void cmbBoxUsers_TextChanged(object sender, EventArgs e)
        {   
            btnAddUser.Enabled = false;
            btnRemoveUser.Enabled = false;
            if (txtBoxSetUsrPassword.Text != "" && cmbBoxUsers.Text != "" && !searchFlag)
            {
                btnAddUser.Enabled = true;
                btnRemoveUser.Enabled = true;
            }
            AutoSetAccSettings(cmbBoxUsers.Text);
        }

        private void cmbBoxUsers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!cmbBoxUsers.Text.Equals("") && !cmbBoxUsers.Text.Equals(null))
                {
                    BtnLaunchApp_Click(sender, e);
                }
                
            }
        }

        // Action Listener for the button that launches the application using information supplied in the user
        // combo box, password text box, and Build Toggle button as well as any additional parameters (-x or -a)
        private void BtnLaunchApp_Click(object sender, EventArgs e)
        {
            String parameters = "";

            if (txtBoxServerName.Text.Equals("") || txtBoxServerName.Text.Equals(null))
                txtBoxServerName.Text = "https://qa1-rest.xhoot.com";

            parameters += "-u " + cmbBoxUsers.Text + " -p " + txtBoxSetUsrPassword.Text + " -r " + txtBoxServerName.Text;
            if (chkBoxMultiApp.Checked == true)
                parameters += " -a";
            if (chkBoxNoUpd.Checked == true)
                parameters += " -x";
            if (!txtBoxExtraParams.Text.Equals("") && !txtBoxExtraParams.Text.Equals(null))
                parameters += " " + txtBoxExtraParams.Text;
            if (!txtBoxTranscriptionServer.Text.Equals("") && !txtBoxTranscriptionServer.Text.Equals(null))
                parameters += " -t " + txtBoxTranscriptionServer.Text;
            ProcessUser pram = new ProcessUser();
            Process p = new Process();
            pram.userName = cmbBoxUsers.Text;
            
            //Launches the app using user settings
            try
            {
                if (MSI_Toggle.Text == "Squirrel")
                {
                    p = Process.Start(lblC9TraderRoot.Text + "\\app-" + cmbBoxVersionsList.Text + "\\C9Shell.exe", parameters);
                }
                else if (MSI_Toggle.Text == "MSI")
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "C9Shell.exe";
                    psi.WorkingDirectory = lblC9TraderRoot.Text;
                    psi.Arguments = parameters;
                    p = Process.Start(psi);
                }
                else
                {
                    p = Process.Start(lblC9TraderRoot.Text + "\\app-" + cmbBoxVersionsList.Text + "\\C9Shell.exe");
                }

                // Overwrites current settings in file with that currently used
                foreach (AppAccount AC in AccountsFromSettings)
                {
                    if (AC.strUserName == cmbBoxUsers.Text)
                    {
                        if (AC.strPassword != txtBoxSetUsrPassword.Text)
                            AC.strPassword = txtBoxSetUsrPassword.Text;
                        if (AC.strFirm != txtBoxFirm.Text)
                            AC.strFirm = txtBoxFirm.Text;
                        if (AC.strGroup != txtBoxGroup.Text)
                            AC.strGroup = txtBoxGroup.Text;
                    }
                }
                SaveAccountsToSettings();
                if (searchFlag)
                {
                    txtBoxNewUserSearch.Text = txtBoxCurrUserSearch.Text;
                    txtBoxNewFirmSearch.Text = txtBoxCurrFirmSearch.Text;
                    txtBoxNewGroupSearch.Text = txtBoxCurrGroupSearch.Text;
                    btnSearch_Click(sender, e);
                }
                    
                cmbBoxUsers.Text = "";
                cmbBoxUsers.Text = pram.userName;

                pram.userProcess = p;
                ActiveProcesses.Add(pram);
                BtnLaunchApp.Enabled = false;
                Thread.Sleep(1500);
                BtnLaunchApp.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Error: Invalid path, C9Trader not found.");
                if (MSI_Toggle.Text == "Squirrel")
                {
                    btnDefaultC9TraderRoot_Click(sender, e);
                }
                else
                {
                    lblC9TraderRoot.Text = @"C:\Program Files (x86)\Cloud9 Technologies LLC\C9Trader";
                }
            }
        }

        // Not Implemented 
        private void lblC9TraderRoot_Click(object sender, EventArgs e)
        {

        }

        // Action Listener for the button that toggles between MSI and Squirrel build to launch
        private void BuildToggle_Click(object sender, EventArgs e)
        {
            if (MSI_Toggle.Text == "Squirrel")
                MSI_Toggle.Text = "MSI";
            else if (MSI_Toggle.Text == "MSI")
                MSI_Toggle.Text = "Production";
            else if (MSI_Toggle.Text == "Production")
                MSI_Toggle.Text = "Squirrel";

            if (MSI_Toggle.Text.Equals("Squirrel") || MSI_Toggle.Text.Equals("Production"))
            {
                if (!lblC9TraderRoot.Text.Equals(traderRoots[0]) && !traderRoots[0].Equals(""))
                    lblC9TraderRoot.Text = traderRoots[0];

            }
            else if (MSI_Toggle.Text.Equals("MSI"))
            {

                if (!lblC9TraderRoot.Text.Equals(traderRoots[1]) && !traderRoots[1].Equals(""))
                    lblC9TraderRoot.Text = traderRoots[1];
            }
            RefreshVersions();
        }

        private void Links_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //Action Listener for the button that opens the JIRA Test Cycles page in default browser
        private void btnTestCycles_Click(object sender, EventArgs e)
        {
            //Opens the JIRA Test Cycles page in default browser
            System.Diagnostics.Process.Start("https://6w46h65ghw56gh7.atlassian.net/projects/CTTEST?selectedItem=com.thed.zephyr.je__project-centric-view-tests-page&testsTab=test-cycles-tab");
        }

        // Action Listener for the button that opens the JIRA Scrum Board
        private void btnScrumBoard_Click(object sender, EventArgs e)
        {
            //Opens the JIRA Scrum Board page in default browser
            System.Diagnostics.Process.Start("https://6w46h65ghw56gh7.atlassian.net/secure/RapidBoard.jspa?rapidView=56");
        }

        // Action Listener for the button that opens the Slack web app
        private void btnWebApp_Click(object sender, EventArgs e)
        {
            //Opens the C9 Slack webapp in default browser
            System.Diagnostics.Process.Start("https://cloud9tec.slack.com/messages/C4N3M42QP/details/");
        }

        // Action Listener for the button that opens the Slack desktop app
        private void btnDesktopApp_Click(object sender, EventArgs e)
        {
            //Opens Slack desktop app
            String curUser = Environment.UserName;
            System.Diagnostics.Process.Start("C:\\Users\\" + curUser + "\\AppData\\Local\\slack\\slack.exe");
        }

        // Not implemented
        private void chkBoxMultiApp_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Action Listener for the - button that removes a user from saved users
        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName == cmbBoxUsers.Text)
                {
                    int index = AccountsFromSettings.IndexOf(AC);
                    AccountsFromSettings.Remove(AC);
                    SaveAccountsToSettings();
                    break;
                }
            }
        }

        // Action Listener for the Close Open Applications button which closes all active instances of the C9 Trader
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            if (ActiveProcesses.Count != 0)
            {
                while (ActiveProcesses.Count != 0)
                {
                    try
                    {
                        ActiveProcesses.ElementAt(0).userProcess.Kill();
                        ActiveProcesses.Remove(ActiveProcesses.ElementAt(0));
                    }
                    catch
                    {
                        if (ActiveProcesses.ElementAt(0).userProcess.HasExited)
                        {
                            ActiveProcesses.Remove(ActiveProcesses.ElementAt(0));
                        }
                        else
                        {
                            ActiveProcesses.ElementAt(0).userProcess.Kill();
                            ActiveProcesses.Remove(ActiveProcesses.ElementAt(0));
                        }
                    }
                }
            }
            else
                MessageBox.Show("Error: No active applications.");

            btnCloseApp.Enabled = false;
            Thread.Sleep(1500);
            btnCloseApp.Enabled = true;
        }

        // For fun button
        private void reeEEE_Click(object sender, EventArgs e)
        {
            //String soundLoc =
            //SoundPlayer simpleSound = new SoundPlayer("reeeeeeeee2.wav");
            String strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            String strFilePath = Path.Combine(strAppPath, "Resources");
            String strFullFilename = Path.Combine(strFilePath, "reeeeeeeee2.wav");
            SoundPlayer simpleSound = new SoundPlayer(strFullFilename);
            simpleSound.Play();
        }

        // Action listener for the Change button for the VPN that changes where vpnui.exe can be found
        private void btnChangeVPNPath_Click(object sender, EventArgs e)
        {
            DialogResult VPNSelectorResult = new DialogResult();
            VPNSelectorResult = opnFDVPNClientSelector.ShowDialog();
            if (VPNSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = opnFDVPNClientSelector.FileName;
                if (strResultChangeCheck != lblVPNClientTarget.Text)
                {
                    lblVPNClientTarget.Clear();
                    lblVPNClientTarget.Text = strResultChangeCheck;
                    btnDefaultVPN.Enabled = true;
                    btnVPNSaveSettings.Enabled = true;
                }

            }
            lblVPNClientTarget.Select();
        }

        // Action listener for the Close Selected User App button that closes an active C9 Trader based on user
        // selected in the user combo box
        private void btnCloseAppSelUser_Click(object sender, EventArgs e)
        {
            List<ProcessUser> closeUsers = new List<ProcessUser>();
            if (ActiveProcesses.Count != 0)
            {
                bool exists = false;
                foreach (ProcessUser p in ActiveProcesses)
                {
                    if (p.userName.Equals(cmbBoxUsers.Text))
                    {
                        try
                        {
                            exists = true;
                            if (!p.userProcess.HasExited)
                                p.userProcess.Kill();

                            closeUsers.Add(p);
                        }
                        catch
                        {
                            MessageBox.Show("Error: Something went wrong!");
                        }
                        
                    }
                }
                if (!exists)
                {
                    MessageBox.Show("Error: User " + cmbBoxUsers.Text + " is not active on the application.");
                }
            }
            else
                MessageBox.Show("Error: No active applications.");
            foreach (ProcessUser PU in closeUsers)
            {
                ActiveProcesses.Remove(PU);
            }
            btnCloseAppSelUser.Enabled = false;
            Thread.Sleep(1500);
            btnCloseAppSelUser.Enabled = true;
        }

        // Action Listener for the VPN Default button, reverts the text box for the VPN filepath to known defaults
        private void btnDefaultVPN_Click(object sender, EventArgs e)
        {
            LoadSettings("VPNCLIENT");
            btnDefaultVPN.Enabled = false;
            btnVPNSaveSettings.Enabled = false;
            lblVPNClientTarget.Select();
        }

        // Action Listener for the trader root text box, enables the save settings and default trader root buttons
        private void lblC9TraderRoot_TextChanged(object sender, EventArgs e)
        {
            btnTraderRootSave.Enabled = true;
            btnDefaultC9TraderRoot.Enabled = true;
            
                
        }

        // Action Listener for the vpn target text box, enables the save settings and default vpn buttons
        private void lblVPNClientTarget_TextChanged(object sender, EventArgs e)
        {
            btnVPNSaveSettings.Enabled = true;
            btnDefaultVPN.Enabled = true;
        }

        // Action Listener for the Server Default button which defaults the server text box to https://qa1-rest.xhoot.com
        private void btnServerDefault_Click(object sender, EventArgs e)
        {
            List<string> strCurrentSettings = new List<string>();
            bool justAdded = false;
            if (!serverList.Contains(txtBoxServerName.Text))
            {
                btnSaveServer_Click(btnSaveServer, EventArgs.Empty);
                justAdded = true;
            }
                

            String Line = "";

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                Line = SR.ReadLine();
                while (Line != null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                }
            }
            bool alrdyDef = false;
            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
            {
                String defServ = "";
                foreach (String s in strCurrentSettings)
                {
                    if (s.Contains("Server=") && !s.Contains("Transcription"))
                    {
                        String append = s;
                        defServ = append.Remove(0, 7);
                        append = "Server=" + txtBoxServerName.Text;
                        SW.WriteLine(append);
                        if (txtBoxServerName.Text.Equals(defServ))
                        {
                            if (!justAdded)
                            {
                                alrdyDef = true;
                                MessageBox.Show(txtBoxServerName.Text + " is already the default server.");
                            }   
                            else
                                MessageBox.Show(txtBoxServerName.Text + " has been added to the server list and designated as the default.");
                            
                            continue;
                        }
                        if (!defServ.Equals("NoDefaultServer"))
                            SW.WriteLine(defServ);
                        if (s.Contains("NoDefaultServer"))
                            txtBoxServerName.Items.Remove("NoDefaultServer");
                        continue;
                    }
                    if (s.Contains(txtBoxServerName.Text))
                        continue;  
                    SW.WriteLine(s);
                }
            }
            if (!alrdyDef && !justAdded)
            {
                MessageBox.Show(txtBoxServerName.Text + " has been set as the default server on startup.");
                if (!txtBoxServerName.Items.Contains(txtBoxServerName.Text))
                    txtBoxServerName.Items.Add(txtBoxServerName.Text);
            }
                
        }

        // Action Listener for the Trader Root Save button which saves the current trader root in the settings file
        private void btnTraderRootSave_Click(object sender, EventArgs e)
        {
            List<string> strCurrentSettings = new List<string>();

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                string Line = SR.ReadLine();
                while (Line != null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                    
                }
            }
            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
            {
                if (strCurrentSettings != null)
                {
                    foreach (string s in strCurrentSettings)
                    {
                        if (MSI_Toggle.Text.Equals("Squirrel") || MSI_Toggle.Text.Equals("Production"))
                        {
                            if (s.Contains("C9TraderRootLocation="))
                            {
                                String append = s;
                                append = "C9TraderRootLocation=" + lblC9TraderRoot.Text;
                                SW.WriteLine(append);
                            }
                            else
                                SW.WriteLine(s);
                        }
                        else
                        {
                            if (s.Contains("C9TraderMSILocation="))
                            {
                                String append = s;
                                append = "C9TraderMSILocation=" + lblC9TraderRoot.Text;
                                SW.WriteLine(append);
                            }
                            else
                                SW.WriteLine(s);
                        }
                    }
                }
            }

            btnTraderRootSave.Enabled = false;
            btnDefaultC9TraderRoot.Enabled = false;
        }

        // Action Listener for the button that starts the local server for the user (api_server.py)
        private void btnStartLocalServer_Click(object sender, EventArgs e)
        {
            string tempFilename = Path.ChangeExtension(Path.GetTempFileName(), ".bat");

            string currUser = Environment.UserName;

            if (!Directory.Exists(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\"))
                Directory.CreateDirectory(@"C: \Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\");

            String strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            String strFilePath = Path.Combine(strAppPath, "Resources");
            String strFullFilename = Path.Combine(strFilePath, "api_server.py");
            System.IO.File.Copy(strFullFilename, @"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\api_server.py", true);
            strFullFilename = Path.Combine(strFilePath, "requirements.txt");
            System.IO.File.Copy(strFullFilename, @"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\requirements.txt", true);
            strFullFilename = Path.Combine(strFilePath, "c9localhost.crt");
            System.IO.File.Copy(strFullFilename, @"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\c9localhost.crt", true);
            strFullFilename = Path.Combine(strFilePath, "nopasskey.pem");
            System.IO.File.Copy(strFullFilename, @"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\nopasskey.pem", true);

            using (StreamWriter writer = new StreamWriter(tempFilename))
            {
                writer.WriteLine(@"@echo off");
                writer.WriteLine("c:");
                writer.WriteLine(@"cd C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\");
                writer.WriteLine("pip install setuptools");
                writer.WriteLine("pip install -r requirements.txt");
                writer.WriteLine("python api_server.py");
            }
            Process.Start(tempFilename);
        }

        private void btnLaunchAccessory_Click(object sender, EventArgs e)
        {
                try
                {
                    Point p = new Point(0, 0);
                    Process.Start(accessoryTabs.SelectedTab.GetChildAtPoint(p).Text);
                }
                catch
                {
                    MessageBox.Show("Error: Invalid Filepath!");
                }
        }

        public String VMDirSearch(String a)
        {
            String filePath = "";
            foreach (string d in Directory.GetDirectories(a))
            {
                foreach (string f in Directory.GetFiles(d, "*.exe"))
                {
                    filePath = Path.GetFullPath(f);
                    if (filePath.Contains("C9VersionManager"))
                        return filePath;
                }
            }
            return "";
        }

        private void btnDefaultVM_Click(object sender, EventArgs e)
        {
            accessoryModifications.Text = "New Accessory Manager";
            btnConfirmAccessory.Text = "Add Accessory";
            txtBoxNewAccessoryFilepath.Clear();
            txtBoxNewAccessoryName.Clear();
            btnDefaultAccessoryEdits.Enabled = false;
            btnLaunchAccessory.Enabled = true;
            btnRemoveAccessory.Enabled = true;
            accessoryTabs.Enabled = true;
            btnDefaultAccessoryEdits.Visible = false;
        }

        private void btnRecordingFolder_Click(object sender, EventArgs e)
        {
            String currUser = Environment.UserName;
            Process.Start(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\C9Trader\recording");
        }

        private void btnAnalyticsUploads_Click(object sender, EventArgs e)
        {
            String currUser = Environment.UserName;
            Process.Start(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads");
        }

        private void btnSplunk_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://qa1-log1.xhoot.com:8443/en-US/account/login?return_to=%2Fen-US%2Fmanager%2Fsearch%2Flicenseusage");
        }

        private void btnSaveServer_Click(object sender, EventArgs e)
        {
            if (txtBoxServerName.Items.Contains(txtBoxServerName.Text))
            {
                MessageBox.Show(txtBoxServerName.Text + " is already in the server list.");
            }
            else
            {
                serverList.Add(txtBoxServerName.Text);
                txtBoxServerName.Items.Add(txtBoxServerName.Text);
                List<string> strCurrentSettings = new List<string>();

                String Line = "";

                using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                {
                    Line = SR.ReadLine();
                    while (Line != null)
                    {
                        strCurrentSettings.Add(Line);
                        Line = SR.ReadLine();
                    }
                }

                using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
                {
                    bool defFlag = false;
                    foreach (String s in strCurrentSettings)
                    {
                        if (s.Contains("NoDefaultServer"))
                        {
                            SW.WriteLine("Server=" + txtBoxServerName.Text);
                            defFlag = true;
                            txtBoxServerName.Items.Remove("NoDefaultServer");
                            continue;
                        }

                        if (s.Contains("</DesignatedServer>") && !defFlag)
                        {
                            SW.WriteLine(txtBoxServerName.Text);
                        }
                        SW.WriteLine(s);
                    }
                }
            } 
        }

        private void txtBoxServerName_TextChanged(object sender, EventArgs e)
        {
            btnSaveServer.Enabled = true;
            btnServerDefault.Enabled = true;
        }

        private void btnDeleteShout_Click(object sender, EventArgs e)
        {
            string currUser = Environment.UserName;
            int count = 0;
            if (Directory.Exists(@"C: \Users\"+currUser+ @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\shoutdowns"))
            {
                foreach (String s in Directory.GetFiles(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\shoutdowns"))
                {
                    File.Delete(s);
                    count++;
                }
            }
            MessageBox.Show(count + " files deleted!");
        }

        private void btnDeleteRD_Click(object sender, EventArgs e)
        {
            string currUser = Environment.UserName;
            int count = 0;
            if (Directory.Exists(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\ringdowns"))
            {
                foreach (String s in Directory.GetFiles(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\ringdowns"))
                {
                    File.Delete(s);
                    count++;
                }
            }
            MessageBox.Show(count + " files deleted!");
        }

        private void btnAudioCodes_Click(object sender, EventArgs e)
        {
            Process.Start("http://qa1-gateway1.xhoot.com/QA%20Dual%20GW1/");
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(lblC9TraderRoot.Text))
            {
                List<String> directoryList = new List<String>();
                List<String> fileList = new List<String>();
                String rootDirectory = lblC9TraderRoot.Text;
                rootDirectory = lblC9TraderRoot.Text.Remove(lblC9TraderRoot.Text.Length - 9, 9);
                String newDirectory = rootDirectory + @"\C9TraderBackup";
                if (!Directory.Exists(newDirectory))
                    Directory.CreateDirectory(newDirectory);
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(lblC9TraderRoot.Text, newDirectory, true);
                Process.Start(newDirectory);
            }
            else
                MessageBox.Show("Invalid C9Trader Squirrel Root Path for Backup");

        }

        private void btnBackupFolder_Click(object sender, EventArgs e)
        {
            String root = lblC9TraderRoot.Text;
            root = root.Remove(root.Length - 9, 9);
            if (Directory.Exists(root + @"\C9TraderBackup"))
            {
                Process.Start(root + @"\C9TraderBackup");
            }
            else
            {
                Directory.CreateDirectory(root + @"\C9TraderBackup");
                Process.Start(root + @"\C9TraderBackup");
            }
        }

        private void btnDefaultTrscpServ_Click(object sender, EventArgs e)
        {
            LoadSettings("TRSCPSERV");
            btnDefaultTrscpServ.Enabled = false;
            btnSaveTrscpServ.Enabled = false;
            txtBoxTranscriptionServer.Select();
        }

        private void btnSaveTrscpServ_Click(object sender, EventArgs e)
        {
            List<string> strCurrentSettings = new List<string>();

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                string Line = SR.ReadLine();
                while (Line != null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                }
            }
            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
            {
                if (strCurrentSettings != null)
                {
                    foreach (string s in strCurrentSettings)
                    {
                        if (s.Contains("TranscriptionServer="))
                        {
                            String append = s;
                            append = "TranscriptionServer=" + txtBoxTranscriptionServer.Text;
                            SW.WriteLine(append);
                        }
                        else
                            SW.WriteLine(s);
                    }
                }
            }

            btnSaveTrscpServ.Enabled = false;
            btnDefaultTrscpServ.Enabled = false;
        }

        private void txtBoxTranscriptionServer_TextChanged(object sender, EventArgs e)
        {
            btnSaveTrscpServ.Enabled = true;
            btnDefaultTrscpServ.Enabled = true;
        }

        private void btnRemoveServer_click(object sender, EventArgs e)
        {
            String Line = "";
            List<String> strCurrentSettings = new List<String>();

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                Line = SR.ReadLine();
                while (Line != null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                }
            }
            String defServ = "";
            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
            {
                foreach (String s in strCurrentSettings)
                {
                    if (s.Contains(txtBoxServerName.Text))
                    {
                        if (s.Equals(txtBoxServerName.Text))
                            continue;
                        else
                        {
                            SW.WriteLine("Server=NoDefaultServer");
                            defServ = "NoDefaultServer";
                            txtBoxServerName.Items.Add(defServ);
                            continue;
                        }
                    }
                    if (s.Contains("Server=") && !s.Contains("Transcription") && !s.Contains(txtBoxServerName.Text))
                        defServ = s.Remove(0, 7);
                    SW.WriteLine(s);
                }
            }
            txtBoxServerName.Items.Remove(txtBoxServerName.SelectedItem);
            txtBoxServerName.SelectedItem = defServ;
        }

        private void txtBoxFirm_TextChanged(object sender, EventArgs e)
        {
            if (!searchFlag)
            {
                foreach(AppAccount AC in AccountsFromSettings)
                {
                    if (AC.strUserName.Equals(cmbBoxUsers.Text))
                    {
                        btnSaveFirm.Enabled = true;
                        btnDefaultFirm.Enabled = true;
                        break;
                    }
                }
                
            }
        }

        private void txtBoxGroup_TextChanged(object sender, EventArgs e)
        {
            if (!searchFlag)
            {
                foreach (AppAccount AC in AccountsFromSettings)
                {
                    if (AC.strUserName.Equals(cmbBoxUsers.Text))
                    {
                        btnDefaultGroup.Enabled = true;
                        btnSaveGroup.Enabled = true;
                        break;
                    }
                }

            }
        }

        private void btnSaveFirm_Click(object sender, EventArgs e)
        {
            String selectedUser = cmbBoxUsers.Text;
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName.Equals(cmbBoxUsers.Text))
                {
                    AC.strFirm = txtBoxFirm.Text;
                    SaveAccountsToSettings();
                    break;
                }
                      
            }
            cmbBoxUsers.SelectedItem = selectedUser;
            btnSaveFirm.Enabled = false;
            btnDefaultFirm.Enabled = false;
        }

        private void btnSaveGroup_Click(object sender, EventArgs e)
        {
            String selectedUser = cmbBoxUsers.Text;
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName.Equals(cmbBoxUsers.Text))
                {
                    AC.strGroup = txtBoxGroup.Text;
                    SaveAccountsToSettings();
                    break;
                }

            }
            cmbBoxUsers.SelectedItem = selectedUser;
            btnSaveGroup.Enabled = false;
            btnDefaultGroup.Enabled = false;
        }

        private void btnDefaultFirm_Click(object sender, EventArgs e)
        {
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName.Equals(cmbBoxUsers.Text))
                {
                    txtBoxFirm.Text = AC.strFirm;
                    btnDefaultFirm.Enabled = false;
                    btnSaveFirm.Enabled = false;
                    break;
                }  
            }
        }

        private void btnDefaultGroup_Click(object sender, EventArgs e)
        {
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName.Equals(cmbBoxUsers.Text))
                {
                    txtBoxGroup.Text = AC.strGroup;
                    btnDefaultGroup.Enabled = false;
                    btnSaveGroup.Enabled = false;
                    break;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int listChangeChecker = AccountsFromSettings.Count();
            txtBoxCurrUserSearch.Text = txtBoxNewUserSearch.Text;
            txtBoxCurrFirmSearch.Text = txtBoxNewFirmSearch.Text;
            txtBoxCurrGroupSearch.Text = txtBoxNewGroupSearch.Text;
            txtBoxCurrFeatureSearch.Text = cmbSearchFeature.Text;

            int searchedFeature = -1;

            if (!(cmbSearchFeature.Text.Equals("") && !cmbSearchFeature.Text.Equals(null)))
            {
                searchedFeature = cmbSearchFeature.SelectedIndex;
            }

            txtBoxNewUserSearch.Clear();
            txtBoxNewFirmSearch.Clear();
            txtBoxNewGroupSearch.Clear();
            cmbSearchFeature.Text = "";

            txtBoxFirm.Clear();
            txtBoxGroup.Clear();
            cmbBoxUsers.Items.Clear();
            cmbBoxUsers.Text = "";

            List<AppAccount> searchResults = new List<AppAccount>();

            foreach (AppAccount AC in AccountsFromSettings)
            {
                
                if ((txtBoxCurrUserSearch.Text.Equals("") || txtBoxCurrUserSearch.Text.Equals(null)))
                {
                    searchResults.Add(AC);
                }
                else
                {
                    if (AC.strUserName.ToLower().Contains(txtBoxCurrUserSearch.Text.ToLower()))
                    {
                        searchResults.Add(AC);
                    }
                }
                if (!(txtBoxCurrFirmSearch.Text.Equals("") && !txtBoxCurrFirmSearch.Text.Equals(null)))
                {
                    if (!AC.strFirm.ToLower().Contains(txtBoxCurrFirmSearch.Text.ToLower()) && searchResults.Contains(AC))
                        searchResults.Remove(AC);
                }
                if (!(txtBoxCurrGroupSearch.Text.Equals("") || txtBoxCurrGroupSearch.Text.Equals(null)))
                {
                    if (!AC.strGroup.ToLower().Contains(txtBoxCurrGroupSearch.Text.ToLower()) && searchResults.Contains(AC))
                        searchResults.Remove(AC);
                }
                if (!(searchedFeature==-1))
                {
                    if (AC.strFeatures.Equals(""))
                    {
                        searchResults.Remove(AC);
                    }
                    else if (AC.strFeatures[searchedFeature].Equals('0'))
                    {
                        searchResults.Remove(AC);
                    }   
                }
            }
            foreach (AppAccount AC in searchResults)
            {
                cmbBoxUsers.Items.Add(AC.strUserName);
            }
            if (cmbBoxUsers.Items.Count > 0)
                cmbBoxUsers.Text = searchResults.ElementAt(0).strUserName;
            if (cmbBoxUsers.Items.Count != listChangeChecker)
            {
                cmbBoxUsers.ForeColor = Color.Goldenrod;
                btnAddUser.Enabled = false;
                btnRemoveUser.Enabled = false;
                btnLoadBatch.Enabled = false;
                searchFlag = true;
                btnSearch.Enabled = false;
                btnUserInfo.Enabled = false;
            }
            else
            {
                cmbBoxUsers.ForeColor = Color.Black;
                btnAddUser.Enabled = true;
                btnRemoveUser.Enabled = true;
                btnLoadBatch.Enabled = true;
                searchFlag = false;
                btnSearch.Enabled = true;
                btnUserInfo.Enabled = true;
            }
                
                
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            btnLoadBatch.Enabled = true;
            btnSearch.Enabled = true;
            btnUserInfo.Enabled = true;
            txtBoxNewUserSearch.Clear();
            txtBoxNewFirmSearch.Clear();
            txtBoxNewGroupSearch.Clear();
            txtBoxCurrUserSearch.Clear();
            txtBoxCurrFirmSearch.Clear();
            txtBoxCurrGroupSearch.Clear();
            txtBoxCurrFeatureSearch.Clear();

            searchFlag = false;


            cmbBoxUsers.ForeColor = Color.Black;
            cmbBoxUsers.Items.Clear();
            foreach (AppAccount AC in AccountsFromSettings)
                cmbBoxUsers.Items.Add(AC.strUserName);
            if (cmbBoxUsers.Items.Count > 0)
                cmbBoxUsers.Text = AccountsFromSettings.ElementAt(0).strUserName;
        }

        private void btnChangeBatchFolder_Click(object sender, EventArgs e)
        {
            DialogResult VMSelectorResult = new DialogResult();
            VMSelectorResult = fldBrwsDiagLoadBatches.ShowDialog();
            if (VMSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = fldBrwsDiagLoadBatches.SelectedPath;
                if (strResultChangeCheck != txtBoxLoadBatchFolder.Text)
                {
                    txtBoxLoadBatchFolder.Text = strResultChangeCheck;
                }

            }
        }

        private void btnLoadBatch_Click(object sender, EventArgs e)
        {
            String loadFromDirectory = txtBoxLoadBatchFolder.Text;
            List<AppAccount> newAccs = new List<AppAccount>();
            String[] LineSplit = null;
            bool newData = false;
            if (Directory.Exists(loadFromDirectory))
            {
                foreach (String file in Directory.GetFiles(loadFromDirectory, "*.bat", SearchOption.AllDirectories))
                {
                    using (StreamReader SR = new StreamReader(file))
                    {
                        string Line = SR.ReadLine();
                        while (Line != null)
                        {
                            LineSplit = Line.Split('-');
                            AppAccount tempAcc = new AppAccount();

                            try
                            {
                                foreach (String f in LineSplit)
                                {
                                    if (f.First() == 'u')
                                    {
                                        tempAcc.strUserName = f.Remove(0, 2);
                                        tempAcc.strUserName = tempAcc.strUserName.Remove(tempAcc.strUserName.Length - 1, 1);
                                    }

                                    if (f.First() == 'p')
                                    {
                                        tempAcc.strPassword = f.Remove(0, 2);
                                        tempAcc.strPassword = tempAcc.strPassword.Remove(tempAcc.strPassword.Length - 1, 1);
                                    }

                                }
                            }
                            catch
                            {

                            }
                            if (!tempAcc.strUserName.Equals("") && !tempAcc.strUserName.Equals(null))
                            {
                                bool exists = false;
                                foreach (AppAccount existingACs in newAccs)
                                {
                                    if (existingACs.strUserName.Equals(tempAcc.strUserName))
                                        exists = true;
                                }
                                if (!exists)
                                    newAccs.Add(tempAcc);
                            }
                            Line = SR.ReadLine();
                        }
                    }
                }
                foreach (AppAccount AC in newAccs)
                {
                    bool isDuplicate = false;
                    foreach (AppAccount OAC in AccountsFromSettings)
                    {
                        if (AC.strUserName.Equals(OAC.strUserName))
                            isDuplicate = true;
                    }
                    if (!isDuplicate)
                    {
                        newData = true;
                        AccountsFromSettings.Add(AC);
                        MessageBox.Show("Adding user " + AC.strUserName + ":" + AC.strPassword + " to settings file");
                    }

                }
                SaveAccountsToSettings();
                if (!newData)
                {
                    MessageBox.Show("No new data found");
                }
            }
            else
                MessageBox.Show("Directory in text box does not exist!");
            
        }

        private void txtBoxNewUserSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxNewUserSearch.Text.Equals("") && txtBoxNewFirmSearch.Equals("") && txtBoxNewGroupSearch.Equals(""))
                btnSearch.Enabled = false;
            else
                btnSearch.Enabled = true;
        }

        private void txtBoxNewFirmSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxNewUserSearch.Text.Equals("") && txtBoxNewFirmSearch.Equals("") && txtBoxNewGroupSearch.Equals(""))
                btnSearch.Enabled = false;
            else
                btnSearch.Enabled = true;
        }

        private void txtBoxNewGroupSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxNewUserSearch.Text.Equals("") && txtBoxNewFirmSearch.Equals("") && txtBoxNewGroupSearch.Equals(""))
                btnSearch.Enabled = false;
            else
                btnSearch.Enabled = true;
        }

        private void btnUserInfo_Click_1(object sender, EventArgs e)
        {
            AppAccount AC = null;
            
            foreach (AppAccount newAC in AccountsFromSettings)
            {
                if (newAC.strUserName.Equals(cmbBoxUsers.Text))
                {
                    AC = newAC;
                    break;
                }
            }
            if (AC==null)
            {
                MessageBox.Show("ERROR: USERINFO NOT FOUND FOR " + cmbBoxUsers.Text);
            }
            else
            {
                userInfoForm = new frmUserInfoForm();
                userInfoForm.loadSettings(AC.strUserName, AC.strFeatures);
                userInfoForm.Activate();
                userInfoForm.BringToFront();
                userInfoForm.Show();
                userInfoForm.FormClosed += userInfoClosedEventHandler;

                mainForm.Enabled = false;
            }
            
        }

        private void userInfoClosedEventHandler(object sender, EventArgs e)
        {
            btnUserInfo.Enabled = true;
            LoadSettings("FEATURES");
            mainForm.Enabled = true;
        }

        private void userOrderingClosedEventHandler(object sender, EventArgs e)
        {
            AccountsFromSettings = userOrderForm.getOrderedAccounts();
            btnReorder.Enabled = true;
            SaveAccountsToSettings();
            LoadSettings("APPACCOUNTS");
            mainForm.Enabled = true;
        }

        private void btnFlushC2C_Click(object sender, EventArgs e)
        {
            string currUser = Environment.UserName;
            int count = 0;
            if (Directory.Exists(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\clicktocalls"))
            {
                foreach (String s in Directory.GetFiles(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\clicktocalls"))
                {
                    File.Delete(s);
                    count++;
                }
            }
            MessageBox.Show(count + " files deleted!");
        }

        private void chkBoxInC9_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxInC9.Checked == true)
            {
                BtnLaunchApp.Enabled = true;
                btnCloseApp.Enabled = true;
                btnCloseAppSelUser.Enabled = true;
                btnLaunchAccessory.Enabled = true;
            }
            if (chkBoxInC9.Checked == false && BtnVPNSwitch.Text.Equals("OFF"))
            {
                BtnLaunchApp.Enabled = false;
                btnCloseApp.Enabled = false;
                btnCloseAppSelUser.Enabled = false;
                btnLaunchAccessory.Enabled = false;
            }
        }

        private void btnARs_Click(object sender, EventArgs e)
        {
            String currUser = Environment.UserName;
            Process.Start(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\C9Trader\calls");
        }

        private void btnTraderLogs_Click(object sender, EventArgs e)
        {
            String currUser = Environment.UserName;
            Process.Start(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\C9Trader\log");
        }

        private void btnOpenARComparer_Click(object sender, EventArgs e)
        {
            arCompareForm = new ARComparer();
            arCompareForm.Activate();
            arCompareForm.BringToFront();
            arCompareForm.Show();
        }

        private void btnFldrBrowser_Click(object sender, EventArgs e)
        {
            DialogResult VMSelectorResult = new DialogResult();
            VMSelectorResult = openFileDialogVM.ShowDialog();
            if (VMSelectorResult == DialogResult.OK)
            {
                txtBoxNewAccessoryFilepath.Text = openFileDialogVM.FileName;
                btnDefaultAccessoryEdits.Enabled = true;
            }
        }

        private void btnEditAccessory_Click(object sender, EventArgs e)
        {
            accessoryModifications.Text = "Edit Accessory Information";
            txtBoxNewAccessoryName.Text = accessoryTabs.SelectedTab.Text;
            Point p = new Point(0, 0);
            txtBoxNewAccessoryFilepath.Text = accessoryTabs.SelectedTab.GetChildAtPoint(p).Text;
            btnConfirmAccessory.Text = "Save Changes";
            btnDefaultAccessoryEdits.Visible = true;
            btnLaunchAccessory.Enabled = false;
            btnRemoveAccessory.Enabled = false;
            accessoryTabs.Enabled = false;
        }

        private void btnConfirmAccessory_Click(object sender, EventArgs e)
        {
            if (!(txtBoxNewAccessoryName.Text.Equals(null) || txtBoxNewAccessoryName.Text.Trim().Equals("") ||
                txtBoxNewAccessoryFilepath.Text.Equals(null) || txtBoxNewAccessoryFilepath.Text.Trim().Equals("")))
            {
                Point p = new Point(0, 0);
                List<string> strCurrentSettings = new List<string>();

                using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                {
                    string Line = SR.ReadLine();
                    while (Line != null)
                    {
                        strCurrentSettings.Add(Line);
                        Line = SR.ReadLine();
                    }
                }

                if (accessoryModifications.Text.Equals("New Accessory Manager"))
                {
                    using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
                    {
                        if (strCurrentSettings != null)
                        {
                            foreach (string s in strCurrentSettings)
                            {

                                if (s.Contains("<Accessories>"))
                                {
                                    SW.WriteLine(s);
                                    SW.WriteLine(txtBoxNewAccessoryName.Text + "=" + txtBoxNewAccessoryFilepath.Text);
                                }
                                else
                                    SW.WriteLine(s);
                            }
                        }
                    }

                    TabPage newTab = new TabPage(txtBoxNewAccessoryName.Text);
                    TextBox newFPBox = new TextBox();
                    newFPBox.SetBounds(0, 0, 300, 50);
                    newFPBox.Text = txtBoxNewAccessoryFilepath.Text;
                    newFPBox.Enabled = false;
                    newTab.Controls.Add(newFPBox);
                    accessoryTabs.Controls.Add(newTab);
                }
                else
                {
                    using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
                    {
                        if (strCurrentSettings != null)
                        {
                            foreach (string s in strCurrentSettings)
                            {

                                if (s.Contains(accessoryTabs.SelectedTab.GetChildAtPoint(p).Text))
                                {
                                    String append = s;
                                    append = txtBoxNewAccessoryName.Text + "=" + txtBoxNewAccessoryFilepath.Text;
                                    SW.WriteLine(append);
                                }
                                else
                                    SW.WriteLine(s);
                            }
                        }
                    }

                    accessoryModifications.Text = "New Accessory Manager";
                    accessoryTabs.SelectedTab.Text = txtBoxNewAccessoryName.Text;
                    accessoryTabs.SelectedTab.GetChildAtPoint(p).Text = txtBoxNewAccessoryFilepath.Text;
                    btnConfirmAccessory.Text = "Add Accessory";
                    btnDefaultAccessoryEdits.Enabled = false;
                    btnLaunchAccessory.Enabled = true;
                    btnRemoveAccessory.Enabled = true;
                    accessoryTabs.Enabled = true;
                    btnDefaultAccessoryEdits.Visible = false;
                }

                txtBoxNewAccessoryFilepath.Clear();
                txtBoxNewAccessoryName.Clear();
            }
            else
                MessageBox.Show("Error: Invalid Accessory Information");
        }

        private void txtBoxNewAccessoryName_TextChanged(object sender, EventArgs e)
        {
            btnDefaultAccessoryEdits.Enabled = true;
        }

        private void txtBoxNewAccessoryFilepath_TextChanged(object sender, EventArgs e)
        {
            btnDefaultAccessoryEdits.Enabled = true;
        }

        private void btnRemoveAccessory_Click(object sender, EventArgs e)
        {
            Point p = new Point(0, 0);
            List<string> strCurrentSettings = new List<string>();

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                string Line = SR.ReadLine();
                while (Line != null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                }
            }
            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.set", false))
            {
                if (strCurrentSettings != null)
                {
                    foreach (string s in strCurrentSettings)
                    {

                        if (!(s.Contains(accessoryTabs.SelectedTab.Text + "=" + accessoryTabs.SelectedTab.GetChildAtPoint(p).Text)))
                        {
                            SW.WriteLine(s);
                        }
                            
                    }
                }
            }

            accessoryTabs.Controls.Remove(accessoryTabs.SelectedTab);
        }

        private void btnLaunchPortal_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(cmbBoxPortalServers.Text);
            }
            catch
            {
                
            }
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            userOrderForm = new userOrderingForm(AccountsFromSettings);
           
            userOrderForm.Activate();
            userOrderForm.BringToFront();
            userOrderForm.Show();
            userOrderForm.FormClosing += userOrderingClosedEventHandler;

            mainForm.Enabled = false;
        }
    }
}
