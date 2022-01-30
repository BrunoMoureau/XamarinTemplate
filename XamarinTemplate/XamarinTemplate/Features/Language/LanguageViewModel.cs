using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Services.Languages;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace XamarinTemplate.Features.Language
{
    public class LanguageViewModel : ObservableObject, IViewModel
    {
        private readonly ILanguageService _languageService;
        public ICommand SetCultureCommand { get; }

        public LanguageViewModel(ILanguageService languageService) 
        {
            _languageService = languageService;
            
            SetCultureCommand = new Command<string>(SetCulture);
        }

        public void Load()
        {
        }

        public Task InitializeAsync(object @params) => Task.CompletedTask;

        public void Unload()
        {
        }

        private void SetCulture(string culture) => _languageService.SetCulture(culture);
    }
}