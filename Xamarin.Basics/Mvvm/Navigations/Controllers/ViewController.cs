using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers
{
    public abstract class ViewController
    {
        public IView Root { get; }

        protected ViewController(IView root)
        {
            Root = root;
        }

        public abstract List<IView> GetAllViews();
    }
}