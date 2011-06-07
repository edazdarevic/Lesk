using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesk.Consumers
{
    public class ConfigurableConsumer : InputConsumer
    {
        private readonly bool _digits;
        private readonly bool _letters;
        private readonly bool _whitespace;
        private readonly bool _puncation;
        private readonly bool _symbols;
        private readonly List<char> _allowed;
        private readonly List<char> _denied;

        public ConfigurableConsumer(
            bool digits = false,
            bool letters = false,
            bool whitespace = false,
            bool puncation = false,
            bool symbols = false,
            List<char> allowed = null,
            List<char> denied = null)
        {
            _digits = digits;
            _letters = letters;
            _whitespace = whitespace;
            _puncation = puncation;
            _symbols = symbols;

            _allowed = allowed;
            _denied = denied;

            if (_allowed == null)
            {
                _allowed = new List<char>();
            }

            if (_denied == null)
            {
                _denied = new List<char>();
            }

            if (_allowed.Intersect(_denied).Count() > 0)
            {
                throw new InvalidOperationException("You cannot have same char both as allowed and denied.");
            }
        }

        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();
            while (context.HasMore() && ComformsWithSpec(context.Current))
            {
                result.Consumed += context.Current.ToString();

                context.Advance();
            }

            if (result.ConsumedLength > 0)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        private bool ComformsWithSpec(char current)
        {
            if (_denied.Contains(current))
            {
                return false;
            }

            if (_digits)
            {
                if (char.IsDigit(current))
                {
                    return true;
                }
            }

            if (_letters && char.IsLetter(current))
            {
                return true;
            }

            if (_puncation && char.IsPunctuation(current))
            {
                return true;
            }

            if (_symbols && char.IsPunctuation(current))
            {
                return true;
            }

            if (_whitespace && char.IsWhiteSpace(current))
            {
                return true;
            }

            if (_allowed.Contains(current))
            {
                return true;
            }

            return false;
        }
    }
}