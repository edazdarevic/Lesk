using System;
using System.Collections.Generic;

namespace Lesk.Consumers
{
    public class RegexConsumer : InputConsumer
    {
        public string Pattern { get; private set; }

        public Dictionary<string, RegexInfo> RegexLookup { get; private set; }

        public RegexConsumer(string pattern, Dictionary<string, RegexInfo> regexLookup)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("pattern");
            }

            if (regexLookup == null)
            {
                throw new ArgumentNullException("regexLookup");
            }

            Pattern = pattern;
            RegexLookup = regexLookup;
        }

        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();

            var match = RegexLookup[Pattern].Regex.Match(context.Input, context.Position);

            if (match.Success && match.Index == context.Position && match.Length > 0)
            {
                result.Success = true;
                result.Consumed = match.Value;
            }

            return result;
        }
    }
}