using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ConsoleCalculator : Calculator
    {
        protected override string RegexFilter => @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`|\(|\)";

        public ConsoleCalculator()
        {
            inputProcessor = new ConsoleProcessor();
        }

        public override void Calculate(string input)
        {
            input = inputProcessor.GetContent(Console.ReadLine())[0];

            if (operators.Contains(input) || operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
            {
                inputProcessor.WriteContent(new string[] { errorMessage });
                return;
            }

            inputProcessor.WriteContent(new string[] { CalculateString(input) });
        }
    }
}
