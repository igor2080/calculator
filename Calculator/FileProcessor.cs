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
            ValidateFileExistsOrThrow(input);
            _currentFilePath = input;
            return File.ReadAllLines(input);
        }

        public void WriteContent(string[] data)
        {
            int extensionLocation = _currentFilePath.IndexOf('.');
            File.WriteAllLines(_currentFilePath.Insert(extensionLocation == -1 ? _currentFilePath.Length : extensionLocation, " result"), data);
        }

        protected void ValidateInput(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'{nameof(text)}' cannot be null or empty", nameof(text));
            }
        }
        private void ValidateFileExistsOrThrow(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given file is invalid or does not exist. ");
            }
        }
    }
}
