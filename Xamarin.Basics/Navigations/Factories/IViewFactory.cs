using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Navigations.Factories
{
    public interface IViewFactory
    {
        TView Create<TView>() where TView : IView;
    }
}