using System;

namespace Lesk
{
    public class ConsumeResult
    {
        public string Consumed { get; set; }

        public int NumberConsumed
        {
            get
            {
                return Consumed != null ? Consumed.Length : 0;
            }
        }

        public int Priority { get; set; }

        public bool Success { get; set; }

        public Func<Token> TokenBuilder { get; set; }

        public Token BuildToken()
        {
            var token = TokenBuilder();
            token.Value = Consumed;
            return token;
        }
    }
}