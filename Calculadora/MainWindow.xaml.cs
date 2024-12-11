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

        private string currentInput = ""; // Almacena la expresión actual
        private CalculadoraCore calculadora = new CalculadoraCore(); // Instanciamos la clase Calculadora

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string buttonContent = button?.Content.ToString();

            // Si el usuario presiona "=" (calcular)
            if (buttonContent == "=")
            {
                try
                {
                    string resultado = calculadora.Evaluar(currentInput); // Llamamos a la clase Calculadora
                    txtDisplay.Text = resultado; // Mostramos el resultado
                    currentInput = resultado; // Almacenamos el resultado para futuras operaciones
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
                    txtDisplay.Text = "Error desconocido: " + ex.Message;
                    currentInput = ""; // Limpiar la entrada actual
                }
            }
            // Si el usuario presiona "C" (borrar)
            else if (buttonContent == "C")
            {
                currentInput = ""; // Limpiamos la entrada
                txtDisplay.Text = "0"; // Reseteamos el display
            }
            // Para cualquier otro botón
            else
            {
                currentInput += buttonContent; // Agregamos el contenido del botón al input actual
                txtDisplay.Text = currentInput; // Mostramos el input en el display
            }
        }
    }
}