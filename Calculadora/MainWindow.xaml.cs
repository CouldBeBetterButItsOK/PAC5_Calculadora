using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string currentInput = "";
        private CalculadoraCore calculadora = new CalculadoraCore(); 

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string buttonContent = button?.Content.ToString();

            if (buttonContent == "=")
            {
                try
                {
                    string resultado = calculadora.Evaluar(currentInput);
                    txtDisplay.Text = resultado;
                    currentInput = resultado;
                }
                catch (FormatException ex)
                {
                    txtDisplay.Text = ex.Message;
                    currentInput = "";
                }
                catch (InvalidOperationException ex)
                {
                    txtDisplay.Text = ex.Message;
                    currentInput = "";
                }
                catch (DivideByZeroException ex)
                {
                    txtDisplay.Text = ex.Message;
                    currentInput = "";
                }
                catch (Exception ex)
                {
                    txtDisplay.Text = ex.Message;
                    currentInput = "";
                }
            }
            else if (buttonContent == "C")
            {
                currentInput = "";
                txtDisplay.Text = "0";
            }
            else
            {
                currentInput += buttonContent;
                txtDisplay.Text = currentInput;
            }
        }
    }
}