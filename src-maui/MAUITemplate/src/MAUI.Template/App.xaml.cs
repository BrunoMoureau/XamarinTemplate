using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Services.Languages;
using MAUI.Template.Features.Main;

namespace MAUI.Template;

public partial class App
{
	public App(ILanguageService languageService, INavigationService navigationService)
	{
		InitializeComponent();

        languageService.Initialize();

        navigationService.SetStackRootAsync<MainView>();
    }
}
