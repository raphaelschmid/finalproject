using System.Diagnostics;

namespace KinectYp.Erkenner
{
    /// <summary>
    /// Erkenner, welche eine einmalige Bewegung (z.B. Punch oder Kick) erkennt
    /// (im Gegensatz zu einer Toggle Aktion (z.B. links/rechts laufen), wo eine
    /// Taste auf unbestimmte Zeit gedrückt werden soll).
    /// </summary>
    public interface ISinglePressErkenner : IErkenner
    {
        Stopwatch Stopwatch { get; set; }
        bool Blocked { get; set; }
        int BlockDuration { get; }
        bool Ok { get; }
        string SingeKeyPressKeys { get; }
    }
}
