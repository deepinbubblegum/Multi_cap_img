
namespace Multi_cap_img
{
    partial class Settings
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
            this.CheckCameraList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // CheckCameraList
            // 
            this.CheckCameraList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckCameraList.FormattingEnabled = true;
            this.CheckCameraList.Location = new System.Drawing.Point(12, 12);
            this.CheckCameraList.Name = "CheckCameraList";
            this.CheckCameraList.Size = new System.Drawing.Size(238, 424);
            this.CheckCameraList.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 450);
            this.Controls.Add(this.CheckCameraList);
            this.MinimumSize = new System.Drawing.Size(278, 489);
            this.Name = "Settings";
            this.Text = "Setting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CheckCameraList;
    }
}