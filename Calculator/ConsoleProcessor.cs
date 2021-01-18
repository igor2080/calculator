using Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class ConsoleProcessor : IProcessor
    {
        public string[] GetContent(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }

            string userInput = Console.ReadLine();
            ValidateInput(userInput);
            return new string[] { userInput };
        }

        public void WriteContent(string[] data)
        {
            if(data==null)
            {
                return;
            }

            foreach (string item in data)
            {
                Console.WriteLine(item ?? "nothing");
            }
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
