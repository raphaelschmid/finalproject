﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return ErkennerStatus.inaktiv;
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

            return ErkennerStatus.inaktiv;

        }
    }
}
