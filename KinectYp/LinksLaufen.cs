using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp
{
    class LinksLaufen : IErkenner
    {
        public bool Pruefe(Skeleton[] history)
        {

            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            return (leftFootX.First() + 0.3 < leftFootX.Max()) && ((rightFootX.Max() - rightFootX.Min()) < 0.2);
        }

        public string GetMessage()
        {
            return "nach links gelaufen";
        }
    }
}
