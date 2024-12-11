using Calculadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PAC5_Calculadora.Tests
{
    public class CalculadoraCoreTests
    {
        private readonly CalculadoraCore _calculadora;

        public CalculadoraCoreTests()
        {
            _calculadora = new CalculadoraCore();
        }

        [Fact]
        public void TestSuma_Success()
        {
            var resultat = _calculadora.Evaluar("5 + 3");
            Assert.Equal("8", resultat);
        }
        
        [Fact]
        public void TestResta_Success()
        {
            var resultat = _calculadora.Evaluar("5 - 3");
            Assert.Equal("2", resultat);
        }

        [Fact]
        public void TestMultiplicacio_Success()
        {
            var resultat = _calculadora.Evaluar("5 * 3");
            Assert.Equal("15", resultat);
        }
        
        [Fact]
        public void TestDivisio_Success()
        {
            var resultat = _calculadora.Evaluar("5 / 2");
            Assert.Equal("2,5", resultat);
        }

        [Fact]
        public void TestDivisio_PerZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculadora.Evaluar("5 / 0"));
        }
        [Fact]
        public void Test_01() { 
            var resultat = _calculadora.Evaluar("5 + 3 * 2 - 4 / 2");
            Assert.Equal("9", resultat);
        }
        [Fact]
        public void Test_02()
        {
            var resultat = _calculadora.Evaluar("2 ^ 3");
            Assert.Equal("8", resultat);
        }
        [Fact]
        public void Test_03()
        {
            var resultat = _calculadora.Evaluar("0 ^ 5");
            Assert.Equal("0", resultat);
        }
        [Fact]
        public void Test_04()
        {
            var resultat = _calculadora.Evaluar("√ 16");
            Assert.Equal("4", resultat);
        }
        [Fact]
        public void Test_05()
        {
            var resultat = _calculadora.Evaluar("√ 0");
            Assert.Equal("0", resultat);
        }
        [Fact]
        public void Test_06()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("3√ 6"));
            
        }
        [Fact]
        public void Test_07()
        {
            var resultat = _calculadora.Evaluar("3 * √ 9");
            Assert.Equal("9", resultat);
        }
        [Fact]
        public void Test_08()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("√ -4"));
            
        }
        [Fact]
        public void Test_09()
        {
            var resultat = _calculadora.Evaluar("( 5 + 3 ) * 2");
            Assert.Equal("16", resultat);
        }
        [Fact]
        public void Test_10()
        {
            var resultat = _calculadora.Evaluar("( ( 2 + 3 ) * 2 )");
            Assert.Equal("10", resultat);
        }
        [Fact]
        public void Test_11()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("5 + ( 3 * 2"));
            
        }
        [Fact]
        public void Test_12()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("5 + 3 ) * 2"));
            
        }
        [Fact]
        public void Test_13()
        {
            var resultat = _calculadora.Evaluar("3 + 5 * ( 2 ^ 3 ) - √ 16 ");
            Assert.Equal("39", resultat);
        }
        [Fact]
        public void Test_14()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("2(8 + 2)"));
            
        }
        [Fact]
        public void Test_15()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("(8 + 2)2"));

        }
        [Fact]
        public void Test_16()
        {
            var resultat = _calculadora.Evaluar("3 + 5 * ( 2 ^ 3 ) - √ 16 ");
            Assert.Equal("39", resultat);
        }
        [Fact]
        public void Test_17()
        {
            Assert.Throws<FormatException>(() => _calculadora.Evaluar("(5)3 + 2"));

        }
        [Fact]
        public void Test_18()
        {
            var resultat = _calculadora.Evaluar("5        * 3 + 2");
            Assert.Equal("17", resultat);
        }
    }
}
