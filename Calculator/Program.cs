using System;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static string[] programArgs;

        public static void Main(string[] args)
        {
            programArgs = args;
            Program program = new Program();
            program.Start();
        }

        private void Start()
        {
            if (programArgs.Length < 1)
            {
                Calculator calculator = new ConsoleCalculator();
                calculator.Calculate(calculator.GetInput());
            }
            else
            {
                Calculator calculator = new FileCalculator();
                calculator.Calculate(calculator.GetInput());
            }
        }
    }
}
