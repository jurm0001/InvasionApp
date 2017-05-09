using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Diagnostics;

namespace InvasionFunctionCallMontor
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;
        public Form1()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = 2000;
            timer.Start();

        }
        private void OnTimedEvent(Object source, EventArgs e)
        {
            //timer.Interval =0;
            Process[] ps = Process.GetProcesses();
            Console.Write(ps[1].StartInfo.FileName);
            Process[] mcp = (from x in ps where x.MainModule.ModuleName.Contains("MouseScripts") select x).OrderByDescending(c => c.StartTime).ToArray();
            Console.WriteLine(mcp.Length.ToString());
            //timer.Interval = 2000;
        }
    }
}
