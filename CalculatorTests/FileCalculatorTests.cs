using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        private readonly Mock<IProcessor> processorMock = new Mock<IProcessor>();

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
            string[] input = { @"5/0" };
            processorMock.Setup(x => x.GetContent(_testFileName)).Returns(input);
            string[] actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output;
            });
            var calculator = new FileCalculator(processorMock.Object);

            //act
            calculator.Calculate(_testFileName);

            //assert
            Assert.AreEqual("5/0 = Division by zero", actualResult[0]);
        }

        [TestMethod]
        public void Calculate_FromValidFileValidCalculation()
        {
            //arrange
            string[] input = { @"2+15/3+4*2" };
            processorMock.Setup(x => x.GetContent(_testFileName)).Returns(input);
            string[] actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output;
            });
            var calculator = new FileCalculator(processorMock.Object);

            //act
            calculator.Calculate(_testFileName);

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15", actualResult[0]);
        }

        [TestMethod]
        public void Calculate_FromValidFileValidMultiLineCalculation()
        {
            //arrange
            string[] input = { @"2+15/3+4*2", @"1+2*(3+2)" };
            processorMock.Setup(x => x.GetContent(_testFileName)).Returns(input);
            string[] actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string[]>())).Callback((string[] output) =>
            {
                actualResult = output;
            });
            var calculator = new FileCalculator(processorMock.Object);

            //act
            calculator.Calculate(_testFileName);

            //assert
            Assert.AreEqual("2+15/3+4*2 = 15", actualResult[0]);
            Assert.AreEqual("1+2*(3+2) = 11", actualResult[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Calculate_FromInvalidFile()
        {
            //arrange
            var calculator = new FileCalculator(new FileProcessor());

            //act
            calculator.Calculate(_testFileName);
        }

        [TestMethod]
        public void Calculate_FromValidFileInvalidCalculation()
        {
            //arrange
            string[] input = { @"1+x+4" };            
            processorMock.Setup(x => x.GetContent(_testFileName)).Returns(input);
            string[] actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output;
            });
            var calculator = new FileCalculator(processorMock.Object);

            //act
            calculator.Calculate(_testFileName);

            //assert
            Assert.AreEqual($"1+x+4 = {_errorMessage}", actualResult[0]);
        }
    }
}
