﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectYp.Erkenner;
using KinectYp.Erkenner.Bewegungen;

namespace KinectYp
{
    public static class ErkennerHandler
    {

        public static ErkennerStatus SinglePress(ISinglePressErkenner erkenner)
        {
            if (erkenner.Stopwatch != null && erkenner.Stopwatch.ElapsedMilliseconds > erkenner.BlockDuration)
            {
                erkenner.Stopwatch.Stop();
                erkenner.Stopwatch = null;
                erkenner.Blocked = false;
                

                foreach (var e in Form1.positionTracker.erkenners)
                {
                    if (e.GetDebugName().Equals("Jump"))
                    {
                        foreach (var ee in Form1.positionTracker.erkenners)
                        {
                            if (ee.GetDebugName().Equals("Ducken"))
                            {
                                IBlockableErkenner DuckenErkenner = (IBlockableErkenner) ee;
                                DuckenErkenner.Blocked = true;
                                DuckenErkenner.BlockStopwatch = Stopwatch.StartNew();

                            }
                        }
                    }
                }
                return ErkennerStatus.nicht_aktiv;
            }

            if (erkenner.Blocked)
            {
                return ErkennerStatus.blocked;
            }

            if (erkenner.Ok)
            {
                MotionFunctions.SendAction(erkenner.SingeKeyPressKeys);
                erkenner.Blocked = true;
                erkenner.Stopwatch = Stopwatch.StartNew();
                return ErkennerStatus.aktiv;
            }

            return ErkennerStatus.nicht_aktiv;
        }
    }
}
