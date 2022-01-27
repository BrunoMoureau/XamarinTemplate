namespace Xamarin.Basics.Services.Messagings
{
    public interface IMessageService
    {
        void Send<TMessage>(TMessage message) where TMessage : IMessage;

        void Send<TSender, TMessage>(TSender sender, TMessage message)
            where TSender : class
            where TMessage : IMessage;

        void Subscribe<TMessage>(IMessageSubscriber<TMessage> receiver)
            where TMessage : IMessage;

        void Subscribe<TSender, TMessage>(IMessageSubscriber<TSender, TMessage> subscriber)
            where TSender : class
            where TMessage : IMessage;

        void Unsubscribe<TMessage>(IMessageSubscriber<TMessage> receiver)
            where TMessage : IMessage;

        void Unsubscribe<TSender, TMessage>(IMessageSubscriber<TSender, TMessage> subscriber)
            where TSender : class
            where TMessage : IMessage;
    }
}