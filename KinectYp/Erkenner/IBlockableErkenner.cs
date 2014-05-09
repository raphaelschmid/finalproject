using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp.Erkenner
{
    public interface IBlockableErkenner : IErkenner
    {
        Stopwatch BlockStopwatch { get; set; }
        bool Blocked { get; set; }
    }
}
