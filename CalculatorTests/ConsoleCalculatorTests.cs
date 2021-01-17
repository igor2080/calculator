using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class ConsoleCalculatorTests
    {
        const string _errorMessage = "Bad Expression";
        private readonly StringWriter _writer = new StringWriter();
        private readonly ConsoleCalculator _calculator = new ConsoleCalculator();

        [TestInitialize]
        public void CalculatorInit()
        {
            Console.SetOut(_writer);
        }

        [TestMethod]
        public void Calculate_InvalidInputReturnsBadExpression()
        {
            //arrange
            string input = @"1+x+4";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(null);

            //assert
            Assert.AreEqual(_errorMessage + "\r\n", _writer.ToString());
        }

        [TestMethod]
        public void Calculate_ValidInputCorrectResult()
        {
            //arrange
            string input = @"2+15/3+4*2";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(null);

            //assert
            Assert.AreEqual("15\r\n", _writer.ToString());
        }

        [TestMethod]
        public void Calculate_ValidInputDivideByZeroReturnsDivideByZeroMessage()
        {
            //arrange
            string input = @"5+5/0";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(null);

            //assert
            Assert.AreEqual("Division by zero\r\n", _writer.ToString());
        }

        [TestMethod]
        public void Calculate_InputBracketsBadExpression()
        {
            //arrange
            string input = @"(2+15)/(3+4*2)";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(null);

            //assert
            Assert.AreEqual(_errorMessage + "\r\n", _writer.ToString());
        }

        [DataTestMethod]
        [DataRow(@"-2+5", "3")]
        [DataRow(@"(-3+5)2", _errorMessage)]
        [DataRow(@"225-", null)]
        [DataRow(@"3526412549563214598", "3526412549563214598")]
        [DataRow(@"0+5", "5")]
        [DataRow(@"/57", null)]
        [DataRow(@"+2-1", "1")]
        public void Calculate_DifferentResults(string input, string result)
        {
            //arrange
            Console.SetIn(new StringReader(input));

            //assert
            if (result == null)
            {
                Assert.ThrowsException<FormatException>(() => _calculator.Calculate(null));
            }
            else
            {
                _calculator.Calculate(null);
                Assert.AreEqual(result + "\r\n", _writer.ToString());
            }

        }
    }
}
