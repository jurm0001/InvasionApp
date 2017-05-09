using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace NoxPoller
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                IntPtr handle = WindowsApis.User32.FindWindow("#32770", "Nox App Player");
                if (handle != IntPtr.Zero)
                {
                    AutomationLib.AutomationControls.Control con = null;
                    AutomationLib.AutomationControls.Controls.Button but = null;
                    AutomationLib.WindowsAdapter main = new AutomationLib.WindowsAdapter("nox");
                    main.BuildControlList(handle);
                    con = main.GetControl("&Close program");
                    but = new AutomationLib.AutomationControls.Controls.Button(con.Handle);
                    but.ClickButton();

                    // perform reset
                    Process[] ps = Process.GetProcesses();

                    //Console.WriteLine(args[0]);
                    //Console.ReadKey();
                    bool running = false;
                    foreach (Process p in ps)
                    {
                        if (p.ProcessName.Equals("InvasionController.exe"))
                        {
                            running = false;
                        }
                        
                    }
                    if(!running)
                        Process.Start(@"C:\Users\Jurm\Desktop\MouseScripts\Outlook\InvasionController.exe", "inv FULL");
                    

                }
                else
                {
                    // check if nox is running
                    // if no perfor rest
                    Process.Start(@"C:\Users\Jurm\Desktop\MouseScripts\Outlook\InvasionController.exe", "inv FULL");
                }

                Thread.Sleep(15000);
            }
        }
    }
}
