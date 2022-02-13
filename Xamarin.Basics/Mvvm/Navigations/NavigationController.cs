using Xamarin.Basics.Mvvm.Navigations.Controllers;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Collections;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Mvvm.Views.Utils;

namespace Xamarin.Basics.Mvvm.Navigations
{
    public class NavigationController : INavigationController
    {
        private IViewController _viewController;

        public void UseRootViewController(IView view)
        {
            _viewController?.Unload();
            _viewController = new RootViewController(view);
            _viewController.Load();
        }
        
        public void UseStackRootViewController(IView view)
        {
            _viewController?.Unload();
            _viewController = new StackViewController(view);
            _viewController.Load();
        }

        public void OnViewPushed(IStackView stackView)
        {
            if (_viewController is IStackViewCollection stackViewCollection)
            {
                stackViewCollection.NavigationStack.Add(stackView);
            }

            ViewUtils.Load(stackView);
        }

        public void OnViewPopped(IStackView stackView)
        {
            if (_viewController is IStackViewCollection stackViewCollection)
            {
                stackViewCollection.NavigationStack.Remove(stackView);
            }

            ViewUtils.Unload(stackView);
        }

        public void OnModalViewPushed(IModalView modalView)
        {
            if (_viewController is IModalViewCollection modalViewCollection)
            {
                modalViewCollection.ModalStack.Add(modalView);
            }

            ViewUtils.Load(modalView);
        }

        public void OnModalViewPopped(IModalView modalView)
        {
            if (_viewController is IModalViewCollection modalViewCollection)
            {
                modalViewCollection.ModalStack.Remove(modalView);
            }

            ViewUtils.Unload(modalView);
        }
    }
}