using MAUI.Basics.Mvvm.Views;

namespace MAUI.Basics.Mvvm.Navigations.Controllers.Collections
{
    public interface IModalViewCollection
    {
        void AddView(IModalView view);
        void RemoveView(IModalView view);
        IModalView GetLastOrDefault();
    }
}