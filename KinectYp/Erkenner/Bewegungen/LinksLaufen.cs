using System;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.Bewegungen
{
    /// <summary>
    /// Erkennung von Rechtslaufen. Sie wird ausgelöst, wenn man
    /// der Rechte Fuss nach aussen ausdehnt, während der linker
    /// Fuss in der Mitte stillsteht. Um aufzuhören geht man mit
    /// beiden Füssen zusammen in der Mitte.
    /// </summary>
    class LinksLaufen : IErkenner
    {
        /// <summary>
        /// Ob man zur Zeit nach links laufend ist oder nicht.
        /// </summary>
        private bool _linksLaufend;

        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "links laufen";
        }

    }
}
