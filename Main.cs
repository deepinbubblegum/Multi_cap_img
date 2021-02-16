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
            get_deviceCamera();

            // Create Thread
            Thread UDPserver = new Thread(new ThreadStart(ServerListen));
            UDPserver.Start();
            Global.isStartUDPServer = true;
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
            foreach (string item in Global.Addlist_device_camera())
            {
                SelectDevicePreview.Items.Add(item);
            }
            SelectDevicePreview.SelectedIndex = 0;
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

            try
            {
                Global.CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[SelectDevicePreview.SelectedIndex].MonikerString);
                Global.CaptureDeviceFrame.NewFrame += new NewFrameEventHandler(CaptureNewFrame);
                Global.CaptureDeviceFrame.Start();
                Global.isPreview = true;
                logs_box(Logs_txt.preview_start);
            }
            catch
            {
                logs_box(Logs_txt.preview_start_err);
            }
        }

        void Preview_stop()
        {
            Global.CaptureDeviceFrame.Stop();
        }

        void CaptureNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap CapFrame = (Bitmap)eventArgs.Frame.Clone();
            PreviewBox.Image = CapFrame;
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
    }
}
