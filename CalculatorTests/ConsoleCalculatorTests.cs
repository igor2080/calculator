using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class ConsoleCalculatorTests
    {
        const string _errorMessage = "Bad Expression";
        private readonly Mock<IProcessor> processorMock = new Mock<IProcessor>();

        [TestMethod]
        public void Calculate_InvalidInputReturnsBadExpression()
        {
            //arrange
            string input = @"1+x+4";
            processorMock.Setup(x => x.GetContent(input)).Returns(new[] { input });
            string actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output.FirstOrDefault();
            });
            var calculator = new ConsoleCalculator(processorMock.Object);

            //act
            calculator.Calculate(input);

            //assert
            Assert.AreEqual(_errorMessage, actualResult);
        }

        [TestMethod]
        public void Calculate_ValidInputCorrectResult()
        {
            //arrange
            string input = @"2+15/3+4*2";
            processorMock.Setup(x => x.GetContent(input)).Returns(new[] { input });
            string actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output.FirstOrDefault();
            });
            var calculator = new ConsoleCalculator(processorMock.Object);

            //act
            calculator.Calculate(input);

            //assert
            Assert.AreEqual("15", actualResult);
        }

        [TestMethod]
        public void Calculate_ValidInputDivideByZeroReturnsDivideByZeroMessage()
        {
            //arrange
            string input = @"5+5/0";
            processorMock.Setup(x => x.GetContent(input)).Returns(new[] { input });
            string actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output.FirstOrDefault();
            });
            var calculator = new ConsoleCalculator(processorMock.Object);

            //act
            calculator.Calculate(input);

            //assert
            Assert.AreEqual("Division by zero", actualResult);
        }

        [TestMethod]
        public void Calculate_InputBracketsBadExpression()
        {
            //arrange
            string input = @"(2+15)/(3+4*2)";
            processorMock.Setup(x => x.GetContent(input)).Returns(new[] { input });
            string actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output.FirstOrDefault();
            });
            var calculator = new ConsoleCalculator(processorMock.Object);

            //act
            calculator.Calculate(input);

            //assert
            Assert.AreEqual(_errorMessage, actualResult);
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
            processorMock.Setup(x => x.GetContent(input)).Returns(new[] { input });
            string actualResult = null;
            processorMock.Setup(x => x.WriteContent(It.IsAny<string>())).Callback((string[] output) =>
            {
                actualResult = output.FirstOrDefault();
            });
            var calculator = new ConsoleCalculator(processorMock.Object);

            //assert
            if (result == null)
            {
                Assert.ThrowsException<FormatException>(() => calculator.Calculate(input));
            }
            else
            {
                calculator.Calculate(input);
                Assert.AreEqual(result, actualResult);
            }
        }
    }
}
