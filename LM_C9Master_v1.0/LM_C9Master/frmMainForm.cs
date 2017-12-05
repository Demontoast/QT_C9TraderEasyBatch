using System;
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

        // Main method, loads all forms and settings
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            this.Width = 1200;
            this.Height = 475;
            foreach (Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Contains("C9Shell"))
                {
                    ProcessUser x = new ProcessUser();
                    x.userProcess = p;
                    ActiveProcesses.Add(x);
                }
            }

            if (!File.Exists("LM_C9MSettings.set"))
                generateSettings();

            LoadSettings("VPNCLIENT");
            LoadSettings("C9TRADERROOT");
            LoadSettings("SERVER");

            LoadSettings("APPACCOUNTS");
            LoadSettings("VERSIONMANAGER");
            LoadSettings("TCPVIEW");
            LoadSettings("SQDB");
            LoadSettings("TRSCPSERV");

            btnDefaultC9TraderRoot.Enabled = false;
            btnTraderRootSave.Enabled = false;
            btnDefaultVPN.Enabled = false;
            btnVPNSaveSettings.Enabled = false;
            btnSaveTCPView.Enabled = false;
            btnDefaultTCPView.Enabled = false;
            btnDefaultVM.Enabled = false;
            btnSaveVM.Enabled = false;
            btnDefaultSQDBLite.Enabled = false;
            btnSavePathSQDBLite.Enabled = false;
            btnDefaultTrscpServ.Enabled = false;
            btnSaveTrscpServ.Enabled = false;
            btnSaveFirm.Enabled = false;
            btnDefaultFirm.Enabled = false;
            btnSaveGroup.Enabled = false;
            btnDefaultGroup.Enabled = false;

            //VPN Manager Startup Setup

            ServiceController svcAcumbrella = new ServiceController("acumbrellaagent");
            ServiceController svcVPNAgent = new ServiceController("vpnagent");
            if (svcAcumbrella.Status == ServiceControllerStatus.Stopped || svcVPNAgent.Status == ServiceControllerStatus.Stopped)
            {
                BtnVPNSwitch.Text = "OFF";
                BtnVPNSwitch.BackColor = Color.LightCoral;
                BtnVPNSwitch.ForeColor = Color.DarkRed;
                btnVPNClientLaunch.Enabled = false;
                BtnLaunchApp.Enabled = false;
                btnCloseApp.Enabled = false;
                btnCloseAppSelUser.Enabled = false;
                btnVersionManager.Enabled = false;
            }
            else if (svcAcumbrella.Status == ServiceControllerStatus.Running && svcVPNAgent.Status == ServiceControllerStatus.Running)
            {
                BtnVPNSwitch.Text = "ON";
                BtnVPNSwitch.BackColor = Color.LightGreen;
                BtnVPNSwitch.ForeColor = Color.DarkGreen;
                btnVPNClientLaunch.Enabled = true;
                BtnLaunchApp.Enabled = true;
                btnCloseApp.Enabled = true;
                btnCloseAppSelUser.Enabled = true;
                btnVersionManager.Enabled = true;
            }
            ///
            //App Manager Startup Setup
            RefreshVersions();
            ///


        }

        // Function generates a new settings file from scratch
        public void generateSettings()
        {
            using (StreamWriter SW = File.CreateText("LM_C9MSettings.set"))
            {
                SW.WriteLine("<VPNClient>");
                SW.WriteLine(@"VPNClientLocation=C:\Program Files (x86)\Cisco\Cisco AnyConnect Secure Mobility Client\vpnui.exe");
                SW.WriteLine("</VPNClient>");
                SW.WriteLine("<AppManager>");
                String currUser = Environment.UserName.ToString();
                SW.WriteLine(@"C9TraderRootLocation=C:\Users\" + currUser + @"\AppData\Local\C9Trader");
                SW.WriteLine("</AppManager>");
                SW.WriteLine("<VersionManager>");
                SW.WriteLine("VMLocation=");
                SW.WriteLine("</VersionManager>");
                SW.WriteLine("<SQDBLite>");
                SW.WriteLine("SQDBLiteLocation=");
                SW.WriteLine("</SQDBLite>");
                SW.WriteLine("<TCPView>");
                SW.WriteLine("TCPViewLocation=");
                SW.WriteLine("</TCPView>");
                SW.WriteLine("<DesignatedServer>");
                SW.WriteLine("Server=https://qa1-rest.xhoot.com");
                SW.WriteLine("</DesignatedServer>");
                SW.WriteLine("<TranscriptionServer>");
                SW.WriteLine("TranscriptionServer=");
                SW.WriteLine("</TranscriptionServer>");
                SW.WriteLine("<UserCollection>");
                SW.WriteLine("</UserCollection>");
                SW.Close();
            }
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
                        Line = SR.ReadLine();
                        while (Line != "</VPNClient>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "VPNClientLocation")
                            {
                                lblVPNClientTarget.Text = LineSplit[1];
                                break;
                            }
                            
                            Line = SR.ReadLine();
                        }
                    }
                    break;
                case "C9TRADERROOT":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "</AppManager>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "C9TraderRootLocation")
                            {
                                lblC9TraderRoot.Text = LineSplit[1];
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                    }
                    break;
                case "APPACCOUNTS": //works

                    cmbBoxUsers.Items.Clear();
                    AccountsFromSettings.Clear();
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while ((Line = SR.ReadLine()) != "<UserCollection>") ;
                        if ((Line = SR.ReadLine()) != "</UserCollection>")
                        {
                            while (Line != "</UserCollection>")
                            {
                                AppAccount TmpAccount = new AppAccount();
                                LineSplit = Line.Split(':');
                                TmpAccount.strUserName = LineSplit[0];
                                TmpAccount.strPassword = LineSplit[1];
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
                                
                                AccountsFromSettings.Add(TmpAccount);

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
                case "VERSIONMANAGER":
                    txtBoxVMPath.Clear();
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "</VersionManager>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "VMLocation")
                            {
                                txtBoxVMPath.Text = LineSplit[1];
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                    }
                    
                    break;
                case "TCPVIEW":
                    txtBoxTCPViewPath.Clear();
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "</TCPView>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "TCPViewLocation")
                            {
                                txtBoxTCPViewPath.Text = LineSplit[1];
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                    }
                    break;
                case "SERVER":
                    txtBoxServerName.Items.Clear();
                    serverList.Clear();
                    String defServ = "";
                    bool flag = false;
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        while ((Line = SR.ReadLine()) != "<DesignatedServer>") ;
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
                case "SQDB":
                    txtBoxSQDBLite.Clear();
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "</SQDBLite>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "SQDBLiteLocation")
                            {
                                txtBoxSQDBLite.Text = LineSplit[1];
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                    }
                    break;
                case "TRSCPSERV":
                    txtBoxTranscriptionServer.Clear();
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "</TranscriptionServer>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "TranscriptionServer")
                            {
                                txtBoxTranscriptionServer.Text = LineSplit[1];
                                break;
                            }
                            Line = SR.ReadLine();
                        }
                    }
                    break;
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

        // @Leo not sure about this one
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
            ServiceController svcAcumbrella = new ServiceController("acumbrellaagent");
            ServiceController svcVPNAgent = new ServiceController("vpnagent");
            if (BtnVPNSwitch.Text == "OFF")
            {
                try
                {
                    if (svcAcumbrella.Status == ServiceControllerStatus.Stopped)
                    {
                        svcAcumbrella.Start();
                    }
                    if (svcVPNAgent.Status == ServiceControllerStatus.Stopped)
                    {
                        svcVPNAgent.Start();
                    }

                    BtnVPNSwitch.Enabled = false;
                    Thread.Sleep(1500);
                    BtnVPNSwitch.Text = "ON";
                    BtnVPNSwitch.BackColor = Color.LightGreen;
                    BtnVPNSwitch.ForeColor = Color.DarkGreen;
                    btnVPNClientLaunch.Enabled = true;
                    BtnVPNSwitch.Enabled = true;
                    BtnLaunchApp.Enabled = true;
                    btnCloseApp.Enabled = true;
                    btnCloseAppSelUser.Enabled = true;
                    btnVersionManager.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("VPN services are not working properly..." + Environment.NewLine + "Please verify their status in Windows Task Manager", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BtnVPNSwitch.Enabled = false;
                }
            }
            else
            {
                try
                {
                    if (svcAcumbrella.Status == ServiceControllerStatus.Running)
                    {
                        svcAcumbrella.Stop();
                    }
                    if (svcVPNAgent.Status == ServiceControllerStatus.Running)
                    {
                        svcVPNAgent.Stop();
                    }
                    BtnVPNSwitch.Enabled = false;
                    Thread.Sleep(1500);
                    BtnVPNSwitch.Text = "OFF";
                    BtnVPNSwitch.BackColor = Color.LightCoral;
                    BtnVPNSwitch.ForeColor = Color.DarkRed;
                    btnVPNClientLaunch.Enabled = false;
                    BtnVPNSwitch.Enabled = true;
                    BtnLaunchApp.Enabled = false;
                    btnCloseApp.Enabled = false;
                    btnCloseAppSelUser.Enabled = false;
                    btnVersionManager.Enabled = false;
                }
                catch
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
            if (txtBoxSetUsrPassword.Text != "" && (cmbBoxUsers.Text != "" && cmbBoxUsers.Text != "NoSavedAccounts"))
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
            if (txtBoxSetUsrPassword.Text != "" && cmbBoxUsers.Text != "")
            {
                btnAddUser.Enabled = true;
                btnRemoveUser.Enabled = true;
            }
            AutoSetAccSettings(cmbBoxUsers.Text);
        }

        // Action Listener for the button that launches the application using information supplied in the user
        // combo box, password text box, and Build Toggle button as well as any additional parameters (-x or -a)
        private void BtnLaunchApp_Click(object sender, EventArgs e)
        {
            String parameters = "";

            if (txtBoxServerName.Text.Equals("") || txtBoxServerName.Text.Equals(null))
                txtBoxServerName.Text = "https://qa1-rest.xhoot.com";

            parameters += "-u " + cmbBoxUsers.Text + " -p " + txtBoxSetUsrPassword.Text + " -r " + txtBoxServerName.Text;
            if (!txtBoxTranscriptionServer.Text.Equals("") || !txtBoxTranscriptionServer.Text.Equals(null))
                parameters += " -t " + txtBoxTranscriptionServer.Text;
            ProcessUser pram = new ProcessUser();
            Process p = new Process();
            pram.userName = cmbBoxUsers.Text;
            if (chkBoxMultiApp.Checked == true)
                parameters += " -a";
            if (chkBoxNoUpd.Checked == true)
                parameters += " -x";
            //Launches the app using user settings
            try
            {
                if (MSI_Toggle.Text == "Squirrel")
                {
                    p.StartInfo.Arguments = parameters;
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

                            ActiveProcesses.Remove(p);
                        }
                        catch
                        {
                            MessageBox.Show("Error: Something went wrong!");
                        }
                        btnCloseAppSelUser.Enabled = false;
                        Thread.Sleep(1500);
                        btnCloseAppSelUser.Enabled = true;
                        break;
                    }
                }
                if (!exists)
                {
                    MessageBox.Show("Error: User " + cmbBoxUsers.Text + " is not active on the application.");
                }
            }
            else
                MessageBox.Show("Error: No active applications.");
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
                        if (s.Contains("C9TraderRootLocation="))
                        {
                            String append = s;
                            append = "C9TraderRootLocation=" + lblC9TraderRoot.Text;
                            SW.WriteLine(append);
                        }
                        else
                            SW.WriteLine(s);
                    }
                }
            }

            btnTraderRootSave.Enabled = false;
            btnDefaultC9TraderRoot.Enabled = false;
        }

        // Action Listener for the button that links the user to the C9 Portal
        private void btnPortalLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://qa1-portal.xhoot.com/c9portal/#/login");
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

        private void btnVersionManager_Click(object sender, EventArgs e)
        {
            if (!txtBoxVMPath.Text.Equals("Enter Custom Path") && !txtBoxVMPath.Text.Equals(""))
            {
                try
                {
                    Process.Start(txtBoxVMPath.Text, "-u " + cmbBoxUsers.Text + " -p " + txtBoxSetUsrPassword.Text);
                }
                catch
                {
                    MessageBox.Show("Error: C9 Version Manager not found");
                }
            }
            else
            {
                String filePath = VMDirSearch("C:\\Program Files (x86)\\Cloud9 Technologies LLC\\");
                txtBoxVMPath.Text = filePath;
                try
                {
                    Process.Start(filePath, "-u " + cmbBoxUsers.Text + " -p " + txtBoxSetUsrPassword.Text);
                }
                catch
                {
                    MessageBox.Show("Error: C9 Version Manager not found");
                }
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

        private void btnTCPView_Click(object sender, EventArgs e)
        {
            String filePath = "";
            try
            {
                filePath = txtBoxTCPViewPath.Text;
                Process.Start(filePath);
            }
            catch
            {
                MessageBox.Show("Error: TCPView not found");
            }          
        }

        private void btnChangeVMPath_Click(object sender, EventArgs e)
        {
            DialogResult VMSelectorResult = new DialogResult();
            VMSelectorResult = openFileDialogVM.ShowDialog();
            if (VMSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = openFileDialogVM.FileName;
                if (strResultChangeCheck != txtBoxVMPath.Text)
                {
                    txtBoxVMPath.Text = strResultChangeCheck;
                    btnDefaultVM.Enabled = true;
                    btnSaveVM.Enabled = true;
                }

            }
        }

        private void btnDefaultVM_Click(object sender, EventArgs e)
        {
            LoadSettings("VERSIONMANAGER");
            btnDefaultVM.Enabled = false;
            btnSaveVM.Enabled = false;
            txtBoxVMPath.Select();
        }

        private void btnSaveVM_Click(object sender, EventArgs e)
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
                        if (s.Contains("VMLocation="))
                        {
                            String append = s;
                            append = "VMLocation=" + txtBoxVMPath.Text;
                            SW.WriteLine(append);
                        }
                        else
                            SW.WriteLine(s);
                    }
                }
            }

            btnSaveVM.Enabled = false;
            btnDefaultVM.Enabled = false;
        }

        private void btnChangeTCPView_Click(object sender, EventArgs e)
        {
            DialogResult VMSelectorResult = new DialogResult();
            VMSelectorResult = openFileDialogTCPView.ShowDialog();
            if (VMSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = openFileDialogTCPView.FileName;
                if (strResultChangeCheck != txtBoxTCPViewPath.Text)
                {
                    txtBoxTCPViewPath.Text = strResultChangeCheck;
                    btnDefaultTCPView.Enabled = true;
                    btnSaveTCPView.Enabled = true;
                }

            }
        }

        private void btnDefaultTCPView_Click(object sender, EventArgs e)
        {
            LoadSettings("TCPVIEW");
            btnDefaultTCPView.Enabled = false;
            btnSaveTCPView.Enabled = false;
            txtBoxTCPViewPath.Select();
        }

        private void btnSaveTCPView_Click(object sender, EventArgs e)
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
                        if (s.Contains("TCPViewLocation="))
                        {
                            String append = s;
                            append = "TCPViewLocation=" + txtBoxTCPViewPath.Text;
                            SW.WriteLine(append);
                        }
                        else
                            SW.WriteLine(s);
                    }
                }
            }

            btnSaveTCPView.Enabled = false;
            btnDefaultTCPView.Enabled = false;
        }

        private void txtBoxVMPath_TextChanged(object sender, EventArgs e)
        {
            btnSaveVM.Enabled = true;
            btnDefaultVM.Enabled = true;
        }

        private void txtBoxTCPViewPath_TextChanged(object sender, EventArgs e)
        {
            btnSaveTCPView.Enabled = true;
            btnDefaultTCPView.Enabled = true;
        }

        private void btnRecordingFolder_Click(object sender, EventArgs e)
        {
            String currUser = Environment.UserName;
            Process.Start(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\C9Trader\recording");
        }

        private void btnAnalyticsUploads_Click(object sender, EventArgs e)
        {
            String currUser = Environment.UserName;
            Process.Start(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics");
        }

        private void btnPortalGateway_Click(object sender, EventArgs e)
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
            foreach (String s in Directory.GetFiles(@"C:\Users\"+currUser+ @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\shoutdowns"))
            {
                File.Delete(s);
            }
        }

        private void btnDeleteRD_Click(object sender, EventArgs e)
        {
            string currUser = Environment.UserName;
            foreach (String s in Directory.GetFiles(@"C:\Users\" + currUser + @"\AppData\Local\Cloud9_Technologies\c9analytics\uploads\ringdowns"))
            {
                File.Delete(s);
            }
        }

        private void btnAudioCodes_Click(object sender, EventArgs e)
        {
            Process.Start("http://qa1-gateway1.xhoot.com/QA%20Dual%20GW1/");
        }

        private void btnLaunchSQDBLite_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtBoxSQDBLite.Text.Contains("DB Browser for SQLite"))
                    MessageBox.Show("SQDBLite not found");
                else
                    Process.Start(txtBoxSQDBLite.Text);
            }
            catch
            {
                MessageBox.Show("Error: SQDBLite not found");
            }
        }

        private void txtBoxSQDBLite_TextChanged(object sender, EventArgs e)
        {
            btnSavePathSQDBLite.Enabled = true;
            btnDefaultSQDBLite.Enabled = true;
        }

        private void btnChangePathSQDBLite_Click(object sender, EventArgs e)
        {
            DialogResult VMSelectorResult = new DialogResult();
            VMSelectorResult = openFileDialogSQDBLite.ShowDialog();
            if (VMSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = openFileDialogSQDBLite.FileName;
                if (strResultChangeCheck != txtBoxSQDBLite.Text)
                {
                    txtBoxSQDBLite.Text = strResultChangeCheck;
                }

            }
        }

        private void btnDefaultSQDBLite_Click(object sender, EventArgs e)
        {
            LoadSettings("SQDB");
            btnDefaultSQDBLite.Enabled = false;
            btnSavePathSQDBLite.Enabled = false;
            txtBoxSQDBLite.Select();
        }

        private void btnSavePathSQDBLite_Click(object sender, EventArgs e)
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
                        if (s.Contains("SQDBLiteLocation="))
                        {
                            String append = s;
                            append = "SQDBLiteLocation=" + txtBoxSQDBLite.Text;
                            SW.WriteLine(append);
                        }
                        else
                            SW.WriteLine(s);
                    }
                }
            }

            btnSavePathSQDBLite.Enabled = false;
            btnDefaultSQDBLite.Enabled = false;
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
            if (!cmbBoxUsers.Text.Equals(""))
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
            if (!cmbBoxUsers.Text.Equals(""))
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

            txtBoxNewUserSearch.Clear();
            txtBoxNewFirmSearch.Clear();
            txtBoxNewGroupSearch.Clear();

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
                    if (AC.strUserName.Contains(txtBoxCurrUserSearch.Text))
                    {
                        searchResults.Add(AC);
                    }
                }
                if (!(txtBoxCurrFirmSearch.Text.Equals("") && !txtBoxCurrFirmSearch.Text.Equals(null)))
                {
                    if (!AC.strFirm.Contains(txtBoxCurrFirmSearch.Text) && searchResults.Contains(AC))
                        searchResults.Remove(AC);
                }
                if (!(txtBoxCurrGroupSearch.Text.Equals("") || txtBoxCurrGroupSearch.Text.Equals(null)))
                {
                    if (!AC.strGroup.Contains(txtBoxCurrGroupSearch.Text) && searchResults.Contains(AC))
                        searchResults.Remove(AC);
                }
            }
            foreach (AppAccount AC in searchResults)
            {
                cmbBoxUsers.Items.Add(AC.strUserName);
            }
            if (cmbBoxUsers.Items.Count > 0)
                cmbBoxUsers.Text = searchResults.ElementAt(0).strUserName;
            if (cmbBoxUsers.Items.Count!=listChangeChecker)
                cmbBoxUsers.ForeColor = Color.Goldenrod;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtBoxNewUserSearch.Clear();
            txtBoxNewFirmSearch.Clear();
            txtBoxNewGroupSearch.Clear();
            txtBoxCurrUserSearch.Clear();
            txtBoxCurrFirmSearch.Clear();
            txtBoxCurrGroupSearch.Clear();


            cmbBoxUsers.ForeColor = Color.Black;
            cmbBoxUsers.Items.Clear();
            foreach (AppAccount AC in AccountsFromSettings)
                cmbBoxUsers.Items.Add(AC.strUserName);
            if (cmbBoxUsers.Items.Count > 0)
                cmbBoxUsers.Text = AccountsFromSettings.ElementAt(0).strUserName;
        }
    }
}
