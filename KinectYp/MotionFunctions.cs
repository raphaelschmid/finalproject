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
         static int tn = 50;
        static int tl = 70;

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
            return "dg;" + tn + ";ug;" + tl + ";";
        }

        public static String Right()
        {
            return "dh;" + tn + ";uh;" + tl + ";";
        }

        public static String Up()
        {
            return "dz;" + tn + ";uz;" + tl + ";";
        }

        public static String Down()
        {
            return "db;" + tn + ";ub;" + tl + ";";
        }

        public static String MPunch()
        {
            return "ds;" + tn + ";us;" + tl + ";";
        }

        public static String LPunch()
        {
            return "da;" + tn + ";ua;" + tl + ";";
        }

        public static String HPunch()
        {
            return "dd;" + tn + ";ud;" + tl + ";";
        }

        public static String TripplePunch()
        {
            return "dq;" + tn + ";uq;" + tl + ";";
        }

        public static String MKick()
        {
            return "dx;" + tn + ";ux;" + tl + ";";
        }

        public static String LKick()
        {
            return "dy;" + tn + ";uy;" + tl + ";";
        }

        public static String HKick()
        {
            return "dc;" + tn + ";uc;" + tl + ";";
        }

        public static String TrippleKick()
        {
            return "de;" + tn + ";ue;" + tl + ";";
        }

        public static String Qfc()
        {
            return "db;" + tn + ";dh;" + tn + ";ub;" + tn + ";uh;" + tl + ";";
        }

        public static String Qbc()
        {
            return "db;" + tn + ";dg;" + tn + ";ub;" + tn + ";ug;" + tl + ";";
        }

        public static String HalfCircle()
        {
            return "dg;" + tn + ";db;" + tn + ";ug;" + tn + ";dh;" + tn + ";ub;" + tn + ";uh;" + tl + ";";
        }

        public static String FullCircle()
        {
            return "dh;" + tn + ";db;" + tn + ";uh;" + tn + ";dg;" + tn + ";ub;" + tn + ";dz;" + tn + ";ug;" + tn + ";dh;" + tn + ";uz;" + tn + ";uh;" + tl + ";";
        }

        public static String ForwardZ()
        {
            return "dh;" + tn + ";uh;" + tn + ";db;" + tn + ";dh;" + tn + ";ub;" + tn + ";uh;" + tl + ";";
        }

        public static String BackwardZ()
        {
            return "dg;" + tn + ";ug;" + tn + ";db;" + tn + ";dg;" + tn + ";ub;" + tn + ";ug;" + tl + ";";
        }
    }
}
