using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Calculator calc;
        public Form1()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.DidUpdateValue += calc_DidUpdate;
            calc.InputError += calc_Error;
            calc.CalculationError += calc_Error;
        }

        private void calc_Error(object sender, string e)
        {
            MessageBox.Show(e, "Calculator Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void calc_DidUpdate(Calculator sender,double value, int precision)
        {
            if (precision > 0)
                label1.Text = string.Format("{0:F" + precision + "}", value);
            else
                label1.Text = $"{value}";
        }

        private void button_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            object tag = (sender as Button).Tag;

            int digit;
            if (int.TryParse(text, out digit))
            {
                calc.AddDigit(digit);
            }
            else
            {
                switch (tag)
                {
                    case "decimal":
                        calc.AddDecimalPoint();
                        break;
                    case "evaluate":
                        calc.Compute();
                        break;
                    case "sqrt":
                        calc.SQRT();
                        break;
                    case "reverce":
                        calc.Reverce();
                        break;
                    case "addition":
                        calc.AddOperation(Operation.Add);
                        break;
                    case "substraction":
                        calc.AddOperation(Operation.Sub);
                        break;
                    case "multiplication":
                        calc.AddOperation(Operation.Mul);
                        break;
                    case "division":
                        calc.AddOperation(Operation.Div);
                        break;
                    case "reset":
                        calc.Reset();
                        break;
                    case "clear":
                        calc.Clear();
                        break;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    calc.AddDigit(0);
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    calc.AddDigit(1);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    calc.AddDigit(2);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    calc.AddDigit(3);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    calc.AddDigit(4);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    calc.AddDigit(5);
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    calc.AddDigit(6);
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    calc.AddDigit(7);
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    calc.AddDigit(8);
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    calc.AddDigit(9);
                    break;
            }
        }
    }
}
