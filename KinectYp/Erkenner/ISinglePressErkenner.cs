using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp
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
