using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Collections
{
    public interface IStackViewCollection
    {
        void AddView(IStackView view);
        void RemoveView(IStackView stackView);
        IStackView GetLastOrDefault();
    }
}