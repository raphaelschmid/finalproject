﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectYp
{
    interface IErkenner
    {
        bool Pruefe(Skeleton[] history);
        string GetDebugName();

    }
}
