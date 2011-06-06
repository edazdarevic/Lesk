using System.Collections.Generic;

namespace Lesk.Consumers
{
    public class FixedStringConsumer : InputConsumer
    {
        public FixedStringConsumer(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();
            result.Priority = 2;
            var queue = new Queue<char>(Value.ToCharArray());
            while (context.HasMore() && queue.Count > 0 && context.Current == queue.Dequeue())
            {
                result.Consumed += context.Current.ToString();
                context.Advance();
            }

            if (result.NumberConsumed == Value.Length)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }
    }
}