using System.Globalization;
using Xamarin.Basics.Services.Languages;
using Xamarin.CommunityToolkit.Helpers;
using XamarinTemplate.Resources.Languages;

namespace XamarinTemplate.Services.Languages
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