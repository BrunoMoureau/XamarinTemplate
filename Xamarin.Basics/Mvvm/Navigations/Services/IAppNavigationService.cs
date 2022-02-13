using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Services
{
    public interface IAppNavigationService
    {       
        void SetRootView<TView>(TView view) where TView : IRootView;
        void SetStackRootView<TView>(TView view) where TView : IStackView;
        
        Task PushViewAsync<TView>(TView view, bool animated = true) where TView : IStackView;
        Task PushModalViewAsync<TView>(TView view, bool animated = true) where TView : IModalView;
        Task PopViewAsync(bool animated);
        Task PopModalViewAsync(bool animated);
        
        void OnRootViewChanging();
        void OnRootViewChanged();
    }
}