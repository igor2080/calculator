using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Calculator
{
    public class BracketCalculator : Calculator
    {
        protected override string CalculateInput(string input)
        {
            if (input == null)
            {
                throw new InvalidOperationException("No input was given");
            }

            if (Operators.Contains(input[0]) || Operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`"))
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
                    string calculationResult = CalculateInput(new string(splitText.GetRange(openBracePosition + 1, closeBracePosition - openBracePosition - 1).ToArray()));
                    splitText.RemoveRange(openBracePosition, closeBracePosition - openBracePosition + 1);
                    splitText.InsertRange(openBracePosition, calculationResult);
                }
            }

            splitText = CalculateString(new string(splitText.ToArray()))?.ToCharArray().ToList();
            return new string(splitText?.ToArray());

        }
    }
}