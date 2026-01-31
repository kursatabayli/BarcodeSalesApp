using System.Reflection;
using BarcodeSalesApp.App.Services;
using BarcodeSalesApp.Contracts.Options;
using BarcodeSalesApp.Contracts.Services;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using MudBlazor.Services;

namespace BarcodeSalesApp.App;

public static class AppServiceRegistration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AppServiceRegistration).Assembly));
        services.AddMudServices(config => config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft);
        services.AddSingleton<IShortcutService, ShortcutService>();
        services.AddSingleton<ILanguageService, LanguageService>();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton(Preferences.Default);
        services.AddLocalization();

        services.Configure<LocalizationSettings>(options => config.GetSection(nameof(LocalizationSettings)).Bind(options));

        return services;
    }
}
