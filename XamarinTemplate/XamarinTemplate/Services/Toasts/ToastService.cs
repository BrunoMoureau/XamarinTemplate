using System.Threading.Tasks;
using Xamarin.Basics.Services.Toasts;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace XamarinTemplate.Services.Toasts
{
    public class ToastService : IToastService
    {
        public Task ShowAsync(string message, int msDuration = 3000)
        {
            return Application.Current.MainPage.DisplayToastAsync(message);
        }
    }
}