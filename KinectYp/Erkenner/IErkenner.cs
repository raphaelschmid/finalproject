using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp
{

    public interface IErkenner
    {
        ErkennerStatus Pruefe(Skeleton[] history);
        string GetDebugName();
    }
}
