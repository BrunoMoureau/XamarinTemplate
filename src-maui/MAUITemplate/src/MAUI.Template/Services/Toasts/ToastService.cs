using CommunityToolkit.Maui.Alerts;
using MAUI.Basics.Services.Toasts;

namespace MAUI.Template.Services.Toasts
{
    public class ToastService : IToastService
    {
        public Task ShowAsync(string message, int msDuration = 3000)
        {
            var toast = Toast.Make(message);
            return toast.Show();
        }
    }
}