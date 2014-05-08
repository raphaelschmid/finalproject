using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.Bewegungen
{
    internal class Jump : ISinglePressErkenner
    {
         public Jump()
        {
            Blocked = false;
            BlockDuration = 200;
            SingeKeyPressKeys = MotionFunctions.Up();
        }

        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var leftFootY = history.Select(x => x.Joints[JointType.FootRight].Position.Z);
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
