using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Factories
{
    public class ViewControllerFactory : IViewControllerFactory
    {
        public ViewController CreateController(IRootView view)
        {
            return new RootViewController(view);
        }

        public ViewController CreateController(IStackView view)
        {
            return new StackViewController(view);
        }
    }
}