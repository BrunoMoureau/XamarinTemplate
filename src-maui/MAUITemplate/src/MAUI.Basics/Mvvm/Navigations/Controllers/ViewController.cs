using MAUI.Basics.Mvvm.Views;

namespace MAUI.Basics.Mvvm.Navigations.Controllers
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