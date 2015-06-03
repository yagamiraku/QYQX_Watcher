using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace dr7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            notifyIcon1.Click += new System.EventHandler(notifyIcon1_Click);
            this.notifyIcon1.ShowBalloonTip(3, "提示", "老司机监控仪已启动", ToolTipIcon.Info);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string webpage = GetWebPageResponse("http://www.douyutv.com/7");
            if (webpage.Contains("\"show_status\":1,"))
            {
                this.notifyIcon1.ShowBalloonTip(3, "提示", "野生的老司机出现了", ToolTipIcon.Info);
                System.Media.SystemSounds.Question.Play();
            }
        }
        static string GetWebPageResponse(string uriArg)
        {
            Stream responseStream = WebRequest.Create(uriArg).GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            return reader.ReadToEnd();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }
    }
}
