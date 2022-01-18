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

            var containerService = new ContainerService();
            var myContainer = containerService.Initialize();
            
            var navigationService = myContainer.Resolve<INavigationService>();
            navigationService.SetStackRootAsync<MainView>();
        }
    }
}