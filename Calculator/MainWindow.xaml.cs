namespace Calculator
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            resultWindow.Text += button.Content.ToString();
        }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string @operator = "";
                int firstNumberLength = 0;

                if (resultWindow.Text.Contains("+")) 
                {
                    firstNumberLength = resultWindow.Text.IndexOf("+");
                }
                else if (resultWindow.Text.Contains("-"))
                {
                    firstNumberLength = resultWindow.Text.IndexOf("-");
                }
                else if (resultWindow.Text.Contains("*"))
                {
                    firstNumberLength = resultWindow.Text.IndexOf("*");
                }
                else if (resultWindow.Text.Contains("/"))
                {
                    firstNumberLength = resultWindow.Text.IndexOf("/");
                }

                @operator = resultWindow.Text.Substring(firstNumberLength, 1); 

                double firstNumber = double.Parse(resultWindow.Text.Substring(0, firstNumberLength));
                double secondNumber = double.Parse(resultWindow.Text.Substring(firstNumberLength + 1, resultWindow.Text.Length - firstNumberLength - 1));

                if (@operator == "+")
                {
                    resultWindow.Text += "=" + (firstNumber + secondNumber);
                }
                else if (@operator == "-")
                {
                    resultWindow.Text += "=" + (firstNumber - secondNumber);
                }
                else if (@operator == "*")
                {
                    resultWindow.Text += "=" + (firstNumber * secondNumber);
                }
                else
                {
                    resultWindow.Text += "=" + (firstNumber / secondNumber);
                }
            }
            catch
            {
                resultWindow.Text = "INVALID INPUT!";
            }
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            resultWindow.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
