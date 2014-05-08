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
        private bool rechtsLaufend = false;
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            

            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            bool rechts = rightFootX.Min() + Paramters.rlAktivierungsSchwelle < rightFootX.First() && (leftFootX.Max() - leftFootX.Min()) < Paramters.rlAndereFussLimite;
            bool mitte = Math.Abs(rightFootX.First() - leftFootX.First()) < Paramters.rlMitteSchwellenwert;

            if (rechtsLaufend && mitte)
            {
                rechtsLaufend = false;
                MotionFunctions.SendAction(MotionFunctions.RightUp());
                return ErkennerStatus.nicht_aktiv;
            }
            if (!rechtsLaufend && rechts)
            {
                rechtsLaufend = true;
                MotionFunctions.SendAction(MotionFunctions.RightDown());
                return ErkennerStatus.aktiv;
            }
            if (rechtsLaufend)
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
            return "nach rechts gelaufen";
        }
    }
}
