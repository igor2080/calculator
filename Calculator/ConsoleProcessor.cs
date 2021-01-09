using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ConsoleProcessor : Processor
    {
        public override void CheckInput(string input)
        {
            ValidateInput(input);
            SetInput(input);
        }

        protected override void SetInput(string input)
        {
            Input = new string[] { input };
        }



        public override void DoOutput(Func<string, string> calculator)
        {
            if (Input.Length > 0)
            {
                string result = calculator(Input[0]);
                Console.WriteLine(result ?? "nothing");
            }
            else
            {
                throw new InvalidOperationException("No input was given");
            }
        }


    }
}
