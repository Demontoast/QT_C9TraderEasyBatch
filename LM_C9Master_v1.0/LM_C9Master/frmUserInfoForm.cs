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
    public partial class frmUserInfoForm : Form
    {
        public frmUserInfoForm()
        {
            InitializeComponent();
        }

        List<CheckBox> chkBoxes = new List<CheckBox>();
        String currUser = "";
        String userFeatures = "";

        private void frmUserInfoForm_Load(object sender, EventArgs e)
        {
            this.Height = 380;
            this.Width = 260;
            this.FormClosed += userInfoClosedEventHandler;
        }

        public void loadSettings(String user, String features)
        {
            chkBoxListSettings.ClearSelected();
            groupBox1.Text = user;
            currUser = user;
            userFeatures = features;
            for (int i = 0; i < userFeatures.Length; i++)
            {
                if (userFeatures[i].Equals('1'))
                {
                    try
                    {
                        chkBoxListSettings.SetItemChecked(i, true);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        chkBoxListSettings.SetItemChecked(i, false);
                    }
                    catch
                    {

                    }
                }
                    
            }
        }

        private void btnResetChanges_Click(object sender, EventArgs e)
        {
            loadSettings(currUser, userFeatures);
        }

        public void userInfoClosedEventHandler(object sender, EventArgs e)
        {
            String Line = null;
            String[] LineSplit;
            using (StreamReader SR = new StreamReader("LM_C9MSettings.set"))
            {
                using (StreamWriter SW = new StreamWriter("LM_C9MSettings.new"))
                {
                    while ((Line = SR.ReadLine()) != "<UserInfo>")
                        SW.WriteLine(Line);
                    SW.WriteLine("<UserInfo>");
                    bool success = false;
                    while ((Line = SR.ReadLine()) != "</UserInfo>")
                    {
                        try
                        {
                            LineSplit = Line.Split(':');
                            if (LineSplit[0].Equals(currUser))
                            {
                                SW.Write(currUser + ":");
                                for (int y = 0; y < chkBoxListSettings.Items.Count; y++)
                                {
                                    if (chkBoxListSettings.GetItemChecked(y) == true)
                                        SW.Write("1");
                                    else
                                        SW.Write("0");
                                }
                                success = true;
                            }
                            else
                                SW.WriteLine(Line);
                        }
                        catch
                        { }
                    }
                    if (!success)
                    {
                        SW.Write(currUser + ":");
                        for (int y = 0; y < chkBoxListSettings.Items.Count; y++)
                        {
                            if (chkBoxListSettings.GetItemChecked(y) == true)
                                SW.Write("1");
                            else
                                SW.Write("0");
                        }
                    }
                    SW.WriteLine("");
                    SW.WriteLine("</UserInfo>");
                    while ((Line = SR.ReadLine()) != null)
                        SW.WriteLine(Line);
                }
            }
            File.Delete("LM_C9MSettings.set");
            File.Copy("LM_C9MSettings.new", "LM_C9MSettings.set");
            File.Delete("LM_C9MSettings.new");
        }
    }
}
