using System;
using System.Collections.Generic;

namespace ModelingPractice1
{
    class IntegralCriterion
    {

        public static double GetIse(double k1, double k2, double k3)
        {
            Form1.CalculateCriterion(ref k1, ref k2,ref k3);
            return SimCalculation.ISE;
        }

        public static double GetIAE(double k1, double k2, double k3)
        {
            Form1.CalculateCriterion(ref k1, ref k2, ref k3);
            return SimCalculation.IAE;
        }

        public static double GetITAE(double k1, double k2, double k3)
        {
            Form1.CalculateCriterion(ref k1, ref k2, ref k3);
            return SimCalculation.ITAE;
        }
        public static double GetITSE(double k1,  double k2,double k3)
        {
            Form1.CalculateCriterion(ref k1, ref k2, ref k3);
            return SimCalculation.ITSE;
        }
        
    }
}
