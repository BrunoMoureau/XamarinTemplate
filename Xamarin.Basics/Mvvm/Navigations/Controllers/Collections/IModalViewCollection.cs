using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Collections
{
    public interface IModalViewCollection
    {
        List<IModalView> ModalStack { get; }
    }
}