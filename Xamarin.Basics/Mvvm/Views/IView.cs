using Xamarin.Basics.Mvvm.Navigations.Interfaces;
using Xamarin.Basics.Mvvm.ViewModels;

namespace Xamarin.Basics.Mvvm.Views
{
    public interface IView : ILoadable
    {
        object BindingContext { get; }
        IViewModel<object> ViewModel => BindingContext as IViewModel<object>;
    }
}