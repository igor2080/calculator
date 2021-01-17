using System;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = args.FirstOrDefault();
            Calculator calculator = string.IsNullOrEmpty(input)
            ? new ConsoleCalculator() as Calculator
            : new FileCalculator() as Calculator;
            calculator.Calculate(input);
            //loop was moved to console calculator(?)
        }
    }
}
