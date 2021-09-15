using LightInject;
using Xamarin.Basics.Navigations;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Navigations;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinTemplate
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            var container = new ServiceContainer();
            container.Register<INavigationService, NavigationService>();
            container.Register<ICurrentNavigation, CurrentNavigation>();
            container.Register<MainPage>();
            
            container.EnableAutoFactories();
            container.RegisterAutoFactory<IViewFactory>();

            var navigationService = container.GetInstance<INavigationService>();
            navigationService.SetRootAsync<MainPage>();
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