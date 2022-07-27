using MAUI.Basics.Mvvm.Views;

namespace MAUI.Basics.Mvvm.Navigations.Factories
{
    public interface IViewFactory
    {
        TView Create<TView>() where TView : IView;
    }
}