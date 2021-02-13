using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using valGlobal = Multi_cap_img.Global;

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
            valGlobal.isOpenFormSetting = false;
            valGlobal.isHideMainForm = false;
        }
    }
}
