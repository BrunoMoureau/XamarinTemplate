using System;
using System.Reflection;
using DryIoc;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Services.Alerts;
using Xamarin.Basics.Settings;
using Xamarin.Forms;
using XamarinTemplate.Abstractions.Photos;
using XamarinTemplate.Api.Collections.Photos.Factories;
using XamarinTemplate.Repositories.Photos;
using XamarinTemplate.Services.Alerts;
using XamarinTemplate.Services.HttpMessageHandlers;
using XamarinTemplate.Services.Navigations;
using XamarinTemplate.Settings;

namespace XamarinTemplate.Services.Containers
{
    public interface IAppContainer
    {
        void Initialize();
        TResult Resolve<TResult>();
    }
    
    public class AppContainer : IAppContainer
    {
        private readonly Container _container = new();
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public void Initialize()
        {
            #region MVVM
            
            _container.RegisterMany(new[] { _assembly }, IsClassWithViewInterface);
            _container.RegisterMany(new[] { _assembly }, IsClassWithViewModelInterface);

            _container.Register<IViewFactory, ViewFactory>();
            
            #endregion
            
            #region Settings
            
            var appSettings = new AppSettings(_assembly);

            _container.RegisterInstance(appSettings.Get<EnvironmentSettings>("Environment"));
            
            #endregion
            
            #region Services
            
            _container.Register<IAlertService, AlertService>();
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IAppNavigationService, AppNavigationService>();

            _container.Register<IPhotoService, PhotoService>();
            
            #endregion
            
            #region Api
            
            var baseApiUrl = "https://jsonplaceholder.typicode.com/";
            _container.Register<ApiFactory>(Reuse.Singleton);
            _container.RegisterDelegate(() => DependencyService.Resolve<IHttpMessageHandlerService>().Create());
            _container.RegisterDelegate(c => c.Resolve<ApiFactory>().CreatePhotoApi(baseApiUrl));
            
            #endregion
            
            _container.RegisterInstance<IAppContainer>(this);
        }

        public TResult Resolve<TResult>() => _container.Resolve<TResult>();
        
        private static bool IsClassWithViewInterface(Type type) =>
            typeof(IView).IsAssignableFrom(type) && type.IsClass;

        private static bool IsClassWithViewModelInterface(Type type) =>
            (typeof(IViewModel).IsAssignableFrom(type) || typeof(IViewModel<>).IsAssignableFrom(type)) &&
            type.IsClass && !type.IsAbstract;
    }
}