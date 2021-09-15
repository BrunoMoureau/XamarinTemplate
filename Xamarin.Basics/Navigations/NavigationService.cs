using System.Linq;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Mvvm.Models;
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
            => SetRootAsync<TView, ViewModelParams>(ViewModelParams.Empty);

        public Task SetRootAsync<TView, TViewModelParams>(TViewModelParams parameters) where TView : IRootView where TViewModelParams : ViewModelParams
        {
            var currentViews = _currentNavigation.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigation.SetRootView(view);

            ViewUtils.UnsubscribeServices(currentViews);
            ViewUtils.SubscribeServices(view);

            return ViewModelUtils.InitializeAsync(view.BindingContext, parameters);
        }

        public Task SetStackRootAsync<TView>() where TView : IStackView
            => SetStackRootAsync<TView, ViewModelParams>(ViewModelParams.Empty);

        public Task SetStackRootAsync<TView, TViewModelParams>(TViewModelParams parameters) where TView : IStackView where TViewModelParams : ViewModelParams
        {
            var currentViews = _currentNavigation.GetViews();
            var view = _viewFactory.Create<TView>();

            _currentNavigation.SetStackView(view);

            ViewUtils.UnsubscribeServices(currentViews);
            ViewUtils.SubscribeServices(view);

            return ViewModelUtils.InitializeAsync(view.BindingContext, parameters);
        }

        public Task PushAsync<TView>(bool animated = true) where TView : IStackView
            => PushAsync<TView, ViewModelParams>(ViewModelParams.Empty, animated);

        public async Task PushAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IStackView
            where TViewModelParams : ViewModelParams
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var view = _viewFactory.Create<TView>();
            await _currentNavigation.PushViewAsync(view, animated);
            
            ViewUtils.SubscribeServices(view);
            
            await ViewModelUtils.InitializeAsync(view.BindingContext, parameters);
        }

        public Task PushModalAsync<TView>(bool animated = true) where TView : IModalView
            => PushModalAsync<TView, ViewModelParams>(ViewModelParams.Empty, animated);

        public async Task PushModalAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IModalView
            where TViewModelParams : ViewModelParams
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var modalView = _viewFactory.Create<TView>();
            await _currentNavigation.PushModalViewAsync(modalView, animated);

            ViewUtils.SubscribeServices(modalView);

            await ViewModelUtils.InitializeAsync(modalView.BindingContext, parameters);
        }

        public async Task PopAsync(bool animated = true)
        {
            if (!_currentNavigation.HasRootStackView()) return;
            
            var lastView = _currentNavigation.GetLastViewOrDefault();
            await _currentNavigation.PopViewAsync(animated);

            ViewUtils.UnsubscribeServices(lastView);
            ViewModelUtils.DisposeViewModel(lastView?.BindingContext);
        }

        public async Task PopModalAsync(bool animated = true)
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var lastModalView = _currentNavigation.GetLastModalViewOrDefault();
            await _currentNavigation.PopModalViewAsync(animated);

            ViewUtils.UnsubscribeServices(lastModalView);
            ViewModelUtils.DisposeViewModel(lastModalView?.BindingContext);
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            if (!_currentNavigation.HasRootStackView()) return;

            var currentViews = _currentNavigation.GetViews();
            var currentViewModels = currentViews.Select(v => v.BindingContext);
            
            await _currentNavigation.PopAllAsync(animated);

            ViewUtils.UnsubscribeServices(currentViews);
            ViewModelUtils.DisposeViewModels(currentViewModels);
        }

        public bool AnyModalDisplayed() => _currentNavigation.HasModalView();
    }
}