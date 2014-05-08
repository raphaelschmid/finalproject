using System.Drawing.Text;
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
        private bool pressed = false;

        public ErkennerStatus Pruefe(Skeleton[] history)
        {

            
            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            bool ok = (leftFootX.First() + 0.3 < leftFootX.Max()) && ((rightFootX.Max() - rightFootX.Min()) < 0.2);

            if (!pressed)
            {
                if (ok)
                {
                    pressed = true;
                    return true;
                }
            }
            if (pressed)
            {
                if (!ok)
                {
                    pressed = false;
                    return false;
                }
            }
            return ok;
        }

        public string GetDebugName()
        {
            return "nach links gelaufen";
        }
    }
}
