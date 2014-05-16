using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KinectYp.Schnittstelle
{
    /// <summary>
    /// Tastenkombinationen für die verschiedene StreetFighter Bewegungen.
    /// </summary>
    static class MotionFunctions
    {
        private const int T = 50;

        /// <summary>
        /// Sends the action to the Java Server, which translates the action into actual KeyPresses.
        /// </summary>
        /// <param name="action">The action.</param>
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
            return LeftDown() + T + ";" + LeftUp() + T + ";";
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
            return RightDown() + T + ";" + RightUp() + T + ";";
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
            return "dz;" + T + ";uz;" + T + ";";
        }

        public static String Down()
        {
            return DownDown() + T + ";" + DownUp() + T + ";";
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
            return "ds;" + T + ";us;" + T + ";";
        }

        public static String LPunch()
        {
            return "da;" + T + ";ua;" + T + ";";
        }

        public static String HPunch()
        {
            return "dd;" + T + ";ud;" + T + ";";
        }

        public static String TripplePunch()
        {
            return "dq;" + T + ";uq;" + T + ";";
        }

        public static String MKick()
        {
            return "dx;" + T + ";ux;" + T + ";";
        }

        public static String LKick()
        {
            return "dy;" + T + ";uy;" + T + ";";
        }

        public static String HKick()
        {
            return "dc;" + T + ";uc;" + T + ";";
        }

        public static String TrippleKick()
        {
            return "de;" + T + ";ue;" + T + ";";
        }

        public static String Qfc()
        {
            return "db;" + T + ";dh;" + T + ";ub;" + T + ";uh;" + T + ";";
        }

        public static String Qbc()
        {
            return "db;" + T + ";dg;" + T + ";ub;" + T + ";ug;" + T + ";";
        }

        public static String HalfCircle()
        {
            return "dg;" + T + ";db;" + T + ";ug;" + T + ";dh;" + T + ";ub;" + T + ";uh;" + T + ";";
        }

        public static String FullCircle()
        {
            return "dh;" + T + ";db;" + T + ";uh;" + T + ";dg;" + T + ";ub;" + T + ";dz;" + T + ";ug;" + T + ";dh;" + T + ";uz;" + T + ";uh;" + T + ";";
        }

        public static String ForwardZ()
        {
            return "dh;" + T + ";uh;" + T + ";db;" + T + ";dh;" + T + ";ub;" + T + ";uh;" + T + ";";
        }

        public static String BackwardZ()
        {
            return "dg;" + T + ";ug;" + T + ";db;" + T + ";dg;" + T + ";ub;" + T + ";ug;" + T + ";";
        }
    }
}
