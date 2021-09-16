using DryIoc;
using Xamarin.Basics.Navigations;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Navigations;
using XamarinTemplate.Views.List;
using XamarinTemplate.Views.Main;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinTemplate
{
    public partial class App
    {
        public static Container Container { get; private set; }
        
        public App()
        {
            InitializeComponent();

            Container = new Container();
            Container.Register<INavigationService, NavigationService>();
            Container.Register<ICurrentNavigation, CurrentNavigation>();
            Container.Register<MainView>();
            Container.Register<MainViewModel>();
            Container.Register<ListView>();
            Container.Register<ListViewModel>();
            
            Container.Register<IViewFactory, ViewFactory>();
            
            var navigationService = Container.Resolve<INavigationService>();
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