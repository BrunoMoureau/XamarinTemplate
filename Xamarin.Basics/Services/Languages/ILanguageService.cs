using System.Globalization;

namespace Xamarin.Basics.Services.Languages
{
    public interface ILanguageService
    {
        CultureInfo CultureInfo { get; }
        
        void Initialize();
        void SetCulture(string culture);
    }
}