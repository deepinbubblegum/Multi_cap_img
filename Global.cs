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
        public static List<string> SelectDeviceList = new List<string>();

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
    }

}
