namespace Lesk.Consumers
{
    public abstract class InputConsumer
    {
        public abstract ConsumeResult Consume(LeskContext context);
    }
}