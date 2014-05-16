using System.Diagnostics;
using System.IO;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectYp {
    public partial class Form1 : Form
    {

        public static PositionTracker positionTracker;

        public Form1()
        {
            this.Show();
            this.Location = Screen.AllScreens[1].WorkingArea.Location;
            InitializeComponent();
            positionTracker = new PositionTracker();
            
            positionTracker.Init();
            
            starteStreetFighter();
            startJavaServer();
        }

        void startJavaServer()
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            //strCommand is path and file name of command to run
            pProcess.StartInfo.FileName = "C:\\Program Files\\Java\\jre7\\bin\\java.exe";

            //strCommandParameters are parameters to pass to program
            pProcess.StartInfo.Arguments = "-jar \"" +
                Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath))) + "\\UDPServer.jar\"";

            pProcess.StartInfo.UseShellExecute = false;

            //Set output of program to be written to process output stream
            pProcess.StartInfo.RedirectStandardOutput = true;

            //Start the process
            pProcess.Start();

        }

        static void starteStreetFighter()
        {
            Process.Start("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Games\\Street Fighter Alpha 2.lnk");
        }
        public void setlblNormal(string s)
        {
            lblNormal.Text = s;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
