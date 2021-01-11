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
        protected override string RegexFilter { get { return @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`|\(|\)"; } }

        public override string[] GetInput()
        {
            return inputProcessor.GetContent(Console.ReadLine());
        }

        public override void Calculate(string[] input)
        {
            if (input == null)
            {
                throw new InvalidOperationException("No input was given");
            }

            string userText = input[0];

            if (operators.Contains(userText[0]) || operators.Contains(userText[userText.Length - 1]) || Regex.IsMatch(userText, RegexFilter))
            {
                inputProcessor.WriteContent(new string[] { errorMessage });
                return;
            }

            inputProcessor.WriteContent(new string[] { CalculateString(userText) });
        }
    }
}
