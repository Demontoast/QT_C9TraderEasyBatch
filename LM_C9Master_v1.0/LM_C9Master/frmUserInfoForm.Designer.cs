namespace LM_C9Master
{
    partial class frmUserInfoForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBoxListSettings = new System.Windows.Forms.CheckedListBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBoxListSettings);
            this.groupBox1.Controls.Add(this.btnSaveChanges);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 632);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "\"User\" Settings";
            // 
            // chkBoxListSettings
            // 
            this.chkBoxListSettings.FormattingEnabled = true;
            this.chkBoxListSettings.Items.AddRange(new object[] {
            "Global Mute",
            "Local Recording",
            "Cloud Recording",
            "OPUS Files",
            "M4A Files",
            "Recording Warning Tone",
            "Local Trader Notification",
            "Click 2 Call",
            "Transcription",
            "600 Buttons",
            "Firmline",
            "Gateway Connections",
            "Positional Audio",
            "SFU"});
            this.chkBoxListSettings.Location = new System.Drawing.Point(28, 60);
            this.chkBoxListSettings.Name = "chkBoxListSettings";
            this.chkBoxListSettings.Size = new System.Drawing.Size(370, 446);
            this.chkBoxListSettings.TabIndex = 15;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSaveChanges.Location = new System.Drawing.Point(110, 536);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(188, 61);
            this.btnSaveChanges.TabIndex = 14;
            this.btnSaveChanges.Text = "Revert Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnResetChanges_Click);
            // 
            // frmUserInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 661);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmUserInfoForm";
            this.Text = "User Settings";
            this.Load += new System.EventHandler(this.frmUserInfoForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.CheckedListBox chkBoxListSettings;
    }
}