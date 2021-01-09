using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class FileProcessor : Processor
    {
        private string _currentFilePath;
        public override void CheckInput(string filepath)
        {
            ValidateInput(filepath);
            ValidateFileExistsOrThrow(filepath);
            SetInput(filepath);
        }

        public override void DoOutput(Func<string, string> calculator)
        {
            if (Input.Length > 0)
            {
                for (int i = 0; i < Input.Length; i++)
                {
                    Input[i] = Input[i] + " = " + calculator(Input[i]);
                }
                WriteFile(_currentFilePath, Input);
            }
            else
            {
                throw new InvalidOperationException("No input was given");
            }
        }

        protected override void SetInput(string input)
        {
            _currentFilePath = input;
            Input = File.ReadAllLines(input);
        }

        private void ValidateFileExistsOrThrow(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given file is invalid or does not exist. ");
            }
        }

        private void WriteFile(string filename, string[] fileContent)
        {
            int extensionLocation = filename.IndexOf('.');
            File.WriteAllLines(filename.Insert(extensionLocation == -1 ? filename.Length : extensionLocation, " result"), fileContent);
        }
    }
}