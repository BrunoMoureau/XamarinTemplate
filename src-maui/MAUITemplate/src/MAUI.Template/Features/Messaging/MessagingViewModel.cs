using CommunityToolkit.Mvvm.ComponentModel;
using MAUI.Basics.Mvvm.ViewModels;
using MAUI.Basics.Services.Messagings;
using MAUI.Template.Features.Messaging.Messages;
using System.Windows.Input;

namespace MAUI.Template.Features.Messaging
{
    public class MessagingViewModel : ObservableObject, IViewModel
    {
        private readonly IMessageService _messageService;
        public ICommand SendMessageCommand { get; }

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