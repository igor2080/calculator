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
            _calculator = new ConsoleCalculator();
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
        public void GetInput_ConsoleNullThrowsException()
        {            
            Console.SetIn(new StringReader(null));
            _calculator.GetInput();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetInput_FileNullThrowsException()
        {
            _calculator = new FileCalculator();
            _calculator.GetInput();
        }

        [TestMethod]
        public void Calculate_InvalidInputReturnsBadExpression()
        {
            //arrange
            string input = @"1+x+4";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(_calculator.GetInput());

            //assert
            Assert.AreEqual(errorMessage + "\r\n", writer.ToString());
        }

        [TestMethod]
        public void Calculate_ValidInputCorrectResult()
        {
            //arrange
            string input = @"2+15/3+4*2";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(_calculator.GetInput());

            //assert
            Assert.AreEqual("15\r\n", writer.ToString());
        }

        [TestMethod]
        public void Calculate_ValidInputDivideByZeroReturnsDivideByZeroMessage()
        {
            //arrange
            string input = @"5+5/0";
            Console.SetIn(new StringReader(input));


            //act
            _calculator.Calculate(_calculator.GetInput());

            //assert
            Assert.AreEqual("Division by zero\r\n", writer.ToString());
        }

        [TestMethod]
        public void Calculate_ConsoleInputBracketsBadExpression()
        {
            //arrange
            string input = @"(2+15)/(3+4*2)";
            Console.SetIn(new StringReader(input));

            //act
            _calculator.Calculate(_calculator.GetInput());

            //assert
            Assert.AreEqual(errorMessage + "\r\n", writer.ToString());
        }

        [TestMethod]
        public void Calculate_FromValidFileValidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
            }

            //act
            Program.Main(new string[] { testFileName });
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n", result);
        }

        [TestMethod]
        public void Calculate_FromValidFileValidMultiLineCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
                writer.WriteLine(@"1+2*(3+2)");
            }

            //act
            Program.Main(new string[] { testFileName });
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n1+2*(3+2) = 11\r\n", result);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Calculate_FromInvalidFile()
        {
            Program.Main(new string[] { "no" });
        }

        [TestMethod]
        public void Calculate_FromValidFileInvalidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"1+x+4");
            }

            //act
            Program.Main(new string[] { testFileName });
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual($"1+x+4 = {errorMessage}\r\n", result);
        }

        [TestMethod]
        public void Calculate_FromValidFileDivisionByZero()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"5/0");
            }

            //act
            Program.Main(new string[] { testFileName });
            string result = File.ReadAllText(testFileName + " result");

            //assert
            Assert.AreEqual("5/0 = Division by zero\r\n", result);
        }


    }
}
