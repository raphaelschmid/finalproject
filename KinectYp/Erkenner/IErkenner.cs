using Microsoft.Kinect;

namespace KinectYp.Erkenner
{

    /// <summary>
    /// Interface für Erkenner - Klassen die eine bestimmte Bewegung erkennen.
    /// </summary>
    public interface IErkenner
    {
        /// <summary>
        /// Prueft die history, ob die jeweilige Bewegung ausgelöst wird oder nicht.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns></returns>
        ErkennerStatus Pruefe(Skeleton[] history);

        /// <summary>
        /// Gibt die Name der Erkenner für Debugzwecke zurück.
        /// </summary>
        /// <returns></returns>
        string GetDebugName();

    }
}
