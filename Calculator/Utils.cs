using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Utils
    {
        public bool ValidateString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or empty", nameof(text));
            }

            return Regex.IsMatch(text, @"\p{L}|!|@|#|\$|%|\^|&|\*|\[|\]|~|=|;|,|_|\\|`");
        }
    }
}
