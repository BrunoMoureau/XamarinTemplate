namespace MAUI.Basics.Services.Messagings
{
    public interface IMessageSubscriber<in TMessage>
        where TMessage : IMessage
    {
        void OnMessageReceived(object sender, TMessage message);
    }

    public interface IMessageSubscriber<in TSender, in TMessage>
        where TMessage : IMessage
    {
        void OnMessageReceived(TSender sender, TMessage message);
    }
}