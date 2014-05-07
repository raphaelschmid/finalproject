using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.Kinect;

namespace KinectYp
{
    class Punch : IErkenner
    {

        public bool Preuefe(SkeletonHistory history)
        {
            var rightFootZ = history.History.Select(x => x.Joints[JointType.FootRight].Position.Z);

            return rightFootZ.Max() > rightFootZ.Min() + 0.5;
        }

        public string GetMessage()
        {
            return "Punched!";
        }
    }
}
