namespace LM_C9Master
{
    partial class ARComparer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ARComparer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorX = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxMediaFile = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.checkMark = new System.Windows.Forms.PictureBox();
            this.btnCompareText = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxMediaFileARDisplay = new System.Windows.Forms.RichTextBox();
            this.txtBoxUserARDisplay = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxUserARs = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkMark)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.errorX);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtBoxMediaFile);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.checkMark);
            this.panel1.Controls.Add(this.btnCompareText);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBoxMediaFileARDisplay);
            this.panel1.Controls.Add(this.txtBoxUserARDisplay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBoxUserARs);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2218, 956);
            this.panel1.TabIndex = 0;
            // 
            // errorX
            // 
            this.errorX.Image = ((System.Drawing.Image)(resources.GetObject("errorX.Image")));
            this.errorX.Location = new System.Drawing.Point(1472, 832);
            this.errorX.Name = "errorX";
            this.errorX.Size = new System.Drawing.Size(91, 85);
            this.errorX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.errorX.TabIndex = 12;
            this.errorX.TabStop = false;
            this.errorX.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1123, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Drag Media File Here";
            // 
            // txtBoxMediaFile
            // 
            this.txtBoxMediaFile.AllowDrop = true;
            this.txtBoxMediaFile.Location = new System.Drawing.Point(1341, 20);
            this.txtBoxMediaFile.Name = "txtBoxMediaFile";
            this.txtBoxMediaFile.Size = new System.Drawing.Size(843, 31);
            this.txtBoxMediaFile.TabIndex = 10;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(744, 826);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(344, 97);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // checkMark
            // 
            this.checkMark.Image = ((System.Drawing.Image)(resources.GetObject("checkMark.Image")));
            this.checkMark.Location = new System.Drawing.Point(1472, 832);
            this.checkMark.Name = "checkMark";
            this.checkMark.Size = new System.Drawing.Size(91, 85);
            this.checkMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.checkMark.TabIndex = 8;
            this.checkMark.TabStop = false;
            this.checkMark.Visible = false;
            // 
            // btnCompareText
            // 
            this.btnCompareText.Location = new System.Drawing.Point(1128, 826);
            this.btnCompareText.Name = "btnCompareText";
            this.btnCompareText.Size = new System.Drawing.Size(323, 97);
            this.btnCompareText.TabIndex = 7;
            this.btnCompareText.Text = "Verify AR\'s";
            this.btnCompareText.UseVisualStyleBackColor = true;
            this.btnCompareText.Click += new System.EventHandler(this.btnCompareText_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1302, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(840, 63);
            this.label3.TabIndex = 6;
            this.label3.Text = "Media File AR\'s for the File Above";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(703, 63);
            this.label2.TabIndex = 5;
            this.label2.Text = "User AR\'s for the File Above";
            // 
            // txtBoxMediaFileARDisplay
            // 
            this.txtBoxMediaFileARDisplay.Location = new System.Drawing.Point(1128, 135);
            this.txtBoxMediaFileARDisplay.Name = "txtBoxMediaFileARDisplay";
            this.txtBoxMediaFileARDisplay.Size = new System.Drawing.Size(1056, 657);
            this.txtBoxMediaFileARDisplay.TabIndex = 4;
            this.txtBoxMediaFileARDisplay.Text = "";
            this.txtBoxMediaFileARDisplay.TextChanged += new System.EventHandler(this.txtBoxMediaFileARDisplay_TextChanged);
            // 
            // txtBoxUserARDisplay
            // 
            this.txtBoxUserARDisplay.Location = new System.Drawing.Point(32, 135);
            this.txtBoxUserARDisplay.Name = "txtBoxUserARDisplay";
            this.txtBoxUserARDisplay.Size = new System.Drawing.Size(1056, 657);
            this.txtBoxUserARDisplay.TabIndex = 3;
            this.txtBoxUserARDisplay.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Drag User AR File Here";
            // 
            // txtBoxUserARs
            // 
            this.txtBoxUserARs.AllowDrop = true;
            this.txtBoxUserARs.Location = new System.Drawing.Point(245, 20);
            this.txtBoxUserARs.Name = "txtBoxUserARs";
            this.txtBoxUserARs.Size = new System.Drawing.Size(843, 31);
            this.txtBoxUserARs.TabIndex = 0;
            // 
            // ARComparer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2262, 983);
            this.Controls.Add(this.panel1);
            this.Name = "ARComparer";
            this.Text = "ARComparer";
            this.Load += new System.EventHandler(this.ARComparer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkMark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtBoxMediaFileARDisplay;
        private System.Windows.Forms.RichTextBox txtBoxUserARDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxUserARs;
        private System.Windows.Forms.Button btnCompareText;
        private System.Windows.Forms.PictureBox checkMark;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxMediaFile;
        private System.Windows.Forms.PictureBox errorX;
    }
}