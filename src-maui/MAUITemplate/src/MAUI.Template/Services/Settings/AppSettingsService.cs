using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using MAUI.Basics.Extensions.Jsons;
using MAUI.Basics.Services.Settings;

namespace MAUI.Basics.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly JsonElement _jsonElement;

        public SettingsService(Assembly assembly)
        {
            try
            {
                var resourceName = GetAppSettingsFilePath(assembly);
                _jsonElement = GetAppSettingsAsJson(assembly, resourceName);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Unable to load AppSettings file from assembly {assembly?.FullName}");
            }
        }

        private string GetAppSettingsFilePath(Assembly assembly) => 
            assembly
                .GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));

        private JsonElement GetAppSettingsAsJson(Assembly assembly, string resourceName)
        {
            using var file = assembly.GetManifestResourceStream(resourceName);
            if(file == null) return new JsonElement();
            
            using var document = JsonDocument.Parse(file);
            return document.RootElement.Clone();
        }

        public T Get<T>(string propertyName) where T : ISettings
        {
            return _jsonElement
                .GetProperty(propertyName)
                .ToObject<T>();
        }

        public T Get<T>(T settings) where T : ISettings
        {
            var sectionName = settings.SectionName;

            return _jsonElement
                .GetProperty(sectionName)
                .ToObject<T>();
        }
    }
}