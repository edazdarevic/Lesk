using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Lesk.Consumers;

namespace Lesk
{
    public class LeskInstance
    {
        public static LeskBuilder Configure
        {
            get
            {
                return new LeskBuilder(new LeskInstance());
            }
        }

        private List<Tuple<Func<InputConsumer>, Func<Token>>> Consumers { get; set; }

        private readonly Dictionary<string, Regex> _regexes = new Dictionary<string, Regex>();

        private readonly List<string> _patterns = new List<string>();

        private bool _shouldCompile;

        private LeskInstance()
        {
            Consumers = new List<Tuple<Func<InputConsumer>, Func<Token>>>();
        }

        public List<Token> Tokenize(string input)
        {
            var context = new LeskContext(input);
            while (context.HasMore())
            {
                var allResults = new List<ConsumeResult>();
                foreach (var consumerBuilder in Consumers)
                {
                    InputConsumer consumer = consumerBuilder.Item1();
                    var clonedContext = context.Clone();
                    ConsumeResult result = consumer.Consume(clonedContext);
                    result.TokenBuilder = consumerBuilder.Item2;
                    allResults.Add(result);
                }

                if (allResults.All(r => r.Success == false))
                {
                    throw new UnrecognizedTokenException(context);
                }

                var succeded = allResults.Where(r => r.Success);

                if (succeded.Count() == 1)
                {
                    ConsumeResult theOne = succeded.FirstOrDefault();
                    context.Apply(theOne);
                }
                else
                {
                    var bestMatches = succeded.Where(r => r.ConsumedLength == succeded.Max(r2 => r2.ConsumedLength));
                    ConsumeResult final = null;

                    if (bestMatches.Count() == 1)
                    {
                        final = bestMatches.FirstOrDefault();
                    }
                    else if (bestMatches.Count() > 0)
                    {
                        if (bestMatches.Count() > 1)
                        {
                            Trace.WriteLine("Warning : Multiple rules matched. Selecting the rule defined first.");
                        }

                        final = bestMatches.FirstOrDefault();
                    }

                    if (final == null)
                    {
                        throw new InvalidOperationException("Unexpected error.");
                    }

                    context.Apply(final);
                }
            }

            return context.Tokens;
        }

        private void DefineToken(string pattern, Func<Token> tokenBuilder)
        {
            if (!_patterns.Contains(pattern))
            {
                _patterns.Add(pattern);
            }

            Consumers.Add(new Tuple<Func<InputConsumer>, Func<Token>>(() => new RegexConsumer(pattern, _regexes), tokenBuilder));
        }

        private void Done()
        {
            _patterns.ForEach(pattern =>
                {
                    if (_shouldCompile)
                    {
                        _regexes.Add(pattern, new Regex(pattern, RegexOptions.Compiled));
                    }
                    else
                    {
                        _regexes.Add(pattern, new Regex(pattern));
                    }
                });

            if (_shouldCompile)
            {
                // Regex objects with Compile option are compiled when first used
                foreach (var regex in _regexes)
                {
                    regex.Value.Match("123456789abcd");
                }
            }
        }

        public class LeskBuilder : ILeskTokenDefiner
        {
            private readonly LeskInstance _leskInstance;

            public LeskBuilder(LeskInstance leskInstance)
            {
                _leskInstance = leskInstance;
            }

            public ILeskTokenDefiner DefineToken(string pattern, Func<Token> tokenBuilder)
            {
                _leskInstance.DefineToken(pattern, tokenBuilder);
                return this;
            }

            public ILeskTokenDefiner AsCompiled()
            {
                _leskInstance._shouldCompile = true;
                return this;
            }

            public LeskInstance Done()
            {
                _leskInstance.Done();
                return _leskInstance;
            }
        }
    }
}