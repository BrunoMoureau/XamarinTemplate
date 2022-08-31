namespace MAUI.Basics.Services.Settings
{
    public interface ISettings
    {
        string SectionName { get; }
    }

    public interface ISettingsService
    {
        public T Get<T>(string propertyName) where T : ISettings;
        public T Get<T>(T settings) where T : ISettings;
    }
}