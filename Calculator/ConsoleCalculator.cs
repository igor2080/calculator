using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ConsoleCalculator : Calculator
    {
        private const string _continueKey = "1";
        protected override string RegexFilter => @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`|\(|\)";
        
        public ConsoleCalculator()
        {
            _inputProcessor = new ConsoleProcessor();
        }

        public override void Calculate(string input)
        {
            do
            {
                input = _inputProcessor.GetContent(null)[0];

                if (_operators.Contains(input) || _operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
                {
                    _inputProcessor.WriteContent(_errorMessage);
                    return;
                }

                _inputProcessor.WriteContent(CalculateString(input));
            }
            while (PromptTryAgain());
        }
        private static bool PromptTryAgain()
        {
            Console.WriteLine("Would you like to do another calculation? Type 1 to restart or anything else to exit: ");
            return Console.ReadLine() == _continueKey;
        }
    }
}
