using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static int ResolutionPreview_Previous = 0;
        public static Dictionary<string, string> DirCameraThr = new Dictionary<string, string>();
        public static List<Thread> Threads_pool = new List<Thread>();
        public static List<VideoCaptureDevice> CaptureDeviceFrame_pool = new List<VideoCaptureDevice>();
        public static Dictionary<int, int> Option_list_sync = new Dictionary<int, int>();

        public static Dictionary<int, int> setResolution_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setFocus_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setPan_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setTilt_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setRoll_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setZoom_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setIris_List = new Dictionary<int, int>();
        public static Dictionary<int, int> setExposure_List = new Dictionary<int, int>();

        public static int[,,] setOptions_sync_List = { };
        public static Dictionary<int, int> setOption_menu = new Dictionary<int, int>();
        public static Dictionary<int, int> set_option_mORa = new Dictionary<int, int>();

        // Option focus value
        public static int focus_minValue = -255;
        public static int focus_maxValue = 255;
        public static int focus_stepSize = 0;
        public static int focus_defaultValue = 0;
        public static CameraControlFlags focus_CameraControlFlags;

        // Option pan value
        public static int pan_minValue = -255;
        public static int pan_maxValue = 255;
        public static int pan_stepSize = 0;
        public static int pan_defaultValue = 0;
        public static CameraControlFlags pan_CameraControlFlags;

        // Option pan value
        public static int tilt_minValue = -255;
        public static int tilt_maxValue = 255;
        public static int tilt_stepSize = 0;
        public static int tilt_defaultValue = 0;
        public static CameraControlFlags tilt_CameraControlFlags;

        // Option roll value
        public static int roll_minValue = -255;
        public static int roll_maxValue = 255;
        public static int roll_stepSize = 0;
        public static int roll_defaultValue = 0;
        public static CameraControlFlags roll_CameraControlFlags;

        // Option zoom value
        public static int zoom_minValue = -255;
        public static int zoom_maxValue = 255;
        public static int zoom_stepSize = 0;
        public static int zoom_defaultValue = 0;
        public static CameraControlFlags zoom_CameraControlFlags;

        // Option exposure value
        public static int exposure_minValue = -255;
        public static int exposure_maxValue = 255;
        public static int exposure_stepSize = 0;
        public static int exposure_defaultValue = 0;
        public static CameraControlFlags exposure_CameraControlFlags;

        // Option iris value
        public static int iris_minValue = -255;
        public static int iris_maxValue = 255;
        public static int iris_stepSize = 0;
        public static int iris_defaultValue = 0;
        public static CameraControlFlags iris_CameraControlFlags;

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

        public static void get_CameraInfomations(int index_select)
        {
            Global.CaptureDeviceFrame = new VideoCaptureDevice(Global.cameraDeviec[index_select].MonikerString);
            int VideoCapabilitie_length = Global.CaptureDeviceFrame.VideoCapabilities.Length;
            Resolution_List.Clear();
            for (int index = 0; index < VideoCapabilitie_length; index++)
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
                //Console.WriteLine(str_concat);
            }

            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Focus, out focus_minValue, out focus_maxValue, out focus_stepSize, out focus_defaultValue, out focus_CameraControlFlags);
            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Pan, out pan_minValue, out pan_maxValue, out pan_stepSize, out pan_defaultValue, out pan_CameraControlFlags);
            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Tilt, out tilt_minValue, out tilt_maxValue, out tilt_stepSize, out tilt_defaultValue, out tilt_CameraControlFlags);
            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Roll, out roll_minValue, out roll_maxValue, out roll_stepSize, out roll_defaultValue, out roll_CameraControlFlags);
            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Zoom, out zoom_minValue, out zoom_maxValue, out zoom_stepSize, out zoom_defaultValue, out zoom_CameraControlFlags);
            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Exposure, out exposure_minValue, out exposure_maxValue, out exposure_stepSize, out exposure_defaultValue, out exposure_CameraControlFlags);
            CaptureDeviceFrame.GetCameraPropertyRange(CameraControlProperty.Iris, out iris_minValue, out iris_maxValue, out iris_stepSize, out iris_defaultValue, out iris_CameraControlFlags);
            Console.WriteLine("GetCameraPropertyRange");
        }

        public static void CameraOption_set_Update(int Option_menu, int value, int option_mORa)
        {
            //Focus
            if ((Option_menu == (int)CameraControlProperty.Focus) && (option_mORa == (int)CameraControlFlags.Auto))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Focus, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Focus) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Focus, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Focus) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Focus, value, CameraControlFlags.None);
            }

            //Pan
            if ((Option_menu == (int)CameraControlProperty.Pan) && (option_mORa == (int)CameraControlFlags.Auto))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Pan, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Pan) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Pan, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Pan) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Pan, value, CameraControlFlags.None);
            }

            //Tilt
            if ((Option_menu == (int)CameraControlProperty.Tilt) && (option_mORa == (int)CameraControlFlags.Auto))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Tilt, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Tilt) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Tilt, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Tilt) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Tilt, value, CameraControlFlags.None);
            }

            //Roll
            if ((Option_menu == (int)CameraControlProperty.Roll) && (option_mORa == (int)CameraControlFlags.Auto))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Roll, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Roll) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Roll, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Roll) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Roll, value, CameraControlFlags.None);
            }

            //Zoom
            if ((Option_menu == (int)CameraControlProperty.Zoom) && (option_mORa == (int)CameraControlFlags.Auto))
            { 
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Zoom, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Zoom) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Zoom, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Zoom) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Zoom, value, CameraControlFlags.None);
            }

            //Exposure
            if ((Option_menu == (int)CameraControlProperty.Exposure) && (option_mORa == (int)CameraControlFlags.Auto))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Exposure, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Exposure) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Exposure, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Exposure) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Exposure, value, CameraControlFlags.None);
            }

            //Iris
            if ((Option_menu == (int)CameraControlProperty.Iris) && (option_mORa == (int)CameraControlFlags.Auto))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Iris, value, CameraControlFlags.Auto);
            }
            else if ((Option_menu == (int)CameraControlProperty.Iris) && (option_mORa == (int)CameraControlFlags.Manual))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Iris, value, CameraControlFlags.Manual);
            }
            else if ((Option_menu == (int)CameraControlProperty.Iris) && (option_mORa == (int)CameraControlFlags.None))
            {
                CaptureDeviceFrame.SetCameraProperty(CameraControlProperty.Iris, value, CameraControlFlags.None);
            }
        }

        public static void CameraOptions_Set(VideoCaptureDevice CaptureDeviceFrame_cap, int number_cramera)
        {
            int value, option_mORa;
            //Focus
            option_mORa = Option_list_sync[(int)CameraControlProperty.Focus];
            value = setFocus_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Focus, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Focus, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Focus, value, CameraControlFlags.None);
            }

            //Pan
            option_mORa = Option_list_sync[(int)CameraControlProperty.Pan];
            value = setPan_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Pan, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Pan, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Pan, value, CameraControlFlags.None);
            }

            //Tilt
            option_mORa = Option_list_sync[(int)CameraControlProperty.Tilt];
            value = setTilt_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Tilt, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Tilt, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Tilt, value, CameraControlFlags.None);
            }

            //Roll
            option_mORa = Option_list_sync[(int)CameraControlProperty.Roll];
            value = setRoll_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Roll, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Roll, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Roll, value, CameraControlFlags.None);
            }

            //Zoom
            option_mORa = Option_list_sync[(int)CameraControlProperty.Zoom];
            value = setZoom_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Zoom, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Zoom, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Zoom, value, CameraControlFlags.None);
            }

            //Exposure
            option_mORa = Option_list_sync[(int)CameraControlProperty.Exposure];
            value = setExposure_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Exposure, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Exposure, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Exposure, value, CameraControlFlags.None);
            }

            //Iris
            option_mORa = Option_list_sync[(int)CameraControlProperty.Iris];
            value = setIris_List[number_cramera];
            if (option_mORa == (int)CameraControlFlags.Auto)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Iris, value, CameraControlFlags.Auto);
            }
            else if (option_mORa == (int)CameraControlFlags.Manual)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Iris, value, CameraControlFlags.Manual);
            }
            else if (option_mORa == (int)CameraControlFlags.None)
            {
                CaptureDeviceFrame_cap.SetCameraProperty(CameraControlProperty.Iris, value, CameraControlFlags.None);
            }
        }
    } 
}
