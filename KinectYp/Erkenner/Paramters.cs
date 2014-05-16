namespace KinectYp.Erkenner
{
    public static class Paramters
    {
        //////////////////////////
        // Rechts und Links laufen
        //////////////////////////

        // Wie nah die Füsse ananeinder sein sollen damit die Bewegung abgebrochen wird
        public const double RlMitteSchwellenwert = 0.3;

        // Wie weit man mit den jeweiligen Fuss nach aussen gehen muss, um in diese Richtung zu bewewgen
        public const double RlAktivierungsSchwelle = 0.2;

        // Um wie viel der andere Fuss sich höchstens bewegen darf
        public const double RlAndereFussLimite = 0.1;

        //////////////////////////
        // Jump
        //////////////////////////
        public const double JumpSchwelle = 0.08;

        //////////////////////////
        // Ryu Hadouken
        //////////////////////////
        public const double RhWieWeitNachVorneMitDenHaenden = 0.3;
        public const double RhWieParallelDieHaendeSeinSollten = 0.1;
    }
}
