using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Controllers;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Navigations.Interfaces;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.ViewModels.Utils;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations
{
    public class NavigationService : INavigationService
    {
        private readonly IAppNavigationService _appNavigationService;
        private readonly INavigationController _navigationController;
        private readonly IViewFactory _viewFactory;

        public NavigationService(
            IAppNavigationService appNavigationService,
            INavigationController navigationController,
            IViewFactory viewFactory)
        {
            _appNavigationService = appNavigationService;
            _navigationController = navigationController;
            _viewFactory = viewFactory;
        }

        public Task SetRootAsync<TView>() where TView : IRootView
            => SetRootAsync<TView, object>(null);

        public Task SetRootAsync<TView, TParams>(TParams parameters) where TView : IRootView
        {
            var view = CreateView<TView>();

            _appNavigationService.OnRootViewChanging();
            _appNavigationService.SetRootView(view);
            _navigationController.SetController(new RootViewController(view));
            _appNavigationService.OnRootViewChanged();

            return ViewModelUtils.InitializeAsync(view.ViewModel, parameters);
        }

        public Task SetStackRootAsync<TView>() where TView : IStackView
            => SetStackRootAsync<TView, object>(null);

        public Task SetStackRootAsync<TView, TParams>(TParams parameters) where TView : IStackView
        {
            var view = CreateView<TView>();

            _appNavigationService.OnRootViewChanging();
            _appNavigationService.SetStackRootView(view);
            _navigationController.SetController(new StackViewController(view));
            _appNavigationService.OnRootViewChanged();

            return ViewModelUtils.InitializeAsync(view.ViewModel, parameters);
        }

        public Task PushAsync<TView>(bool animated = true) where TView : IStackView
            => PushAsync<TView, object>(null, animated);

        public async Task PushAsync<TView, TParams>(TParams parameters, bool animated = true)
            where TView : IStackView
        {
            var view = CreateView<TView>();
            await _appNavigationService.PushViewAsync(view, animated);

            await ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PushModalAsync<TView>(bool animated = true) where TView : IModalView
            => PushModalAsync<TView, object>(null, animated);

        public async Task PushModalAsync<TView, TParams>(TParams parameters, bool animated = true)
            where TView : IModalView
        {
            var view = CreateView<TView>();
            await _appNavigationService.PushModalViewAsync(view, animated);

            await ViewModelUtils.InitializeAsync(view?.ViewModel, parameters);
        }

        public Task PopAsync(bool animated = true)
        {
            var view = _navigationController.GetPoppableView();
            return view != null 
                ? _appNavigationService.PopViewAsync(view, animated)
                : Task.CompletedTask;
        }

        public Task PopModalAsync(bool animated = true)
        {
            var view = _navigationController.GetPoppableModalView();
            return view != null 
                ? _appNavigationService.PopModalViewAsync(view, animated)
                : Task.CompletedTask;
        }

        private TView CreateView<TView>() where TView : IView
            => _viewFactory.Create<TView>();
    }
}