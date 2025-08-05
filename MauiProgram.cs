using CommunityToolkit.Maui;
using maui_sample.Configuration;
using Microsoft.Extensions.Logging;

namespace maui_sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		MauiAppBuilder? builder = MauiApp.CreateBuilder();

		builder.UseMauiApp<App>()
			   // That configuration is more normaly
			   .StartAppFullSize()
			   // But, this setup is more cool. Like game mode.
			   //.StartAppFullScreen()
			   .ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				})
				.UseMauiCommunityToolkit();

		builder.Services.AddDialogService();
		builder.Services.AddDataBaseService();
		builder.Services.AddViewModels();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}