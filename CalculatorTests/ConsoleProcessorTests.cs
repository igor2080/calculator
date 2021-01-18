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
    public class ConsoleProcessorTests
    {
        private readonly ConsoleProcessor _processor = new ConsoleProcessor();
        private readonly StringWriter _writer = new StringWriter();

        [TestInitialize]
        public void ProcessorInit()
        {
            Console.SetOut(_writer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInput_ConsoleNullThrowsException()
        {
            _processor.GetContent(null);
        }

        [TestMethod]
        public void GetInput_CorrectContent()
        {
            //arrange
            string input = @"1+x+4";
            Console.SetIn(new StringReader(input));

            //act
            string actualResult = _processor.GetContent(null)[0];

            //assert
            Assert.AreEqual(input, actualResult);
        }
    }
}
