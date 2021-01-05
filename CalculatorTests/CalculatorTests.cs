using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        private readonly Calculator.Calculator _calculator = new Calculator.Calculator();
        const string errorMessage = "Bad Expression";

        [TestMethod]
        public void CalculateInput_NullReturnsNull()
        {
            //arrange
            string input = null;

            //act
            string result = _calculator.CalculateInput(input);

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CalculateInput_InvalidInputReturnsBadExpression()
        {
            //arrange
            string input = @"1+x+4";

            //act
            string result = _calculator.CalculateInput(input);

            //assert
            Assert.AreEqual(errorMessage, result);
        }

        [TestMethod]
        public void CalculateInput_ValidInputCorrectResult()
        {
            //arrange
            string input = @"2+15/3+4*2";

            //act
            string result = _calculator.CalculateInput(input);

            //assert
            Assert.AreEqual("15", result);
        }

        [TestMethod]
        public void CalculateInput_ValidInputDivideByZeroReturnsDivideByZeroMessage()
        {
            //arrange
            string input = @"5+5/(5-5)";

            //act
            string result = _calculator.CalculateInput(input);

            //assert
            Assert.AreEqual("Division by zero", result);
        }

        [TestMethod]
        public void CalculateInput_FromValidFileValidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"2+15/3+4*2");
            }

            //act
            Calculator.Program.Main(new string[] { "test" });
            File.Delete("test");
            string result = File.ReadAllText("test result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n", result);
        }

        [TestMethod]
        public void CalculateInput_FromValidFileValidMultiLineCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"2+15/3+4*2");
                writer.WriteLine(@"1+2*(3+2)");
            }

            //act
            Calculator.Program.Main(new string[] { "test" });
            File.Delete("test");
            string result = File.ReadAllText("test result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n1+2*(3+2) = 11\r\n", result);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CalculateInput_FromInvalidFile()
        {
            Calculator.Program.Main(new string[] { "test" });
        }

        [TestMethod]
        public void CalculateInput_FromValidFileInvalidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"1+x+4");
            }

            //act
            Calculator.Program.Main(new string[] { "test" });
            File.Delete("test");
            string result = File.ReadAllText("test result");

            //assert
            Assert.AreEqual($"1+x+4 = {errorMessage}\r\n", result);
        }

        [TestMethod]
        public void CalculateInput_FromValidFileDivisionByZero()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"5/0");
            }

            //act
            Calculator.Program.Main(new string[] { "test" });
            File.Delete("test");
            string result = File.ReadAllText("test result");

            //assert
            Assert.AreEqual("5/0 = Division by zero\r\n", result);
        }
    }
}
