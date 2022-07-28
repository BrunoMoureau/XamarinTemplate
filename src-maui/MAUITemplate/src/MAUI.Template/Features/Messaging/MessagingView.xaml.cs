using MAUI.Basics.Mvvm.Views;
using MAUI.Basics.Services.Alerts;
using MAUI.Basics.Services.Messagings;
using MAUI.Template.Features.Messaging.Messages;
using MAUI.Template.Resources.Languages;

namespace MAUI.Template.Features.Messaging
{
    public partial class MessagingView : IStackView, IMessageSubscriber<HelloMessage>
    {
        private readonly IMessageService _messageService;
        private readonly IAlertService _alertService;
        public bool HasNavigationBar => true;

        public MessagingView(MessagingViewModel viewModel, IMessageService messageService, IAlertService alertService)
        {
            _messageService = messageService;
            _alertService = alertService;
            
            InitializeComponent();
            BindingContext = viewModel;
        }

        public void Load()
        {
            _messageService.Subscribe(this);
        }

        public void Unload()
        {
            _messageService.Unsubscribe(this);
        }

        public void OnMessageReceived(object sender, HelloMessage message)
        {
            _alertService.Show(AppResources.Alert_Hello, "(☞ﾟヮﾟ)☞");
        }
    }
}