using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectYp.Erkenner;
using Microsoft.Kinect;

namespace KinectYp
{
    class Ducken : IBlockableErkenner
    {
        public Ducken()
        {
            Blocked = false;

        }
        private bool geduckt = false;
        public ErkennerStatus Pruefe(Skeleton[] history)
        {


            var headY = history.Select(x => x.Joints[JointType.Head].Position.Y);
            var leftFootY = history.Select(x => x.Joints[JointType.FootLeft].Position.Y);



            bool unten = (headY.Max() - headY.First() > 0.15) && (leftFootY.First() - leftFootY.Min()) < 0.02;
            bool oben = (headY.First() - headY.Min() > 0.1) && (leftFootY.Max()-leftFootY.First()) < 0.02;

            if (Blocked)
            {
                if (BlockStopwatch.ElapsedMilliseconds > 700)
                {
                    Blocked = false;
                    BlockStopwatch = null;
                }
            }


            if (!Blocked)
            {
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
            }
            
            return geduckt ? ErkennerStatus.aktiv : ErkennerStatus.nicht_aktiv;
        }

        public string GetDebugName()
        {
            return "Ducken";
        }

        public Stopwatch BlockStopwatch { get; set; }
        public bool Blocked { get; set; }
    }
}