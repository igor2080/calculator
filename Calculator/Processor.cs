using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public abstract class Processor
    {
        protected const string Operators = "+_*/";        
        public abstract void CheckInput(string input);
        public string[] Input { get; protected set; }

        protected abstract void SetInput(string input);

        public abstract void DoOutput(Func<string, string> calculation);

        protected virtual void ValidateInput(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'{nameof(text)}' cannot be null or empty", nameof(text));
            }
        }

        
    }
}
