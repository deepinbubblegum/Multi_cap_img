
namespace Multi_cap_img
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.ResolutionBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CameraControl_tab = new System.Windows.Forms.TabControl();
            this.Camera_control = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.B_Auto = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.CheckSync = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.Apply_setprop = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.StopPreview = new System.Windows.Forms.Button();
            this.Preview = new System.Windows.Forms.Button();
            this.SelectDevicePreview = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PreviewBox = new System.Windows.Forms.PictureBox();
            this.LogsBoxGroup = new System.Windows.Forms.GroupBox();
            this.logs_box_display = new System.Windows.Forms.TextBox();
            this.menubar.SuspendLayout();
            this.GroupBoxDisplay.SuspendLayout();
            this.CameraControl_tab.SuspendLayout();
            this.Camera_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).BeginInit();
            this.LogsBoxGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.Settings});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(1264, 24);
            this.menubar.TabIndex = 1;
            this.menubar.Text = "menubar";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.outputToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configToolStripMenuItem.Text = "Config";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.importToolStripMenuItem.Text = "Load config";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exportToolStripMenuItem.Text = "Save config";
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.outputToolStripMenuItem.Text = "Output Path";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Settings
            // 
            this.Settings.Name = "Settings";
            this.Settings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.Settings.Size = new System.Drawing.Size(61, 20);
            this.Settings.Text = "Settings";
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // GroupBoxDisplay
            // 
            this.GroupBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxDisplay.Controls.Add(this.btnCapture);
            this.GroupBoxDisplay.Controls.Add(this.ResolutionBox);
            this.GroupBoxDisplay.Controls.Add(this.label2);
            this.GroupBoxDisplay.Controls.Add(this.CameraControl_tab);
            this.GroupBoxDisplay.Controls.Add(this.btnRef);
            this.GroupBoxDisplay.Controls.Add(this.StopPreview);
            this.GroupBoxDisplay.Controls.Add(this.Preview);
            this.GroupBoxDisplay.Controls.Add(this.SelectDevicePreview);
            this.GroupBoxDisplay.Controls.Add(this.label1);
            this.GroupBoxDisplay.Controls.Add(this.PreviewBox);
            this.GroupBoxDisplay.Location = new System.Drawing.Point(12, 27);
            this.GroupBoxDisplay.Name = "GroupBoxDisplay";
            this.GroupBoxDisplay.Size = new System.Drawing.Size(1240, 599);
            this.GroupBoxDisplay.TabIndex = 2;
            this.GroupBoxDisplay.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapture.Location = new System.Drawing.Point(801, 570);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 9;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // ResolutionBox
            // 
            this.ResolutionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResolutionBox.FormattingEnabled = true;
            this.ResolutionBox.Location = new System.Drawing.Point(864, 46);
            this.ResolutionBox.Name = "ResolutionBox";
            this.ResolutionBox.Size = new System.Drawing.Size(168, 21);
            this.ResolutionBox.TabIndex = 8;
            this.ResolutionBox.SelectedIndexChanged += new System.EventHandler(this.ResolutionBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(798, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Resolution:";
            // 
            // CameraControl_tab
            // 
            this.CameraControl_tab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CameraControl_tab.Controls.Add(this.Camera_control);
            this.CameraControl_tab.Location = new System.Drawing.Point(796, 73);
            this.CameraControl_tab.Name = "CameraControl_tab";
            this.CameraControl_tab.SelectedIndex = 0;
            this.CameraControl_tab.Size = new System.Drawing.Size(438, 491);
            this.CameraControl_tab.TabIndex = 6;
            // 
            // Camera_control
            // 
            this.Camera_control.Controls.Add(this.label3);
            this.Camera_control.Controls.Add(this.B_Auto);
            this.Camera_control.Controls.Add(this.numericUpDown1);
            this.Camera_control.Controls.Add(this.CheckSync);
            this.Camera_control.Controls.Add(this.trackBar1);
            this.Camera_control.Controls.Add(this.Apply_setprop);
            this.Camera_control.Location = new System.Drawing.Point(4, 22);
            this.Camera_control.Name = "Camera_control";
            this.Camera_control.Padding = new System.Windows.Forms.Padding(3);
            this.Camera_control.Size = new System.Drawing.Size(430, 465);
            this.Camera_control.TabIndex = 0;
            this.Camera_control.Text = "Camera Control";
            this.Camera_control.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Auto";
            // 
            // B_Auto
            // 
            this.B_Auto.AutoSize = true;
            this.B_Auto.Location = new System.Drawing.Point(396, 49);
            this.B_Auto.Name = "B_Auto";
            this.B_Auto.Size = new System.Drawing.Size(15, 14);
            this.B_Auto.TabIndex = 12;
            this.B_Auto.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(340, 47);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown1.TabIndex = 11;
            // 
            // CheckSync
            // 
            this.CheckSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckSync.AutoSize = true;
            this.CheckSync.Checked = true;
            this.CheckSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckSync.Location = new System.Drawing.Point(6, 440);
            this.CheckSync.Name = "CheckSync";
            this.CheckSync.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CheckSync.Size = new System.Drawing.Size(64, 17);
            this.CheckSync.TabIndex = 10;
            this.CheckSync.Text = "Sync All";
            this.CheckSync.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.trackBar1.Location = new System.Drawing.Point(79, 36);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar1.Size = new System.Drawing.Size(255, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // Apply_setprop
            // 
            this.Apply_setprop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Apply_setprop.Location = new System.Drawing.Point(349, 436);
            this.Apply_setprop.Name = "Apply_setprop";
            this.Apply_setprop.Size = new System.Drawing.Size(75, 23);
            this.Apply_setprop.TabIndex = 0;
            this.Apply_setprop.Text = "Apply";
            this.Apply_setprop.UseVisualStyleBackColor = true;
            this.Apply_setprop.Click += new System.EventHandler(this.Apply_setprop_Click);
            // 
            // btnRef
            // 
            this.btnRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRef.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRef.BackgroundImage")));
            this.btnRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRef.Location = new System.Drawing.Point(1008, 17);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(25, 25);
            this.btnRef.TabIndex = 5;
            this.btnRef.UseVisualStyleBackColor = true;
            this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
            // 
            // StopPreview
            // 
            this.StopPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StopPreview.Location = new System.Drawing.Point(1157, 570);
            this.StopPreview.Name = "StopPreview";
            this.StopPreview.Size = new System.Drawing.Size(75, 23);
            this.StopPreview.TabIndex = 4;
            this.StopPreview.Text = "StopPreview";
            this.StopPreview.UseVisualStyleBackColor = true;
            this.StopPreview.Click += new System.EventHandler(this.StopPreview_Click);
            // 
            // Preview
            // 
            this.Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Preview.Location = new System.Drawing.Point(1080, 570);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(75, 23);
            this.Preview.TabIndex = 3;
            this.Preview.Text = "Preview";
            this.Preview.UseVisualStyleBackColor = true;
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // SelectDevicePreview
            // 
            this.SelectDevicePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectDevicePreview.FormattingEnabled = true;
            this.SelectDevicePreview.Location = new System.Drawing.Point(880, 19);
            this.SelectDevicePreview.Name = "SelectDevicePreview";
            this.SelectDevicePreview.Size = new System.Drawing.Size(122, 21);
            this.SelectDevicePreview.TabIndex = 2;
            this.SelectDevicePreview.SelectedIndexChanged += new System.EventHandler(this.SelectDevicePreview_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(798, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "DevicePreview : ";
            // 
            // PreviewBox
            // 
            this.PreviewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PreviewBox.Location = new System.Drawing.Point(3, 9);
            this.PreviewBox.Name = "PreviewBox";
            this.PreviewBox.Size = new System.Drawing.Size(787, 555);
            this.PreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewBox.TabIndex = 0;
            this.PreviewBox.TabStop = false;
            // 
            // LogsBoxGroup
            // 
            this.LogsBoxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogsBoxGroup.Controls.Add(this.logs_box_display);
            this.LogsBoxGroup.Location = new System.Drawing.Point(14, 626);
            this.LogsBoxGroup.Name = "LogsBoxGroup";
            this.LogsBoxGroup.Size = new System.Drawing.Size(1238, 70);
            this.LogsBoxGroup.TabIndex = 4;
            this.LogsBoxGroup.TabStop = false;
            this.LogsBoxGroup.Text = "Logs";
            // 
            // logs_box_display
            // 
            this.logs_box_display.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logs_box_display.Location = new System.Drawing.Point(6, 19);
            this.logs_box_display.Multiline = true;
            this.logs_box_display.Name = "logs_box_display";
            this.logs_box_display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logs_box_display.Size = new System.Drawing.Size(1226, 45);
            this.logs_box_display.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 708);
            this.Controls.Add(this.LogsBoxGroup);
            this.Controls.Add(this.GroupBoxDisplay);
            this.Controls.Add(this.menubar);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Main";
            this.Text = "Multi Camera Capture";
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.GroupBoxDisplay.ResumeLayout(false);
            this.GroupBoxDisplay.PerformLayout();
            this.CameraControl_tab.ResumeLayout(false);
            this.Camera_control.ResumeLayout(false);
            this.Camera_control.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).EndInit();
            this.LogsBoxGroup.ResumeLayout(false);
            this.LogsBoxGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Settings;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.GroupBox GroupBoxDisplay;
        private System.Windows.Forms.PictureBox PreviewBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SelectDevicePreview;
        private System.Windows.Forms.Button StopPreview;
        private System.Windows.Forms.Button Preview;
        private System.Windows.Forms.GroupBox LogsBoxGroup;
        private System.Windows.Forms.TextBox logs_box_display;
        private System.Windows.Forms.Button btnRef;
        private System.Windows.Forms.TabControl CameraControl_tab;
        private System.Windows.Forms.TabPage Camera_control;
        private System.Windows.Forms.ComboBox ResolutionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.CheckBox CheckSync;
        private System.Windows.Forms.Button Apply_setprop;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox B_Auto;
        private System.Windows.Forms.Label label3;
    }
}

