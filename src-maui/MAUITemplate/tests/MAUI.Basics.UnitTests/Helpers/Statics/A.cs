using MAUI.Basics.Mvvm.Navigations.Controllers.Interfaces;
using MAUI.Basics.Mvvm.Navigations.Factories;
using MAUI.Basics.Mvvm.ViewModels;
using MAUI.Basics.Mvvm.Views;
using MAUI.Basics.UnitTests.Helpers.Builders;

namespace MAUI.Basics.UnitTests.Helpers.Statics
{
    public static class A
    {      
        public static MockBuilder<INavigationController> NavigationController => new();
        public static MockBuilder<IViewFactory> ViewFactory => new();
        
        public static MockBuilder<IRootView> RootView => new();
        public static MockBuilder<IStackView> StackView => new();
        public static MockBuilder<IModalView> ModalView => new();
        
        public static MockBuilder<IViewModel<object>> ViewModel => new();
    }
}