using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Multi_cap_img
{
    public partial class Main : Form
    {

        public Settings settingsForm;

        public Main()
        {
            InitializeComponent();
            logs_box_display.ScrollBars = ScrollBars.Vertical;
        }

        private void Main_Load(object sender, EventArgs e)
        {

            // Create Thread
            Thread UDPserver = new Thread(new ThreadStart(ServerListen));
            UDPserver.Start();
            Global.isStartUDPServer = true;
            CameraControl_tab.Enabled = false;
        }
        private void Settings_Click(object sender, EventArgs e)
        {
            if (Global.isOpenFormSetting)
            {
                settingsForm.Focus();
            }
            else
            {
                settingsForm = new Settings();
                settingsForm.Show();
                Global.isOpenFormSetting = true;
                Console.WriteLine("OpenFormSetting Done");
            }
        }

        public void get_deviceCamera()
        {
            SelectDevicePreview.Items.Clear();
            foreach (int index in Global.SelectedDeviceList)
            {
                SelectDevicePreview.Items.Add(Global.Addlist_device_camera()[index]);
            }
        }

        public void logs_box(string DataLogs = "")
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(logs_box), new object[] { DataLogs });
                return;
            }
            string sent = string.Format("{0:HH:mm:ss tt} : ", DateTime.Now);
            if (DataLogs == "")
            {
                DataLogs = Global.logs_global;
            }
            sent += DataLogs;
            logs_box_display.AppendText(sent);
            logs_box_display.AppendText(Environment.NewLine);
        }

        void CaptureNewFramePreview(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap CapFrame = (Bitmap)eventArgs.Frame.Clone();
            PreviewBox.Image = CapFrame;
        }

        public void ServerListen()
        {
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5005);

            Socket newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram, ProtocolType.Udp);
            newsock.Bind(ipep);
            logs_box("Waiting for a client...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);

            while (true)
            {
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref Remote);
                string udp_data = Convert.ToString(Encoding.ASCII.GetString(data, 0, recv));
                logs_box(udp_data);
                UDP_controler(udp_data);
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
        }

        void UDP_controler(string udp_data)
        {
            string str = udp_data.ToUpper();
            switch (str)
            {
                case "P":
                    this.Invoke(new Action(() => {
                        Preview_start();
                    }));
                    break;
                case "!P":
                    this.Invoke(new Action(() => {
                        Preview_stop();
                    }));
                    break;
                default:
                    logs_box(Logs_txt.CMD_not_found);
                    break;
            }
        }

        void Preview_start()
        {
            if (Global.isPreview)
            {
                Global.CaptureDeviceFrame.Stop();
                Global.isPreview = false;
            }
            if (SelectDevicePreview.SelectedIndex != -1)
            {
                try
                {
                    Global.CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[SelectDevicePreview.SelectedIndex].MonikerString);
                    Global.CaptureDeviceFrame.NewFrame += new NewFrameEventHandler(CaptureNewFramePreview);
                    Global.CaptureDeviceFrame.VideoResolution = Global.CaptureDeviceFrame.VideoCapabilities[Global.ResolutionPreview_Previous];
                    Global.CaptureDeviceFrame.Start();
                    Global.isPreview = true;
                    logs_box(Logs_txt.preview_start);
                }
                catch
                {
                    logs_box(Logs_txt.preview_start_err);
                }
            }
            else
            {
                MessageBox.Show(Logs_txt.pleaseSelected_dvice);
            }
        }

        void Preview_stop()
        {
            try
            {
                Global.CaptureDeviceFrame.SignalToStop();
                Global.CaptureDeviceFrame.Stop();
            }
            catch
            {
                logs_box(Logs_txt.ErrSignalStop);
            }

        }

        private void Preview_Click(object sender, EventArgs e)
        {
            this.Preview_start();
        }

        private void StopPreview_Click(object sender, EventArgs e)
        {
            this.Preview_stop();
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            get_deviceCamera();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Update_resolution_box()
        {
            ResolutionBox.Items.Clear();
            for (int index = 0; index < Global.Resolution_List.Count; index++)
            {
                ResolutionBox.Items.Add(Global.Resolution_List[index]);
            }
            try
            {
                ResolutionBox.SelectedIndex = Global.ResolutionPreview_Previous;
            }
            catch
            {
                ResolutionBox.SelectedIndex = 0;
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            get_deviceCamera();
        }

        void CameraOptionsFocus_Update_Set()
        {
            if (Focus_Auto.Enabled && Focus_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Focus, (int)Focus_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Focus, (int)Focus_numericBox.Value, (int)CameraControlFlags.Manual);
        }

        void CameraOptionsPan_Update_Set()
        {
            if (Pan_Auto.Enabled && Pan_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Pan, (int)Pan_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Pan, (int)Pan_numericBox.Value, (int)CameraControlFlags.None);
        }
        void CameraOptionsTilt_Update_Set()
        {
            if (Tilt_Auto.Enabled && Tilt_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Tilt, (int)Tilt_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Tilt, (int)Tilt_numericBox.Value, (int)CameraControlFlags.Manual);
        }

        void CameraOptionsRoll_Update_Set()
        {
            if (Roll_Auto.Enabled && Roll_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Roll, (int)Roll_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Roll, (int)Roll_numericBox.Value, (int)CameraControlFlags.Manual);
        }

        void CameraOptionsZoom_Update_Set()
        {
            if (Zoom_Auto.Enabled && Zoom_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Zoom, (int)Zoom_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Zoom, (int)Zoom_numericBox.Value, (int)CameraControlFlags.Auto);
        }

        void CameraOptionsExposure_Update_Set()
        {
            if (Exposure_Auto.Enabled && Exposure_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Exposure, (int)Exposure_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Exposure, (int)Exposure_numericBox.Value, (int)CameraControlFlags.Manual);
        }

        void CameraOptionsIris_Update_Set()
        {
            if (Iris_Auto.Enabled && Iris_Auto.Checked)
                Global.CameraOption_set_Update((int)CameraControlProperty.Iris, (int)Iris_numericBox.Value, (int)CameraControlFlags.Auto);
            else
                Global.CameraOption_set_Update((int)CameraControlProperty.Iris, (int)Iris_numericBox.Value, (int)CameraControlFlags.Manual);
        }

        private void SelectDevicePreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Global.isPreview)
            {
                Global.CaptureDeviceFrame.Stop();
            }
            Global.get_CameraInfomations(SelectDevicePreview.SelectedIndex);
            Update_resolution_box();
        }

        private void Focus_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            Focus_numericBox.Value = Focus_TrackBar.Value;
            CameraOptionsFocus_Update_Set();
        }

        private void Focus_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Focus_TrackBar.Value = Convert.ToInt32(Focus_numericBox.Value);
        }

        private void Pan_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            Pan_numericBox.Value = Pan_TrackBar.Value;
            CameraOptionsPan_Update_Set();
        }

        private void Pan_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Pan_TrackBar.Value = Convert.ToInt32(Pan_numericBox.Value);
        }

        private void Tilt_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            Tilt_numericBox.Value = Tilt_TrackBar.Value;
            CameraOptionsTilt_Update_Set();
        }

        private void Tilt_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Tilt_TrackBar.Value = Convert.ToInt32(Tilt_numericBox.Value);
        }

        private void Roll_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Roll_TrackBar.Value = Convert.ToInt32(Roll_numericBox.Value);
            CameraOptionsRoll_Update_Set();
        }

        private void Roll_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            Roll_numericBox.Value = Roll_TrackBar.Value;
        }

        private void Zoom_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            Zoom_numericBox.Value = Zoom_TrackBar.Value;
            CameraOptionsZoom_Update_Set();
        }

        private void Zoom_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Zoom_TrackBar.Value = Convert.ToInt32(Zoom_numericBox.Value);
        }

        private void Exposure_trackBar_ValueChanged(object sender, EventArgs e)
        {
            Exposure_numericBox.Value = Exposure_trackBar.Value;
            CameraOptionsExposure_Update_Set();
        }

        private void Exposure_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Exposure_trackBar.Value = Convert.ToInt32(Exposure_numericBox.Value);
        }

        private void Iris_TrackBar_ValueChanged(object sender, EventArgs e)
        {
            Iris_numericBox.Value = Iris_TrackBar.Value;
            CameraOptionsTilt_Update_Set();
        }

        private void Iris_numericBox_ValueChanged(object sender, EventArgs e)
        {
            Iris_TrackBar.Value = Convert.ToInt32(Iris_numericBox.Value);
        }

        private void ResolutionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Global.isPreview)
            {
                Global.CaptureDeviceFrame.Stop();
            }
            Global.ResolutionPreview_Previous = ResolutionBox.SelectedIndex;

            if (ResolutionBox.SelectedIndex != -1)
            {
                CameraControl_tab.Enabled = true;
                get_CameraPropertyReange_set_FormCameraSetting();
            }
            //Preview_Click(null, null);
        }

        private void get_CameraPropertyReange_set_FormCameraSetting()
        {
            Focus_ctl_set();
            Tilt_ctl_set();
            Pan_ctl_set();
            Roll_ctl_set();
            Zoom_ctl_set();
            Exposure_ctl_set();
            Iris_ctl_set();
            All_ctl_set_val();
        }
        private void All_ctl_set_val()
        {
            Focus_TrackBar.Value = Convert.ToInt32(Global.focus_defaultValue);
            Pan_TrackBar.Value = Convert.ToInt32(Global.pan_defaultValue);
            Tilt_TrackBar.Value = Convert.ToInt32(Global.tilt_defaultValue);
            Roll_TrackBar.Value = Convert.ToInt32(Global.roll_defaultValue);
            Zoom_TrackBar.Value = Convert.ToInt32(Global.zoom_defaultValue);
            Iris_TrackBar.Value = Convert.ToInt32(Global.iris_defaultValue);
            Exposure_trackBar.Value = Convert.ToInt32(Global.exposure_defaultValue);
            Focus_numericBox.Value = Convert.ToInt32(Global.focus_defaultValue);
            Pan_numericBox.Value = Convert.ToInt32(Global.pan_defaultValue);
            Tilt_numericBox.Value = Convert.ToInt32(Global.tilt_defaultValue);
            Roll_numericBox.Value = Convert.ToInt32(Global.roll_defaultValue);
            Zoom_numericBox.Value = Convert.ToInt32(Global.zoom_defaultValue);
            Exposure_numericBox.Value = Convert.ToInt32(Global.exposure_defaultValue);
            Iris_numericBox.Value = Convert.ToInt32(Global.iris_defaultValue);
        }

        private void Focus_ctl_set()
        {
            Focus_numericBox.Maximum = Global.focus_maxValue;
            Focus_numericBox.Minimum = Global.focus_minValue;
            Focus_TrackBar.Maximum = Global.focus_maxValue;
            Focus_TrackBar.Minimum = Global.focus_minValue;
            if (Global.focus_CameraControlFlags == CameraControlFlags.None)
            {
                Focus_form_ctl_disable();
                Focus_Auto.Enabled = false;
            }
            else
            {
                Focus_form_ctl_enable();
                Focus_Auto.Enabled = true;
                Focus_Auto.Checked = true;
            }
        }

        private void Pan_ctl_set()
        {
            Pan_numericBox.Maximum = Global.pan_maxValue;
            Pan_numericBox.Minimum = Global.pan_minValue;
            Pan_TrackBar.Maximum = Global.pan_maxValue;
            Pan_TrackBar.Minimum = Global.pan_minValue;
            if (Global.pan_CameraControlFlags == CameraControlFlags.None)
            {
                Pan_form_ctl_disable();
                Pan_Auto.Enabled = false;
            }
            else
            {
                Pan_form_ctl_enable();
                Pan_Auto.Enabled = true;
                Pan_Auto.Checked = true;
            }
        }
        private void Tilt_ctl_set()
        {
            Tilt_numericBox.Maximum = Global.tilt_maxValue;
            Tilt_numericBox.Minimum = Global.tilt_minValue;
            Tilt_TrackBar.Maximum = Global.tilt_maxValue;
            Tilt_TrackBar.Minimum = Global.tilt_minValue;
            if (Global.tilt_CameraControlFlags == CameraControlFlags.None)
            {
                Tilt_form_ctl_disable();
                Tilt_Auto.Enabled = false;
            }
            else
            {
                Tilt_form_ctl_enable();
                Tilt_Auto.Enabled = true;
                Tilt_Auto.Checked = true;
            }
        }

        private void Roll_ctl_set()
        {
            Roll_numericBox.Maximum = Global.roll_maxValue;
            Roll_numericBox.Minimum = Global.roll_minValue;
            Roll_TrackBar.Maximum = Global.roll_maxValue;
            Roll_TrackBar.Minimum = Global.roll_minValue;
            if(Global.tilt_CameraControlFlags == CameraControlFlags.None)
            {
                Roll_form_ctl_disable();
                Roll_Auto.Enabled = false;
            }
            else
            {
                Roll_form_ctl_enable();
                Roll_Auto.Enabled = true;
                Roll_Auto.Checked = true;
            }
        }

        private void Zoom_ctl_set()
        {
            Zoom_numericBox.Maximum = Global.zoom_maxValue;
            Zoom_numericBox.Minimum = Global.zoom_minValue;
            Zoom_TrackBar.Maximum = Global.zoom_maxValue;
            Zoom_TrackBar.Minimum = Global.zoom_minValue;
            if (Global.zoom_CameraControlFlags == CameraControlFlags.None)
            {
                Zoom_form_ctl_disable();
                Zoom_Auto.Enabled = false;
            }
            else
            {
                Zoom_form_ctl_enable();
                Zoom_Auto.Enabled = false;
                Zoom_Auto.Checked = false;
            }
        }

        private void Exposure_ctl_set()
        {
            Exposure_numericBox.Maximum = Global.exposure_maxValue;
            Exposure_numericBox.Minimum = Global.exposure_minValue;
            Exposure_trackBar.Maximum = Global.exposure_maxValue;
            Exposure_trackBar.Minimum = Global.exposure_minValue;
            if(Global.exposure_CameraControlFlags == CameraControlFlags.None)
            {
                Exposure_form_ctl_disable();
                Exposure_Auto.Enabled = false;
            }
            else
            {
                Exposure_form_ctl_enable();
                Exposure_Auto.Enabled = true;
                Exposure_Auto.Checked = true;
            }
        }

        private void Iris_ctl_set()
        {
            Iris_numericBox.Maximum = Global.iris_maxValue;
            Iris_numericBox.Minimum = Global.iris_minValue;
            Iris_TrackBar.Maximum = Global.iris_maxValue;
            Iris_TrackBar.Minimum = Global.iris_minValue;
            if (Global.iris_CameraControlFlags == CameraControlFlags.None)
            {
                Iris_form_ctl_disable();
                Iris_Auto.Enabled = false;
            }
            else
            {
                Iris_form_ctl_enable();
                Iris_Auto.Enabled = true;
                Iris_Auto.Checked = true;
            }
        }

        private void Focus_form_ctl_disable() {
            Focus_label.Enabled = false;
            Focus_TrackBar.Enabled = false;
            Focus_numericBox.Enabled = false;
        }

        private void Focus_form_ctl_enable()
        {
            Focus_label.Enabled = true;
            Focus_TrackBar.Enabled = true;
            Focus_numericBox.Enabled = true;
        }

        private void Pan_form_ctl_disable()
        {
            Pan_label.Enabled = false;
            Pan_TrackBar.Enabled = false;
            Pan_numericBox.Enabled = false;
        }

        private void Pan_form_ctl_enable()
        {
            Pan_label.Enabled = true;
            Pan_TrackBar.Enabled = true;
            Pan_numericBox.Enabled = true;
        }

        private void Tilt_form_ctl_disable()
        {
            Tilt_label.Enabled = false;
            Tilt_TrackBar.Enabled = false;
            Tilt_numericBox.Enabled = false;
        }

        private void Tilt_form_ctl_enable()
        {
            Tilt_label.Enabled = true;
            Tilt_TrackBar.Enabled = true;
            Tilt_numericBox.Enabled = true;
        }

        private void Roll_form_ctl_disable()
        {
            Roll_label.Enabled = false;
            Roll_TrackBar.Enabled = false;
            Roll_numericBox.Enabled = false;
        }

        private void Roll_form_ctl_enable()
        {
            Roll_label.Enabled = true;
            Roll_TrackBar.Enabled = true;
            Roll_numericBox.Enabled = true;
        }

        private void Zoom_form_ctl_disable()
        {
            Zoom_label.Enabled = false;
            Zoom_TrackBar.Enabled = false;
            Zoom_numericBox.Enabled = false;

        }

        private void Zoom_form_ctl_enable()
        {
            Zoom_label.Enabled = true;
            Zoom_TrackBar.Enabled = true;
            Zoom_numericBox.Enabled = true;
        }

        private void Exposure_form_ctl_disable()
        {
            Exposure_label.Enabled = false;
            Exposure_trackBar.Enabled = false;
            Exposure_numericBox.Enabled = false;
        }

        private void Exposure_form_ctl_enable()
        {
            Exposure_label.Enabled = true;
            Exposure_trackBar.Enabled = true;
            Exposure_numericBox.Enabled = true;
        }

        private void Iris_form_ctl_disable()
        {
            Iris_label.Enabled = false;
            Iris_TrackBar.Enabled = false;
            Iris_numericBox.Enabled = false;
        }

        private void Iris_form_ctl_enable()
        {
            Iris_label.Enabled = true;
            Iris_TrackBar.Enabled = true;
            Iris_numericBox.Enabled = true;
        }

        private void Focus_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Focus_Auto.Checked ? (Action)Focus_form_ctl_disable: Focus_form_ctl_enable)();
            CameraOptionsFocus_Update_Set();
        }

        private void Pan_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Pan_Auto.Checked ? (Action)Pan_form_ctl_disable : Pan_form_ctl_enable)();
            CameraOptionsPan_Update_Set();
        }
        private void Tilt_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Tilt_Auto.Checked ? (Action)Tilt_form_ctl_disable : Tilt_form_ctl_enable)();
            CameraOptionsTilt_Update_Set();
        }

        private void Roll_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Roll_Auto.Checked ? (Action)Roll_form_ctl_disable : Roll_form_ctl_enable)();
            CameraOptionsRoll_Update_Set();
        }

        private void Zoom_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Zoom_Auto.Checked ? (Action)Zoom_form_ctl_disable : Zoom_form_ctl_enable)();
            CameraOptionsZoom_Update_Set();
        }

        private void Exposure_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Exposure_Auto.Checked ? (Action)Exposure_form_ctl_disable : Exposure_form_ctl_enable)();
            CameraOptionsExposure_Update_Set();
        }

        private void Iris_Auto_CheckedChanged(object sender, EventArgs e)
        {
            (Iris_Auto.Checked ? (Action)Iris_form_ctl_disable : Iris_form_ctl_enable)();
            CameraOptionsIris_Update_Set();
        }


        [Obsolete]
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (btnCapture.Text == "Capture")
            {
                btnCapture.Text = "StopCapture";
                if (Global.isPreview)
                {
                    Global.CaptureDeviceFrame.SignalToStop();
                    Global.isPreview = false;
                    StopPreview_Click(null, null);
                }
                CreateThrCameraCapture();
                CameraControl_tab.Enabled = false;
            }
            else
            {
                btnCapture.Text = "Capture";
                for (int index = 0; index < Global.CaptureDeviceFrame_pool.Count; index++)
                {
                    Global.CaptureDeviceFrame_pool[index].SignalToStop();
                    Global.Threads_pool[index].Abort();
                }
                Global.DirCameraThr.Clear();
                CameraControl_tab.Enabled = true;
            }
        }

        private void Apply_setprop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to change the setting for camera", "Setting Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Confirm_apply();
            }
        }

        void Confirm_apply()
        {
            if (!CheckSync.Checked)
            {
                Global.setResolution_List[SelectDevicePreview.SelectedIndex] = ResolutionBox.SelectedIndex;
            }
            else
            {
                for (int index = 0; index < Global.SelectedDeviceList.Count; index++)
                {
                    Global.setResolution_List[Global.SelectedDeviceList[index]] = ResolutionBox.SelectedIndex;
                    Global.setFocus_List[Global.SelectedDeviceList[index]] = Focus_TrackBar.Value;
                    Global.setPan_List[Global.SelectedDeviceList[index]] = Pan_TrackBar.Value;
                    Global.setTilt_List[Global.SelectedDeviceList[index]] = Tilt_TrackBar.Value;
                    Global.setRoll_List[Global.SelectedDeviceList[index]] = Roll_TrackBar.Value;
                    Global.setZoom_List[Global.SelectedDeviceList[index]] = Zoom_TrackBar.Value;
                    Global.setIris_List[Global.SelectedDeviceList[index]] = Iris_TrackBar.Value;
                    Global.setExposure_List[Global.SelectedDeviceList[index]] = Exposure_trackBar.Value;
                }
                AddOptions_list();
            }
        }

        void AddOptions_list()
        {
            Global.Option_list_sync.Clear();
            if (Focus_Auto.Enabled && Focus_Auto.Checked) {
                Global.Option_list_sync.Add((int)CameraControlProperty.Focus, (int)CameraControlFlags.Auto);
            } else {
                Global.Option_list_sync.Add((int)CameraControlProperty.Focus, (int)CameraControlFlags.Manual);
            }

            if (Pan_Auto.Enabled && Pan_Auto.Checked)
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Pan, (int)CameraControlFlags.Auto);
            }
            else
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Pan, (int)CameraControlFlags.Manual);
            }

            if (Tilt_Auto.Enabled && Tilt_Auto.Checked)
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Tilt, (int)CameraControlFlags.Auto);
            }
            else
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Tilt, (int)CameraControlFlags.Manual);
            }

            if (Roll_Auto.Enabled && Roll_Auto.Checked)
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Roll, (int)CameraControlFlags.Auto);
            }
            else
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Roll, (int)CameraControlFlags.Manual);
            }

            if(Zoom_Auto.Enabled && Zoom_Auto.Checked)
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Zoom, (int)CameraControlFlags.Auto);
            }
            else
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Zoom, (int)CameraControlFlags.Manual);
            }

            if(Exposure_Auto.Enabled && Exposure_Auto.Checked)
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Exposure, (int)CameraControlFlags.Auto);
            }
            else
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Exposure, (int)CameraControlFlags.Manual);
            }

            if (Iris_Auto.Enabled && Iris_Auto.Checked)
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Iris, (int)CameraControlFlags.Auto);
            }
            else
            {
                Global.Option_list_sync.Add((int)CameraControlProperty.Iris, (int)CameraControlFlags.Manual);
            }
        }

        [Obsolete]
        private void CreateThrCameraCapture()
        {
            for (int index = 0; index < Global.SelectedDeviceList.Count; index++)
            {
                int indexDevice = Global.SelectedDeviceList[index];
                Thread thread = new Thread(() => CameraCapture(index, indexDevice));
                thread.Name = "Camera_" + index.ToString();
                thread.IsBackground = true;
                thread.Start();
                Global.Threads_pool.Add(thread);
            }

            foreach (Thread thread in Global.Threads_pool)
            {
                thread.Join(1);
            }
        }

        void Create_dir_exists(string path_dir)
        {
            string filepath = Environment.CurrentDirectory;
            string MainPath = "ImagesPath"; // Your code goes here
            bool exists = System.IO.Directory.Exists(filepath + "/" + MainPath);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(filepath + "/" + MainPath);
                logs_box(Logs_txt.CreateMainDir + MainPath);
            }

            
            bool exists_thr = System.IO.Directory.Exists(filepath + "/" + MainPath + "/" + path_dir);
            if (!exists_thr)
            {
                System.IO.Directory.CreateDirectory(filepath + "/" + MainPath + "/" + path_dir);
                logs_box(Logs_txt.CreateSubDir + path_dir);
            }
        }

        void CaptureNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            string filepath = Environment.CurrentDirectory;
            string subPath = Global.DirCameraThr[sender.GetHashCode().ToString()];
            string mainPath = "ImagesPath";
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Create_dir_exists(subPath);
            string fileName = System.IO.Path.Combine(filepath + @"\" + mainPath + @"\"+ subPath, @"" + subPath + "_" + Global.DirCameraThr[subPath] + ".jpg");
            string fileName_conv = fileName.Replace(@"\\", @"\"); // Save file path bug wait fix
            bitmap.Save(fileName_conv, ImageFormat.Jpeg);
            try
            {
                Global.DirCameraThr[subPath] = (Convert.ToInt32(Global.DirCameraThr[subPath]) + 1).ToString();
            }
            catch { }
            bitmap.Dispose();
            logs_box(fileName_conv);
        }

        [Obsolete]
        void CameraCapture(int number_cramera, int indexDevice)
        {
            Global.Addlist_device_camera();
            VideoCaptureDevice CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[indexDevice].MonikerString);
            CaptureDeviceFrame.NewFrame += new NewFrameEventHandler(CaptureNewFrame);
            CameraCaptureSet(CaptureDeviceFrame, indexDevice);
            CaptureDeviceFrame.VideoResolution = CaptureDeviceFrame.VideoCapabilities[Convert.ToInt32(Global.setResolution_List[indexDevice])];
            Global.CaptureDeviceFrame_pool.Add(CaptureDeviceFrame);
            Thread theard = Thread.CurrentThread;
            string souce_device = CaptureDeviceFrame.Source;
            Global.DirCameraThr.Add(CaptureDeviceFrame.GetHashCode().ToString(), theard.Name);
            Global.DirCameraThr.Add(theard.Name, 0.ToString());
            CaptureDeviceFrame.Start();
            logs_box(Logs_txt.theard_start + number_cramera);
        }
        void CameraCaptureSet(VideoCaptureDevice CaptureDeviceFrame, int indexDevice)
        {
            Global.CameraOptions_Set(CaptureDeviceFrame, indexDevice);
        }
    }
}
