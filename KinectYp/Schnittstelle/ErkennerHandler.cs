using System.Diagnostics;
using KinectYp.Erkenner;

namespace KinectYp.Schnittstelle
{
    /// <summary>
    /// Führt Tastendrücke aus.
    /// </summary>
    public static class ErkennerHandler
    {

        /// <summary>
        /// Führt einen einzigen Tastendruck aus. Der Erkenner wird unmittelbar
        /// danach für eine kurze Zeitlang geblockt, damit die Bewegung nicht
        /// ungewollt mehrmals ausgeführt wird.
        /// </summary>
        /// <param name="erkenner">The erkenner.</param>
        /// <returns></returns>
        public static ErkennerStatus SinglePress(ISinglePressErkenner erkenner)
        {
            if (erkenner.Stopwatch != null && erkenner.Stopwatch.ElapsedMilliseconds > erkenner.BlockDuration)
            {
                erkenner.Stopwatch.Stop();
                erkenner.Stopwatch = null;
                erkenner.Blocked = false;

                // Falls erkenner "Jump" ist, muss der "Ducken" erkenner für eine Zitlang geblockt
                // werden, damit Ducken nicht ungewollt ausgelöst wird.
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
