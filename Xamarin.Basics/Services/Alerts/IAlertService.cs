namespace Xamarin.Basics.Services.Alerts
{
    public interface IAlertService
    {
        void Show(string title, string message);
        void Show(string title, string message, string cancel);
        void Show(string title, string message, string cancel, string accept);
    }
}