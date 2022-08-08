using System.Reflection;
using MAUI.Basics.Mvvm.Navigations;
using MAUI.Basics.Mvvm.Navigations.Controllers.Interfaces;
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
using MAUI.Template.Dependencies;
using MAUI.Template.Repositories.Photos;
using MAUI.Template.Services.Alerts;
using MAUI.Template.Services.Languages;
using MAUI.Template.Services.Loggers;
using MAUI.Template.Services.Messagings;
using MAUI.Template.Services.Navigations;
using MAUI.Template.Services.Toasts;
using MAUI.Template.Settings;

namespace MAUI.Template
{
    public static class AppContainer
    {
        public static void Initialize(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            #region MVVM

            var viewTypes = GetViewTypes(assembly);
            foreach (var viewType in viewTypes)
            {
                services.AddScoped(viewType);
            }

            var viewModelTypes = GetViewModelTypes(assembly);
            foreach (var viewModelType in viewModelTypes)
            {
                services.AddScoped(viewModelType);
            }

            services.AddScoped<IViewFactory, ViewFactory>();

            #endregion

            #region Settings

            services.AddScoped(_ =>
            {
                var appSettings = new AppSettings(assembly);
                return appSettings.Get<EnvironmentSettings>("Environment");
            });

            #endregion

            #region Services

            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IToastService, ToastService>();
            
            services.AddScoped<INavigationService, NavigationService>();
            services.AddScoped<IAppNavigationService, AppNavigationService>();
            
            services.AddSingleton<NavigationController>();
            services.AddSingleton<INavigationController>(s => s.GetRequiredService<NavigationController>());
            services.AddSingleton<INavigationCallback>(s => s.GetRequiredService<NavigationController>());

            services.AddScoped<IPhotoService, PhotoService>();

            #endregion

            #region Api

            var baseApiUrl = "https://jsonplaceholder.typicode.com/";

            services.AddScoped<ApiFactory>();
            services.AddScoped(_ =>
            {
                var httpClientHandler = new HttpMessageHandlerService();
                return httpClientHandler.Create();
            });

            services.AddScoped(sp =>
            {
                var apiFactory = sp.GetRequiredService<ApiFactory>();
                return apiFactory.CreatePhotoApi(baseApiUrl);
            });

            #endregion
        }
        
        private static IEnumerable<Type> GetViewModelTypes(Assembly assembly) =>
            assembly.GetTypes()
                .Where(IsClassWithViewModelInterface)
                .ToArray();

        private static IEnumerable<Type> GetViewTypes(Assembly assembly) =>
            assembly.GetTypes()
                .Where(IsClassWithViewInterface)
                .ToArray();

        private static bool IsClassWithViewInterface(Type type) =>
            typeof(IView).IsAssignableFrom(type) && type.IsClass;

        private static bool IsClassWithViewModelInterface(Type type) =>
            (typeof(IViewModel).IsAssignableFrom(type) || typeof(IViewModel<>).IsAssignableFrom(type)) 
            && type.IsClass 
            && !type.IsAbstract;
    }
}