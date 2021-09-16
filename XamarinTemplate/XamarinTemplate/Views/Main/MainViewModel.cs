using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;
using Xamarin.Basics.Navigations;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Views.List;

namespace XamarinTemplate.Views.Main
{
    public class MainViewModel : ObservableObject, IViewModel
    {
        private readonly INavigationService _navigationService;
        public AsyncCommand ListCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            ListCommand = new AsyncCommand(OpenListViewAsync);
        }

        private Task OpenListViewAsync() => _navigationService.PushAsync<ListView>();

        public Task InitializeAsync(object @params) => Task.CompletedTask;
    }
}