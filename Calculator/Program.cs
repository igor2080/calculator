using System;

namespace Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            string input = @"1+(2+3+(4+5+(6*7+8)+9)+10)";

            calculator.ParseString(input);
        }
    }
}
