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

            ContainerService.Initialize();
            
            var navigationService = ContainerService.Resolve<INavigationService>();
            navigationService.SetStackRootAsync<MainView>();
        }
    }
}