﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

using System.IO;
using System.Configuration;


namespace InvasionController
{
    class Program
    {

        protected enum RssType : int
        {
            Food = 1, 
            Oil,
            Energy,
            Steel
        }

        protected enum RssTypeDPDT : int
        {            
            Oil = 1,
            Energy            
            
        }
        protected enum RssTypeDPLT : int
        {
            Steel = 1,
            Food
        }

        static void Main(string[] args)
        {

            Console.Title = "InvasionController";

            IntPtr d = WindowsApis.User32.FindWindow(null, "InvasionController");
            WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT pp = new WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT();

            WindowsApis.User32.GetWindowPlacement(d, ref pp);
            // 773 desk 993 laptop
            WindowsApis.User32.SetWindowPos(d.ToInt32(), -1, 993, 0, (pp.rcNormalPosition.right - pp.rcNormalPosition.left),
                pp.rcNormalPosition.bottom - pp.rcNormalPosition.top, 0x0040);
            
            //AutomationLib.AutomationControls.Control con = null;

            //AutomationLib.WindowsAdapter bs = new AutomationLib.WindowsAdapter("Nox");
            //bs.StartApplication(@"C:\Program Files (x86)\Nox\bin\Nox.exe");

            //bs.BuildControlList();

            //// main screen board
            //con = bs._ControlList[0].ControlList[2];

            //WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT pp = new WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT();

            //WindowsApis.User32.GetWindowPlacement(bs.MainHandle, ref pp);
            //WindowsApis.User32.SetWindowPos(bs.MainHandle.ToInt32(), -1, 0, 0, 511, 850, 0x0040);


            //IntPtr d = WindowsApis.User32.FindWindow("Qt5QWindowToolSaveBits", "Nox");
            //pp = new WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT();
            //WindowsApis.User32.GetWindowPlacement(d, ref pp);
            //WindowsApis.User32.SetWindowPos(d.ToInt32(), -1, 511, 35, (pp.rcNormalPosition.right - pp.rcNormalPosition.left),
            //    pp.rcNormalPosition.bottom - pp.rcNormalPosition.top, 0x0040);

            displayPercedntages();
            Reset(args);            
        }

        static void displayPercedntages()
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();
            TextReader rdr = new
            StreamReader(@"C:\Users\Jurm\Desktop\MouseScripts\Type.dat");
            string online = "";
            while ((online = rdr.ReadLine()) != null)
            {
                if (!dic.ContainsKey(online.Trim()))
                    dic.Add(online, 1);
                else
                    dic[online] = dic[online] + 1;
            }

            double total = dic.Sum(x => x.Value);
            Dictionary<string, double> dicRssPercent = new Dictionary<string, double>();
            foreach (RssType val in Enum.GetValues(typeof(RssType)))
            {
                if (!dicRssPercent.ContainsKey(val.ToString()))
                {
                    if(dic.ContainsKey(val.ToString()))
                        dicRssPercent.Add(val.ToString(), (dic[val.ToString()] / total) * 100);
                }
            }

            foreach (KeyValuePair<string, double> x in dicRssPercent)
            {
                Console.WriteLine(x.Key.ToString() + ": " +  x.Value.ToString("0.00") + "%");
            }

            //double en = (dic["Energy"] / total) * 100;
            //double oil = (dic["Oil"] / total) * 100;
            //double food = (dic["Food"] / total) * 100;
            //double st = (dic["Steel"] / total) * 100;
            //double yg = (dic["Yeggs"] / total) * 100;

            //Console.WriteLine("Energy: " + en.ToString("0.00") + "%");
            //Console.WriteLine("Oil: " + oil.ToString("0.00") + "%");
            //Console.WriteLine("Food: " + food.ToString("0.00") + "%");
            //Console.WriteLine("Steel: " + st.ToString("0.00") + "%");
            //Console.WriteLine("Yeggs: " + yg.ToString("0.00") + "%");
            Console.WriteLine("\n\n\n");

            rdr.Close();

            //Console.ReadKey();
        }

