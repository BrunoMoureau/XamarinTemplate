namespace MAUI.Basics.Services.Toasts
{
    public interface IToastService
    {
        Task ShowAsync(string message, int msDuration = 3000);
    }
}