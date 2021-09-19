using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Factories
{
    public interface IViewFactory
    {
        TView Create<TView>() where TView : IView;
    }
}