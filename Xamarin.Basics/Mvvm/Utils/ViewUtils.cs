using Xamarin.Basics.Mvvm.Contracts.Views;

namespace Xamarin.Basics.Mvvm.Utils
{
    public static class ViewUtils
    {
        public static void SubscribeServices(IView view)
        {
            view.SubscribeServices();
        }

        public static void UnsubscribeServices(IView view)
        {
            view.UnsubscribeServices();
            ViewModelUtils.DisposeViewModel(view.BindingContext);
        }

        public static void UnsubscribeServices(params IView[] views)
        {
            foreach (var view in views)
            {
                UnsubscribeServices(view);
            }
        }
    }
}