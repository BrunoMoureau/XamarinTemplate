using DryIoc;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Features.Main;
using XamarinTemplate.Services.Containers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinTemplate
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            var appContainer = new AppContainer();
            appContainer.Initialize();
            
            var navigationService = appContainer.Resolve<INavigationService>();
            navigationService.SetStackRootAsync<MainView>();
        }
    }
}