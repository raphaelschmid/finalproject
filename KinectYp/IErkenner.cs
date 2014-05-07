using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp
{
    interface IErkenner
    {
        bool Preuefe(SkeletonHistory history);
        string GetMessage();

    }
}
