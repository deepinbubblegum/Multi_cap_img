using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

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
            Global.GetDevice_Camera();
            foreach (FilterInfo Item in Global.cameraDeviec)
            {
                CheckCameraList.Items.Add(Item.Name);
            }
        }
    }
}
