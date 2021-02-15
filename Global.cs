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

        public static void GetDevice_Camera()
        {
            cameraDeviec = null;
            cameraDeviec = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }
    }

}
