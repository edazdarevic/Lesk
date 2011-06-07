namespace Lesk.Consumers
{
    public class NumberConsumer : InputConsumer
    {
        private readonly int _howMany;

        public NumberConsumer()
            : this(0)
        {
        }

        public NumberConsumer(int howMany)
        {
            _howMany = howMany;
        }

        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();
            bool scanned = false;
            while (context.HasMore() && char.IsDigit(context.Current) &&
                   (_howMany == 0 || result.ConsumedLength < _howMany))
            {
                result.Consumed += context.Current.ToString();
                scanned = true;
                context.Advance();
            }

            if (scanned && ((_howMany == 0 && result.ConsumedLength > 0) || (_howMany == result.ConsumedLength)))
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