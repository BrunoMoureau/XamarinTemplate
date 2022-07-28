using MAUI.Basics.Services.Messagings;

namespace MAUI.Template.Services.Messagings
{
    public class MessageService : IMessageService
    {
        public void Subscribe<TMessage>(IMessageSubscriber<TMessage> receiver)
            where TMessage : IMessage
        {
            MessagingCenter.Subscribe<object, TMessage>(receiver, typeof(TMessage).FullName,
                receiver.OnMessageReceived);
        }

        public void Subscribe<TSender, TMessage>(IMessageSubscriber<TSender, TMessage> subscriber)
            where TSender : class
            where TMessage : IMessage
        {
            MessagingCenter.Subscribe<TSender, TMessage>(subscriber, typeof(TMessage).FullName,
                subscriber.OnMessageReceived);
        }

        public void Unsubscribe<TMessage>(IMessageSubscriber<TMessage> receiver)
            where TMessage : IMessage
        {
            MessagingCenter.Unsubscribe<object, TMessage>(receiver, typeof(TMessage).FullName);
        }

        public void Unsubscribe<TSender, TMessage>(IMessageSubscriber<TSender, TMessage> subscriber)
            where TSender : class
            where TMessage : IMessage
        {
            MessagingCenter.Unsubscribe<TSender, TMessage>(subscriber, typeof(TMessage).FullName);
        }

        public void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            var sender = new object();
            MessagingCenter.Send(sender, typeof(TMessage).FullName, message);
        }

        public void Send<TSender, TMessage>(TSender sender, TMessage message)
            where TSender : class
            where TMessage : IMessage
        {
            MessagingCenter.Send(sender, typeof(TMessage).FullName, message);
        }
    }
}