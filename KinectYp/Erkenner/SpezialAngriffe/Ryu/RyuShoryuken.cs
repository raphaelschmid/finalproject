using System.Diagnostics;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.SpezialAngriffe.Ryu
{
    /// <summary>
    /// Erkennung von "Shoryuken" von der Charakter Ryu. Sie wird durch einen 
    /// Faustschlag nach oben links mit der rechter Hand ausgelöst.
    /// </summary>
    class RyuShoryuken : ISinglePressErkenner
    {
        public RyuShoryuken()
        {
            Blocked = false;
            BlockDuration = 400;
            SingeKeyPressKeys = MotionFunctions.Right() + MotionFunctions.Qfc() + MotionFunctions.MPunch();
        }

        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var rightHandY    = history.Select(x => x.Joints[JointType.HandRight].Position.Y);
            var rightHandX = history.Select(x => x.Joints[JointType.HandRight].Position.X);
            var headX = history.Select(x => x.Joints[JointType.Head].Position.X);
            var leftShoulderY = history.Select(x => x.Joints[JointType.ShoulderLeft].Position.Y);

            Ok = rightHandX.First() < rightHandX.Max() - 0.2 && (rightHandY.First() > leftShoulderY.First()) && (rightHandX.First() < headX.First() - 0.2);

            return ErkennerHandler.SinglePress(this);
        }

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "Shoryuken";
        }


        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }
        private string _SingeKeyPressKeys;
        public string SingeKeyPressKeys
        {
            get
            {
                if (Form1.positionTracker.Normal)
                {
                    return MotionFunctions.Right() + MotionFunctions.Qfc() + MotionFunctions.MPunch();
                }
                else
                {
                    return MotionFunctions.Left() + MotionFunctions.Qbc() + MotionFunctions.MPunch();
                }

            }
            private set { _SingeKeyPressKeys = value; }
        }
    }
}
