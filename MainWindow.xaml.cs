using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double num1 = 0;
        double num2 = 0;
        string op = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_num_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string num = button.Content.ToString();

            if (num2 == 0 && txtValue.Text[txtValue.Text.Length - 1] != ',')
            {
                txtValue.Text = num;
            }
            else
            {
                txtValue.Text += num;
            }

            if (op == "eq")
            {
                num1 = 0;
            }

            num2 = double.Parse(txtValue.Text);
        }

        private void btn_op_Click(object sender, RoutedEventArgs e)
        {
            if (op != String.Empty && op != "eq")
            {
                num1 = NextOp();
                num2 = 0;
            }

            Button button = (Button)sender;
            op = button.Content.ToString();

            if (num1 == 0)
            {
                num1 = num2;
            }
            else if (num2 != 0)
            {
                num1 = NextOp();
            }

            num2 = 0;
            txtValue.Text = num1.ToString();
        }


        private void btn_C_Click(object sender, RoutedEventArgs e)
        {
            num1 = 0;
            num2 = 0;

            op = String.Empty;
            txtValue.Text = "0";
        }

        private void btn_CE_Click(object sender, RoutedEventArgs e)
        {
            if (op == "eq")
            {
                num1 = 0;
                op = String.Empty;
            }

            num2 = 0;
            txtValue.Text = "0";
        }

        private void btn_bkspce_Click(object sender, RoutedEventArgs e)
        {
            if (num2 == 0 && op != String.Empty)
            {
                return;
            }

            txtValue.Text = DropLastChar(txtValue.Text);
            num2 = Double.Parse(txtValue.Text);
        }

        private void btn_pm_Click(object sender, RoutedEventArgs e)
        {
            if (num2 != 0)
            {
                num2 *= -1;
                txtValue.Text = num2.ToString();
            }
            else
            {
                num1 *= -1;
                txtValue.Text = num1.ToString();
            }

        }

        private void btn_com_Click(object sender, RoutedEventArgs e)
        {
            if (txtValue.Text.Contains(',') || (op == "eq" && num1 != 0))
            {
                return;
            }

            txtValue.Text += ',';
        }

        private void btn_eq_Click(object sender, RoutedEventArgs e)
        {
            double result = NextOp();
            txtValue.Text = result.ToString();

            op = "eq";
            num1 = result;
            num2 = 0;
        }

        private double Pow(double x, int y)
        {
            if (y == 0)
            {
                return 1;
            }

            if (y % 2 == 0)
            {
                double a = Pow(x, y / 2);
                return a * a;
            }
            else
            {
                return x * Pow(x, y - 1);
            }
        }

        private string DropLastChar(string text)
        {
            if (text.Length == 1)
            {
                return "0";
            }

            text = text.Remove(text.Length - 1, 1);
            if (text[text.Length - 1] == ',')
            {
                text = text.Remove(text.Length - 1, 1);
            }

            return text;
        }

        private double NextOp()
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;

                case "-":
                    return num1 - num2;

                case "*":
                    return num1 * num2;

                case "/":
                    if (num2 != 0)
                    {
                        return num1 / num2;
                    }
                    else
                    {
                        throw new DivideByZeroException();
                    }

                case "min":
                    return Math.Min(num1, num2);

                case "max":
                    return Math.Max(num1, num2);

                case "avg":
                    return (num1 + num2) / 2;

                case "x^y":
                    double a = Pow(num1, Math.Abs((int)num2));
                    return num2 > 0 ? a : 1.0 / a;
            }

            return num2;
        }
    }
}
