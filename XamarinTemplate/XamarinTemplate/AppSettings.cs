using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace XamarinTemplate
{
    public class AppSettings
    {
        private static AppSettings _current;
        public static AppSettings Current => _current ??= new AppSettings();

        private readonly JObject _configuration;

        private AppSettings()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = GetAppSettingsFilePath(assembly);
                _configuration = GetAppSettingsAsJson(assembly, resourceName);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to load configuration file");
            }
        }

        private string GetAppSettingsFilePath(Assembly assembly) => 
            assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));

        private JObject GetAppSettingsAsJson(Assembly assembly, string resourceName)
        {
            using var file = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(file ?? throw new InvalidOperationException());

            var fileContent = reader.ReadToEnd();
            return JObject.Parse(fileContent);
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _configuration[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        var s = path[index];
                        node = node[s];
                    }

                    return node?.ToString() ?? string.Empty;
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve configuration '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}