namespace Calculator
{
    public interface IProcessor
    {
        string[] GetContent(string input);
        void WriteContent(string[] data);
    }
}