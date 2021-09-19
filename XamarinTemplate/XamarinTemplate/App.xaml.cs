using Xamarin.Basics.Navigations;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Services.Containers;
using XamarinTemplate.Views.Main;

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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}