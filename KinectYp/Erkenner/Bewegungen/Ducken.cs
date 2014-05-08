using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp
{
    class Ducken : IErkenner
    {
        private bool geduckt = false;
        public ErkennerStatus Pruefe(Skeleton[] history)
        {


            var headY = history.Select(x => x.Joints[JointType.Head].Position.Y);
            var leftFootY = history.Select(x => x.Joints[JointType.FootLeft].Position.Y);

            bool unten =  (headY.Max() - headY.First() > 0.1) && (leftFootY.Max() - leftFootY.Min()) < 0.03;
            bool oben = (headY.First() - headY.Min() > 0.1) && (leftFootY.Max() - leftFootY.Min()) < 0.03; 

            if (geduckt && oben)
            {
                geduckt = false;
                MotionFunctions.SendAction(MotionFunctions.DownUp());
                return ErkennerStatus.nicht_aktiv;
            }
            if (!geduckt && unten)
            {
                geduckt = true;
                MotionFunctions.SendAction(MotionFunctions.DownDown());
                return ErkennerStatus.aktiv;
            }
            if (geduckt)
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
            return "geduckt";
        }
    }
}