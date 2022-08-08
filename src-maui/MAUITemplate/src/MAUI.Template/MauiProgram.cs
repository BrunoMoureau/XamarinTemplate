using CommunityToolkit.Maui;
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
		appContainer.Initialize(builder.Services);

		return builder.Build();
	}
}
