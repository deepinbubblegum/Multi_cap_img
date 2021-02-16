using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Multi_cap_img
{
    static class Global
    {
        public static bool isOpenFormSetting = false;
        public static bool isPreview = false;
        public static FilterInfoCollection cameraDeviec;
        public static VideoCaptureDevice CaptureDeviceFrame;
        public static bool isStartUDPServer = false;
        public static string logs_global = "";
        public static List<string> DevicesList = new List<string>();
        public static List<int> SelectedDeviceList = new List<int>();
        public static List<string> Resolution_List = new List<string>();

        private static void GetDevice_Camera()
        {
            cameraDeviec = null;
            cameraDeviec = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public static List<string> Addlist_device_camera()
        {
            int counting = 1;
            string previous = "";
            // SelectDevicePreview.Items.Clear();
            DevicesList.Clear();
            GetDevice_Camera();
            foreach (FilterInfo Item in Global.cameraDeviec)
            {
                if (previous == Item.Name)
                {
                    counting += 1;
                    DevicesList.Add(Item.Name + "_" + Convert.ToString(counting));
                   // SelectDevicePreview.Items.Add(Item.Name + "_" + Convert.ToString(counting));
                }
                else
                {
                    DevicesList.Add(Item.Name);
                   // SelectDevicePreview.Items.Add(Item.Name);
                }
                previous = Item.Name;
            }
            return DevicesList;
        }

        public static void get_resolution(int index_select)
        {
            Global.CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[index_select].MonikerString);
            int VideoCapabilitie_length = Global.CaptureDeviceFrame.VideoCapabilities.Length;
            for(int index = 0; index < VideoCapabilitie_length; index++)
            {
                //string resolution = "Resolution Number " + Convert.ToString(index);
                //string resolution_size = CaptureDeviceFrame.VideoCapabilities[index].FrameSize.ToString();
                //string fps_Device = CaptureDeviceFrame.VideoCapabilities[index].MaximumFrameRate.ToString();
                string Width_resolution = CaptureDeviceFrame.VideoCapabilities[index].FrameSize.Width.ToString();
                string Height_resolution = CaptureDeviceFrame.VideoCapabilities[index].FrameSize.Height.ToString();
                string fps_Device = CaptureDeviceFrame.VideoCapabilities[index].MaximumFrameRate.ToString();
                //Console.WriteLine("resolution , resolution_size>> " + resolution + " " + resolution_size + " " + fps_Device);
                string str_concat = Width_resolution + " X " + Height_resolution + " fps(" + fps_Device + ")";
                Resolution_List.Add(str_concat);
                Console.WriteLine(str_concat);
            }
        }
    } 
}
