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
           public string strUserName{get;set;}
           public string strPassword{get;set;}
        }

        // ProcessUser is a tuple of username and the process associated with it
        // @userProcess the associated process started with a username
        // @userName the user name associated with a started process
        public class ProcessUser
        {
            public Process userProcess { get; set; }
            public String userName { get; set; }
        }

        // Data lists containing app accounts and active processes used in the executable
        List<AppAccount> AccountsFromSettings = new List<AppAccount>();
        List<ProcessUser> ActiveProcesses = new List<ProcessUser>();

        // Main method, loads all forms and settings
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            this.Width = 1073;
            this.Height = 345;
            foreach (Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Equals("C9Shell.exe"))
                {
                    ProcessUser x = new ProcessUser();
                    x.userProcess = p;

                    ActiveProcesses.Add(x);
                }
            }
                
            LoadSettings("VPNCLIENT");
            LoadSettings("C9TRADERROOT");
            
            LoadSettings("APPACCOUNTS");

            //VPN Manager Startup Setup

            ServiceController svcAcumbrella = new ServiceController("acumbrellaagent");
            ServiceController svcVPNAgent = new ServiceController("vpnagent");
            if(svcAcumbrella.Status==ServiceControllerStatus.Stopped&&svcVPNAgent.Status==ServiceControllerStatus.Stopped)
            {
                BtnVPNSwitch.Text = "OFF";
                BtnVPNSwitch.BackColor = Color.LightCoral;
                BtnVPNSwitch.ForeColor = Color.DarkRed;
                //btnOpenSharedFolder.Enabled = false;
                btnVPNClientLaunch.Enabled = false;
            }
            else if(svcAcumbrella.Status == ServiceControllerStatus.Running && svcVPNAgent.Status == ServiceControllerStatus.Running)
            {
                BtnVPNSwitch.Text = "ON";
                BtnVPNSwitch.BackColor = Color.LightGreen;
                BtnVPNSwitch.ForeColor = Color.DarkGreen;
                btnVPNClientLaunch.Enabled = true;
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
            string[] LineSplit=null;
            string Line=null;

            switch(strWhat2Load)
            {
                
                case "VPNCLIENT":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while(Line!="</VPNManager>")
                        {
                            LineSplit = Line.Split('=');
                            if(LineSplit[0]== "VPNClientLocation")
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
                        if((Line = SR.ReadLine())!="</UserCollection>")
                        {
                            while (Line!= "</UserCollection>")
                            {
                                AppAccount TmpAccount = new AppAccount();
                                LineSplit = Line.Split(':');
                                TmpAccount.strUserName = LineSplit[0];
                                TmpAccount.strPassword = LineSplit[1];
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
                        AutoSetPWD(cmbBoxUsers.Text); //works
                        btnAddUser.Enabled = false;
                        btnRemoveUser.Enabled = false;
                    }
                    break;
            }
        }

        // Sets the password text box to the password known to be associated with an account
        // @string usrname: Parameter denoting which user's password to be supplied
        public void AutoSetPWD(string usrname)
        {
            foreach (AppAccount AC in AccountsFromSettings)
            {
                if(AC.strUserName==usrname)
                {
                    txtBoxSetUsrPassword.Text = AC.strPassword;
                }
            }
        }

        // @Leo TBH I'm not entirely sure where this button can even be found
        private void btnChangeVPNClient_Click(object sender, EventArgs e)
        {
            DialogResult VPNSelectorResult = new DialogResult();
            VPNSelectorResult = opnFDVPNClientSelector.ShowDialog();
            if(VPNSelectorResult==DialogResult.OK)
            {
                string strResultChangeCheck = opnFDVPNClientSelector.FileName;
                if(strResultChangeCheck!=lblVPNClientTarget.Text)
                {
                    lblVPNClientTarget.Text = strResultChangeCheck;
                    btnDefaultVPNClient.Enabled = true;
                    btnSaveSettings.Enabled = true;
                }
                
            }
        }

        // @Leo Same with this one
        private void btnDefaultVPNClient_Click(object sender, EventArgs e)
        {
            LoadSettings("VPNCLIENT");
            btnDefaultVPNClient.Enabled = false;
            //IF ALL OTHER DEFAULT BUTTONS ARE DISABLED, SAVE BUTTON SHOULD ALSO BE DISABLED
            DefaultButtonsCheck();
        }

        // @Leo Not entirely sure what this method does
        public void DefaultButtonsCheck()
        {
            btnSaveSettings.Enabled = false;
            if (btnDefaultVPNClient.Enabled || btnDefaultC9TraderRoot.Enabled)
                btnSaveSettings.Enabled = true;
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
                    btnSaveSettings.Enabled = true;
                }
            }
        }

        // Action Listener for the Default button used to revert the C9 Trader root directory to the default
        // designated in the settings file
        private void btnDefaultC9TraderRoot_Click(object sender, EventArgs e)
        {
            LoadSettings("C9TRADERROOT");
            btnDefaultC9TraderRoot.Enabled = false;
            DefaultButtonsCheck();
        }

        // @Leo not sure about this one
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            List<string> strCurrentSettings = new List<string>();
            
            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                string Line = SR.ReadLine();
                while(Line!=null)
                {
                    strCurrentSettings.Add(Line);
                    Line = SR.ReadLine();
                }
            }
            if(btnDefaultVPNClient.Enabled)
            {
                string[] strSplitCheck = null;
                int intIndex = 0;
                foreach (string s in strCurrentSettings)
                {
                    strSplitCheck = s.Split('=');
                    if(strSplitCheck[0]=="VPNClientLocation")
                    {
                        strCurrentSettings[intIndex] = "VPNClientLocation=" + lblVPNClientTarget.Text;
                        break;
                    }
                    intIndex++;
                }
            }
            if (btnDefaultC9TraderRoot.Enabled)
            {
                string[] strSplitCheck = null;
                int intIndex = 0;
                foreach (string s in strCurrentSettings)
                {
                    strSplitCheck = s.Split('=');
                    if (strSplitCheck[0] == "C9TraderRootLocation")
                    {
                        strCurrentSettings[intIndex] = "C9TraderRootLocation=" + lblC9TraderRoot.Text;
                        break;
                    }
                    intIndex++;
                }

            }
            //add more settings here

            using (StreamWriter SW = new StreamWriter("LM_C9MSettings.new"))
            {
                if (strCurrentSettings != null)
                {
                    foreach(string s in strCurrentSettings)
                    {
                        SW.WriteLine(s);
                    }
                }
            }
            File.Delete("LM_C9MSettings.set");
            File.Copy("LM_C9MSettings.new", "LM_C9MSettings.set");
            File.Delete("LM_C9MSettings.new");

            btnDefaultVPNClient.Enabled = false;
            btnSaveSettings.Enabled = false;


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
            
            ProcessStartInfo ProcVPNCli= new ProcessStartInfo();
            ProcVPNCli.FileName = lblVPNClientTarget.Text;
            if (ProcVPNCli.FileName != "vpnui.exe")
            {
                MessageBox.Show("Error: vpnui.exe not selected.");
                btnDefaultVPNClient_Click(sender, e);      
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
                if (MSI_Toggle.Text == "Squirrel")
                {
                    string currUser = Environment.UserName;
                    startPath = @"C:\Users\" + currUser + "\\AppData\\Local\\C9Trader";
                    lblC9TraderRoot.Text = startPath;
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
                else
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

            foreach(AppAccount AC in AccountsFromSettings)
            {
                if (AC.strUserName == cmbBoxUsers.Text)
                {
                    flgMustAddNewUser = false;
                    break;
                }                   
            }
            if(flgMustAddNewUser)
            {
                AppAccount tmpAcc = new AppAccount();
                tmpAcc.strUserName = cmbBoxUsers.Text;
                tmpAcc.strPassword = txtBoxSetUsrPassword.Text;
                AccountsFromSettings.Add(tmpAcc);
                SaveAccountsToSettings();
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
                    foreach(AppAccount AC in AccountsFromSettings)
                    {
                        SW.WriteLine(AC.strUserName + ":" + AC.strPassword);
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
            AutoSetPWD(cmbBoxUsers.Text);
        }

        // Action Listener for the button that launches the application using information supplied in the user
        // combo box, password text box, and Build Toggle button as well as any additional parameters (-x or -a)
        private void BtnLaunchApp_Click(object sender, EventArgs e)
        {
            String parameters = "";
            parameters += "-u " + cmbBoxUsers.Text + " -p " + txtBoxSetUsrPassword.Text + " -r https://qa1-rest.xhoot.com";
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
                    p = Process.Start(lblC9TraderRoot.Text + "\\app-" + cmbBoxVersionsList.Text + "\\C9Shell.exe", parameters);
                }
                else
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "C9Shell.exe";
                    psi.WorkingDirectory = lblC9TraderRoot.Text;
                    psi.Arguments = parameters;
                    p = Process.Start(psi);
                }

                // Overwrites password in settings file with that currently used
                foreach (AppAccount AC in AccountsFromSettings)
                {
                    if (AC.strUserName == cmbBoxUsers.Text)
                    {
                        if (AC.strPassword != txtBoxSetUsrPassword.Text)
                        {
                            AC.strPassword = txtBoxSetUsrPassword.Text;
                        }
                    }
                }
                SaveAccountsToSettings();

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
                    string currUser = Environment.UserName;
                    lblC9TraderRoot.Text = @"C:\Users\" + currUser + "\\AppData\\Local\\C9Trader";
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
            else
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
                foreach (Process p in System.Diagnostics.Process.GetProcesses())
                {
                    while (ActiveProcesses.Count!=0)
                    {
                        ActiveProcesses.ElementAt(0).userProcess.Kill();
                        ActiveProcesses.Remove(ActiveProcesses.ElementAt(0));
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
            SoundPlayer simpleSound = new SoundPlayer("reeeeeeeee2.wav");
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
                    btnDefaultVPNClient.Enabled = true;
                    btnSaveSettings.Enabled = true;
                }

            }
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
    }
}
