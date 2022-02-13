using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Collections;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Mvvm.Views.Utils;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers
{
    public class StackViewController : IViewController, IStackViewCollection, IModalViewCollection
    {
        public IView Root { get; }
        public List<IStackView> NavigationStack { get; } = new();
        public List<IModalView> ModalStack { get; } = new();

        public StackViewController(IView root)
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

            foreach (var stackView in NavigationStack)
            {
                ViewUtils.Unload(stackView);
            }

            foreach (var modalView in ModalStack)
            {
                ViewUtils.Unload(modalView);
            }
        }
    }
}