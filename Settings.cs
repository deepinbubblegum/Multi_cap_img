using System;
using System.Web;
using System.Windows.Forms;

namespace Multi_cap_img
{
    public partial class Settings : Form
    {

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.isOpenFormSetting = false;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            foreach (string Item in Global.Addlist_device_camera())
            {
                CheckCameraList.Items.Add(Item);
            }
        }

        private void Close_setting_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            // Get Device Selected in CheckBoxList
            Global.SelectedDeviceList.Clear();
            for (int index = 0; index < CheckCameraList.Items.Count; index++)
            {
                if (CheckCameraList.GetItemChecked(index))
                {
                    Global.SelectedDeviceList.Add(index);
                    Console.WriteLine(index.ToString());
                }
            }
            this.Close();
        }
    }
}
