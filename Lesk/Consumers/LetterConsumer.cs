namespace Lesk.Consumers
{
    public class LetterConsumer : InputConsumer
    {
        private readonly int _howMany;

        public LetterConsumer(int howMany)
        {
            _howMany = howMany;
        }

        public override ConsumeResult Consume(LeskContext context)
        {
            var result = new ConsumeResult();
            var hasMore = _howMany == 0 || result.ConsumedLength < _howMany;
            while (context.HasMore() && char.IsLetter(context.Current) && hasMore)
            {
                result.Consumed += context.Current.ToString();
                context.Advance();
            }

            if (_howMany == 0 || (result.ConsumedLength == _howMany))
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