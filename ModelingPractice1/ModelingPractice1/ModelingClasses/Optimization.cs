using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelingPractice1
{
    class Optimization
    {

        public static void GetResultOptByHG(Func<double, double, double,double> f, ref double k1, ref double k2,ref double k3,
            bool k1Check,bool k2Check,bool k3Check)
        {
            // На начальном этапе задаётся стартовая точка 1
            double x1 = k1;
            double y1 = k2;
            double z1 = k3;
            //объявление и инициализация точек 2 и 3
            double x2 = k1;
            double y2 = k2;
            double z2 = k3;
            double x3 = k1;
            double y3 = k2;
            double z3 = k3;
            double errMax = 0.01;//объявление и инициализация допустимой погрешности
            double err = errMax + 1;//объявление и инициализация текущей погрешности
            double h = 0.01;//объявление и инициализация шага поиска
            double prevF = f(x1, y1,z1); //вычисляем значение функции в начальной точке
            while (err > errMax) //Когда ошибка станет меньше допустимой, алгоритм завершается, и точка 1 признаётся точкой минимума.
            {
                //Этап исследующего поиска. Определяем лучшее значение функции в окрестностях точки x1 с шагом h. Точка с найденным значением становится точкой x2.

                if (k1Check)
                {
                    if (prevF > f(x1 + h, y1, z1))
                        x2 = x1 + h;
                    else if (prevF > f(x1 - h, y1, z1))
                        x2 = x1 - h;
                }
                if (k2Check)
                {
                    if (prevF > f(x2, y1 + h, z1))
                        y2 = y1 + h;
                    else if (prevF > f(x2, y1 - h, z1))
                        y2 = y1 - h;
                }
                if (k3Check)
                {
                    if (prevF > f(x2, y2, z1 + h))
                        z2 = z1 + h;
                    else if (prevF > f(x2, y2, z1 - h))
                        z2 = z1 - h;
                }

                //если лучшее значение не найдено, производится определение ошибки
                if (x2 == x1 && y2 == y1 && z2 == z1)
                {
                    err = Math.Pow((Math.Pow(h, 2) + Math.Pow(h, 2)), (1 / 2d));
                    h = h / 2; //если на следующей итерации ошибка не окажется меньше допустимой, шаг сокращается в 3 раза
                }
                else //если лучшее значение найдено
                {
                    //На этапе поиска по образцу откладывается точка 3 в направлении от 1 к 2 на том же расстоянии.
                    //Вычисляется значение функции в точке 3
                    if (k1Check)
                    {
                        x3 = x1 + 2 * (x2 - x1);
                    }
                    if (k2Check)
                    {
                        y3 = y1 + 2 * (y2 - y1);
                    }
                    if (k3Check)
                    {
                        z3 = z1 + 2 * (z2 - z1);
                    }
                    while (f(x3, y3, z3) < f(x2, y2, z2))
                    {
                        x1 = x2;
                        y1 = y2;
                        z1 = z2;
                        x2 = x3;
                        y2 = y3;
                        z2 = z3;

                        if (k1Check)
                        {
                            x3 = x1 + 2 * (x2 - x1);
                        }
                        if (k2Check)
                        {
                            y3 = y1 + 2 * (y2 - y1);
                        }
                        if (k3Check)
                        {
                            z3 = z1 + 2 * (z2 - z1);
                        }
                    }//  Поиск по образцу продолжается до тех пор, пока он не окажется неудачным
                    x1 = x2;
                    y1 = y2;
                    z1 = z2;
                    prevF = f(x1, y1, z1);
                }
            }
        }
    }
}
