using System;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Start(args.FirstOrDefault());
        }

        private static void Start(string file)
        {
            Calculator calculator = new Calculator();
            Utils utils = new Utils();

            if (string.IsNullOrEmpty(file))
            {
                string input = Console.ReadLine();
                string result = calculator.CalculateInput(input);
                Console.WriteLine(input + " = " + (result ?? "nothing"));
                Console.ReadKey();
            }
            else
            {
                string[] fileContent = utils.ReadFile(file);
                fileContent= calculator.CalculateFileContent(fileContent);
                utils.WriteFile(file, fileContent);
            }
        }
    }
}
