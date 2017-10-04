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
        
        public frmMainForm()
        {
            InitializeComponent();
        }

        public class AppAccount
        {
           public string strUserName{get;set;}
           public string strPassword{get;set;}
        }

        List<AppAccount> AccountsFromSettings = new List<AppAccount>();

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            this.Width = 1073;
            this.Height = 345;
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

        private void btnDefaultVPNClient_Click(object sender, EventArgs e)
        {
            LoadSettings("VPNCLIENT");
            btnDefaultVPNClient.Enabled = false;
            //IF ALL OTHER DEFAULT BUTTONS ARE DISABLED, SAVE BUTTON SHOULD ALSO BE DISABLED
            DefaultButtonsCheck();
        }

        public void DefaultButtonsCheck()
        {
            btnSaveSettings.Enabled = false;
            if (btnDefaultVPNClient.Enabled || btnDefaultC9TraderRoot.Enabled)
                btnSaveSettings.Enabled = true;
        }

        private void btnChangeC9TraderRoot_Click(object sender, EventArgs e)
        {
            DialogResult C9TraderRootSelectorResult = new DialogResult();
            C9TraderRootSelectorResult = fldBrwsDiagSharedFolder.ShowDialog();
            if (C9TraderRootSelectorResult == DialogResult.OK)
            {
                string strResultChangeCheck = fldBrwsDiagSharedFolder.SelectedPath;
                if (strResultChangeCheck != lblC9TraderRoot.Text)
                {
                    lblC9TraderRoot.Text = strResultChangeCheck;
                    btnDefaultC9TraderRoot.Enabled = true;
                    btnSaveSettings.Enabled = true;
                }
            }
        }

        private void btnDefaultC9TraderRoot_Click(object sender, EventArgs e)
        {
            LoadSettings("C9TRADERROOT");
            btnDefaultC9TraderRoot.Enabled = false;
            DefaultButtonsCheck();
        }

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
                foreach(string s in strCurrentSettings)
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

        // VPN Services switch, turns VPN services on / off
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

        private void btnVPNClientLaunch_Click(object sender, EventArgs e)
        {
            
            ProcessStartInfo ProcVPNCli= new ProcessStartInfo();
            ProcVPNCli.FileName = lblVPNClientTarget.Text;
            Process.Start(ProcVPNCli);
        }

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

        private void btnRefreshVersions_Click(object sender, EventArgs e)
        {
            RefreshVersions();
        }

        private void chkBoxSetViewPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxSetUsrPassword.UseSystemPasswordChar = !chkBoxSetViewPassword.Checked;
        }
         
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

        private void BtnLaunchApp_Click(object sender, EventArgs e)
        {
            String parameters = "";
            parameters += "-u " + cmbBoxUsers.Text + " -p " + txtBoxSetUsrPassword.Text + " -r https://qa1-rest.xhoot.com";
            if (chkBoxMultiApp.Checked == true)
                parameters += " -a";
            if (chkBoxNoUpd.Checked == true)
                parameters += " -x";
            //Launches the app using user settings
            if (MSI_Toggle.Text == "Squirrel")
                Process.Start(lblC9TraderRoot.Text + "\\app-" + cmbBoxVersionsList.Text + "\\C9Shell.exe", parameters);
            else
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "C9Shell.exe";
                psi.WorkingDirectory = lblC9TraderRoot.Text;
                psi.Arguments = parameters;
                Process.Start(psi);
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

            BtnLaunchApp.Enabled = false;
            Thread.Sleep(1500);
            BtnLaunchApp.Enabled = true;
        }
        private void lblC9TraderRoot_Click(object sender, EventArgs e)
        {

        }

        // Toggles between MSI and Squirrel build to launch
        private void Button1_Click(object sender, EventArgs e)
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

        private void btnTestCycles_Click(object sender, EventArgs e)
        {
            //Opens the JIRA Test Cycles page in default browser
            System.Diagnostics.Process.Start("https://6w46h65ghw56gh7.atlassian.net/projects/CTTEST?selectedItem=com.thed.zephyr.je__project-centric-view-tests-page&testsTab=test-cycles-tab");
        }

        private void btnScrumBoard_Click(object sender, EventArgs e)
        {
            //Opens the JIRA Scrum Board page in default browser
            System.Diagnostics.Process.Start("https://6w46h65ghw56gh7.atlassian.net/secure/RapidBoard.jspa?rapidView=56");
        }

        private void btnWebApp_Click(object sender, EventArgs e)
        {
            //Opens the C9 Slack webapp in default browser
            System.Diagnostics.Process.Start("https://cloud9tec.slack.com/messages/C4N3M42QP/details/");
        }

        private void btnDesktopApp_Click(object sender, EventArgs e)
        {
            //Opens Slack desktop app
            String curUser = Environment.UserName;
            System.Diagnostics.Process.Start("C:\\Users\\" + curUser + "\\AppData\\Local\\slack\\slack.exe");
        }

        private void chkBoxMultiApp_CheckedChanged(object sender, EventArgs e)
        {

        }

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

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            foreach(Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains("C9Shell"))
                    p.Kill();
            }
            btnCloseApp.Enabled = false;
            Thread.Sleep(1500);
            btnCloseApp.Enabled = true;
        }

        private void reeEEE_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("reeeeeeeee2.wav");
            simpleSound.Play();
        }
    }
}
