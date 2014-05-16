using System.Diagnostics;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.StandardAngriffe
{
    /// <summary>
    /// Erkennung von "Middle Punch". Sie wird durch einen Faustschlag nach vorne
    /// mit der Linker Hand ausgelöst.
    /// </summary>
    class Punch : ISinglePressErkenner
    {
        public Punch()
        {
            Blocked = false;
            BlockDuration = 200;
            SingeKeyPressKeys = MotionFunctions.MPunch();
        }

        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var leftHandZ    = history.Select(x => x.Joints[JointType.HandLeft].Position.Z);
            var leftHandY    = history.Select(x => x.Joints[JointType.HandLeft].Position.Y);
            var leftShoulderY = history.Select(x => x.Joints[JointType.ShoulderLeft].Position.Y);
            var rightHandZ     = history.Select(x => x.Joints[JointType.HandRight].Position.Z);

            Ok = leftHandZ.First() < leftHandZ.Max() - 0.27 && (leftHandY.First() < leftShoulderY.First() + 0.1) && (rightHandZ.Max() - rightHandZ.First() < 0.15);

            return ErkennerHandler.SinglePress(this);
        }

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "Punch";
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        public string SingeKeyPressKeys { get; private set; }
    }
}
