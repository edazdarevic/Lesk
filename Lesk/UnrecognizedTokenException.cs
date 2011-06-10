using System;
using System.Collections.ObjectModel;

namespace Lesk
{
    public class UnrecognizedTokenException
        : Exception
    {
        private LeskContext Context { get; set; }

        public string Input
        {
            get
            {
                return Context.Input;
            }
        }

        public int Position
        {
            get
            {
                return Context.Position;
            }
        }

        public char CurrentCharacter
        {
            get
            {
                return Context.Current;
            }
        }

        public ReadOnlyCollection<Token> Tokens
        {
            get
            {
                return new ReadOnlyCollection<Token>(Context.Tokens);
            }
        }

        public UnrecognizedTokenException(LeskContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            Context = context;
        }
    }
}
