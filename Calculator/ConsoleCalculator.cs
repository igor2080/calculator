using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ConsoleCalculator : Calculator
    {
        public ConsoleCalculator()
        {
            inputProcessor = new ConsoleProcessor();
        }
        protected override string RegexFilter => @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`|\(|\)";

        protected string GetInput()
        {
            return inputProcessor.GetContent(Console.ReadLine())[0];
        }

        public override void Calculate(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                input = this.GetInput();
                
            }
            else
            {
                throw new InvalidOperationException("No input was given");
            }

            

            if (operators.Contains(input) || operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
            {
                inputProcessor.WriteContent(new string[] { errorMessage });
                return;
            }

            inputProcessor.WriteContent(new string[] { CalculateString(input) });
        }
    }
}
