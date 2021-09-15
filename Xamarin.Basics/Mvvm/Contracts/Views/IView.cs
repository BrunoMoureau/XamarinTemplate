namespace Xamarin.Basics.Mvvm.Contracts.Views
{
    public interface IView
    {
        object BindingContext { get; }

        void SubscribeServices()
        {
        }

        void UnsubscribeServices()
        {
        }
    }
}