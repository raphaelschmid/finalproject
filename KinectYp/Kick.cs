using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.Kinect;

namespace KinectYp
{
    class Kick : IErkenner
    {

        public bool Pruefe(Skeleton[] history)
        {
            System.Diagnostics.Debug.WriteLine("asdf");

            var rightFootZ = history.Select(x => x.Joints[JointType.FootRight].Position.Z);
            var rightFootY = history.Select(x => x.Joints[JointType.FootRight].Position.Y);

            return (rightFootZ.Max() > rightFootZ.First() + 0.5) && (rightFootY.First() > rightFootY.Min() + 0.2);
        }

        public string GetDebugName()
        {
            return "Kick";
        }
    }
}
