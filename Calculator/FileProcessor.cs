using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Calculator
{
    public class FileProcessor : IProcessor
    {
        private string _currentFilePath;

        public string[] GetContent(string input)
        {
            ValidateInput(input);
            _currentFilePath = input;
            return File.ReadAllLines(input);
        }

        public void WriteContent(string[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"Attempt to write nothing into a file.");
            }

            int extensionLocation = _currentFilePath.IndexOf('.');
            string fileName = _currentFilePath.Insert(extensionLocation == -1 ? _currentFilePath.Length : extensionLocation, " result");
            File.WriteAllLines(fileName, data);
        }

        protected void ValidateInput(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'{nameof(text)}' cannot be null or empty", nameof(text));
            }

            if (!File.Exists(text))
            {
                throw new FileNotFoundException("The given file is invalid or does not exist. ");
            }
        }
    }
}
