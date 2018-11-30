using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LM_C9Master
{
    public partial class userOrderingForm : Form
    {


        List<frmMainForm.AppAccount> userList = new List<frmMainForm.AppAccount>();
        List<frmMainForm.AppAccount> reOrderedUserList = new List<frmMainForm.AppAccount>();

        public userOrderingForm(List<frmMainForm.AppAccount> passedUserList)
        {
            InitializeComponent();
            this.FormClosing += userOrderingFormClosedEventHandler;
            userList = passedUserList;
            foreach (LM_C9Master.frmMainForm.AppAccount AC in userList)
            {
                listBoxUserOrdering.Items.Add(AC.strUserName);
            }
        }

        private void userOrderingForm_Load(object sender, EventArgs e)
        {
            this.Width = 302;
            this.Height = 325;
            this.CenterToScreen();
        }

        private void btnShiftUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxUserOrdering.SelectedIndex;
            if (selectedIndex != 0)
            {
                string selectedUser = listBoxUserOrdering.SelectedItem.ToString();
                listBoxUserOrdering.Items.Insert(selectedIndex - 1, selectedUser);
                listBoxUserOrdering.Items.RemoveAt(selectedIndex+1);
                listBoxUserOrdering.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnShiftDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxUserOrdering.SelectedIndex;
            if (selectedIndex != listBoxUserOrdering.Items.Count-1)
            {
                string selectedUser = listBoxUserOrdering.SelectedItem.ToString();
                listBoxUserOrdering.Items.Insert(selectedIndex + 2, selectedUser);
                listBoxUserOrdering.Items.RemoveAt(selectedIndex);
                listBoxUserOrdering.SelectedIndex = selectedIndex + 1;
            }
        }

        public void userOrderingFormClosedEventHandler(object sender, EventArgs e)
        {
            bool[] foundFlag = new bool[userList.Count];
            foreach (String s in listBoxUserOrdering.Items)
            {
                foreach (LM_C9Master.frmMainForm.AppAccount AC in userList)
                {
                    if (foundFlag[userList.IndexOf(AC)] == false)
                    {
                        if (AC.strUserName == s)
                        {
                            reOrderedUserList.Add(AC);
                            foundFlag[userList.IndexOf(AC)] = true;
                        }
                    }    
                }
            }
        }

        public List<LM_C9Master.frmMainForm.AppAccount> getOrderedAccounts()
        {
            return reOrderedUserList;
        }
    }
}
