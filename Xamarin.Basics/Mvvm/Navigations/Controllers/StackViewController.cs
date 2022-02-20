using System.Collections.Generic;
using System.Linq;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Collections;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers
{
    public class StackViewController : ViewController, IStackViewCollection, IModalViewCollection
    {
        public List<IStackView> NavigationStack { get; } = new();
        public List<IModalView> ModalStack { get; } = new();

        public StackViewController(IStackView rootView) : base(rootView)
        {
        }

        public void AddView(IStackView view) => NavigationStack.Add(view);
        public void RemoveView(IStackView view) => NavigationStack.Remove(view);
        IStackView IStackViewCollection.GetLastOrDefault() => NavigationStack.LastOrDefault();

        public void AddView(IModalView view) => ModalStack.Add(view);
        public void RemoveView(IModalView view) => ModalStack.Remove(view);
        IModalView IModalViewCollection.GetLastOrDefault() => ModalStack.LastOrDefault();

        public override List<IView> GetAllViews()
        {
            var views = new List<IView> { Root };
            views.AddRange(NavigationStack);
            views.AddRange(ModalStack);

            return views;
        }
    }
}