using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceRest_051_MuhammadRifqiFirmansyah;

namespace Server_051
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceHost hostObj = null;
            try
            {
                hostObj = new ServiceHost(typeof(TI_UMY));
                hostObj.Open();
                label2.Text = "Server ON";
                label3.Text = " Klik OFF untuk mematikan server";
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                hostObj = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceHost hostObj = null;
            try
            {
                hostObj = new ServiceHost(typeof(TI_UMY));
                hostObj.Close();
                label2.Text = "Server OFF";
                label3.Text = " Klik ON untuk menjalankan server";
                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                hostObj = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Server OFF";
            label3.Text = " Klik ON untuk menjalankan server";
            button1.Enabled = true;
            button2.Enabled = false;
        }

    }
}
