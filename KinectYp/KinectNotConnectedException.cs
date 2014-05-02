using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp {
    public class KinectNotConnectedException : Exception {

        public override string Message {
            get {
                return "Sorry something went wrong with your Kinect. Probably its not connected.";
            }
        }
    }
}
