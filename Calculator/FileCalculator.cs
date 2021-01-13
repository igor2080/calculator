using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class FileCalculator : Calculator
    {
        protected override string RegexFilter => @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`";

        public FileCalculator()
        {
            inputProcessor = new FileProcessor();
        }

        protected string[] GetInput(string fileName)
        {
            return inputProcessor.GetContent(fileName);
        }

        public override void Calculate(string fileName)
        {
            string[] input = this.GetInput(fileName);
            List<string> fileContent = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                fileContent.Add(input[i] + " = " + CalculateLine(input[i]));
            }
            inputProcessor.WriteContent(fileContent.ToArray());
        }

        private string CalculateLine(string input)
        {
            if (input == null)
            {
                throw new InvalidOperationException("No input was given");
            }

            if (operators.Contains(input[0]) || operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
            {
                return errorMessage;
            }

            List<char> splitText = input.ToCharArray().ToList();

            while (splitText.Any(x => x == '(' || x == ')'))
            {
                int openBracePosition = splitText.LastIndexOf('(');

                if (openBracePosition == -1)
                {//Input contains imbalanced braces
                    return errorMessage;
                }

                int closeBracePosition = splitText.IndexOf(')', openBracePosition);

                if (openBracePosition == -1 || closeBracePosition == -1)
                {//Input contains imbalanced braces
                    return errorMessage;
                }
                else
                {
                    List<char> splitRange = splitText.GetRange(openBracePosition + 1, closeBracePosition - openBracePosition - 1);
                    string splitRangeString = new string(splitRange.ToArray());
                    string calculationResult = CalculateLine(splitRangeString);
                    splitText.RemoveRange(openBracePosition, closeBracePosition - openBracePosition + 1);
                    splitText.InsertRange(openBracePosition, calculationResult);
                }
            }

            string calculationResultString = CalculateString(new string(splitText.ToArray()));
            splitText = calculationResultString?.ToCharArray().ToList();
            return new string(splitText?.ToArray());
        }
    }
}
