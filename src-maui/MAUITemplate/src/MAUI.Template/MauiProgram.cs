using CommunityToolkit.Maui;
using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Services.Languages;
using MAUI.Template.Features.Main;
using MAUI.Template.Services.Containers;

namespace MAUI.Template;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit();
		
		var appContainer = new AppContainer();
		appContainer.Initialize();
            
		var languageService = appContainer.Resolve<ILanguageService>();
		languageService.Initialize();
            
		var navigationService = appContainer.Resolve<INavigationService>();
		navigationService.SetStackRootAsync<MainView>();
		
		return builder.Build();
	}
}
