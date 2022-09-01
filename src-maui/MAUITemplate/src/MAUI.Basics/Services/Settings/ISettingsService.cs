namespace MAUI.Basics.Services.Settings
{
    public interface ISettings
    {
    }

    public interface ISettingsService
    {
        public T Get<T>(string propertyName);
        public T Get<T>(T type, string propertyName);
    }
}