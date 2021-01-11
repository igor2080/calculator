using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class FileCalculator : Calculator
    {
        protected override string RegexFilter { get { return @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`"; } }

        public FileCalculator()
        {
            inputProcessor = new FileProcessor();
        }

        public override string[] GetInput()
        {
            if (Program.programArgs.Length>0)
            {
                return inputProcessor.GetContent(Program.programArgs[0]);
            }

            return null;
        }

        public override void Calculate(string[] input)
        {
            List<string> fileContent = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                fileContent.Add(input[i] + " = " + Calculate(input[i]));
            }
            inputProcessor.WriteContent(fileContent.ToArray());
        }

        private string Calculate(string input)
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
                    string calculationResult = Calculate(new string(splitText.GetRange(openBracePosition + 1, closeBracePosition - openBracePosition - 1).ToArray()));
                    splitText.RemoveRange(openBracePosition, closeBracePosition - openBracePosition + 1);
                    splitText.InsertRange(openBracePosition, calculationResult);
                }
            }

            splitText = CalculateString(new string(splitText.ToArray()))?.ToCharArray().ToList();
            return new string(splitText?.ToArray());
        }
    }
}
