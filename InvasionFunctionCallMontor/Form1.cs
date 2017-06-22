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
            timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 2000;
            timer.Enabled = true;
            
            timer.Start();

        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            Process[] ps = Process.GetProcesses();
            Console.Write(ps[1].StartInfo.FileName);
            Process[] mcp;
            try
            {
                //OrderByDescending(c => c.StartTime).
                mcp = (from x in ps where x.MainModule.ModuleName.Contains("MouseScripts") select x).ToArray();
            }
            catch { timer.Interval = 2000; return; }
            Console.WriteLine(mcp.Length.ToString());
            listBox1.Items.Clear();
            foreach (Process p in ps)
                //if (p.MainModule.ModuleName.Contains("AutoMouseClick"))
                   listBox1.Items.Add(p.ProcessName);
            timer.Start();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
