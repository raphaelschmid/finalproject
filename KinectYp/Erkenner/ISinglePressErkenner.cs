using System.Diagnostics;

namespace KinectYp.Erkenner
{
    public interface ISinglePressErkenner : IErkenner
    {
        Stopwatch Stopwatch { get; set; }
        bool Blocked { get; set; }
        int BlockDuration { get; }
        bool Ok { get; }
        string SingeKeyPressKeys { get; }
    }
}
