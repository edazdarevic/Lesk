using System;
using System.Text.RegularExpressions;

namespace Lesk.Consumers
{
    public class RegexConsumer : InputConsumer
    {
        public string Pattern { get; private set; }

        public int Priority { get; private set; }

        public RegexConsumer(string pattern, int priority = Lesk.NormalPriority)
        {
            Pattern = pattern;
            Priority = priority;
        }

        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();
            result.Priority = Priority;

            var input = context.Input.Substring(context.Position, context.Input.Length - context.Position);

            var regex = new Regex(Pattern);
            var match = regex.Match(input);

            if (match.Success && match.Index == 0 && match.Length > 0)
            {
                result.Success = true;
                result.Consumed = match.Value;
            }

            return result;
        }
    }
}