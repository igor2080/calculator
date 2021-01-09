using Calculator;
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
        const string errorMessage = "Bad Expression";
        const string testFileName = "test";
        private readonly StringWriter writer = new StringWriter();
        private Calculator.Calculator _calculator;

        [TestInitialize]
        public void CalculatorInit()
        {
            _calculator = new BracketCalculator();
            _calculator.inputProcessor = new ConsoleProcessor();
            Console.SetOut(writer);
        }

        [TestCleanup]
        public void CalculatorClean()
        {
            File.Delete(testFileName);
            File.Delete(testFileName + " result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadInput_ConsoleNullThrowsException()
        {
            _calculator.ReadInput(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadInput_FileNullThrowsException()
        {
            _calculator.inputProcessor = new FileProcessor();
            _calculator.ReadInput(null);
        }

        [TestMethod]
        public void CalculateInput_InvalidInputReturnsBadExpression()
        {
            //arrange
            string input = @"1+x+4";
            _calculator.ReadInput(input);

            //act
            _calculator.GetResult();

            //assert
            Assert.AreEqual(errorMessage + "\r\n", writer.ToString());
        }

        [TestMethod]
        public void CalculateInput_ValidInputCorrectResult()
        {
            //arrange
            string input = @"2+15/3+4*2";
            _calculator.ReadInput(input);

            //act
            _calculator.GetResult();

            //assert
            Assert.AreEqual("15\r\n", writer.ToString());
        }

        [TestMethod]
        public void CalculateInput_ValidInputDivideByZeroReturnsDivideByZeroMessage()
        {
            //arrange
            string input = @"5+5/(5-5)";
            _calculator.ReadInput(input);

            //act
            _calculator.GetResult();

            //assert
            Assert.AreEqual("Division by zero\r\n", writer.ToString());
        }

        [TestMethod]
        public void CalculateInput_ConsoleInputBracketsBadExpression()
        {
            //arrange
            string input = @"(2+15)/(3+4*2)";
            _calculator = new BracketlessCalculator();
            _calculator.inputProcessor = new ConsoleProcessor();
            _calculator.ReadInput(input);

            //act
            _calculator.GetResult();

            //assert
            Assert.AreEqual(errorMessage + "\r\n", writer.ToString());
        }

        [TestMethod]
        public void CalculateInput_FromValidFileValidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
            }

            _calculator.inputProcessor = new FileProcessor();
            _calculator.ReadInput(testFileName);

            //act
            _calculator.GetResult();
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n", result);
        }

        [TestMethod]
        public void CalculateInput_FromValidFileValidMultiLineCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
                writer.WriteLine(@"1+2*(3+2)");
            }

            _calculator.inputProcessor = new FileProcessor();
            _calculator.ReadInput(testFileName);

            //act
            _calculator.GetResult();
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n1+2*(3+2) = 11\r\n", result);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CalculateInput_FromInvalidFile()
        {
            _calculator.inputProcessor = new FileProcessor();
            _calculator.ReadInput(testFileName);
        }

        [TestMethod]
        public void CalculateInput_FromValidFileInvalidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"1+x+4");
            }

            _calculator.inputProcessor = new FileProcessor();
            _calculator.ReadInput(testFileName);

            //act
            _calculator.GetResult();
            string result = File.ReadAllText(testFileName + " result");

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

            _calculator.inputProcessor = new FileProcessor();
            _calculator.ReadInput(testFileName);

            //act
            _calculator.GetResult();
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual("5/0 = Division by zero\r\n", result);
        }


    }
}
