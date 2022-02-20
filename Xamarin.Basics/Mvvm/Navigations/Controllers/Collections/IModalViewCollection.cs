using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Collections
{
    public interface IModalViewCollection
    {
        void AddView(IModalView view);
        void RemoveView(IModalView view);
        IModalView GetLastOrDefault();
    }
}