using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingPractice1
{
    class SimCalculation
    {
        public static List<CalculationPoint> points = new List<CalculationPoint>();
        public static double ISE; //Интегральный квадратичный критерий качества
        public static double IAE; //Интегральный абсолютный критерий качества
        public static double ITSE; //Интегральный квадратичный критерий качества по времени
        public static double ITAE; //Интегральный абсолютный критерий качества по времени

        public static void CalculatePoints(double a, double b, double step, double k,
              double ti, double indignation, double target, double tau, double k1, double k2,double k3)
        {
            Eraser.ClearPoints();
            double YX = 0, IT = 0, Y = 0, X1 = 0, X2 = 0, U, XT, Y1;
            int j = 1;
            double T0 = a, //Начальное значение времени
            TK = b, //Конечное значение времени
                  HT = step, //Шаг
                  K = k,  //Коэффициент передачи
                  T = ti,  //Постоянная времени
                  TAY = tau, //Запаздывание

                    K1 = k1,        //Коэффициент усиления пропорциональной составляющей
                    K2 = k2,        //Коэффициент усиления интегральной составляющей
                    K3=k3, //Коэффициент усиления дифференциальной составляющей

            YS = target, //Задающее воздействие
            F = indignation; //Внешнее воздействие

            int N1 = Convert.ToInt32(TAY / HT); //Размерность массива
            double[] YR = new double[N1];
            for (int i = 1; i <= N1; i++) { YR[i - 1] = 0; }
            double C1 = K / T;
            double C2 = -1 / T;

            Eraser.ClearIntegralCriterion();

            points.Add(new CalculationPoint
            {
                t = 0,
                y = 0,
                x1 = YS,
                x2 = X2
            });
            for (T = T0; T <= TK; T += HT)
            {
                double XP = X1;                   //Предыдущее значение ошибки
                X1 = YS - Y;                        //Ошибка
                X2 = (X1 - XP) / HT;              //Х2 - производная
                IT = IT + (X1 + XP) / 2 * HT;     //Интеграл
                U = K1 * X1 + K2 * IT+K3*X2;      //Управляющее воздействие
                XT = F + U;                       //Суммирование возмущения
                Y1 = C1 * XT + C2 * YX;
                YX = YX + Y1 * HT;

                ISE += Math.Pow(X1, 2) * HT;
                IAE += Math.Abs(X1) * HT;
                ITAE += T * Math.Abs(X1) * HT;
                ITSE += T * Math.Pow(X1, 2) * HT;

                Y = YR[j - 1];                      //Моделирование запаздывания
                YR[j - 1] = YX;
                j = j + 1;
                if (j > N1) j = 1;

                points.Add(new CalculationPoint
                {
                    t = T,
                    y = Y,
                    x1 = X1,
                    x2 = X2
                });
            }
        }
    }
}
