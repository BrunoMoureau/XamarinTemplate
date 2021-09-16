using Xamarin.Basics.Mvvm.Contracts.OpenClose;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;

namespace Xamarin.Basics.Mvvm.Contracts.Views
{
    public interface IView : IOpenClose
    {
        object BindingContext { get; }
        IViewModel<object> ViewModel => BindingContext as IViewModel<object>;
    }
}