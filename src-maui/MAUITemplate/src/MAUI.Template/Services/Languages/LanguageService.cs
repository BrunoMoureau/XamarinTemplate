using System.Globalization;
using MAUI.Basics.Services.Languages;
using MAUI.Template.Markups;
using MAUI.Template.Resources.Languages;

namespace MAUI.Template.Services.Languages
{
    public class LanguageService : ILanguageService
    {
        public CultureInfo CultureInfo => LocalizationResourceManager.Current.CurrentCulture;

        public void Initialize()
        {
            LocalizationResourceManager.Current.PropertyChanged += (_, _) =>
                AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
        }

        public void SetCulture(string culture)
        {
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(culture);
        }
    }
}