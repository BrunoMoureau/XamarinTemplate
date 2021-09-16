using System.Linq;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Mvvm.Utils;
using Xamarin.Basics.Navigations.Factories;
using Xamarin.Basics.Navigations.Services;

namespace Xamarin.Basics.Navigations
{
    public class NavigationService : INavigationService
    {
        private readonly ICurrentNavigation _currentNavigation;
        private readonly IViewFactory _viewFactory;

        public NavigationService(ICurrentNavigation currentNavigation, IViewFactory viewFactory)
        {
            _currentNavigation = currentNavigation;
            _viewFactory = viewFactory;
        }

        public Task SetRootAsync<TView>() where TView : IRootView
            => SetRootAsync<TView, object>(null);

        public Task SetRootAsync<TView, TViewModelParams>(TViewModelParams parameters) where TView : IRootView
        {
            var currentViews = _currentNavigation.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigation.SetRootView(view);

            ViewUtils.Close(currentViews);
            ViewUtils.Open(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task SetStackRootAsync<TView>() where TView : IStackView
            => SetStackRootAsync<TView, object>(null);

        public Task SetStackRootAsync<TView, TViewModelParams>(TViewModelParams parameters) where TView : IStackView
        {
            var currentViews = _currentNavigation.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigation.SetStackView(view);

            ViewUtils.Close(currentViews);
            ViewUtils.Open(view);

            return ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushAsync<TView>(bool animated = true) where TView : IStackView
            => PushAsync<TView, object>(null, animated);

        public async Task PushAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IStackView
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var view = _viewFactory.Create<TView>();
            await _currentNavigation.PushViewAsync(view, animated);
            
            ViewUtils.Open(view);

            await ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushModalAsync<TView>(bool animated = true) where TView : IModalView
            => PushModalAsync<TView, object>(null, animated);

        public async Task PushModalAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IModalView
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var modalView = _viewFactory.Create<TView>();
            await _currentNavigation.PushModalViewAsync(modalView, animated);

            ViewUtils.Open(modalView);

            await ViewModelUtils.InitializeAsync(modalView?.ViewModel, parameters);
        }

        public async Task PopAsync(bool animated = true)
        {
            if (!_currentNavigation.HasRootStackView()) return;
            
            var lastView = _currentNavigation.GetLastViewOrDefault();
            await _currentNavigation.PopViewAsync(animated);

            ViewUtils.Close(lastView);
        }

        public async Task PopModalAsync(bool animated = true)
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var lastModalView = _currentNavigation.GetLastModalViewOrDefault();
            await _currentNavigation.PopModalViewAsync(animated);

            ViewUtils.Close(lastModalView);
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var currentViews = _currentNavigation.GetViews();
            
            await _currentNavigation.PopAllAsync(animated);

            ViewUtils.Close(currentViews);
        }

        public bool AnyModalDisplayed() => _currentNavigation.HasModalView();
    }
}