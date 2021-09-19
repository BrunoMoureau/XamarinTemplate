using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Services
{
    public interface ICurrentNavigationService
    {
        IView[] GetViews();
        void SetRootView<TView>(TView view) where TView : IRootView;
        void SetStackView<TView>(TView view) where TView : IStackView;

        public bool HasRootStackView();
        public IView GetLastViewOrDefault();
        public IView GetLastModalViewOrDefault();
        Task PushViewAsync<TView>(TView view, bool animated = true) where TView : IStackView;
        Task PushModalViewAsync<TView>(TView view, bool animated = true) where TView : IModalView;
        Task PopViewAsync(bool animated);
        Task PopModalViewAsync(bool animated);
        Task PopAllAsync(bool animated);
        bool HasModalView();
    }
}