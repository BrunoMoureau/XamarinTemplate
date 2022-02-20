using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces
{
    public interface INavigationController : INavigationCallback
    {
        void UseRootViewController(IView view); 
        void UseStackRootViewController(IView view);
        IStackView GetPoppableView();
        IModalView GetPoppableModalView();
    }
}