        static void Reset(string[] args)
        {
            try
            {

                string runType = ConfigurationManager.AppSettings["RuntType"].ToString();
                string processType = ConfigurationManager.AppSettings["ProcessType"].ToString();

                RunMouseClickerApp mgMC = null;

                TextWriter wtr = new StreamWriter(@"C:\Users\Jurm\Desktop\MouseScripts\Type.dat", true);

                string type = "";                
                type = "FULL";
                Boolean JomasRandomize = type.Equals("JOMASRANDOMIZE") ? true : false;
                Boolean Manuvs = type.Equals("MANUVS") ? true : false;
                Boolean reset = type.Equals("RESET") ? true : false;
                Boolean full = type.Equals("FULL") ? true : false;
                Boolean stop = type.Equals("STOP") ? true : false;
                Process[] ps = Process.GetProcesses();

                //Console.WriteLine(args[0]);
                //Console.ReadKey();

                foreach (Process p in ps)
                {
                    
                    if (p.ProcessName.Equals("AutoMouseClick"))
                    {
                        if (!p.HasExited)
                        {
                            Console.WriteLine("Killing " + p.ProcessName);
                            p.Kill();

                        }
                    }
                    if (p.ProcessName.Equals("Nox") && full)
                    {
                        if (!p.HasExited)
                        {
                            Console.WriteLine("Killing " + p.ProcessName);
                            p.Kill();
                        }
                    }
                }

                if (stop)
                    return;
                IntPtr handle = IntPtr.Zero;
                AutomationLib.AutomationControls.Control con = null;
                AutomationLib.AutomationControls.Controls.Button but = null;
                AutomationLib.WindowsAdapter bs = new AutomationLib.WindowsAdapter("Nox");

                if (full)
                {
                    Console.WriteLine("Starting Nox...");
                    //Thread.Sleep(60000);

                    
                    bs.StartApplication(@"C:\Program Files (x86)\Nox\bin\Nox.exe");
                    Thread.Sleep(60000);

                    Console.WriteLine("Starting Invasion...");
                    //mgMC = new RunMouseClickerApp("CloseTab", @"C:\Users\Jurm\Desktop\MouseScripts\BlueStacksSetUp.mamc");
                    //mgMC.Run(0, 3000, "BlueStacksSetUp.mamc - Auto Mouse Click");
                    AutomationLib.WindowsAdapter bsm = new AutomationLib.WindowsAdapter("StartInvasion");
                    bsm.StartApplication(@"C:\Users\Jurm\Desktop\MouseScripts\StartInvasion.mamc");
                    Thread.Sleep(5000);
                    //close.ShutDown();
                    IntPtr handle1 = WindowsApis.User32.FindWindow("MurGeeAutoMouseClick", "StartInvasion.mamc - Auto Mouse Click");
                    bsm.BuildControlList(handle1);
                    AutomationLib.AutomationControls.Control con1 = bsm.GetControl("S&tart");
                    AutomationLib.AutomationControls.Controls.Button but1 = new AutomationLib.AutomationControls.Controls.Button(con1.Handle);
                    but1.ClickButton();
                    Thread.Sleep(60000);
                    bsm.ShutDown();



                    // after invasion started, move and size
                    Console.WriteLine("Moving Nox");
                    bs.BuildControlList();

                    // main screen board
                    
                    WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT pp = new WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT();
                    WindowsApis.User32.GetWindowPlacement(bs.MainHandle, ref pp);
                    // 511 des 490 lap
                    WindowsApis.User32.SetWindowPos(bs.MainHandle.ToInt32(), -1, 0, 0, pp.rcNormalPosition.right - pp.rcNormalPosition.left,
                        pp.rcNormalPosition.bottom - pp.rcNormalPosition.top, 0x0040);

                    // move side bar
                    IntPtr d = WindowsApis.User32.FindWindow("Qt5QWindowToolSaveBits", "Nox");
                    WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT pp2 = new WindowsApis.Data.WindowsApiStructs.WINDOWPLACEMENT();
                    WindowsApis.User32.GetWindowPlacement(d, ref pp2);
                    WindowsApis.User32.SetWindowPos(d.ToInt32(), -1, pp.rcNormalPosition.right - pp.rcNormalPosition.left, pp2.rcNormalPosition.top, (pp2.rcNormalPosition.right - pp2.rcNormalPosition.left),
                        pp2.rcNormalPosition.bottom - pp2.rcNormalPosition.top, 0x0040);



                    Console.WriteLine("Logging out...");
                    AutomationLib.WindowsAdapter logout = new AutomationLib.WindowsAdapter("LogOutSlowReset");
                    logout.StartApplication(@"C:\Users\Jurm\Desktop\MouseScripts\Parts\LogOutSlowReset.mamc");
                    //close.ShutDown();
                    handle = WindowsApis.User32.FindWindow("MurGeeAutoMouseClick", "LogOutSlowReset.mamc - Auto Mouse Click");
                    logout.BuildControlList(handle);
                    con = logout.GetControl("S&tart");
                    but = new AutomationLib.AutomationControls.Controls.Button(con.Handle);
                    Thread.Sleep(5000);
                    but.ClickButton();
                    Thread.Sleep(120000);
                    logout.ShutDown();
                }






                if ((reset || full))
                {
                    //mgMC = new RunMouseClickerApp("Main", @"C:\Users\Jurm\Desktop\MouseScripts\Main.mamc");
                    //mgMC.Run(5000, 25000, "Main.mamc - Auto Mouse Click");
                    int start = 1;
                    int end = 5;                    
                    Type t = typeof(RssType);
                    if (processType == "D")
                    {
                        Console.WriteLine("Dual Processing Mode Active.");
                        if (Environment.MachineName == "JURM-OFFICEPC")
                        {
                            end = 3;
                            t = typeof(RssTypeDPDT);
                        }
                        else
                        {
                            end = 3;
                            t = typeof(RssTypeDPLT);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Single Processing Mode Active.");
                        
                    }

                    Random r = new Random((int)DateTime.Now.Millisecond);
                    int r2 = r.Next(start, end);
                    string rssType = Enum.Parse(t, r2.ToString()).ToString();

                    AutomationLib.WindowsAdapter main = new AutomationLib.WindowsAdapter("Main");
                    //close.ShutDown();
                    
                    string path = @"C:\Users\Jurm\Desktop\MouseScripts\Accounts\" ;

                    if (runType.ToUpper().Trim().Equals("T"))
                    {
                        path += @"_TradesOnly\";
                        Console.WriteLine("\n*** Trade Only *** \n");
                    }
                    else
                        Console.WriteLine("\n*** Full Processing *** \n");


                    

                    wtr.WriteLine(rssType);
                    wtr.Close();

                    Console.WriteLine("Starting "+ rssType);

                    string filename = "Process" + rssType + ".mamc";
                    //main.StartApplication(@"C:\Users\Jurm\Desktop\MouseScripts\MainTradeOnly.mamc");                
                    main.StartApplication(path + filename);

                    handle = WindowsApis.User32.FindWindow("MurGeeAutoMouseClick", filename + " - Auto Mouse Click");
                    //handle = WindowsApis.User32.FindWindow("MurGeeAutoMouseClick", "MainTradeOnly.mamc - Auto Mouse Click");
                    main.BuildControlList(handle);
                    con = main.GetControl("S&tart");
                    but = new AutomationLib.AutomationControls.Controls.Button(con.Handle);
                    but.ClickButton();
                }
                
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }

            Thread.Sleep(5000);
        }
    }
}
