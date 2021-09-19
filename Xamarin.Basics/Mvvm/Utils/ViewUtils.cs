using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Contracts.Views;

namespace Xamarin.Basics.Mvvm.Utils
{
    public static class ViewUtils
    {
        public static void Open(IView view)
        {
            view?.Open();
            view?.ViewModel?.Open();
        }

        public static void Close(IView view)
        {
            view?.Close();
            view?.ViewModel?.Close();
        }

        public static void Close(IEnumerable<IView> views)
        {
            foreach (var view in views)
            {
                Close(view);
            }
        }
    }
}