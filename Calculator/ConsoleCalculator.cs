using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ConsoleCalculator : Calculator
    {
        protected override string RegexFilter => @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`|\(|\)";

        public ConsoleCalculator(IProcessor processor)
            : base(processor)
        {

        }

        public override void Calculate(string input)
        {
            input = _inputProcessor.GetContent(input ?? "Enter expression to calculate:").FirstOrDefault();
            if (input != null)
            {
                if (_operators.Contains(input) || _operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
                {
                    _inputProcessor.WriteContent(_errorMessage);
                    return;
                }

                _inputProcessor.WriteContent(CalculateString(input));
                Calculate("Enter another expression to calculate or press ctrl+c to exit:");
            }
        }
    }
}
