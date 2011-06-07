using System;
using Lesk.ConsoleApp.ExampleTokens;

namespace Lesk.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = string.Empty;

            var lesk =
                LeskInstance.Configure
                .DefineToken("[0-9]+", () => new NumberToken())
                .DefineToken(":ap", () => new CommandToken())
                .DefineToken("SUM", () => new CommandToken())
                .DefineToken(@"\s+", () => new WhitespaceToken())
                .DefineToken(@"\w+", () => new WordToken())
                .DefineToken("\".*", () => new StringLiteral())
                .AsCompiled()
                .Done();

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
}
