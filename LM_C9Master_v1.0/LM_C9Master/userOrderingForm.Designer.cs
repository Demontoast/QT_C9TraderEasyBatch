namespace LM_C9Master
{
    partial class userOrderingForm
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
            this.listBoxUserOrdering = new System.Windows.Forms.ListBox();
            this.btnShiftUp = new System.Windows.Forms.Button();
            this.btnShiftDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxUserOrdering
            // 
            this.listBoxUserOrdering.FormattingEnabled = true;
            this.listBoxUserOrdering.ItemHeight = 25;
            this.listBoxUserOrdering.Location = new System.Drawing.Point(32, 29);
            this.listBoxUserOrdering.Name = "listBoxUserOrdering";
            this.listBoxUserOrdering.Size = new System.Drawing.Size(322, 479);
            this.listBoxUserOrdering.TabIndex = 0;
            // 
            // btnShiftUp
            // 
            this.btnShiftUp.Location = new System.Drawing.Point(386, 163);
            this.btnShiftUp.Name = "btnShiftUp";
            this.btnShiftUp.Size = new System.Drawing.Size(168, 71);
            this.btnShiftUp.TabIndex = 1;
            this.btnShiftUp.Text = "Shift Up";
            this.btnShiftUp.UseVisualStyleBackColor = true;
            this.btnShiftUp.Click += new System.EventHandler(this.btnShiftUp_Click);
            // 
            // btnShiftDown
            // 
            this.btnShiftDown.Location = new System.Drawing.Point(386, 280);
            this.btnShiftDown.Name = "btnShiftDown";
            this.btnShiftDown.Size = new System.Drawing.Size(168, 71);
            this.btnShiftDown.TabIndex = 2;
            this.btnShiftDown.Text = "Shift Down";
            this.btnShiftDown.UseVisualStyleBackColor = true;
            this.btnShiftDown.Click += new System.EventHandler(this.btnShiftDown_Click);
            // 
            // userOrderingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 559);
            this.Controls.Add(this.btnShiftDown);
            this.Controls.Add(this.btnShiftUp);
            this.Controls.Add(this.listBoxUserOrdering);
            this.Name = "userOrderingForm";
            this.Text = "User Ordering";
            this.Load += new System.EventHandler(this.userOrderingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUserOrdering;
        private System.Windows.Forms.Button btnShiftUp;
        private System.Windows.Forms.Button btnShiftDown;
    }
}