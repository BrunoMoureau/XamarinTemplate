using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Xamarin.Basics.Extensions.Jsons;

namespace Xamarin.Basics.Settings
{
    public class AppSettings
    {
        private readonly JsonElement _jsonElement;

        public AppSettings(Assembly assembly)
        {
            try
            {
                var resourceName = GetAppSettingsFilePath(assembly);
                _jsonElement = GetAppSettingsAsJson(assembly, resourceName);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to load configuration file");
            }
        }

        private string GetAppSettingsFilePath(Assembly assembly) => 
            assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));

        private JsonElement GetAppSettingsAsJson(Assembly assembly, string resourceName)
        {
            using var file = assembly.GetManifestResourceStream(resourceName);
            if(file == null) return new JsonElement();
            using var document = JsonDocument.Parse(file);
            return document.RootElement.Clone();
        }

        public T Get<T>(string propertyName) => _jsonElement.GetProperty(propertyName).ToObject<T>();
    }
}