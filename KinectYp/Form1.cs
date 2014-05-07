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
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            PositionTracker positionTracker = new PositionTracker();
            try {
                positionTracker.Init();
            }
            catch (Exception e) {
                lblError.Text = e.Message;
            }
            positionTracker.Punched += Punched;
            positionTracker.Stay += Stay;
            positionTracker.PositionChanged += PositionChanged;
            lblKick.Text = "normal";


        }

        private void Stay(object sender)
        {
            setlblKick("normal");
        }

        private void PositionChanged(object sender, Skeleton s) {

            SkeletonPoint foot = s.Joints[JointType.FootRight].Position;
            SkeletonPoint head = s.Joints[JointType.Head].Position;
            lblFootPosition.Text = string.Format("X-Position: {0}{3}Y-Position: {1}{3}Z-Position: {2}", foot.X, foot.Y, foot.Z, Environment.NewLine);
            lblHeadPosition.Text = string.Format("X-Position: {0}{3}Y-Position: {1}{3}Z-Position: {2}{3}Delta head-foot: {4}", head.X, head.Y, head.Z, Environment.NewLine, head.Z-foot.Z);
        }

        public void Punched(object sender, string message)
        {
                setlblKick(message);
        }

        private void setlblKick(string s)
        {
            lblKick.Text = s;
        }

        private void lblKick_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
   
    }
}
