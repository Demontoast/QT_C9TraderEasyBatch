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
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using TagLib;

namespace LM_C9Master
{
    public partial class ARComparer : Form
    {
        public ARComparer()
        {
            InitializeComponent();
            txtBoxUserARs.DragEnter += txtBoxUserARs_DragEnter;
            txtBoxUserARs.DragOver += textBoxUserARs_DragOver;
            txtBoxMediaFile.DragEnter += txtBoxMediaFile_DragEnter;
            txtBoxMediaFile.DragOver += textBoxMediaFile_DragOver;
            
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

            try
            {
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
            catch
            {
                MessageBox.Show("WARNING: INVALID USER AR LOG FILE DETECTED");
            }
            
        }

        private void textBoxUserARs_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBoxMediaFile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtBoxMediaFile_DragEnter(object sender, DragEventArgs e)
        {
            txtBoxMediaFileARDisplay.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txtBoxMediaFile.Text = files[0];
            }

            
            try
            {
                var mediaFile = TagLib.File.Create(txtBoxMediaFile.Text);
                string Comments = mediaFile.Tag.Comment;
                txtBoxMediaFileARDisplay.Text = Comments;
            }
            catch
            {
                MessageBox.Show("WARNING: INVALID MEDIA FILE");
            }      
        }

        private void ARComparer_Load(object sender, EventArgs e)
        {
            this.Width = 1138;
            this.Height = 540;
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

            List<String> uniqueARs = new List<String>();
            List<String> missingARs = new List<String>();
            char[] parameters = new char[2];
            parameters[0] = '\n';
            parameters[1] = ' ';
            int startEventCount = 0;
            int stopEventCount = 0;
            int pttEventCount = 0;

            string[] mediaARSplitter = txtBoxMediaFileARDisplay.Text.Split(parameters); ;

            foreach(string s in mediaARSplitter)
            {
                if(s.Contains("crEvent=Initiated"))
                    startEventCount++;
                if (s.Contains("cr=ptt"))
                    pttEventCount++;
                if (s.Contains("crEvent=UserRelease") || s.Contains("crEvent=AutoRelease"))
                    stopEventCount++;

                if (!txtBoxUserARDisplay.Text.Trim(' ', '\n').Contains(s.Trim(' ', '\n')))
                {
                    missingARs.Add(s);
                }

                if (!uniqueARs.Contains(s))
                    uniqueARs.Add(s);
                else if (!s.Equals(" ") && !s.Equals(""))
                {
                    errorX.Visible = true;
                    int index = -1;
                    int searchIndex = 0;
                    if ((index = txtBoxMediaFileARDisplay.Find(s, 0, RichTextBoxFinds.WholeWord)) > -1)
                    {
                        txtBoxMediaFileARDisplay.SelectionColor = Color.Blue;
                        txtBoxMediaFileARDisplay.Select(index, s.Length);
                        searchIndex = index + s.Length;
                        while ((index = txtBoxMediaFileARDisplay.Find(s, searchIndex, RichTextBoxFinds.WholeWord)) > -1)
                        {
                            txtBoxMediaFileARDisplay.SelectionColor = Color.Red;
                            txtBoxMediaFileARDisplay.Select(index, s.Length);
                            searchIndex = index + s.Length;
                        }
                    }
                    
                }
            }

            if (errorX.Visible == false)
            {
                if (missingARs.Count > 0)
                {
                    errorX.Visible = true;
                    int startSearch = 0;
                    int index = -1;
                    foreach (string s in missingARs)
                    {
                        if ((index = txtBoxMediaFileARDisplay.Find(s, startSearch, RichTextBoxFinds.WholeWord)) > -1)
                        {
                            txtBoxMediaFileARDisplay.SelectionColor = Color.Red;
                            txtBoxMediaFileARDisplay.Select(index, s.Length);

                            startSearch = index + s.Length;
                        }
                    }
                }
                else
                {
                    txtBoxMediaFileARDisplay.ForeColor = Color.Green;
                    checkMark.Visible = true;
                }
            }

            if (startEventCount != stopEventCount)
            {
                if (startEventCount > stopEventCount)
                    MessageBox.Show("WARNING: MORE START EVENTS DETECTED THAN STOP EVENTS");
                else
                    MessageBox.Show("WARNING: MORE STOP EVENTS DETECTED THAN START EVENTS");
            }

            if (pttEventCount % 2 != 0)
                MessageBox.Show("WARNING: UNEVEN NUMBER OF PTT EVENTS");

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnCompareText.Enabled = true;
            txtBoxMediaFileARDisplay.Clear();
            txtBoxMediaFile.Clear();
            checkMark.Visible = false;
            txtBoxMediaFileARDisplay.ForeColor = Color.Black;
            txtBoxMediaFileARDisplay.TextChanged += txtBoxMediaFileARDisplay_TextChanged;
            errorX.Visible = false;
        }
    }
}
