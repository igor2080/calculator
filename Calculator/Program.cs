using System;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        const string ContinueKey = "1";

        public static void Main(string[] args)
        {
            do
            {
                string input = args.FirstOrDefault();
                Calculator calculator = string.IsNullOrEmpty(input)
                ? new ConsoleCalculator() as Calculator
                : new FileCalculator() as Calculator;
                calculator.Calculate(input);
                
            }
            while (PromptTryAgain());
        }

        private static bool PromptTryAgain()
        {
            Console.WriteLine("Would you like to do another calculation? Type 1 to restart or anything else to exit: ");
            return Console.ReadLine() == ContinueKey;
        }
    }
}
