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
    class RechtsLaufen : IErkenner
    {
        /// <summary>
        /// Ob man zur Zeit nach rechts laufend ist oder nicht.
        /// </summary>
        private bool _rechtsLaufend;

        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "rechts laufen";
        }

    }
}
