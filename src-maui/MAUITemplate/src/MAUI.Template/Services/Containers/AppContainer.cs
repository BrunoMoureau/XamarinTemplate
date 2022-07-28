using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DryIoc;
using MAUI.Basics.Mvvm.Navigations;
using MAUI.Basics.Mvvm.Navigations.Factories;
using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Mvvm.Navigations.Services;
using MAUI.Basics.Mvvm.ViewModels;
using MAUI.Basics.Services.Alerts;
using MAUI.Basics.Services.Languages;
using MAUI.Basics.Services.Loggers;
using MAUI.Basics.Services.Messagings;
using MAUI.Basics.Services.Toasts;
using MAUI.Basics.Settings;
using MAUI.Template.Abstractions.Photos;
using MAUI.Template.Api.Collections.Photos.Factories;
using MAUI.Template.Repositories.Photos;
using MAUI.Template.Services.Alerts;
using MAUI.Template.Services.HttpMessageHandlers;
using MAUI.Template.Services.Languages;
using MAUI.Template.Services.Loggers;
using MAUI.Template.Services.Messagings;
using MAUI.Template.Services.Navigations;
using MAUI.Template.Services.Toasts;
using MAUI.Template.Settings;
using Microsoft.Maui.Controls;

namespace MAUI.Template.Services.Containers
{
    public interface IAppContainer
    {
        TResult Resolve<TResult>();
    }

    public class AppContainer : IAppContainer
    {
        private Container _container;
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public void Initialize()
        {
            _container = CreateContainer();
            
            #region MVVM

            var viewTypes = GetViewTypes();
            _container.RegisterMany(viewTypes);

            var viewModelTypes = GetViewModelTypes();
            _container.RegisterMany(viewModelTypes);

            _container.Register<IViewFactory, ViewFactory>();

            #endregion

            #region Settings

            var appSettings = new AppSettings(_assembly);
            _container.RegisterInstance(appSettings.Get<EnvironmentSettings>("Environment"));

            #endregion

            #region Services

            _container.Register<IAlertService, AlertService>();
            _container.Register<ILanguageService, LanguageService>();
            _container.Register<ILoggerService, LoggerService>();
            _container.Register<IMessageService, MessageService>();
            _container.Register<INavigationService, NavigationService>();
            _container.RegisterMany<NavigationController>(Reuse.Singleton);
            _container.Register<IToastService, ToastService>();

            _container.Register<IAppNavigationService, AppNavigationService>();

            _container.Register<IPhotoService, PhotoService>();

            #endregion

            #region Api

            var baseApiUrl = "https://jsonplaceholder.typicode.com/";
            _container.Register<ApiFactory>(Reuse.Singleton);
            _container.RegisterDelegate(_ => DependencyService.Resolve<IHttpMessageHandlerService>().Create());
            _container.RegisterDelegate(c => c.Resolve<ApiFactory>().CreatePhotoApi(baseApiUrl));

            #endregion

            _container.RegisterInstance<IAppContainer>(this);
        }

        public TResult Resolve<TResult>() => _container.Resolve<TResult>();

        private Container CreateContainer()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                // Without this rule, the iOS app crashes.
                // https://github.com/dadhi/DryIoc/issues/156
                return new(rules => rules.WithUseInterpretation());
            }

            return new();
        }

        private IEnumerable<Type> GetViewModelTypes() =>
            _assembly.GetTypes().Where(IsClassWithViewModelInterface).ToArray();

        private IEnumerable<Type> GetViewTypes() => 
            _assembly.GetTypes().Where(IsClassWithViewInterface).ToArray();

        private static bool IsClassWithViewInterface(Type type) =>
            typeof(IView).IsAssignableFrom((Type)type) && type.IsClass;

        private static bool IsClassWithViewModelInterface(Type type) =>
            (typeof(IViewModel).IsAssignableFrom(type) || typeof(IViewModel<>).IsAssignableFrom(type)) &&
            type.IsClass && !type.IsAbstract;
    }
}