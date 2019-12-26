using Modelirovanie_lab1_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ModelingPractice1
{
    class ExcelPrinter
    {
        public static List<CalculationPoint> points = SimCalculation.points;

        public static void ExcelPrint(int count,double T0,double TK,int TCol, int YCol, int X1Col, int X2Col,bool WP,bool Phase,String fileName)
        {
            int c = count;
            int n = 0;
            double t0 = T0;
            double tk = TK;
            Excel excel = new Excel(fileName, 1);
            int tCol = TCol;
            int yCol = YCol;
            int x1Col = X1Col;
            int x2Col = X2Col;
            bool wp=WP;
            bool phase = Phase;

            for (int i = 0; i < points.Count; i++)
            {
                if (Math.Round(points[i].t, 1) == Math.Round((tk - t0) / c * n, 1)) //Ограничение вывода кол-ва значений в Excel
                {
                    if (wp == true)
                    {
                        //Переходный процесс
                        excel.WriteToCell(n, tCol, points[i].t); //Запись значений в Excel
                        excel.WriteToCell(n, yCol, points[i].y);
                    }
                    if (phase == true)
                    {
                        //Фазовый портрет
                        excel.WriteToCell(n, x1Col, points[i].x1);
                        excel.WriteToCell(n, x2Col, points[i].x2);
                    }
                    n++;
                }
            }
            excel.Save(); //Сохранение значений в Excel
            excel.Close();
        }
    }
}
