using System;
using System.Reflection;
using DryIoc;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Navigations;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;
using Xamarin.Forms;
using XamarinTemplate.Api;
using XamarinTemplate.Api.Collections.Photos.Factories;
using XamarinTemplate.Interfaces;
using XamarinTemplate.Services.Navigations;

namespace XamarinTemplate.Services.Containers
{
    public static class ContainerService
    {
        private static Container _container;
        
        public static void Initialize()
        {
            _container = new Container();
            
            _container.Register<INavigationService, NavigationService>();
            _container.Register<ICurrentNavigationService, CurrentNavigationService>();
            
            _container.RegisterMany(new[] { Assembly.GetExecutingAssembly() }, IsClassWithViewInterface);
            _container.RegisterMany(new[] { Assembly.GetExecutingAssembly() }, IsClassWithViewModelInterface);

            _container.Register<IViewFactory, ViewFactory>();
            _container.Register<IPhotoService, PhotoService>();

            var baseApiUrl = "https://jsonplaceholder.typicode.com/";
            _container.Register<ApiFactory>(Reuse.Singleton);
            _container.RegisterDelegate(() => DependencyService.Resolve<IHttpMessageHandlerService>().Create());
            _container.RegisterDelegate(c => c.Resolve<ApiFactory>().CreatePhotoApi(baseApiUrl));
        }

        public static TResult Resolve<TResult>() => _container.Resolve<TResult>();
        
        private static bool IsClassWithViewInterface(Type type) =>
            typeof(IView).IsAssignableFrom(type) && type.IsClass;

        private static bool IsClassWithViewModelInterface(Type type) =>
            (typeof(IViewModel).IsAssignableFrom(type) || typeof(IViewModel<>).IsAssignableFrom(type)) && type.IsClass && !type.IsAbstract;
    }
}