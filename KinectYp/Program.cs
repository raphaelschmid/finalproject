using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectYp {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            staarteStreetFighter();
        }

        static void staarteStreetFighter()
        {
            Process.Start("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Games\\Street Fighter Alpha 2.lnk");
            System.Threading.Thread.Sleep(6000);
            MotionFunctions.SendAction(MotionFunctions.LKick());
            System.Threading.Thread.Sleep(2000);

            // select training
            MotionFunctions.SendAction("d2;50;u2;");
            System.Threading.Thread.Sleep(100);
            MotionFunctions.SendAction("d2;50;u2;");
            System.Threading.Thread.Sleep(100);
            MotionFunctions.SendAction(MotionFunctions.LKick());
            System.Threading.Thread.Sleep(1000);

            //ryu vs ryu
            MotionFunctions.SendAction(MotionFunctions.LKick());
            System.Threading.Thread.Sleep(50);
            MotionFunctions.SendAction(MotionFunctions.LKick());
            System.Threading.Thread.Sleep(1000);

            // start game

            for (int i = 0; i < 20; i++)
            {
                MotionFunctions.SendAction(MotionFunctions.LKick());
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
