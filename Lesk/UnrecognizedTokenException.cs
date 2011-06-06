using System;

namespace Lesk
{
    public class UnrecognizedTokenException
        : Exception
    {
        public LeskContext Context { get; private set; }

        public UnrecognizedTokenException(LeskContext context)
        {
            Context = context;
        }
    }
}
