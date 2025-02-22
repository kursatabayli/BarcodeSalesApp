using CommunityToolkit.Maui;
using Barkod.Services;
using Barkod.Models;
using Barkod.Repositories;
using Barkod.Interfaces;
using SQLite;
using MudBlazor.Services;
using Barkod.Helpers;
using MudBlazor;

namespace Barkod.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AppServices(this IServiceCollection services)
        {
            services.AddMauiBlazorWebView();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "BarkodDB.db3");
            services.AddSingleton(new SQLiteAsyncConnection(dbPath));

            services.AddMudServices(config =>
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft);


            services.AddScoped<IRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<IRepository<SalesRecord>, GenericRepository<SalesRecord>>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalesRecordRepository, SalesRecordRepository>();
            services.AddScoped<INavigationService, NavigationService>();

            services.AddScoped<ProductService>();
            services.AddScoped<SalesService>();
            services.AddScoped<SalesReportService>();
            services.AddSingleton<FocusManager>();


            services.AddLocalization();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IShortcutService, ShortcutService>();
            services.AddScoped<IUIStateService, UIStateService>();
            
            
            return services;
        }
    }
}
