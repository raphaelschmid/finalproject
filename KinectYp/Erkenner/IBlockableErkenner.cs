using System.Diagnostics;

namespace KinectYp.Erkenner
{
    public interface IBlockableErkenner : IErkenner
    {
        Stopwatch BlockStopwatch { set; }
        bool Blocked { set; }
    }
}
