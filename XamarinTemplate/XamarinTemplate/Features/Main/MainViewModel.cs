using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Interfaces;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Features.Gallery;
using XamarinTemplate.Features.Language;
using XamarinTemplate.Features.Messaging;

namespace XamarinTemplate.Features.Main
{
    public class MainViewModel : ObservableObject, IViewModel
    {
        private readonly INavigationService _navigationService;
        public AsyncCommand GalleryCommand { get; }
        public AsyncCommand MessagingCommand { get; }
        public AsyncCommand LanguageCommand { get; }

        public MainViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            GalleryCommand = new AsyncCommand(OpenGalleryAsync, allowsMultipleExecutions: false);
            MessagingCommand = new AsyncCommand(OpenMessagingAsync, allowsMultipleExecutions: false);
            LanguageCommand = new AsyncCommand(OpenLanguageAsync, allowsMultipleExecutions: false);
        }

        public void Load()
        {
        }

        public Task InitializeAsync(object @params) => Task.CompletedTask;

        public void Unload()
        {
        }

        private Task OpenGalleryAsync() => _navigationService.PushAsync<GalleryView>();
        private Task OpenMessagingAsync() => _navigationService.PushAsync<MessagingView>();
        private Task OpenLanguageAsync() => _navigationService.PushAsync<LanguageView>();
    }
}