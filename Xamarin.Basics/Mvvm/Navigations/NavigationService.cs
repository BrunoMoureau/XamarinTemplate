using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.ViewModels.Utils;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Mvvm.Views.Utils;

namespace Xamarin.Basics.Mvvm.Navigations
{
    public class NavigationService : INavigationService
    {
        private readonly ICurrentNavigationService _currentNavigationService;
        private readonly IViewFactory _viewFactory;

        public NavigationService(ICurrentNavigationService currentNavigationService, IViewFactory viewFactory)
        {
            _currentNavigationService = currentNavigationService;
            _viewFactory = viewFactory;
        }

        public Task SetRootAsync<TView>() where TView : IRootView
            => SetRootAsync<TView, object>(null);

        public Task SetRootAsync<TView, TParams>(TParams parameters) where TView : IRootView
        {
            var currentViews = _currentNavigationService.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigationService.SetRootView(view);

            ViewUtils.Unload(currentViews);
            ViewUtils.Load(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task SetStackRootAsync<TView>() where TView : IStackView
            => SetStackRootAsync<TView, object>(null);

        public Task SetStackRootAsync<TView, TParams>(TParams parameters) where TView : IStackView
        {
            var currentViews = _currentNavigationService.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigationService.SetStackView(view);

            ViewUtils.Unload(currentViews);
            ViewUtils.Load(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushAsync<TView>(bool animated = true) where TView : IStackView
            => PushAsync<TView, object>(null, animated);

        public async Task PushAsync<TView, TParams>(TParams parameters, bool animated = true)
            where TView : IStackView
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var view = _viewFactory.Create<TView>();
            await _currentNavigationService.PushViewAsync(view, animated);
            
            ViewUtils.Load(view);

            await ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushModalAsync<TView>(bool animated = true) where TView : IModalView
            => PushModalAsync<TView, object>(null, animated);

        public async Task PushModalAsync<TView, TParams>(TParams parameters, bool animated = true)
            where TView : IModalView
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var modalView = _viewFactory.Create<TView>();
            await _currentNavigationService.PushModalViewAsync(modalView, animated);

            ViewUtils.Load(modalView);

            await ViewModelUtils.InitializeAsync(modalView?.ViewModel, parameters);
        }

        public async Task PopAsync(bool animated = true)
        {
            if (!_currentNavigationService.HasRootStackView()) return;
            
            var lastView = _currentNavigationService.GetLastViewOrDefault();
            await _currentNavigationService.PopViewAsync(animated);

            ViewUtils.Unload(lastView);
        }

        public async Task PopModalAsync(bool animated = true)
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var lastModalView = _currentNavigationService.GetLastModalViewOrDefault();
            await _currentNavigationService.PopModalViewAsync(animated);

            ViewUtils.Unload(lastModalView);
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var currentViews = _currentNavigationService.GetViews();
            
            await _currentNavigationService.PopAllAsync(animated);

            ViewUtils.Unload(currentViews);
        }

        public bool AnyModalDisplayed() => _currentNavigationService.HasModalView();
    }
}