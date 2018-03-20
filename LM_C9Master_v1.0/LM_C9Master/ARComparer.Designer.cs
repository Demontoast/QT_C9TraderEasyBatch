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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxMediaFileARDisplay = new System.Windows.Forms.RichTextBox();
            this.txtBoxUserARDisplay = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxUserARs = new System.Windows.Forms.TextBox();
            this.btnCompareText = new System.Windows.Forms.Button();
            this.checkMark = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkMark)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.checkMark);
            this.panel1.Controls.Add(this.btnCompareText);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBoxMediaFileARDisplay);
            this.panel1.Controls.Add(this.txtBoxUserARDisplay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBoxUserARs);
            this.panel1.Location = new System.Drawing.Point(11, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2541, 1146);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1443, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(982, 63);
            this.label3.TabIndex = 6;
            this.label3.Text = "Paste Media File AR\'s Here to Compare";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(250, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(703, 63);
            this.label2.TabIndex = 5;
            this.label2.Text = "User AR\'s for the File Above";
            // 
            // txtBoxMediaFileARDisplay
            // 
            this.txtBoxMediaFileARDisplay.Location = new System.Drawing.Point(1275, 125);
            this.txtBoxMediaFileARDisplay.Name = "txtBoxMediaFileARDisplay";
            this.txtBoxMediaFileARDisplay.Size = new System.Drawing.Size(1250, 877);
            this.txtBoxMediaFileARDisplay.TabIndex = 4;
            this.txtBoxMediaFileARDisplay.Text = "";
            this.txtBoxMediaFileARDisplay.TextChanged += new System.EventHandler(this.txtBoxMediaFileARDisplay_TextChanged);
            // 
            // txtBoxUserARDisplay
            // 
            this.txtBoxUserARDisplay.Location = new System.Drawing.Point(14, 125);
            this.txtBoxUserARDisplay.Name = "txtBoxUserARDisplay";
            this.txtBoxUserARDisplay.Size = new System.Drawing.Size(1250, 877);
            this.txtBoxUserARDisplay.TabIndex = 3;
            this.txtBoxUserARDisplay.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Drag User AR File Here";
            // 
            // txtBoxUserARs
            // 
            this.txtBoxUserARs.AllowDrop = true;
            this.txtBoxUserARs.Location = new System.Drawing.Point(246, 10);
            this.txtBoxUserARs.Name = "txtBoxUserARs";
            this.txtBoxUserARs.Size = new System.Drawing.Size(2279, 31);
            this.txtBoxUserARs.TabIndex = 0;
            // 
            // btnCompareText
            // 
            this.btnCompareText.Location = new System.Drawing.Point(1292, 1033);
            this.btnCompareText.Name = "btnCompareText";
            this.btnCompareText.Size = new System.Drawing.Size(323, 85);
            this.btnCompareText.TabIndex = 7;
            this.btnCompareText.Text = "Compare AR\'s";
            this.btnCompareText.UseVisualStyleBackColor = true;
            this.btnCompareText.Click += new System.EventHandler(this.btnCompareText_Click);
            // 
            // checkMark
            // 
            this.checkMark.Image = ((System.Drawing.Image)(resources.GetObject("checkMark.Image")));
            this.checkMark.Location = new System.Drawing.Point(1640, 1033);
            this.checkMark.Name = "checkMark";
            this.checkMark.Size = new System.Drawing.Size(91, 85);
            this.checkMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.checkMark.TabIndex = 8;
            this.checkMark.TabStop = false;
            this.checkMark.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(908, 1033);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(344, 85);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ARComparer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1170);
            this.Controls.Add(this.panel1);
            this.Name = "ARComparer";
            this.Text = "ARComparer";
            this.Load += new System.EventHandler(this.ARComparer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}