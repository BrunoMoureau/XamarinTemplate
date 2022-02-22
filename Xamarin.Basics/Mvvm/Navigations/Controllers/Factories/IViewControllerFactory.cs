using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Factories
{
    public interface IViewControllerFactory
    {
        ViewController CreateController(IRootView view);
        ViewController CreateController(IStackView view);
    }
}