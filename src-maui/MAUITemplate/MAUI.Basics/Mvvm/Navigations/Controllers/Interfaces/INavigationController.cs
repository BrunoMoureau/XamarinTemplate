using MAUI.Basics.Mvvm.Views;

namespace MAUI.Basics.Mvvm.Navigations.Controllers.Interfaces
{
    public interface INavigationController : INavigationCallback
    {
        void SetController(ViewController viewController); 
        IStackView GetPoppableView();
        IModalView GetPoppableModalView();
    }
}