using Xamarin.Basics.Services.Alerts;
using Xamarin.Forms;

namespace XamarinTemplate.Services.Alerts
{
    public class AlertService : IAlertService
    {
        public void Show(string title, string message)
        {
            Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
        
        public void Show(string title, string message, string cancel)
        {
            Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        
        public void Show(string title, string message, string cancel, string accept)
        {
            Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}