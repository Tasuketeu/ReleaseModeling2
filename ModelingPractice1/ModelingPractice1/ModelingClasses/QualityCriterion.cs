using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingPractice1
{
    class QualityCriterion
    {
        public static List<CalculationPoint> points = SimCalculation.points;

        public static double RegulationTime(double precision, double target)
        {
            for (int i = points.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(points[i].y - target) > precision)
                {
                    if (i == points.Count - 1)
                    {
                        return double.PositiveInfinity;
                    }
                    return points[i].t;
                }
            }

            return double.PositiveInfinity;
        }

        public static double MaxReregulation(double target)
        {
            if (target == 0)
            {
                return (double)(((points.Max(c => c.y) - target)) * 100d);
            }
            return (double)(((points.Max(c => c.y) - target) / target) * 100d);
        }

        public static double Oscillation(double target)
        {
            double amplitude_1 = points[0].y;
            int index1 = 0;
            double amplitude_3 = points[0].y;
            double dy = 0;

            // Поиск первой положительной амплитуды
            for (int i = 1; i < points.Count; i++)
            {
                // Переход с положительной производной на отрицательную.
                if (dy > 0 && (points[i].y - points[i - 1].y) <= 0 && points[i].y - target > 0)
                {
                    amplitude_1 = points[i].y - target;
                    index1 = i;
                    dy = 0;
                    break;
                }
                dy = (points[i].y - points[i - 1].y);
            }

            // Чтобы не было выхода за границы массива,
            // если положительных амплитуд нет.
            if (index1 >= points.Count || index1 < 1)
            {
                return 0;
            }

            // Поиск второй положительной амплитуды
            for (int i = index1; i < points.Count; i++)
            {
                // Переход с положительной производной на отрицательную.
                if (dy > 0 && (points[i].y - points[i - 1].y) <= 0 && points[i].y - target > 0)
                {
                    amplitude_3 = points[i].y - target;
                    break;
                }
                dy = (points[i].y - points[i - 1].y);
            }

            if (amplitude_1 == 0)
            {
                throw new Exception("Колебательность. Деление на 0");
            }
            return (1 - (amplitude_3 / amplitude_1)) * 100d;
        }
    }
}
