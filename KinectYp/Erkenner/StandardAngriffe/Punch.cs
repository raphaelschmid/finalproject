using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp
{
    class Punch : ISinglePressErkenner
    {
        public Punch()
        {
            Blocked = false;
            BlockDuration = 200;
            SingeKeyPressKeys = MotionFunctions.MPunch();
        }

        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var rightHandZ    = history.Select(x => x.Joints[JointType.HandRight].Position.Z);
            var rightHandY    = history.Select(x => x.Joints[JointType.HandRight].Position.Y);
            var leftShoulderY = history.Select(x => x.Joints[JointType.ShoulderLeft].Position.Y);

            Ok = (rightHandZ.First() < rightHandZ.Max() - 0.27) && (rightHandY.First() < leftShoulderY.First() + 0.1);

            return ErkennerHandler.SinglePress(this);
        }

        public string GetDebugName()
        {
            return "Punch!";
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        public string SingeKeyPressKeys { get; private set; }
    }
}
