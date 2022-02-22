using Xamarin.Basics.Mvvm.Navigations.Controllers;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Collections;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Factories;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Mvvm.Views.Utils;

namespace Xamarin.Basics.Mvvm.Navigations
{
    public class NavigationController : INavigationController
    {
        private readonly IViewControllerFactory _viewControllerFactory;
        
        private ViewController _viewController;

        public NavigationController(IViewControllerFactory viewControllerFactory)
        {
            _viewControllerFactory = viewControllerFactory;
        }

        public void UseRootViewController(IRootView view)
        {
            UnloadCurrentViews();
            _viewController = _viewControllerFactory.CreateController(view);
            ViewUtils.Load(view);
        }

        public void UseStackRootViewController(IStackView view)
        {
            UnloadCurrentViews();
            _viewController = _viewControllerFactory.CreateController(view);
            ViewUtils.Load(view);
        }

        public void OnViewPushed(IStackView stackView)
        {
            if (_viewController is IStackViewCollection collection)
            {
                collection.AddView(stackView);
            }

            ViewUtils.Load(stackView);
        }

        public void OnViewPopped(IStackView stackView)
        {
            if (_viewController is IStackViewCollection collection)
            {
                collection.RemoveView(stackView);
            }

            ViewUtils.Unload(stackView);
        }

        public void OnModalViewPushed(IModalView modalView)
        {
            if (_viewController is IModalViewCollection collection)
            {
                collection.AddView(modalView);
            }

            ViewUtils.Load(modalView);
        }

        public void OnModalViewPopped(IModalView modalView)
        {
            if (_viewController is IModalViewCollection collection)
            {
                collection.RemoveView(modalView);
            }

            ViewUtils.Unload(modalView);
        }
        
        public IStackView GetPoppableView()
        {
            if (_viewController is IStackViewCollection collection)
            {
                return collection.GetLastOrDefault();
            }

            return null;
        }
        
        public IModalView GetPoppableModalView()
        {
            if (_viewController is IModalViewCollection collection)
            {
                return collection.GetLastOrDefault();
            }

            return null;
        }
        
        private void UnloadCurrentViews()
        {
            if (_viewController == null) return;
            
            var views = _viewController.GetAllViews();
            foreach (var view in views)
            {
                ViewUtils.Unload(view);
            }
        }
    }
}