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

        public override void Calculate(string fileName)
        {
            string[] input = this.inputProcessor.GetContent(fileName);
            string[] fileContent = new string[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                fileContent[i] = input[i] + " = " + CalculateLine(input[i]);
            }

            inputProcessor.WriteContent(fileContent.ToArray());
        }

        private string CalculateLine(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }

            if (operators.Contains(input[0]) || operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
            {
                return errorMessage;
            }

            while (input.Any(x => x == '(' || x == ')'))
            {
                int openBracePosition = input.LastIndexOf('(');

                if (openBracePosition == -1)
                {//Input contains imbalanced braces
                    return errorMessage;
                }

                int closeBracePosition = input.IndexOf(')', openBracePosition);

                if (closeBracePosition == -1)
                {//Input contains imbalanced braces
                    return errorMessage;
                }
                else
                {                    
                    string splitRange = input.Substring(openBracePosition + 1, closeBracePosition - openBracePosition - 1);
                    string calculationResult = CalculateLine(splitRange);                    
                    input=input.Remove(openBracePosition, closeBracePosition - openBracePosition + 1);
                    input= input.Insert(openBracePosition, calculationResult);                    
                }
            }
            
            input = CalculateString(input);
            return input;
        }
    }
}
