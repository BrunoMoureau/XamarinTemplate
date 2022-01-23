using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Features.Gallery;
using XamarinTemplate.Settings;

namespace XamarinTemplate.Features.Main
{
    public class MainViewModel : ObservableObject, IViewModel
    {
        private readonly INavigationService _navigationService;
        public AsyncCommand GalleryCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GalleryCommand = new AsyncCommand(OpenGalleryAsync, allowsMultipleExecutions: false);
        }

        public Task InitializeAsync(object @params) => Task.CompletedTask;
        private Task OpenGalleryAsync() => _navigationService.PushAsync<GalleryView>();
    }
}