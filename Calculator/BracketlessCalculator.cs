using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class BracketlessCalculator : Calculator
    {
        protected override string CalculateInput(string input)
        {
            if (input == null)
            {
                throw new InvalidOperationException("No input was given");
            }

            if (Operators.Contains(input[0]) || Operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`|\(|\)"))
            {
                return errorMessage;
            }

            return CalculateString(input);
        }
    }
}
