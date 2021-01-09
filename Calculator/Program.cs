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
            Console.ReadKey();
        }

        private void Start(string arg)
        {
            if (arg == null)
            {
                Calculator calculator = new BracketlessCalculator();
                calculator.inputProcessor = new ConsoleProcessor();
                calculator.ReadInput(Console.ReadLine());
                calculator.GetResult();
            }
            else
            {
                Calculator calculator = new BracketCalculator();
                calculator.inputProcessor = new FileProcessor();
                calculator.ReadInput(arg);
                calculator.GetResult();
            }
        }
    }
}
