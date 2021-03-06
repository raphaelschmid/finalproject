﻿using System;
using System.Diagnostics;
using System.Linq;
using KinectYp.Schnittstelle;
using Microsoft.Kinect;

namespace KinectYp.Erkenner.SpezialAngriffe.Ryu
{
    /// <summary>
    /// Erkennung von "Hadouken" von der Charakter Ryu. Sie ausgelöst wenn
    /// man mit beiden Händen parallel geradeaus nach vorne stosst.
    /// </summary>
    class RyuHadouken : ISinglePressErkenner
    {
        public RyuHadouken()
        {
            Blocked = false;
            BlockDuration = 400;
            SingeKeyPressKeys = MotionFunctions.Qfc() + MotionFunctions.MPunch();
        }
        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
        public ErkennerStatus Pruefe(Skeleton[] history)
        {
            var rightHandZ = history.Select(x => x.Joints[JointType.HandRight].Position.Z);
            var leftHandZ = history.Select(x => x.Joints[JointType.HandLeft].Position.Z);

            
            Ok = (rightHandZ.Max() - rightHandZ.First()  > Paramters.RhWieWeitNachVorneMitDenHaenden)
                 && (leftHandZ.Max() - leftHandZ.First() > Paramters.RhWieWeitNachVorneMitDenHaenden)
                 && (leftHandZ.First() - leftHandZ.First() < Paramters.RhWieParallelDieHaendeSeinSollten);

            return ErkennerHandler.SinglePress(this);
        }

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        public string GetDebugName()
        {
            return "hadouken";
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Blocked { get; set; }
        public int BlockDuration { get; private set; }
        public bool Ok { get; private set; }

        public string SingeKeyPressKeys
        {
            get
            {
                if (Form1.positionTracker.Normal)
                {
                    return MotionFunctions.Qfc() + MotionFunctions.MPunch();
                }
                else
                {
                    return MotionFunctions.Qbc() + MotionFunctions.MPunch();
                }
                
            }
            private set { if (value == null) throw new ArgumentNullException("value"); }
        }
    }
}
