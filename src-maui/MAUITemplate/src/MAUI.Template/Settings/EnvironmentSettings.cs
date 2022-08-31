using MAUI.Basics.Services.Settings;

namespace MAUI.Template.Settings
{
    public class EnvironmentSettings : ISettings
    {
        public string SectionName => "Environment";

        public string Name { get; set; }
    }
}