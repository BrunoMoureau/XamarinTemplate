using CommunityToolkit.Maui;
using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Basics.Services.Languages;
using MAUI.Template.Features.Main;
using MAUI.Template.Services.Containers;

namespace MAUI.Template;

public static class MauiProgram
{
	private static AppContainer2 _appContainer;

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit();

		_appContainer = new AppContainer2();
		_appContainer.Initialize(builder.Services);

		// _appContainer = new AppContainer();
        // _appContainer.Initialize();

		var app = builder.Build();

		var languageService = app.Services.GetRequiredService<ILanguageService>();
		languageService.Initialize();
		
		return app;
	}
}
