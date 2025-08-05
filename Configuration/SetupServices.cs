using maui_sample.Data;
using maui_sample.Services.Dialog;

namespace maui_sample.Configuration;

public static class SetupServices
{
    public static IServiceCollection AddDataBaseService(this IServiceCollection services)
        => services.AddSingleton<IDatabaseService, DatabaseService>();

    public static IServiceCollection AddDialogService(this IServiceCollection services)
        => services.AddSingleton<IDialogService, DialogService>();
}