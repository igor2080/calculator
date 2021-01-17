using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class FileProcessorTests
    {
        private readonly FileProcessor _processor = new FileProcessor();

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetInput_NullThrowsException()
        {
            _processor.GetContent(" ");
        }
    }
}
