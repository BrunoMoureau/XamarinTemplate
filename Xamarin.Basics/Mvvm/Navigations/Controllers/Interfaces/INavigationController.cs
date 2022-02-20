using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces
{
    public interface INavigationController : INavigationCallback
    {
        void UseRootViewController(IRootView view); 
        void UseStackRootViewController(IStackView view);
        IStackView GetPoppableView();
        IModalView GetPoppableModalView();
    }
}