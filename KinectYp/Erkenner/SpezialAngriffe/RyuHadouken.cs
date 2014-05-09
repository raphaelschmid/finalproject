using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.SpezialAngriffe
{
    class RyuHadouken : ISinglePressErkenner
    {
        public RyuHadouken()
        {
            Blocked = false;
            BlockDuration = 800;
            SingeKeyPressKeys = MotionFunctions.Qfc() + MotionFunctions.MPunch();
        }
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var rightHandZ = history.Select(x => x.Joints[JointType.HandRight].Position.Z);
            var leftHandZ = history.Select(x => x.Joints[JointType.HandLeft].Position.Z);

            Ok = (rightHandZ.Max() - rightHandZ.First()  > Paramters.rhWieWeitNachVorneMitDenHaenden)
                 && (leftHandZ.Max() - leftHandZ.First() > Paramters.rhWieWeitNachVorneMitDenHaenden)
                 && (leftHandZ.First() - leftHandZ.First() < Paramters.rhWieParallelDieHaendeSeinSollten);

            return ErkennerHandler.SinglePress(this);
        }

        public string GetDebugName()
        {
            return "hadouken";
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        public string SingeKeyPressKeys { get; private set; }
    }
}
