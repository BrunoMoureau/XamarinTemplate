using Xamarin.Basics.Mvvm.Navigations.Controllers;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Factories;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Tests.Helpers.Builders;

namespace Xamarin.Basics.Tests.Helpers.Statics
{
    public static class A
    {      
        public static MockBuilder<INavigationController> NavigationController => new();
        public static MockBuilder<IViewFactory> ViewFactory => new();
        
        public static MockBuilder<IRootView> RootView => new();
        public static MockBuilder<IStackView> StackView => new();
        public static MockBuilder<IModalView> ModalView => new();
        
        public static MockBuilder<IViewModel<object>> ViewModel => new();
        public static MockBuilder<IViewControllerFactory> ViewControllerFactory => new();
    }
}