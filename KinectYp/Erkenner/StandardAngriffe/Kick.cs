using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.Kinect;
using System.Diagnostics;

namespace KinectYp
{
    class Kick : ISinglePressErkenner
    {
        public Kick()
        {
            Blocked = false;
            BlockDuration = 200;
            SingeKeyPressKeys = MotionFunctions.MKick();
        }

        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var rightFootZ = history.Select(x => x.Joints[JointType.FootRight].Position.Z);
            var rightFootY = history.Select(x => x.Joints[JointType.FootRight].Position.Y);

            Ok = (rightFootZ.Max() > rightFootZ.First() + 0.5) && (rightFootY.First() > rightFootY.Min() + 0.2);

            return ErkennerHandler.SinglePress(this);
        }

        public string GetDebugName()
        {
            return "Kick";
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        public string SingeKeyPressKeys { get; private set; }
    }
}
