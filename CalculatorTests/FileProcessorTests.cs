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
        const string _testFileName = "test";

        [TestCleanup]
        public void CalculatorClean()
        {
            File.Delete(_testFileName);
            File.Delete(_testFileName + " result");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetInput_NullThrowsException()
        {
            _processor.GetContent(" ");
        }

        [TestMethod]
        public void GetInput_CorrectContent()
        {
            //arrange
            using (StreamWriter writer = File.CreateText(_testFileName))
            {
                writer.WriteLine(@"2+15/3+4*2");
                writer.WriteLine(@"12345");
                writer.WriteLine(@"e");
            }
            string[] expectedResult = { @"2+15/3+4*2", @"12345", @"e" };

            //act
            string[] actualResult = _processor.GetContent(_testFileName);

            //assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
