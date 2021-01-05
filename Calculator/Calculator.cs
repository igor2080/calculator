using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class Calculator
    {
        private readonly Utils _utils = new Utils();

        public string ParseString(string text)
        {
            if (!_utils.ValidateString(text))
                throw new FormatException("text contains bad characters");

            while (text.Any(x => x == '(' || x == ')'))
            {
                int openBracePosition = text.LastIndexOf('(');

                if (openBracePosition == -1)
                {
                    throw new FormatException("text contains imbalanced braces");
                }

                int closeBracePosition = text.Substring(openBracePosition).IndexOf(')');

                if (openBracePosition != -1 && closeBracePosition != -1)
                {
                    ParseString(text.Substring(openBracePosition+1,closeBracePosition-1));
                }
                else
                {
                    throw new FormatException("text contains imbalanced braces");
                }

            }


            return text;
        }
    }
}
