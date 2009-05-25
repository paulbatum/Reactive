namespace Reactive
{
    public class EventResult<TSender, TArgs>
    {
        public EventResult(TSender sender, TArgs args)
        {
            Sender = sender;
            EventArgs = args;
        }

        public TSender Sender { get; private set; }
        public TArgs EventArgs { get; private set; }
    }
}