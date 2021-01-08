using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Calculator
    {
        private readonly Utils _utils = new Utils();
        const string errorMessage = "Bad Expression";
        const string divisionByZeroMessage = "Division by zero";

        public string CalculateInput(string text, bool bracketsAllowed)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            if (!_utils.ValidateString(text, bracketsAllowed))
            {
                return errorMessage;
            }

            List<char> splitText = text.ToCharArray().ToList();

            while (splitText.Any(x => x == '(' || x == ')'))
            {
                int openBracePosition = splitText.LastIndexOf('(');

                if (openBracePosition == -1)
                {//Input contains imbalanced braces
                    return errorMessage;
                }

                int closeBracePosition = splitText.IndexOf(')', openBracePosition);

                if (openBracePosition == -1 || closeBracePosition == -1)
                {//Input contains imbalanced braces
                    return errorMessage;
                }
                else
                {
                    string calculationResult = CalculateInput(new string(splitText.GetRange(openBracePosition + 1, closeBracePosition - openBracePosition - 1).ToArray()), bracketsAllowed);
                    splitText.RemoveRange(openBracePosition, closeBracePosition - openBracePosition + 1);
                    splitText.InsertRange(openBracePosition, calculationResult);
                }
            }

            splitText = CalculateString(new string(splitText.ToArray()))?.ToCharArray().ToList();
            return new string(splitText?.ToArray());
        }

        private string CalculateString(string text)
        {
            List<string> separatedNumbers = Regex.Split(text, @"(\*|\/|\+|\-)").ToList(); //split but keep signs

            if (separatedNumbers.Count % 2 == 0)
            {//Input contains bad operations
                return errorMessage;
            }

            //first multiplication and division
            DoMultiplicationDivision(separatedNumbers);

            if (separatedNumbers[0] != divisionByZeroMessage)
            {
                //then addition and subtraction
                DoAdditionSubtraction(separatedNumbers);
            }

            return separatedNumbers[0];
        }

        private void DoAdditionSubtraction(List<string> separatedNumbers)
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

        private void DoMultiplicationDivision(List<string> separatedNumbers)
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
                            Console.WriteLine("Division by zero is not allowed");
                            separatedNumbers[0] = divisionByZeroMessage;
                            return;
                        }

                        SetOperationResult(separatedNumbers, i, '/');
                    }

                    i -= 2;//keep iterator in the same position
                }
            }
        }

        private void SetOperationResult(List<string> separatedNumbers, int index, char operation)
        {
            float result = 0;
            float leftNumber = float.Parse(separatedNumbers[index - 1]);
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

        public string[] CalculateFileContent(string[] fileContent)
        {
            for (int i = 0; i < fileContent.Length; i++)
            {
                fileContent[i] = fileContent[i] + " = " + CalculateInput(fileContent[i], true);
            }

            return fileContent;
        }
    }
}
