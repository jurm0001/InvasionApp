using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;



namespace InvasionController
{
    public class RunMouseClickerApp
    {
        private string exe;
        AutomationLib.WindowsAdapter bsm = null;
        public RunMouseClickerApp(string name, string exe)
        {
            this.exe = exe;
            bsm = new AutomationLib.WindowsAdapter(name);
        }

        public void Run(int SleepBeforeButton, int SleepAfterRun, string windowName)
        {
            bsm.StartApplication(this.exe);
            //close.ShutDown();
            IntPtr handle1 = WindowsApis.User32.FindWindow("MurGeeAutoMouseClick", windowName);
            bsm.BuildControlList(handle1);
            AutomationLib.AutomationControls.Control con1 = bsm.GetControl("S&tart");
            AutomationLib.AutomationControls.Controls.Button but1 = new AutomationLib.AutomationControls.Controls.Button(con1.Handle);
            Thread.Sleep(SleepAfterRun);
            but1.ClickButton();
            Thread.Sleep(SleepBeforeButton);
            bsm.ShutDown();
        }
    }
}
