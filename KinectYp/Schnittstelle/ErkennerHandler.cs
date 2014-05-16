using System.Diagnostics;
using KinectYp.Erkenner;

namespace KinectYp.Schnittstelle
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
                

                foreach (var e in Form1.positionTracker.Erkenners)
                {
                    if (e.GetDebugName().Equals("Jump"))
                    {
                        foreach (var ee in Form1.positionTracker.Erkenners)
                        {
                            if (ee.GetDebugName().Equals("Ducken"))
                            {
                                IBlockableErkenner duckenErkenner = (IBlockableErkenner) ee;
                                duckenErkenner.Blocked = true;
                                duckenErkenner.BlockStopwatch = Stopwatch.StartNew();

                            }
                        }
                    }
                }
                return ErkennerStatus.NichtAktiv;
            }

            if (erkenner.Blocked)
            {
                return ErkennerStatus.Blocked;
            }

            if (erkenner.Ok)
            {
                MotionFunctions.SendAction(erkenner.SingeKeyPressKeys);
                erkenner.Blocked = true;
                erkenner.Stopwatch = Stopwatch.StartNew();
                return ErkennerStatus.Aktiv;
            }

            return ErkennerStatus.NichtAktiv;
        }
    }
}
