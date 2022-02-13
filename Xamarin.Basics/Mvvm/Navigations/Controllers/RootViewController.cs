using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Collections;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Mvvm.Views.Utils;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers
{
    public class RootViewController : IViewController, IModalViewCollection
    {
        public IView Root { get; }
        public List<IModalView> ModalStack { get; } = new();

        public RootViewController(IView root)
        {
            Root = root;
        }

        public void Load()
        {
            ViewUtils.Load(Root);
        }

        public void Unload()
        {
            ViewUtils.Unload(Root);
            foreach (var modalView in ModalStack)
            {
                ViewUtils.Unload(modalView);
            }
        }
    }
}