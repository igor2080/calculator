using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CalculatorTests
{
    [TestClass]
    public class UtilsTests
    {
        private readonly Utils _utils = new Utils();

        [TestInitialize]
        public void UtilsInit()
        {
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"2+15/3+4*2");
            }
        }

        [TestCleanup]
        public void UtilsClean()
        {
            File.Delete("test");
            File.Delete("test result");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateString_NullThrowsException()
        {
            _utils.ValidateString(null);
        }

        [TestMethod]
        public void ValidateString_ValidStringTrue()
        {
            //arrange
            string input = @"2+15/3+4*2";

            //act
            bool result = _utils.ValidateString(input);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateString_InvalidStringFalse()
        {
            //arrange
            string input = @"1+x+4";

            //act
            bool result = _utils.ValidateString(input);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReadFile_ReturnsContent()
        {
            //arrange
            
            //act
            string[] result = _utils.ReadFile("test");

            //assert
            Assert.AreEqual(@"2+15/3+4*2", result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFile_InvalidFile()
        {
            string[] result = _utils.ReadFile("no");

        }

        [TestMethod]
        public void WriteFile_MakesFile()
        {
            //arrange
            using (StreamWriter writer = File.CreateText("test"))
            {
                writer.WriteLine(@"2+15/3+4*2");
            }

            //act
            _utils.WriteFile("test", new string[] { "text" });

            //assert
            Assert.IsTrue(File.Exists("test result"));
        }

    }
}
