using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp
{
    class Punch : IErkenner
    {

        public bool Pruefe(Skeleton[] history)
        {
            System.Diagnostics.Debug.WriteLine("asddf");

            var rightHandZ = history.Select(x => x.Joints[JointType.HandRight].Position.Z);
            var rightHandY = history.Select(x => x.Joints[JointType.HandRight].Position.Y);
            var leftShoulderY = history.Select(x => x.Joints[JointType.ShoulderLeft].Position.Y);

            return (rightHandZ.First() < rightHandZ.Max() - 0.27) && (rightHandY.First() < leftShoulderY.First() + 0.1);
        }

        public string GetDebugName()
        {
            return "Punch!";
        }
    }
}
