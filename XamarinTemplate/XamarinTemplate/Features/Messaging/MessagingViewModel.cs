using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Services.Messagings;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using XamarinTemplate.Features.Messaging.Messages;

namespace XamarinTemplate.Features.Messaging
{
    public class MessagingViewModel : ObservableObject, IViewModel
    {
        private readonly IMessageService _messageService;
        public Command SendMessageCommand { get; }

        public MessagingViewModel(IMessageService messageService) 
        {
            _messageService = messageService;
            SendMessageCommand = new Command(SendMessage);
        }

        public void Load()
        {
        }

        public Task InitializeAsync(object @params) => Task.CompletedTask;

        public void Unload()
        {
        }

        private void SendMessage() => _messageService.Send(new HelloMessage());
    }
}