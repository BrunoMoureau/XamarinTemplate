using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Mvvm.Utils;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;

namespace Xamarin.Basics.Navigations
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

        public Task SetRootAsync<TView, TViewModelParams>(TViewModelParams parameters) where TView : IRootView
        {
            var currentViews = _currentNavigationService.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigationService.SetRootView(view);

            ViewUtils.Close(currentViews);
            ViewUtils.Open(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task SetStackRootAsync<TView>() where TView : IStackView
            => SetStackRootAsync<TView, object>(null);

        public Task SetStackRootAsync<TView, TViewModelParams>(TViewModelParams parameters) where TView : IStackView
        {
            var currentViews = _currentNavigationService.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigationService.SetStackView(view);

            ViewUtils.Close(currentViews);
            ViewUtils.Open(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushAsync<TView>(bool animated = true) where TView : IStackView
            => PushAsync<TView, object>(null, animated);

        public async Task PushAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IStackView
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var view = _viewFactory.Create<TView>();
            await _currentNavigationService.PushViewAsync(view, animated);
            
            ViewUtils.Open(view);

            await ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushModalAsync<TView>(bool animated = true) where TView : IModalView
            => PushModalAsync<TView, object>(null, animated);

        public async Task PushModalAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IModalView
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var modalView = _viewFactory.Create<TView>();
            await _currentNavigationService.PushModalViewAsync(modalView, animated);

            ViewUtils.Open(modalView);

            await ViewModelUtils.InitializeAsync(modalView?.ViewModel, parameters);
        }

        public async Task PopAsync(bool animated = true)
        {
            if (!_currentNavigationService.HasRootStackView()) return;
            
            var lastView = _currentNavigationService.GetLastViewOrDefault();
            await _currentNavigationService.PopViewAsync(animated);

            ViewUtils.Close(lastView);
        }

        public async Task PopModalAsync(bool animated = true)
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var lastModalView = _currentNavigationService.GetLastModalViewOrDefault();
            await _currentNavigationService.PopModalViewAsync(animated);

            ViewUtils.Close(lastModalView);
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            if (!_currentNavigationService.HasRootStackView()) return;

            var currentViews = _currentNavigationService.GetViews();
            
            await _currentNavigationService.PopAllAsync(animated);

            ViewUtils.Close(currentViews);
        }

        public bool AnyModalDisplayed() => _currentNavigationService.HasModalView();
    }
}