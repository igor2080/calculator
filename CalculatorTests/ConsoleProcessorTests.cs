using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
