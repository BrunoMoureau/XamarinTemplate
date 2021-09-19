using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Navigations
{
    public interface INavigationService
    {
        Task SetRootAsync<TView>() where TView : IRootView;

        Task SetRootAsync<TView, TViewModelParams>(TViewModelParams parameters)
            where TView : IRootView;

        Task SetStackRootAsync<TView>() where TView : IStackView;

        Task SetStackRootAsync<TView, TViewModelParams>(TViewModelParams parameters)
            where TView : IStackView;

        Task PushAsync<TView>(bool animated = true) where TView : IStackView;

        Task PushAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IStackView;

        Task PushModalAsync<TView>(bool animated = true) where TView : IModalView;

        Task PushModalAsync<TView, TViewModelParams>(TViewModelParams parameters, bool animated = true)
            where TView : IModalView;

        Task PopAsync(bool animated = true);
        Task PopModalAsync(bool animated = true);
        Task PopToRootAsync(bool animated = true);
        bool AnyModalDisplayed();
    }
}