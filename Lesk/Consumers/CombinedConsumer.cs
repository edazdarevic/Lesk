using System.Collections.Generic;
using System.Linq;

namespace Lesk.Consumers
{
    public class CombinedConsumer : InputConsumer
    {
        public CombinedConsumer(params InputConsumer[] consumers)
        {
            Consumers = new List<InputConsumer>();
            if (consumers != null && consumers.Length > 0)
            {
                Consumers.AddRange(consumers);
            }
        }

        public List<InputConsumer> Consumers { get; private set; }

        public override ConsumeResult Consume(LeskContext context)
        {
            var results = new List<ConsumeResult>();
            foreach (InputConsumer inputConsumer in Consumers)
            {
                results.Add(inputConsumer.Consume(context));
            }

            var result = new ConsumeResult();
            result.Priority = 3;
            if (results.ToList().Any(r => r.Success == false))
            {
                result.Success = false;
            }
            else
            {
                result.Success = true;
                results.ForEach(r => result.Consumed += r.Consumed);
            }

            return result;
        }
    }
}