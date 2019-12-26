using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingPractice1
{
    class Eraser
    {
        public static void ClearPoints()
        {
            SimCalculation.points.Clear();
        }
        public static void ClearIntegralCriterion()
        {
            SimCalculation.ISE = 0;
            SimCalculation.IAE = 0;
            SimCalculation.ITSE = 0;
            SimCalculation.ITAE = 0;
        }
    }
}
