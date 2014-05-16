using System.Diagnostics;

namespace KinectYp.Erkenner
{
    /// <summary>
    /// Erkenner welche für eine bestimmte Zeit geblockt werden kann.
    /// Während der Blockzeit wird der Erkenner seine jeweilige Bewegung
    /// nicht erkennen können.
    /// </summary>
    public interface IBlockableErkenner : IErkenner
    {
        Stopwatch BlockStopwatch { set; }
        bool Blocked { set; }
    }
}
