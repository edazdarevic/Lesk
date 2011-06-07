namespace Lesk.Consumers
{
    public class WhitespaceConsumer : InputConsumer
    {
        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();
            var scanned = false;
            while (context.HasMore() && char.IsWhiteSpace(context.Current))
            {
                scanned = true;
                result.Consumed += context.Current.ToString();

                context.Advance();
            }

            if (scanned)
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