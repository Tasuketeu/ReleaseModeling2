using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Modelirovanie_lab1_Console;

namespace ModelingPractice1
{
    public partial class Form1 : Form
    {
        static double T0, //Начальное значение времени
               TK, //Конечное значение времени
               HT, //Шаг
               K, //Коэффициент передачи
               T, //Постоянная времени
               F, //Внешнее воздействие
               YS, //Задающее воздействие
               TAY, //Запаздывание
               K1, //Коэффициент усиления пропорциональной составляющей
               K2, //Коэффициент усиления интегральной составляющей
               K3; //Коэффициент усиления дифференциальной составляющей

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public static void CalculateCriterion(ref double k1, ref double k2,ref double k3)
        {
            SimCalculation.CalculatePoints(T0,TK,HT,K,T, F, YS,TAY,
           k1, //K1
           k2, //K2
           k3); //K3
            K1 = k1;
            K2 = k2;
            K3 = k3;
        }

       
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void NmCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WpcheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OpenFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Очистка точек и значений критериев качества
            Eraser.ClearPoints();
            Eraser.ClearIntegralCriterion();

            //Объявление переменных
            T0 =double.Parse(T0textBox.Text.Replace(".", ",")); //T0
            TK =double.Parse(TKtextBox.Text.Replace(".", ",")); //TK
            HT = double.Parse(HTtextBox.Text.Replace(".", ",")); //HT
            K  =double.Parse(KtextBox.Text.Replace(".", ",")); //K
            T = double.Parse(TtextBox.Text.Replace(".", ",")); //T
            F = double.Parse(IndignationtextBox.Text); //F
            YS = double.Parse(targetTextBox.Text); //YS
            TAY = double.Parse(TAYtextBox.Text.Replace(".", ",")); //Tau
            K1 = double.Parse(k1TextBox.Text.Replace(".", ",")); //K1
            K2 =double.Parse(K2textBox.Text.Replace(".", ",")); //K2
            K3 = double.Parse(K3textBox.Text.Replace(".", ",")); //K3

            Func<double, double, double,double> retFunc=null; //делегат, необходимый для передачи в неё функции
            if (HT >= 0.1f * T) //Определение шага моделирования
            {
                HT = 0.1f * T;
                HTtextBox.Text = HT.ToString();
            }

            //передача функции для определения критерия качества
            if (ISEcheckBox.Checked == true)
            {
                retFunc = IntegralCriterion.GetIse; 
            }
            if (ITSEcheckBox.Checked == true)
            {
                retFunc = IntegralCriterion.GetITSE;
            }
            if (IAEcheckBox.Checked == true)
            {
                retFunc = IntegralCriterion.GetIAE;
            }
            if (ITAEcheckBox.Checked == true)
            {
                retFunc = IntegralCriterion.GetITAE;
            }

            if (retFunc!=null)
            {
                if (hgCheckBox.Checked == true)
                {
                    //оптимизация методом Хука-Дживса с использованием критерия качества
                    Optimization.GetResultOptByHG(retFunc,ref K1,ref K2,ref K3,K1checkBox.Checked, K2checkBox.Checked, K3checkBox.Checked);
                }
                k1TextBox.Text = K1.ToString(); //K1
                K2textBox.Text = K2.ToString(); //K2
                K3textBox.Text = K3.ToString(); //K3
            }
            else if (retFunc == null || hgCheckBox.Checked == false)
            {
                //расчёт точек для построения графика переходного процесса
                SimCalculation.CalculatePoints(T0,TK,HT,K,T,F,YS,TAY,K1,K2,K3);
            }
            //вывод на экран результатов подсчитанных критериев качества
            ISEtextBox.Text = SimCalculation.ISE.ToString();
            IAEtextBox.Text = SimCalculation.IAE.ToString();
            ITAEtextBox.Text = SimCalculation.ITAE.ToString();
            ITSEtextBox.Text = SimCalculation.ITSE.ToString();

            //вывод на экран результатов подсчитанных времени регулирования, перерегулирования, колебательности
            regTimeTextBox.Text = QualityCriterion.RegulationTime(Convert.ToDouble(precisionTextBox.Text.Replace(".", ",")), Convert.ToDouble(targetTextBox.Text)).ToString();
            MaxRegtextBox.Text =  QualityCriterion.MaxReregulation(double.Parse(targetTextBox.Text)).ToString();
            OsciltextBox.Text  =  QualityCriterion.Oscillation(Convert.ToDouble(targetTextBox.Text)).ToString();

            if (wpcheckBox.Checked == true || phasecheckBox.Checked == true)
            {
                String fileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName=openFileDialog1.FileName;
                }
                //вывод графиков переходного процесса и/или фазового портрета
                ExcelPrinter.ExcelPrint(Convert.ToInt32(countTextBox.Text), //кол-во точек в Excel
                                     double.Parse(T0textBox.Text),  //начальное время
                                     double.Parse(TKtextBox.Text),  //конечное время
                                     Convert.ToInt32(tcoltextBox.Text) , //ячейка для T
                                     Convert.ToInt32(ycoltextBox.Text) , //ячейка для Y
                                     Convert.ToInt32(x1coltextBox.Text), //ячейка для X1
                                     Convert.ToInt32(x2coltextBox.Text), //ячейка для X2
                                     wpcheckBox.Checked, //Выводится ли на график переходный процесс?
                                     phasecheckBox.Checked, //Выводится ли на график фазовый портрет?
                                     fileName //полный путь к файлу
                 ); //печатаем значения
            }
        }                         
                   
        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Label17_Click(object sender, EventArgs e)
        {

        }
    }
}
