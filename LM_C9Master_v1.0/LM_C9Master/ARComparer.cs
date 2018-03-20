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
using System.Drawing;

namespace LM_C9Master
{
    public partial class ARComparer : Form
    {
        public ARComparer()
        {
            InitializeComponent();
            txtBoxUserARs.DragEnter += txtBoxUserARs_DragEnter;
            txtBoxUserARs.DragOver += textBoxUserARs_DragOver;
            
        }

        private void txtBoxUserARs_DragEnter(object sender, DragEventArgs e)
        {
            txtBoxUserARDisplay.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txtBoxUserARs.Text = files[0];
            }

            String Line = "";
            List<String> arFileContents = new List<String>();

            using (StreamReader SR = new StreamReader(txtBoxUserARs.Text))
            {
                while ((Line = SR.ReadLine()) != null)
                {
                    arFileContents.Add(Line);
                }
            }

            foreach (String textLine in arFileContents)
            {
                string[] lineSplitter = null;
                lineSplitter = textLine.Split('|');
                txtBoxUserARDisplay.AppendText(lineSplitter[1]);
                txtBoxUserARDisplay.AppendText("\n\n");
            }
        }

        private void textBoxUserARs_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ARComparer_Load(object sender, EventArgs e)
        {
            this.Width = 1600;
            this.Height = 650;
            this.CenterToScreen();
        }

        private void txtBoxMediaFileARDisplay_TextChanged(object sender, EventArgs e)
        {
            txtBoxMediaFileARDisplay.TextChanged -= txtBoxMediaFileARDisplay_TextChanged;
            string[] txtBoxAlteredText = null;
            string txtBoxUnalteredText = txtBoxMediaFileARDisplay.Text;
            char[] parameters = new char[2];
            parameters[0] = '^';
            parameters[1] = '|';
            txtBoxAlteredText = txtBoxUnalteredText.Split(parameters);
            txtBoxMediaFileARDisplay.Clear();
            foreach (string s in txtBoxAlteredText)
            {
                if (s.Length > 0)
                {
                    txtBoxMediaFileARDisplay.AppendText(s);
                    txtBoxMediaFileARDisplay.AppendText("\n\n");
                } 
            }
            txtBoxMediaFileARDisplay.TextChanged += txtBoxMediaFileARDisplay_TextChanged;
        }

        private void btnCompareText_Click(object sender, EventArgs e)
        {
            txtBoxMediaFileARDisplay.TextChanged -= txtBoxMediaFileARDisplay_TextChanged;
            btnCompareText.Enabled = false;
            if (txtBoxUserARDisplay.Text.Trim(' ', '\n').Contains(txtBoxMediaFileARDisplay.Text.Trim(' ', '\n')))
            {
                txtBoxMediaFileARDisplay.ForeColor = Color.Green;
                checkMark.Visible = true;
            } 
            else
            {
                string[] mediaARSplitter = txtBoxMediaFileARDisplay.Text.Split('\n');
                List<String> missingARs = new List<String>();
                foreach (string s in mediaARSplitter)
                {
                    if (!txtBoxUserARDisplay.Text.Trim(' ', '\n').Contains(s.Trim(' ', '\n')))
                        missingARs.Add(s);
                }

                int startSearch = 0;
                int index = -1;
                foreach(string s in missingARs)
                {
                    if ((index = txtBoxMediaFileARDisplay.Find(s, startSearch, RichTextBoxFinds.WholeWord)) > -1)
                    {
                        txtBoxMediaFileARDisplay.SelectionColor = Color.Red;
                        txtBoxMediaFileARDisplay.Select(index, s.Length);
                        
                        startSearch = index + 1;
                    }
                }
            }
                
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnCompareText.Enabled = true;
            txtBoxMediaFileARDisplay.Clear();
            checkMark.Visible = false;
            txtBoxMediaFileARDisplay.ForeColor = Color.Black;
            txtBoxMediaFileARDisplay.TextChanged += txtBoxMediaFileARDisplay_TextChanged;
        }
    }
}
