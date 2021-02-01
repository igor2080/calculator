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
            ? new ConsoleCalculator(new ConsoleProcessor()) as Calculator
            : new FileCalculator(new FileProcessor()) as Calculator;
            calculator.Calculate(input);
        }
    }
}
