using System;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.Bewegungen
{
    class RechtsLaufen : IErkenner
    {
        private bool _rechtsLaufend;
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            
            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            bool rechts = rightFootX.Min() + Paramters.RlAktivierungsSchwelle < rightFootX.First() && (leftFootX.Max() - leftFootX.Min()) < Paramters.RlAndereFussLimite;
            bool mitte = Math.Abs(rightFootX.First() - leftFootX.First()) < Paramters.RlMitteSchwellenwert;

            if (_rechtsLaufend && mitte)
            {
                _rechtsLaufend = false;
                MotionFunctions.SendAction(MotionFunctions.RightUp());
                return ErkennerStatus.NichtAktiv;
            }
            if (!_rechtsLaufend && rechts)
            {
                _rechtsLaufend = true;
                MotionFunctions.SendAction(MotionFunctions.RightDown());
                return ErkennerStatus.Aktiv;
            }
            return _rechtsLaufend ? ErkennerStatus.Aktiv : ErkennerStatus.NichtAktiv;
        }

        public string GetDebugName()
        {
            return "rechts laufen";
        }

    }
}
