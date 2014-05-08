using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectYp
{
    public static class Paramters
    {
        //////////////////////////
        // Rechts und Links laufen
        //////////////////////////

        // Wie nah die Füsse ananeinder sein sollen damit die Bewegung abgebrochen wird
        public static double rlMitteSchwellenwert = 0.3;

        // Wie weit man mit den jeweiligen Fuss nach aussen gehen muss, um in diese Richtung zu bewewgen
        public static double rlAktivierungsSchwelle = 0.2;

        // Um wie viel der andere Fuss sich höchstens bewegen darf
        public static double rlAndereFussLimite = 0.2;

        //////////////////////////
        // Jump
        //////////////////////////
        public static double jumpSchwelle = 0.05;

        //////////////////////////
        // Ryu Hadouken
        //////////////////////////
        public static double rhWieWeitNachVorneMitDenHaenden = 0.3;
        public static double rhWieParallelDieHaendeSeinSollten = 0.5;

    }
}
