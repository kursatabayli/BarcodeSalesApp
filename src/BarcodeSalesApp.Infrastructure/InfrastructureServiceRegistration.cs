using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Infrastructure.Context;
using BarcodeSalesApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Infrastructure;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "BarcodeAppDb.db3");
    services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Filename={dbPath}"), ServiceLifetime.Scoped);

    services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ISalesRecordRepository, SalesRecordRepository>();
    services.AddScoped<IStockRepository, StockRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    return services;
  }
}
