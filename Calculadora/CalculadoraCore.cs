using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculadora
{
    public class CalculadoraCore
    {
        public string Evaluar(string expresion)
        {
            expresion = expresion.Replace(",", ".");
            // Validar la expresión antes de procesarla
            if (!ValidarExpresion(expresion))
            {
                throw new FormatException("Syntax Error");
            }

            
           
                // Convertir a notación postfija
                var postfija = ConvertirAPostfija(expresion);

                // Evaluar la notación postfija
                var resultado = EvaluarPostfija(postfija);

                return resultado.ToString();
            
        }

        private bool ValidarExpresion(string expresion)
        {
            // Eliminar espacios en blanco
            expresion = expresion.Replace(" ", "");

            // Validar caracteres permitidos
            if (!Regex.IsMatch(expresion, "^[0-9+\\-*/^()√.]+$"))
            {
                return false;
            }

            // Validar paréntesis balanceados
            int balance = 0;
            foreach (var c in expresion)
            {
                if (c == '(') balance++;
                else if (c == ')') balance--;

                if (balance < 0) return false; // Paréntesis cerrados sin abrir
            }
            if (balance != 0) return false; // Paréntesis desbalanceados

            // Validar operadores consecutivos o al inicio/final
            if (Regex.IsMatch(expresion, "[+*/^]{2,}") ||
                Regex.IsMatch(expresion, "^[+*/^]") ||
                Regex.IsMatch(expresion, "[+\\-*/^]$"))
            {
                return false;
            }

            return true;
        }

        private List<string> ConvertirAPostfija(string expresion)
        {
            var salida = new List<string>();
            var operadores = new Stack<char>();

            for (int i = 0; i < expresion.Length; i++)
            {
                char c = expresion[i];

                if (char.IsDigit(c) || c == '.')
                {
                    string numero = c.ToString();
                    bool puntoEncontrado = c == '.';

                    while (i + 1 < expresion.Length && (char.IsDigit(expresion[i + 1]) || expresion[i + 1] == '.'))
                    {
                        if (expresion[i + 1] == '.')
                        {
                            if (puntoEncontrado) throw new FormatException("Syntax Error");
                            puntoEncontrado = true;
                        }
                        numero += expresion[++i];
                    }
                    salida.Add(numero);
                }
                else if (c == '(')
                {
                    operadores.Push(c);
                }
                else if (c == ')')
                {
                    while (operadores.Peek() != '(')
                    {
                        salida.Add(operadores.Pop().ToString());
                    }
                    operadores.Pop();
                }
                else if (EsOperador(c))
                {
                    if (c == '-' && (i == 0 || expresion[i - 1] == '(' || EsOperador(expresion[i - 1])))
                    {
                        string numeroNegativo = "-";
                        while (i + 1 < expresion.Length && (char.IsDigit(expresion[i + 1]) || expresion[i + 1] == '.'))
                        {
                            numeroNegativo += expresion[++i];
                        }
                        salida.Add(numeroNegativo);
                        continue;
                    }

                    while (operadores.Count > 0 && Prioridad(operadores.Peek()) >= Prioridad(c))
                    {
                        salida.Add(operadores.Pop().ToString());
                    }
                    operadores.Push(c);
                }
            }

            while (operadores.Count > 0)
            {
                salida.Add(operadores.Pop().ToString());
            }

            return salida;
        }

        private double EvaluarPostfija(List<string> postfija)
        {
            var pila = new Stack<double>();

            foreach (var token in postfija)
            {
                if (double.TryParse(token, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double numero))
                {
                    pila.Push(numero);
                }
                else if (EsOperador(token[0]))
                {
                    if (token[0] == '√') // Raíz cuadrada
                    {
                        if (pila.Count < 1) throw new FormatException("Syntax Error");
                        double a = pila.Pop();
                        if (a < 0) throw new InvalidOperationException("Negative Square Root");
                        pila.Push(Math.Sqrt(a));
                    }
                    else
                    {
                        if (pila.Count < 2) throw new FormatException("Syntax Error");
                        double b = pila.Pop();
                        double a = pila.Pop();
                        if (token[0] == '/' && b == 0) throw new DivideByZeroException("Cannot divide by zero");
                        pila.Push(Calcular(a, b, token[0]));
                    }
                }
            }

            if (pila.Count != 1) throw new FormatException("Syntax Error");
            return pila.Pop();
        }

        private static double Calcular(double a, double b, char operador)
        {
            return operador switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                '^' => Math.Pow(a, b),
                _ => throw new FormatException("Operador desconocido")
            };
        }

        private static bool EsOperador(char c)
        {
            return "+-*/^√".Contains(c);
        }

        private static int Prioridad(char operador)
        {
            return operador switch
            {
                '+' or '-' => 1,
                '*' or '/' => 2,
                '^' or '√' => 3,
                _ => 0
            };
        }
    }

    public static class Extensions
    {
        public static bool StartsWithAny(this string input, params string[] values)
        {
            return values.Any(input.StartsWith);
        }

        public static bool EndsWithAny(this string input, params string[] values)
        {
            return values.Any(input.EndsWith);
        }
    }

}
