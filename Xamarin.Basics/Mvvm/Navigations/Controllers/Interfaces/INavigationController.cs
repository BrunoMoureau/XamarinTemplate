using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces
{
    public interface INavigationController : INavigationCallback
    {
        void SetController(ViewController viewController); 
        IStackView GetPoppableView();
        IModalView GetPoppableModalView();
    }
}