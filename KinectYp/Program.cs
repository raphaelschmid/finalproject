using System;
using System.Windows.Forms;

namespace KinectYp {
    static class Program
    {
        public static Form1 Form1;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 = new Form1();
            Application.Run(Form1);
        }


    }
}
