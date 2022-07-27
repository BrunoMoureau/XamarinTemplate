using MAUI.Basics.Mvvm.Navigations.Controllers;
using MAUI.Basics.Mvvm.Navigations.Controllers.Collections;
using MAUI.Basics.Mvvm.Navigations.Controllers.Interfaces;
using MAUI.Basics.Mvvm.Views;
using MAUI.Basics.Mvvm.Views.Utils;

namespace MAUI.Basics.Mvvm.Navigations
{
    public class NavigationController : INavigationController
    {
        private ViewController _viewController;
        
        public void SetController(ViewController viewController)
        {
            UnloadCurrentViews(_viewController);
            _viewController = viewController;
            LoadView(viewController.Root);
        }

        public void OnViewPushed(IStackView stackView)
        {
            if (_viewController is IStackViewCollection collection)
            {
                collection.AddView(stackView);
            }

            LoadView(stackView);
        }

        public void OnViewPopped(IStackView stackView)
        {
            if (_viewController is IStackViewCollection collection)
            {
                collection.RemoveView(stackView);
            }
            
            UnloadView(stackView);
        }

        public void OnModalViewPushed(IModalView modalView)
        {
            if (_viewController is IModalViewCollection collection)
            {
                collection.AddView(modalView);
            }

            LoadView(modalView);
        }

        public void OnModalViewPopped(IModalView modalView)
        {
            if (_viewController is IModalViewCollection collection)
            {
                collection.RemoveView(modalView);
            }

            UnloadView(modalView);
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
        
        private void LoadView(IView view)
        {
            ViewUtils.Load(view);
        }
        
        private void UnloadView(IView view)
        {
            ViewUtils.Unload(view);
        }
        
        private void UnloadCurrentViews(ViewController viewController)
        {
            if (viewController == null) return;
            
            var views = viewController.GetAllViews();
            foreach (var view in views)
            {
                ViewUtils.Unload(view);
            }
        }
    }
}