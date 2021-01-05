using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Utils
    {
        const string Operators = "+_*/";
        public bool ValidateString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'{nameof(text)}' cannot be null or empty", nameof(text));
            }
            else if (Operators.Contains(text[0]) || Operators.Contains(text[text.Length - 1]))
            {
                return false;
            }

            return !Regex.IsMatch(text, @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`");
        }

        public string[] ReadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllLines(fileName);
            }

            throw new FileNotFoundException("The file doesn't exist");
        }

        public void WriteFile(string filename, string[] fileContent)
        {
            int extensionLocation = filename.IndexOf('.');
            File.WriteAllLines(filename.Insert(extensionLocation == -1 ? filename.Length : extensionLocation, " result"), fileContent);
        }

       
    }
}
