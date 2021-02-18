﻿using System;
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
            // get_deviceCamera();

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

            try
            {
                Global.CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[SelectDevicePreview.SelectedIndex].MonikerString);
                Global.CaptureDeviceFrame.NewFrame += new NewFrameEventHandler(CaptureNewFramePreview);
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

        private void SelectDevicePreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Global.isPreview)
            {
                Global.CaptureDeviceFrame.Stop();
            }
            Global.get_resolution(SelectDevicePreview.SelectedIndex);
            Update_resolution_box();
            Preview_Click(null, null);
        }

        private void ResolutionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.ResolutionPreview_Previous = ResolutionBox.SelectedIndex;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (btnCapture.Text == "Capture")
            {
                btnCapture.Text = "StopCapture";
                if (Global.isPreview)
                {
                    Global.CaptureDeviceFrame.Stop();
                    Global.isPreview = false;
                }
                CreateThrCameraCapture();
            }
            else
            {
                btnCapture.Text = "Capture";
                Global.DirCameraThr.Clear();
            }
        }

        private void CreateThrCameraCapture()
        {
            for (int index = 0; index < Global.SelectedDeviceList.Count; index++)
            {
                int indexDevice = Global.SelectedDeviceList[index];
                Thread thread = new Thread(() => CameraCapture(index, indexDevice));
                thread.Name = "Camera_" + index.ToString();
                thread.Start();
                //logs_box("indexDevice" + Global.SelectedDeviceList[index]);
            }
        }

        void Create_dir_exists(string path_dir)
        {
            string filepath = Environment.CurrentDirectory;
            string subPath = "ImagesPath"; // Your code goes here
            bool exists = System.IO.Directory.Exists(filepath + "/" + subPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(filepath + "/" + subPath);
            
            bool exists_thr = System.IO.Directory.Exists(filepath + "/" + subPath + "/" + path_dir);
            if (!exists_thr)
                System.IO.Directory.CreateDirectory(filepath + "/" + subPath + "/" + path_dir);
        }

        void CaptureNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Console.WriteLine(sender.GetHashCode());
            //sender.AForge.Video.DirectShow.VideoCaptureDevice
            //Console.WriteLine(sender.ToString());
            string filepath = Environment.CurrentDirectory;
            string subPath = Global.DirCameraThr[sender.GetHashCode().ToString()];
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Create_dir_exists(subPath);
            string fileName = System.IO.Path.Combine(filepath + @"\" + subPath, @"" + subPath + "_" + Global.DirCameraThr[subPath] + ".jpg");
            string fileName_conv = fileName.Replace(@"\\", @"\"); // Save file path bug wait fix
            bitmap.Save(fileName_conv, ImageFormat.Jpeg);
            Global.DirCameraThr[subPath] = (Convert.ToInt32(Global.DirCameraThr[subPath]) + 1).ToString();
            bitmap.Dispose();
            //string fileName = System.IO.Path.Combine(filepath, @"name.bmp");
        }

        //Task_saveImage
        void CameraCapture(int number_cramera, int indexDevice)
        {
            Global.Addlist_device_camera();
            VideoCaptureDevice CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[indexDevice].MonikerString);
            CaptureDeviceFrame.NewFrame += new NewFrameEventHandler(CaptureNewFrame);
            Thread theard = Thread.CurrentThread;
            //Console.WriteLine("Thread Name: {0}", CaptureDeviceFrame.GetHashCode());
            string souce_device = CaptureDeviceFrame.Source;
            Global.DirCameraThr.Add(CaptureDeviceFrame.GetHashCode().ToString(), theard.Name);
            Global.DirCameraThr.Add(theard.Name, 0.ToString());
            CaptureDeviceFrame.Start();
            logs_box(Logs_txt.theard_start + number_cramera);
        }
    }
}
