using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Mvvm.ViewModels;

namespace MAUI.Basics.Mvvm.Views
{
    public interface IView : ILoadable
    {
        object BindingContext { get; }
        IViewModel<object> ViewModel => BindingContext as IViewModel<object>;
    }
}