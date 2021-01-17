using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class FileCalculatorTests
    {
        const string _testFileName = "test";
        const string _errorMessage = "Bad Expression";

        [TestCleanup]
        public void CalculatorClean()
        {
            File.Delete(_testFileName);
            File.Delete(_testFileName + " result");
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
            Program.Main(new string[] { _testFileName });
            string result = File.ReadAllText(_testFileName + " result");

            //assert
            Assert.AreEqual("5/0 = Division by zero\r\n", result);
        }

        [TestMethod]
        public void Calculate_FromValidFileValidCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(_testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
            }

            //act
            Program.Main(new string[] { _testFileName });
            string result = File.ReadAllText(_testFileName + " result");

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15\r\n", result);
        }

        [TestMethod]
        public void Calculate_FromValidFileValidMultiLineCalculation()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(_testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
                writer.WriteLine(@"1+2*(3+2)");
            }

            //act
            Program.Main(new string[] { _testFileName });
            string result = File.ReadAllText(_testFileName + " result");

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
            Program.Main(new string[] { _testFileName });
            string result = File.ReadAllText(_testFileName + " result");

            //assert
            Assert.AreEqual($"1+x+4 = {_errorMessage}\r\n", result);
        }
    }
}
