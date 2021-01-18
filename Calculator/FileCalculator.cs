﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class FileCalculator : Calculator
    {
        protected override string RegexFilter => @"\p{L}|!|@|#|\$|%|\^|&|\[|\]|~|=|;|,|_|\\|`";

        public FileCalculator(IProcessor processor)
            :base(processor)
        {
            
        }

        public override void Calculate(string fileName)
        {
            string[] input = this._inputProcessor.GetContent(fileName);
            string[] fileContent = new string[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                fileContent[i] = input[i] + " = " + CalculateLine(input[i]);
            }

            _inputProcessor.WriteContent(fileContent.ToArray());
        }

        private string CalculateLine(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }

            if (_operators.Contains(input[0]) || _operators.Contains(input[input.Length - 1]) || Regex.IsMatch(input, RegexFilter))
            {
                return _errorMessage;
            }

            while (input.Any(x => x == '(' || x == ')'))
            {
                int openBracePosition = input.LastIndexOf('(');
                int closeBracePosition = openBracePosition == -1
                    ? openBracePosition
                    : input.IndexOf(')', openBracePosition);

                if (closeBracePosition == -1)
                {//Input contains imbalanced braces
                    return _errorMessage;
                }

                string splitRange = input.Substring(openBracePosition + 1, closeBracePosition - openBracePosition - 1);
                string calculationResult = CalculateLine(splitRange);
                input = input.Remove(openBracePosition, closeBracePosition - openBracePosition + 1);
                input = input.Insert(openBracePosition, calculationResult);

            }

            input = CalculateString(input);
            return input;
        }
    }
}
