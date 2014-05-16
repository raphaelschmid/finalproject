using System;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.Bewegungen
{
    class LinksLaufen : IErkenner
    {
        private bool _linksLaufend;

        public ErkennerStatus Pruefe(Skeleton[] history)
        {

            
            var rightFootX = history.Select(x => x.Joints[JointType.FootRight].Position.X);
            var leftFootX = history.Select(x => x.Joints[JointType.FootLeft].Position.X);

            bool links = (leftFootX.First() + Paramters.RlAktivierungsSchwelle < leftFootX.Max()) && ((rightFootX.Max() - rightFootX.Min()) < Paramters.RlAndereFussLimite);
            bool mitte = Math.Abs(rightFootX.First() - leftFootX.First()) < Paramters.RlMitteSchwellenwert;

            if (_linksLaufend && mitte)
            {
                _linksLaufend = false;
                MotionFunctions.SendAction(MotionFunctions.LeftUp());
                return ErkennerStatus.NichtAktiv;
            }
            if (!_linksLaufend && links)
            {
                _linksLaufend = true;
                MotionFunctions.SendAction(MotionFunctions.LeftDown());
                return ErkennerStatus.Aktiv;
            }
            return _linksLaufend ? ErkennerStatus.Aktiv : ErkennerStatus.NichtAktiv;
        }
        


        public string GetDebugName()
        {
            return "links laufen";
        }

    }
}
