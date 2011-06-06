using System;
using System.Collections.Generic;
using System.Linq;
using Lesk.Consumers;

namespace Lesk
{
    public class Lesk
    {
        public List<Tuple<Func<InputConsumer>, Func<Token>>> Consumers { get; private set; }

        public Lesk()
        {
            Consumers = new List<Tuple<Func<InputConsumer>, Func<Token>>>();
        }

        public void AddConsumer(Func<InputConsumer> consumer, Func<Token> tokenBuilder)
        {
            Consumers.Add(new Tuple<Func<InputConsumer>, Func<Token>>(consumer, tokenBuilder));
        }

        public List<Token> Process(string input)
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
                    var highConsumed = succeded.Where(r => r.NumberConsumed == succeded.Max(r2 => r2.NumberConsumed));
                    ConsumeResult finalFinal = null;

                    if (highConsumed.Count() == 1)
                    {
                        finalFinal = highConsumed.FirstOrDefault();
                    }
                    else if (highConsumed.Count() > 0)
                    {
                        if (highConsumed.Where(r => r.Priority == highConsumed.Min(r2 => r2.Priority)).Count() > 1)
                        {
                            throw new InvalidOperationException("Cannot decided which token it is.");
                        }

                        finalFinal = highConsumed.OrderBy(r => r.Priority).FirstOrDefault();
                    }

                    if (finalFinal == null)
                    {
                        throw new InvalidOperationException("Unexpected error.");
                    }

                    context.Apply(finalFinal);
                }
            }

            return context.Tokens;
        }
    }
}