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
using valGlobal = Multi_cap_img.Global;

namespace Multi_cap_img
{
    public partial class Main : Form
    {
        Settings settingsForm = new Settings();
        public Main()
        {
            InitializeComponent();
        }

        public static void Thread_FormManagement()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                // Yield the rest of the time slice.
                Thread.Sleep(1);
            }
        }

        public static void Main_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Thread_FormManagement));
            t.Start();
            t.Join();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if (valGlobal.isOpenFormSetting)
            {
                settingsForm.Focus();
            }
            else
            {
                this.Hide();
                settingsForm.Show();
                valGlobal.isOpenFormSetting = true;
                valGlobal.isHideMainForm  = true;
                Console.WriteLine("OpenFormSetting Done");
            }
        }


    }
}
