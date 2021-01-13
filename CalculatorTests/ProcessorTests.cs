using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class ProcessorTests
    {
        private IProcessor _processor;
        const string testFileName = "test";
        private readonly StringWriter writer = new StringWriter();

        [TestInitialize]
        public void ProcessorInit()
        {
            _processor = new ConsoleProcessor();
            Console.SetOut(writer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInput_ConsoleNullThrowsException()
        {
            _processor.GetContent(null);
        }

        
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetInput_FileNullThrowsException()
        {
            _processor = new FileProcessor();
            _processor.GetContent(" ");
        }
    }
}
