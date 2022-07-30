using CommunityToolkit.Maui;
using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Services.Languages;
using MAUI.Template.Features.Main;
using MAUI.Template.Services.Containers;

namespace MAUI.Template;

public static class MauiProgram
{
	private static AppContainer _appContainer;

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit();

        _appContainer = new AppContainer();
        _appContainer.Initialize();
            
		var languageService = _appContainer.Resolve<ILanguageService>();
		languageService.Initialize();
		
		return builder.Build();
	}

	public static void NavigateToFirstPage()
	{
        var navigationService = _appContainer.Resolve<INavigationService>();
        navigationService.SetStackRootAsync<MainView>();
    }
}
