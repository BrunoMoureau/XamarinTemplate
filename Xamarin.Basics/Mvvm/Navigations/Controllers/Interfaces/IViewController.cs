using Xamarin.Basics.Mvvm.Navigations.Interfaces;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces
{
    public interface IViewController : ILoadable
    {
        IView Root { get; }
    }
}