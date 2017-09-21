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
using System.IO;
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
            LoadSettings("SHAREDFOLDER");
            LoadSettings("C9TRADERROOT");
            
            LoadSettings("APPACCOUNTS");
            
            //VPN Manager Startup Setup
            ServiceController[] scActiveServices=ServiceController.GetServices();
            foreach(ServiceController service in scActiveServices)
            {
                if(service.ServiceName== "acumbrellaagent"||service.ServiceName== "vpnagent")
                {
                    if(service.Status==ServiceControllerStatus.Running)
                    {
                        BtnVPNSwitch.BackColor = Color.LightGreen;
                        BtnVPNSwitch.ForeColor = Color.DarkGreen;
                        BtnVPNSwitch.Text = "ON";
                        Thread.Sleep(1000);
                        btnVPNClientLaunch.Enabled = true;
                        break;
                    }
                    break;
                }
                             
                
            }
            if (BtnVPNSwitch.Text == "ON")
            {
                try
                {
                    ProcessStartInfo ProcSharedTry = new ProcessStartInfo();
                    ProcSharedTry.FileName = @"\\c9-fs01";
                    ProcSharedTry.WindowStyle = ProcessWindowStyle.Hidden;
                    ProcSharedTry.CreateNoWindow = true;
                    Process.Start(ProcSharedTry);

                    btnOpenSharedFolder.Enabled = true;
                    

                }
                catch
                { btnOpenSharedFolder.Enabled = false; }
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

                
                case "SHAREDFOLDER":
                    using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                    {
                        Line = SR.ReadLine();
                        while (Line != "</VPNManager>")
                        {
                            LineSplit = Line.Split('=');
                            if (LineSplit[0] == "SharedFolderLocation")
                            {
                                lblSharedFolderTarget.Text =  LineSplit[1];
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

        private void btnChangeSharedFolder_Click(object sender, EventArgs e)
        {
            DialogResult SharedFolderSelectorResult = new DialogResult();
            SharedFolderSelectorResult = fldBrwsDiagSharedFolder.ShowDialog();
            if(SharedFolderSelectorResult==DialogResult.OK)
            {
                string strResultChangeCheck = fldBrwsDiagSharedFolder.SelectedPath;
                if(strResultChangeCheck!=lblSharedFolderTarget.Text)
                {
                    lblSharedFolderTarget.Text = strResultChangeCheck;
                    btnDefaultSharedFolder.Enabled = true;
                    btnSaveSettings.Enabled = true;
                }
            }
        }

        private void btnDefaultSharedFolder_Click(object sender, EventArgs e)
        {
            LoadSettings("SHAREDFOLDER");
            btnDefaultSharedFolder.Enabled = false;
            //IF ALL OTHER DEFAULT BUTTONS ARE DISABLED, SAVE BUTTON SHOULD ALSO BE DISABLED
            DefaultButtonsCheck();
        }

        public void DefaultButtonsCheck()
        {
            btnSaveSettings.Enabled = false;
            if (btnDefaultVPNClient.Enabled || btnDefaultSharedFolder.Enabled||btnDefaultC9TraderRoot.Enabled)
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
            if(btnDefaultSharedFolder.Enabled)
            {
                string[] strSplitCheck = null;
                int intIndex = 0;
                foreach (string s in strCurrentSettings)
                {
                    strSplitCheck = s.Split('=');
                    if (strSplitCheck[0] == "SharedFolderLocation")
                    {
                        strCurrentSettings[intIndex] = "SharedFolderLocation=" + lblSharedFolderTarget.Text;
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
            btnDefaultSharedFolder.Enabled = false;
            btnSaveSettings.Enabled = false;


        }

        private void BtnVPNSwitch_Click(object sender, EventArgs e)
        {
            ServiceController[] scActiveServices = ServiceController.GetServices();

            if(BtnVPNSwitch.Text=="ON")
            {
                foreach(ServiceController service in scActiveServices)
                {
                    if(service.ServiceName == "acumbrellaagent" || service.ServiceName == "vpnagent")
                    {
                        if(service.Status!=ServiceControllerStatus.Stopped)
                        {
                            service.Stop();
                            
                        }
                        
                    }
                }
                BtnVPNSwitch.Text = "OFF";
                BtnVPNSwitch.BackColor = Color.LightCoral;
                BtnVPNSwitch.ForeColor = Color.DarkRed;
                btnOpenSharedFolder.Enabled = false;
                btnVPNClientLaunch.Enabled = false;
            }
            else
            {
                foreach (ServiceController service in scActiveServices)
                {
                    if (service.ServiceName == "acumbrellaagent" || service.ServiceName == "vpnagent")
                    {
                        if (service.Status != ServiceControllerStatus.Running)
                        {
                            service.Start();
                            
                        }

                    }
                }
                BtnVPNSwitch.Text = "ON";
                BtnVPNSwitch.BackColor = Color.LightGreen;
                BtnVPNSwitch.ForeColor = Color.DarkGreen;
                
                btnVPNClientLaunch.Enabled = true;
            }
        }

        private void btnVPNClientLaunch_Click(object sender, EventArgs e)
        {
            
            ProcessStartInfo ProcVPNCli= new ProcessStartInfo();
            ProcVPNCli.FileName = lblVPNClientTarget.Text;
            Process.Start(ProcVPNCli);
            btnOpenSharedFolder.Enabled = true;
        }

        private void btnOpenSharedFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", lblSharedFolderTarget.Text);

        }

        public void RefreshVersions()
        {
           /*
            string[]strSubDirList= Directory.GetDirectories(lblC9TraderRoot.Text);
            foreach(string s in strSubDirList)
            {
                string[] strPathSplit = s.Split('\\') ;
                string[] strAppSplit = strPathSplit[strPathSplit.Length-1].Split('-');
                if (strAppSplit[0] == "app")
                   cmbBoxVersionsList.SelectedIndex= cmbBoxVersionsList.Items.Add(strAppSplit[1]);
            }
            
            */
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

            bool flgMustAddNewUser = false;
            if (AccountsFromSettings[0].strUserName == "NoSavedAccounts")
                AccountsFromSettings[0].strUserName = "";

            foreach(AppAccount AC in AccountsFromSettings)
            {
                if(AC.strUserName==cmbBoxUsers.Text)
                {
                    
                    if(AC.strPassword!=txtBoxSetUsrPassword.Text)
                    {
                       
                        AC.strPassword = txtBoxSetUsrPassword.Text;
                    }
                }
                else
                {
                    flgMustAddNewUser = true;
                    
                    
                   // AccountsFromSettings.Sort();
                }

            }
            if(flgMustAddNewUser)
            {
                AppAccount tmpAcc = new AppAccount();
                tmpAcc.strUserName = cmbBoxUsers.Text;
                tmpAcc.strPassword = txtBoxSetUsrPassword.Text;
                AccountsFromSettings.Add(tmpAcc);
            }
            SaveAccountsToSettings();

                      
            
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
    }
}
