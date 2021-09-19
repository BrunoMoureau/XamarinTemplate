using Xamarin.Basics.Mvvm.ViewModels;

namespace Xamarin.Basics.Mvvm.Views
{
    public interface IView
    {
        object BindingContext { get; }
        IViewModel<object> ViewModel => BindingContext as IViewModel<object>;
        
        public void Load()
        {
        }

        public void Unload()
        {
        }
    }
}