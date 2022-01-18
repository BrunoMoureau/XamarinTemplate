using System;
using System.Reflection;
using DryIoc;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Settings;
using Xamarin.Forms;
using XamarinTemplate.Api.Collections.Photos.Factories;
using XamarinTemplate.Repositories.Photos;
using XamarinTemplate.Services.HttpMessageHandler;
using XamarinTemplate.Services.Navigations;
using XamarinTemplate.Settings;

namespace XamarinTemplate.Services.Containers
{
    public class MyContainer : IMyContainer
    {
        private readonly Container _container;

        public MyContainer(Container container)
        {
            _container = container;
        }
        
        public TResult Resolve<TResult>() => _container.Resolve<TResult>();
    }

    public interface IMyContainer
    {
        TResult Resolve<TResult>();
    }

    public class ContainerService
    {
        public IMyContainer Initialize()
        {
            var container = new Container();

            container = RegisterMvvm(container);
            container = RegisterSettings(container);
            container = RegisterServices(container);
            container = RegisterApi(container);

            var myContainer = new MyContainer(container);
            container.RegisterInstance<IMyContainer>(myContainer);

            return myContainer;
        }

        private Container RegisterMvvm(Container container)
        {
            container.RegisterMany(new[] { Assembly.GetExecutingAssembly() }, IsClassWithViewInterface);
            container.RegisterMany(new[] { Assembly.GetExecutingAssembly() }, IsClassWithViewModelInterface);

            container.Register<IViewFactory, ViewFactory>();

            return container;
        }

        private Container RegisterSettings(Container container)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var appSettings = new AppSettings(assembly);
            
            container.RegisterInstance(appSettings.Get<EnvironmentSettings>("Environment"));

            return container;
        }

        private Container RegisterServices(Container container)
        {
            container.Register<INavigationService, NavigationService>();
            container.Register<ICurrentNavigationService, CurrentNavigationService>();

            container.Register<IPhotoService, PhotoService>();

            return container;
        }

        private Container RegisterApi(Container container)
        {
            var baseApiUrl = "https://jsonplaceholder.typicode.com/";
            container.Register<ApiFactory>(Reuse.Singleton);
            container.RegisterDelegate(() => DependencyService.Resolve<IHttpMessageHandlerService>().Create());
            container.RegisterDelegate(c => c.Resolve<ApiFactory>().CreatePhotoApi(baseApiUrl));

            return container;
        }

        private static bool IsClassWithViewInterface(Type type) =>
            typeof(IView).IsAssignableFrom(type) && type.IsClass;

        private static bool IsClassWithViewModelInterface(Type type) =>
            (typeof(IViewModel).IsAssignableFrom(type) || typeof(IViewModel<>).IsAssignableFrom(type)) &&
            type.IsClass && !type.IsAbstract;
    }
}