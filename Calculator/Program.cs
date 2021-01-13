using System;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start(args.FirstOrDefault());
        }

        private void Start(string arg)
        {
            Calculator calculator;
            if (string.IsNullOrWhiteSpace(arg))
            {
                calculator = new ConsoleCalculator();
                calculator.Calculate(null);
            }
            else
            {
                calculator = new FileCalculator();
                calculator.Calculate(arg);
            }
        }
    }
}
