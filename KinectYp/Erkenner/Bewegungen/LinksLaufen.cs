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
        private bool linksLaufend = false;

        public ErkennerStatus Pruefe(Skeleton[] history)
        {

            
            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            bool links = (leftFootX.First() + Paramters.rlAktivierungsSchwelle < leftFootX.Max()) && ((rightFootX.Max() - rightFootX.Min()) < Paramters.rlAndereFussLimite);
            bool mitte = Math.Abs(rightFootX.First() - leftFootX.First()) < Paramters.rlMitteSchwellenwert;

            if (linksLaufend && mitte)
            {
                linksLaufend = false;
                MotionFunctions.SendAction(MotionFunctions.LeftUp());
                return ErkennerStatus.nicht_aktiv;
            }
            if (!linksLaufend && links)
            {
                linksLaufend = true;
                MotionFunctions.SendAction(MotionFunctions.LeftDown());
                return ErkennerStatus.aktiv;
            }
            if (linksLaufend)
            {
                return ErkennerStatus.aktiv;
            }
            else
            {
                return ErkennerStatus.nicht_aktiv;
            }
        }
        


        public string GetDebugName()
        {
            return "nach links gelaufen";
        }
    }
}
