using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Services.Alerts;
using Xamarin.Basics.Services.Messagings;
using XamarinTemplate.Features.Messaging.Messages;
using XamarinTemplate.Resources.Languages;

namespace XamarinTemplate.Features.Messaging
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