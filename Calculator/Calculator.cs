using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public abstract class Calculator
    {
        protected const string errorMessage = "Bad Expression";
        protected const string divisionByZeroMessage = "Division by zero";
        protected const string operators = "+_*/";
        protected abstract string RegexFilter { get; }
        protected IProcessor inputProcessor;
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
                return errorMessage;
            }

            //first multiplication and division
            DoMultiplicationDivision(separatedNumbers);

            if (separatedNumbers[0] != divisionByZeroMessage && separatedNumbers[0] != errorMessage)
            {
                //then addition and subtraction
                DoAdditionSubtraction(separatedNumbers);
            }

            return separatedNumbers[0];
        }

        protected void DoAdditionSubtraction(List<string> separatedNumbers)
        {
            for (int i = 1; i < separatedNumbers.Count - 1; i += 2)
            {
                if (separatedNumbers[i].Contains('+') || separatedNumbers[i].Contains('-'))
                {
                    if (separatedNumbers[i].Contains('+'))
                    {
                        SetOperationResult(separatedNumbers, i, '+');
                    }
                    else//subtraction
                    {
                        SetOperationResult(separatedNumbers, i, '-');
                    }

                    i -= 2;//keep iterator in the same position
                }
            }
        }

        protected void DoMultiplicationDivision(List<string> separatedNumbers)
        {
            for (int i = 1; i < separatedNumbers.Count - 1; i += 2)
            {
                if (separatedNumbers[i].Contains('*') || separatedNumbers[i].Contains('/'))//could be addition or subtraction instead
                {
                    if (separatedNumbers[i].Contains('*'))
                    {
                        SetOperationResult(separatedNumbers, i, '*');
                    }
                    else//division
                    {
                        if (separatedNumbers[i + 1] == "0")
                        {
                            separatedNumbers[0] = divisionByZeroMessage;
                            return;
                        }

                        SetOperationResult(separatedNumbers, i, '/');
                    }

                    i -= 2;//keep iterator in the same position
                }
            }
        }

        protected void SetOperationResult(List<string> separatedNumbers, int index, char operation)
        {
            float result = 0;
            float leftNumber;
            if (separatedNumbers[index - 1] == "" && (operation == '+' || operation == '-'))
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
                case '*':
                    result = leftNumber * rightNumber;
                    break;
                case '/':
                    result = leftNumber / rightNumber;
                    break;
                case '+':
                    result = leftNumber + rightNumber;
                    break;
                case '-':
                    result = leftNumber - rightNumber;
                    break;
            }

            separatedNumbers.RemoveRange(index - 1, 3);
            separatedNumbers.Insert(index - 1, result.ToString());
        }
    }
}
