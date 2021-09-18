using System;
using System.Net.Http;
using DryIoc;
using ImTools;
using Xamarin.Basics.Navigations;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Api;
using XamarinTemplate.Api.Collections.Photos.Factories;
using XamarinTemplate.Interfaces;
using XamarinTemplate.Navigations;
using XamarinTemplate.Views.List;
using XamarinTemplate.Views.Main;
using ListView = XamarinTemplate.Views.List.ListView;

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
            
            Container.Register<IPhotoService, PhotoService>();
            Container.Register<IPhotoApiFactory, PhotoApiFactory>();

            var baseApiUrl = "https://jsonplaceholder.typicode.com/";
            Container.RegisterDelegate(() => DependencyService.Resolve<IHttpMessageHandlerService>().Create());

            Container.Register<IPhotoApiFactory, PhotoApiFactory>(Reuse.Singleton);
            Container.RegisterDelegate(c => c.Resolve<IPhotoApiFactory>().Create(baseApiUrl));
            
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