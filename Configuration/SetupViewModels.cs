
using maui_sample.ViewModels;

namespace maui_sample.Configuration;

public static class SetupViewModels
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
        => services.AddTransient<MainViewModel>();
}