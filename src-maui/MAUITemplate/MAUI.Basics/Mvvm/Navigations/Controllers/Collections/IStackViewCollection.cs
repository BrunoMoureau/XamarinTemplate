using MAUI.Basics.Mvvm.Views;

namespace MAUI.Basics.Mvvm.Navigations.Controllers.Collections
{
    public interface IStackViewCollection
    {
        void AddView(IStackView view);
        void RemoveView(IStackView stackView);
        IStackView GetLastOrDefault();
    }
}