using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KillAutoMouseClick
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] ps = Process.GetProcesses();
            //Console.SetWindowPosition(1, 1);
            //Console.WriteLine(args[0]);
            //Console.ReadKey();
            try
            {
                foreach (Process p in ps)
                {
                    
                    if (p.ProcessName.Equals("AutoMouseClick"))
                    {
                        if (!p.HasExited)
                        {
                            Console.WriteLine("Killing all...");
                            p.Kill();
                        }
                    }
                }
            }
            catch { }
        }
    }
}
