using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public abstract class Calculator
    {
        protected const string _errorMessage = "Bad Expression";
        protected const string _divisionByZeroMessage = "Division by zero";
        protected const string _operators = "*/";
        private const char _multi = '*';
        private const char _divide = '/';
        private const char _add = '+';
        private const char _subtract = '-';

        protected IProcessor _inputProcessor;
        protected abstract string RegexFilter { get; }

        protected Calculator(IProcessor processor)
        {
            this._inputProcessor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public abstract void Calculate(string text);

        protected string CalculateString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            List<string> separatedNumbers = Regex.Split(text, @"(\*|\/|\+|\-)").ToList(); //split but keep signs

            if (separatedNumbers.Count % 2 == 0)
            {//Input contains bad operations
                return _errorMessage;
            }

            //first multiplication and division
            try
            {
                DoOperations(separatedNumbers, _multi, _divide);
            }
            catch (DivideByZeroException)
            {
                return _divisionByZeroMessage;
            }

            //then addition and subtraction
            DoOperations(separatedNumbers, _add, _subtract);

            return separatedNumbers[0];
        }

        protected void DoOperations(List<string> separatedNumbers, char firstSign, char secondSign)
        {
            for (int i = 1; i < separatedNumbers.Count - 1; i += 2)
            {
                if (separatedNumbers[i].Contains(firstSign) || separatedNumbers[i].Contains(secondSign))
                {
                    if (separatedNumbers[i].Contains(firstSign))
                    {
                        SetOperationResult(separatedNumbers, i, firstSign);
                    }
                    else
                    {
                        SetOperationResult(separatedNumbers, i, secondSign);
                    }

                    i -= 2;//keep iterator in the same position
                }
            }
        }

        protected void SetOperationResult(List<string> separatedNumbers, int index, char operation)
        {
            float result = 0;
            float leftNumber;
            if (string.IsNullOrEmpty(separatedNumbers[index - 1]) && (operation == '+' || operation == '-'))
            {
                leftNumber = 0;
            }
            else
            {
                leftNumber = float.Parse(separatedNumbers[index - 1]);
            }
            float rightNumber = float.Parse(separatedNumbers[index + 1]);

            switch (operation)
            {
                case _multi:
                    result = leftNumber * rightNumber;
                    break;
                case _divide:
                    if (rightNumber == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    result = leftNumber / rightNumber;
                    break;
                case _add:
                    result = leftNumber + rightNumber;
                    break;
                case _subtract:
                    result = leftNumber - rightNumber;
                    break;
            }

            separatedNumbers.RemoveRange(index - 1, 3);
            separatedNumbers.Insert(index - 1, result.ToString());
        }
    }
}
