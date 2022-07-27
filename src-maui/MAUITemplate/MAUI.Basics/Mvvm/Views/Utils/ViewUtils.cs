namespace MAUI.Basics.Mvvm.Views.Utils
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
    }
}