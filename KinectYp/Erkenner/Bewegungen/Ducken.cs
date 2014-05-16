using System.Diagnostics;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.Bewegungen
{
    /// <summary>
    /// Erkenner von Ducken. Wird durch abducken ausgelöst.
    /// </summary>
    class Ducken : IBlockableErkenner
    {
        public Ducken()
        {
            Blocked = false;
        }

        /// <summary>
        /// Ob man zur Zeit geduckt ist oder nicht.
        /// </summary>
        private bool _geduckt;

        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
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
                if (_geduckt && oben)
                {
                    _geduckt = false;
                    MotionFunctions.SendAction(MotionFunctions.DownUp());
                    return ErkennerStatus.NichtAktiv;
                }
                if (!_geduckt && unten)
                {
                    _geduckt = true;
                    MotionFunctions.SendAction(MotionFunctions.DownDown());
                    return ErkennerStatus.Aktiv;
                }
            }
            
            return _geduckt ? ErkennerStatus.Aktiv : ErkennerStatus.NichtAktiv;
        }

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "Ducken";
        }

        public Stopwatch BlockStopwatch { private get; set; }
        public bool Blocked { private get; set; }
    }
}