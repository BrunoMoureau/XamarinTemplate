using System.Collections.Generic;
using System.Linq;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Collections;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers
{
    public class RootViewController : ViewController, IModalViewCollection
    {
        public List<IModalView> ModalStack { get; } = new();

        public RootViewController(IView rootView) : base(rootView)
        {
        }

        public void AddView(IModalView view) => ModalStack.Add(view);
        public void RemoveView(IModalView view) => ModalStack.Remove(view);
        public IModalView GetLastOrDefault() => ModalStack.LastOrDefault();

        public override List<IView> GetAllViews()
        {
            var views = new List<IView> { Root };
            views.AddRange(ModalStack);

            return views;
        }
    }
}