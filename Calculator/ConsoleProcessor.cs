using Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class ConsoleProcessor : IProcessor
    {
        public string[] GetContent(string input)
        {
            ValidateInput(input);
            return new string[] { input };
        }

        public void WriteContent(string[] data)
        {
            Console.WriteLine(data[0] ?? "nothing");
        }

        protected void ValidateInput(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'{nameof(text)}' cannot be null or empty", nameof(text));
            }
        }
    }
}
