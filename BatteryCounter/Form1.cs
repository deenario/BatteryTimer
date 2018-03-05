using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitTimer();
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // in miliseconds
            timer1.Start();
        }

        int min = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;

            if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
            {

                label1.Text = "Laptop is on Charge";
                label2.Visible = false;
                label3.Visible = false;
                //TextWriter tw = new StreamWriter("BatteryTimer.txt");
                //// write lines of text to the file
                //tw.WriteLine("0");
                //// close the strea
                //tw.Close();
                label3.Text = "0";

            }

            else
            {

                label1.Text = "Laptop is on Battery For";
                label2.Visible = true;
                label3.Visible = true;
                incrementTime();
                if (i == 60000)
                {
                    min += 1;
                    label3.Text = min.ToString();
                    notifyIcon1.BalloonTipText = min.ToString();
                    i = 0;
                }
                
            }
        }


        void incrementTime()
        {
            i += 1000;
        }

        int i = 0;

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            TextWriter tw = new StreamWriter("BatteryTimer.txt",true);
            // write lines of text to the file
            tw.WriteLine(min.ToString());
            // close the stream
            tw.Close();
        }
        

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}