using System.Collections.Generic;

namespace Xamarin.Basics.Mvvm.Views.Utils
{
    public static class ViewUtils
    {
        public static void Load(IView view)
        {
            view?.Load();
            view?.ViewModel?.Load();
        }

        public static void Unload(IView view)
        {
            view?.Unload();
            view?.ViewModel?.Unload();
        }

        public static void Unload(IEnumerable<IView> views)
        {
            foreach (var view in views)
            {
                Unload(view);
            }
        }
    }
}