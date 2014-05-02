using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.Kinect;

namespace KinectYp
{
    class Punch : IErkenner
    {
        private Form1 _form1;

        public Punch(Form1 form1)
        {
            _form1 = form1;
        }

        public void Preuefe(SkeletonHistory history)
        {
            var zs = history.History.Select(x => x.Joints[JointType.FootRight].Position.Z);
            
            throw new NotImplementedException();
        }
    }
}
