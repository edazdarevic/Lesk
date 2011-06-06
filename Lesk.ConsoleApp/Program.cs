using System;

namespace Lesk.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;
            var lesk = new Lesk();

            lesk.Add("[0-9]+", () => new NumberToken(), Lesk.AboveNormalPriority);
            lesk.Add(":ap", () => new CommandToken());
            lesk.Add(@"\s+", () => new WhitespaceToken());
            do
            {
                Console.WriteLine("Enter your input : ");
                input = Console.ReadLine();
                try
                {
                    var tokens = lesk.Tokenize(input);
                    tokens.ForEach(t => Console.WriteLine(string.Format("Token {0}, value: {1}", t, t.Value)));
                }
                catch (UnrecognizedTokenException tokenException)
                {
                    Console.WriteLine(tokenException);
                }

            } while (input != string.Empty);
        }
    }

    public class WhitespaceToken : Token
    {
    }

    public class CommandToken : Token
    {
    }

    public class NumberToken : Token
    {
    }
}
