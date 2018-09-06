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

namespace LM_C9Master
{
    public partial class generateNewSettingsFilePrompt : Form
    {

        public generateNewSettingsFilePrompt()
        {
            InitializeComponent();
        }

        private void generateNewSettingsFilePrompt_Load(object sender, EventArgs e)
        {
            this.Width = 302;
            this.Height = 156;
            this.CenterToScreen();

            if (!File.Exists("LM_C9MSettings.set"))
            {
                generateSettings();
            }
            else
            {
                using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
                {
                    if (SR.ReadLine() == this.Text)
                        this.Close();
                }
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Function generates a new settings file from scratch
        public void generateSettings()
        {
            using (StreamWriter SW = File.CreateText("LM_C9MSettings.set"))
            {
                SW.WriteLine(this.Text);
                SW.WriteLine("<VPNClient>");
                SW.WriteLine(@"VPNClientLocation=C:\Program Files (x86)\Cisco\Cisco AnyConnect Secure Mobility Client\vpnui.exe");
                SW.WriteLine("</VPNClient>");
                SW.WriteLine("<AppManager>");
                String currUser = Environment.UserName.ToString();
                SW.WriteLine(@"C9TraderRootLocation=C:\Users\" + currUser + @"\AppData\Local\C9Trader");
                SW.WriteLine(@"C9TraderMSILocation=C:\");
                SW.WriteLine("</AppManager>");
                SW.WriteLine("<Accessories>");
                SW.WriteLine("</Accessories>");
                SW.WriteLine("<DesignatedServer>");
                SW.WriteLine("Server=https://qa1-rest.xhoot.com");
                SW.WriteLine("</DesignatedServer>");
                SW.WriteLine("<TranscriptionServer>");
                SW.WriteLine("TranscriptionServer=");
                SW.WriteLine("</TranscriptionServer>");
                SW.WriteLine("<UserInfo>");
                SW.WriteLine("</UserInfo>");
                SW.WriteLine("<UserCollection>");
                SW.WriteLine("</UserCollection>");
                SW.Close();
            }
        }

        private void btnYes_Click_1(object sender, EventArgs e)
        {
            string vmLoc = "";
            string vpnLoc = "";
            string[] traderRoots = new string[2];
            traderRoots[0] = "";
            traderRoots[1] = "";
            string sqdbLoc = "";
            string tcpLoc = "";
            string trscpLoc = "";
            List<string> servers = new List<string>();
            List<string> userInfo = new List<string>();
            List<string> users = new List<string>();
            List<string> accessories = new List<string>();

            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                string Line = "";
                Line = SR.ReadLine();
                while (Line != null && Line != "")
                {
                    if (Line.Contains("VMLocation="))
                    {
                        vmLoc = Line;
                    }

                    if (Line.Contains("VPNClientLocation="))
                    {
                        vpnLoc = Line;
                    }

                    if (Line.Contains("C9TraderRootLocation="))
                    {
                        traderRoots[0] = Line;
                    }

                    if (Line.Contains("C9TraderMSILocation="))
                    {
                        traderRoots[1] = Line;
                    }

                    if (Line.Contains("SQDBLiteLocation="))
                    {
                        sqdbLoc = Line;
                    }

                    if (Line.Contains("TCPViewLocation="))
                    {
                        tcpLoc = Line;
                    }

                    if (Line.Contains("TranscriptionServer="))
                    {
                        trscpLoc = Line;
                    }

                    if (Line.Contains("<DesignatedServer>"))
                    {
                        Line = SR.ReadLine();
                        while (!Line.Contains("</DesignatedServer>"))
                        {
                            servers.Add(Line);
                            Line = SR.ReadLine();
                        }
                    }

                    if (Line.Contains("<UserInfo>"))
                    {
                        Line = SR.ReadLine();
                        while (!Line.Contains("</UserInfo>"))
                        {
                            userInfo.Add(Line);
                            Line = SR.ReadLine();
                        }
                    }

                    if (Line.Contains("<UserCollection>"))
                    {
                        Line = SR.ReadLine();
                        while (!Line.Contains("</UserCollection>"))
                        {
                            users.Add(Line);
                            Line = SR.ReadLine();
                        }
                    }

                    if (Line.Contains("<Accessories>"))
                    {
                        Line = SR.ReadLine();
                        while (!Line.Contains("</Accessories>"))
                        {
                            accessories.Add(Line);
                            Line = SR.ReadLine();
                        }
                    }

                    Line = SR.ReadLine();
                }
            }
            using (StreamWriter SW = File.CreateText("LM_C9MSettings.new"))
            {
                SW.WriteLine(this.Text);
                SW.WriteLine("<VPNClient>");
                if (vpnLoc != "" && vpnLoc != null)
                    SW.WriteLine(vpnLoc);
                else
                    SW.WriteLine("VPNClientLocation=");
                SW.WriteLine("</VPNClient>");
                SW.WriteLine("<AppManager>");
                if (traderRoots[0] != "" && traderRoots[0] != null)
                    SW.WriteLine(traderRoots[0]);
                else
                    SW.WriteLine("C9TraderRootLocation=");
                if (traderRoots[1] != "" && traderRoots[1] != null)
                    SW.WriteLine(traderRoots[1]);
                else
                    SW.WriteLine("C9TraderMSILocation=");
                SW.WriteLine("</AppManager>");
                SW.WriteLine("<Accessories>");
                if (accessories.Count > 0)
                {
                    foreach (string access in accessories)
                        SW.WriteLine(access);
                }
                else
                {
                    if (vmLoc != "" && vmLoc != null)
                        SW.WriteLine(vmLoc);
                    else
                        SW.WriteLine("VMLocation=");
                    if (sqdbLoc != "" && sqdbLoc != null)
                        SW.WriteLine(sqdbLoc);
                    else
                        SW.WriteLine("SQDBLiteLocation=");
                    if (tcpLoc != "" && tcpLoc != null)
                        SW.WriteLine(tcpLoc);
                    else
                        SW.WriteLine("TCPViewLocation=");
                }
                SW.WriteLine("</Accessories>");
                SW.WriteLine("<DesignatedServer>");
                if (servers.Count > 0)
                {
                    foreach (string serv in servers)
                        SW.WriteLine(serv);
                }
                else
                    SW.WriteLine("Server=https://qa1-rest.xhoot.com");
                SW.WriteLine("</DesignatedServer>");
                SW.WriteLine("<TranscriptionServer>");
                if (trscpLoc != "" && trscpLoc != null)
                    SW.WriteLine(trscpLoc);
                else
                    SW.WriteLine("TranscriptionServer=");
                SW.WriteLine("</TranscriptionServer>");
                SW.WriteLine("<UserInfo>");
                if (userInfo.Count > 0)
                {
                    foreach (string uInf in userInfo)
                        SW.WriteLine(uInf);
                }
                SW.WriteLine("</UserInfo>");
                SW.WriteLine("<UserCollection>");
                if (users.Count > 0)
                {
                    foreach (string user in users)
                        SW.WriteLine(user);
                }
                SW.WriteLine("</UserCollection>");
                SW.Close();
            }

            File.Delete("LM_C9MSettings.set");
            File.Copy("LM_C9MSettings.new", "LM_C9MSettings.set");
            File.Delete("LM_C9MSettings.new");
            this.Close();
        }
    }
}
