namespace Calculator
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class MainWindow : Window
    {
        private bool newNumbers = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void KeyDown_(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {             
                case Key.NumPad0:
                    ResultWindow.Text += "0";
                    break;
                case Key.NumPad1:
                    ResultWindow.Text += "1";
                    break;
                case Key.NumPad2:
                    ResultWindow.Text += "2";
                    break;
                case Key.NumPad3:
                    ResultWindow.Text += "3";
                    break;
                case Key.NumPad4:
                    ResultWindow.Text += "4";
                    break;
                case Key.NumPad5:
                    ResultWindow.Text += "5";
                    break;
                case Key.NumPad6:
                    ResultWindow.Text += "6";
                    break;
                case Key.NumPad7:
                    ResultWindow.Text += "7";
                    break;
                case Key.NumPad8:
                    ResultWindow.Text += "8";
                    break;
                case Key.NumPad9:
                    ResultWindow.Text += "9";
                    break;
                case Key.Multiply:
                    ResultWindow.Text += "*";
                    break;
                case Key.Divide:
                    ResultWindow.Text += "/";
                    break;
                case Key.Subtract:
                    ResultWindow.Text += "-";
                    break;
                case Key.OemPlus:
                    ResultWindow.Text += "+";
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (newNumbers) ResultWindow.Text = "";
            newNumbers = false;

            ResultWindow.Text += button.Content.ToString();
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string @operator = "";

                int firstNumberLength = GetFirstNumberLength();

                @operator = ResultWindow.Text.Substring(firstNumberLength, 1);

                double firstNumber = double.Parse(ResultWindow.Text.Substring(0, firstNumberLength));
                double secondNumber = double.Parse(ResultWindow.Text.Substring(firstNumberLength + 1, ResultWindow.Text.Length - firstNumberLength - 1));

                GetResult(@operator, firstNumber, secondNumber);

                newNumbers = true;
            }
            catch
            {
                ResultWindow.Text = "INVALID INPUT!";
            }
        }

        private int GetFirstNumberLength()
        {
            int firstNumberLength = 0;

            if (ResultWindow.Text.Contains("+"))
            {
                firstNumberLength = ResultWindow.Text.IndexOf("+");
            }
            else if (ResultWindow.Text.Contains("*"))
            {
                firstNumberLength = ResultWindow.Text.IndexOf("*");
            }
            else if (ResultWindow.Text.Contains("/"))
            {
                firstNumberLength = ResultWindow.Text.IndexOf("/");
            }
            else if (ResultWindow.Text.Contains("%"))
            {
                firstNumberLength = ResultWindow.Text.IndexOf("%");
            }
            else if (ResultWindow.Text.Contains("-"))
            {
                firstNumberLength = ResultWindow.Text[ResultWindow.Text.LastIndexOf("-") - 1].ToString() == "-" ? ResultWindow.Text.LastIndexOf("-") - 1 : ResultWindow.Text.LastIndexOf("-");
            }

            return firstNumberLength;
        }

        private void GetResult(string @operator, double firstNumber, double secondNumber)
        {
            switch (@operator)
            {
                case "+": ResultWindow.Text = (firstNumber + secondNumber).ToString(); break;
                case "-": ResultWindow.Text = (firstNumber - secondNumber).ToString(); break;
                case "*": ResultWindow.Text = (firstNumber * secondNumber).ToString(); break;
                case "/": ResultWindow.Text = (firstNumber / secondNumber).ToString(); break;
                case "%": ResultWindow.Text = ((firstNumber * secondNumber) / 100).ToString(); break;
            }
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow.Text = "";
        }
    }
}
