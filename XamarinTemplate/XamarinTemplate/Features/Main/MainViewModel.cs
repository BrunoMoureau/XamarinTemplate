using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Services.Messagings;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Features.Gallery;

namespace XamarinTemplate.Features.Main
{
    public class MainViewModel : ObservableObject, IViewModel, IMessageSender
    {
        private readonly INavigationService _navigationService;
        public AsyncCommand GalleryCommand { get; }

        public MainViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            GalleryCommand = new AsyncCommand(OpenGalleryAsync, allowsMultipleExecutions: false);
        }

        public void Load()
        {
        }

        public Task InitializeAsync(object @params) => Task.CompletedTask;

        public void Unload()
        {
        }

        private Task OpenGalleryAsync() => _navigationService.PushAsync<GalleryView>();
    }
}