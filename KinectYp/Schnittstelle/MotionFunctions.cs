using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp
{
    class MotionFunctions
    {
        static int t = 50;

        public static void SendAction(string action)
        {
            var client = new UdpClient();
            IPAddress address = IPAddress.Parse(IPAddress.Broadcast.ToString());
            client.Connect(address, 9876);
            byte[] sendBytes = Encoding.ASCII.GetBytes(action);
            client.Send(sendBytes, sendBytes.GetLength(0));
        }

        public static String Left()
        {
            return LeftDown() + t + ";" + LeftUp() + t + ";";
        }
        public static String LeftDown()
        {
            return "dg;";
        }
        public static String LeftUp()
        {
            return "ug;";
        }
        public static String Right()
        {
            return RightDown() + t + ";" + RightUp() + t + ";";
        }

        public static String RightDown()
        {
            return "dh;";
        }
        public static String RightUp()
        {
            return "uh;";
        }

        public static String Up()
        {
            return "dz;" + t + ";uz;" + t + ";";
        }

        public static String Down()
        {
            return DownDown() + t + DownUp() + t + ";";
        }

        public static String DownDown()
        {
            return "db;";
        }

        public static String DownUp()
        {
            return "ub;";
        }

        public static String MPunch()
        {
            return "ds;" + t + ";us;" + t + ";";
        }

        public static String LPunch()
        {
            return "da;" + t + ";ua;" + t + ";";
        }

        public static String HPunch()
        {
            return "dd;" + t + ";ud;" + t + ";";
        }

        public static String TripplePunch()
        {
            return "dq;" + t + ";uq;" + t + ";";
        }

        public static String MKick()
        {
            return "dx;" + t + ";ux;" + t + ";";
        }

        public static String LKick()
        {
            return "dy;" + t + ";uy;" + t + ";";
        }

        public static String HKick()
        {
            return "dc;" + t + ";uc;" + t + ";";
        }

        public static String TrippleKick()
        {
            return "de;" + t + ";ue;" + t + ";";
        }

        public static String Qfc()
        {
            return "db;" + t + ";dh;" + t + ";ub;" + t + ";uh;" + t + ";";
        }

        public static String Qbc()
        {
            return "db;" + t + ";dg;" + t + ";ub;" + t + ";ug;" + t + ";";
        }

        public static String HalfCircle()
        {
            return "dg;" + t + ";db;" + t + ";ug;" + t + ";dh;" + t + ";ub;" + t + ";uh;" + t + ";";
        }

        public static String FullCircle()
        {
            return "dh;" + t + ";db;" + t + ";uh;" + t + ";dg;" + t + ";ub;" + t + ";dz;" + t + ";ug;" + t + ";dh;" + t + ";uz;" + t + ";uh;" + t + ";";
        }

        public static String ForwardZ()
        {
            return "dh;" + t + ";uh;" + t + ";db;" + t + ";dh;" + t + ";ub;" + t + ";uh;" + t + ";";
        }

        public static String BackwardZ()
        {
            return "dg;" + t + ";ug;" + t + ";db;" + t + ";dg;" + t + ";ub;" + t + ";ug;" + t + ";";
        }
    }
}
