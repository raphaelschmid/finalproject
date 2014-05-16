using System.Diagnostics;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.StandardAngriffe
{
    /// <summary>
    /// Erkennung von "Middle Kick". Sie wird durch einen Fusstoss nach vorne
    /// mit der Rechter Fuss ausgelöst.
    /// </summary>
    class Kick : ISinglePressErkenner
    {
        public Kick()
        {
            Blocked = false;
            BlockDuration = 200;
            SingeKeyPressKeys = MotionFunctions.MKick();
        }

        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var rightFootZ = history.Select(x => x.Joints[JointType.FootRight].Position.Z);
            var rightFootY = history.Select(x => x.Joints[JointType.FootRight].Position.Y);

            Ok = rightFootZ.Max() > rightFootZ.First() + 0.5 && (rightFootY.First() > rightFootY.Min() + 0.2);

            return ErkennerHandler.SinglePress(this);
        }

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "Kick";
        }


        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        public string SingeKeyPressKeys { get; private set; }
    }
}
