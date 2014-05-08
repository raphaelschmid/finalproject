using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp
{
    class RechtsLaufen : IErkenner
    {
        public ErkennerStatus Pruefe(Skeleton[] history)
        {

            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            return rightFootX.Min() + 0.3 < rightFootX.First() && (leftFootX.Max() - leftFootX.Min()) < 0.2;
        }

        public string GetDebugName()
        {
            return "nach rechts gelaufen";
        }
    }
}
