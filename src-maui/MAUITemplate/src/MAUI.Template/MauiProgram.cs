using CommunityToolkit.Maui;

namespace MAUI.Template;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit();

		AppContainer.Initialize(builder.Services);

		return builder.Build();
	}
}
