using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Navigations;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Api;
using XamarinTemplate.Api.Collections.Photos.Factories;
using XamarinTemplate.Interfaces;
using XamarinTemplate.Navigations;
using XamarinTemplate.Views.Main;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinTemplate
{
    public partial class App
    {
        public static IContainer Container { get; private set; }
        
        public App()
        {
            InitializeComponent();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<CurrentNavigation>().As<ICurrentNavigation>();
            
            containerBuilder = RegisterViews(containerBuilder);
            containerBuilder = RegisterViewModels(containerBuilder);

            containerBuilder.RegisterType<ViewFactory>().As<IViewFactory>();
            containerBuilder.RegisterType<PhotoService>().As<IPhotoService>();
            containerBuilder.RegisterType<PhotoApiFactory>().As<IPhotoApiFactory>();

            var baseApiUrl = "https://jsonplaceholder.typicode.com/";
            containerBuilder
                .Register(c => DependencyService.Resolve<IHttpMessageHandlerService>().Create())
                .As<Func<HttpMessageHandler>>();

            containerBuilder.RegisterType<PhotoApiFactory>().As<IPhotoApiFactory>();
            containerBuilder.Register(c => c.Resolve<IPhotoApiFactory>().Create(baseApiUrl));

            Container = containerBuilder.Build();
            
            var navigationService = Container.Resolve<INavigationService>();
            navigationService.SetStackRootAsync<MainView>();
        }
        
        private static ContainerBuilder RegisterViews(ContainerBuilder builder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            types
                .Where(IsClassWithViewInterface)
                .ForEach(v => builder.RegisterType(v));

            return builder;
        }

        private static ContainerBuilder RegisterViewModels(ContainerBuilder builder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            types
                .Where(IsClassWithViewModelInterface)
                .Where(t => !HasSingleInstanceInterface(t))
                .ForEach(t => builder.RegisterType(t));

            types
                .Where(IsClassWithViewModelInterface)
                .Where(HasSingleInstanceInterface)
                .ForEach(t => builder.RegisterType(t).SingleInstance());

            return builder;
        }

        private static bool IsClassWithViewInterface(Type type) =>
            typeof(IView).IsAssignableFrom(type) && type.IsClass;

        private static bool IsClassWithViewModelInterface(Type type) =>
            typeof(IViewModel).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract;

        private static bool HasSingleInstanceInterface(Type type) => false;
        
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