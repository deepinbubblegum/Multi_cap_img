
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
            this.Close_setting = new System.Windows.Forms.Button();
            this.SaveConfig = new System.Windows.Forms.Button();
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
            this.CheckCameraList.Size = new System.Drawing.Size(238, 394);
            this.CheckCameraList.TabIndex = 0;
            // 
            // Close_setting
            // 
            this.Close_setting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_setting.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_setting.Location = new System.Drawing.Point(94, 415);
            this.Close_setting.Name = "Close_setting";
            this.Close_setting.Size = new System.Drawing.Size(75, 23);
            this.Close_setting.TabIndex = 1;
            this.Close_setting.Text = "Close";
            this.Close_setting.UseVisualStyleBackColor = true;
            this.Close_setting.Click += new System.EventHandler(this.Close_setting_Click);
            // 
            // SaveConfig
            // 
            this.SaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveConfig.Location = new System.Drawing.Point(175, 415);
            this.SaveConfig.Name = "SaveConfig";
            this.SaveConfig.Size = new System.Drawing.Size(75, 23);
            this.SaveConfig.TabIndex = 1;
            this.SaveConfig.Text = "OK";
            this.SaveConfig.UseVisualStyleBackColor = true;
            this.SaveConfig.Click += new System.EventHandler(this.SaveConfig_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.SaveConfig;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Close_setting;
            this.ClientSize = new System.Drawing.Size(262, 450);
            this.Controls.Add(this.SaveConfig);
            this.Controls.Add(this.Close_setting);
            this.Controls.Add(this.CheckCameraList);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(278, 489);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(278, 489);
            this.Name = "Settings";
            this.Text = "Setting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CheckCameraList;
        private System.Windows.Forms.Button Close_setting;
        private System.Windows.Forms.Button SaveConfig;
    }
}