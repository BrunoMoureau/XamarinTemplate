using CommunityToolkit.Maui;
using MAUI.Basics.Services.Languages;
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
		_appContainer.Initialize(builder.Services);

		var app = builder.Build();

		var languageService = app.Services.GetRequiredService<ILanguageService>();
		languageService.Initialize();
		
		return app;
	}
}
