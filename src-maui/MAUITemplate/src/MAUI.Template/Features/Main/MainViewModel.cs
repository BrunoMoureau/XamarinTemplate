using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Mvvm.ViewModels;
using MAUI.Template.Features.Gallery;
using MAUI.Template.Features.Language;
using MAUI.Template.Features.Messaging;

namespace MAUI.Template.Features.Main
{
    public class MainViewModel : ObservableObject, IViewModel
    {
        private readonly INavigationService _navigationService;
        public IAsyncRelayCommand GalleryCommand { get; }
        public IAsyncRelayCommand MessagingCommand { get; }
        public IAsyncRelayCommand LanguageCommand { get; }

        public MainViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            GalleryCommand = new AsyncRelayCommand(OpenGalleryAsync);
            MessagingCommand = new AsyncRelayCommand(OpenMessagingAsync);
            LanguageCommand = new AsyncRelayCommand(OpenLanguageAsync);
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