using System.Diagnostics;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.Bewegungen
{
    /// <summary>
    /// Erkenner von Jump. Wird durch einen Jump ausgelöst.
    /// </summary>
    internal class Jump : ISinglePressErkenner
    {
         public Jump()
        {
            Blocked = false;
            BlockDuration = 200;
            SingeKeyPressKeys = MotionFunctions.Up();
        }

         /// <summary>
         /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
         /// </summary>
         /// <param name="history">The history.</param>
         /// <returns></returns>
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var leftFootY = history.Select(x => x.Joints[JointType.FootLeft].Position.Y);
            var rightFootY = history.Select(x => x.Joints[JointType.FootRight].Position.Y);

            Ok = (leftFootY.First() - leftFootY.Min()> Paramters.JumpSchwelle) &&
                (rightFootY.First() - rightFootY.Min() > Paramters.JumpSchwelle);

            return ErkennerHandler.SinglePress(this);
        }

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "Jump";
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        public string SingeKeyPressKeys { get; private set; }
    }
}
