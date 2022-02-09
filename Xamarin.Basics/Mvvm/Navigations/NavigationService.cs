using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Navigations.Interfaces;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.ViewModels.Utils;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Mvvm.Views.Utils;

namespace Xamarin.Basics.Mvvm.Navigations
{
    public class NavigationService : INavigationService
    {
        private readonly IAppNavigationService _appNavigationService;
        private readonly IViewFactory _viewFactory;

        public NavigationService(IAppNavigationService appNavigationService, IViewFactory viewFactory)
        {
            _appNavigationService = appNavigationService;
            _appNavigationService.ViewAdded += OnViewAdded;
            _appNavigationService.ViewRemoved += OnViewRemoved;
            
            _viewFactory = viewFactory;
        }

        public Task SetRootAsync<TView>() where TView : IRootView
            => SetRootAsync<TView, object>(null);

        public Task SetRootAsync<TView, TParams>(TParams parameters) where TView : IRootView
        {
            var view = _viewFactory.Create<TView>();
            _appNavigationService.SetRootView(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task SetStackRootAsync<TView>() where TView : IStackView
            => SetStackRootAsync<TView, object>(null);

        public Task SetStackRootAsync<TView, TParams>(TParams parameters) where TView : IStackView
        {
            var view = _viewFactory.Create<TView>();
            _appNavigationService.SetStackView(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushAsync<TView>(bool animated = true) where TView : IStackView
            => PushAsync<TView, object>(null, animated);

        public async Task PushAsync<TView, TParams>(TParams parameters, bool animated = true)
            where TView : IStackView
        {
            var view = _viewFactory.Create<TView>();
            await _appNavigationService.PushViewAsync(view, animated);
            
            await ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushModalAsync<TView>(bool animated = true) where TView : IModalView
            => PushModalAsync<TView, object>(null, animated);

        public async Task PushModalAsync<TView, TParams>(TParams parameters, bool animated = true)
            where TView : IModalView
        {
            var modalView = _viewFactory.Create<TView>();
            await _appNavigationService.PushModalViewAsync(modalView, animated);

            await ViewModelUtils.InitializeAsync(modalView?.ViewModel, parameters);
        }

        public Task PopAsync(bool animated = true) => _appNavigationService.PopViewAsync(animated);
        public Task PopModalAsync(bool animated = true) => _appNavigationService.PopModalViewAsync(animated);

        public bool AnyModalDisplayed() => _appNavigationService.HasModalView();
        
        private void OnViewAdded(object sender, IView view)
        {
            ViewUtils.Load(view);
        }

        private void OnViewRemoved(object sender, IView view)
        {
            ViewUtils.Unload(view);
        }
    }
}