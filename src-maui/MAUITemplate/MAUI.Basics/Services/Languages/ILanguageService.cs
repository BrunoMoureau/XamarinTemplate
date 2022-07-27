using System.Globalization;

namespace MAUI.Basics.Services.Languages
{
    public interface ILanguageService
    {
        CultureInfo CultureInfo { get; }
        
        void Initialize();
        void SetCulture(string culture);
    }
}