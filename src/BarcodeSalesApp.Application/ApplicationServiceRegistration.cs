using BarcodeSalesApp.Application.Services;
using BarcodeSalesApp.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BarcodeSalesApp.Application;

public static class ApplicationServiceRegistration
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApplicationServiceRegistration).Assembly));
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));
    services.AddScoped<ISalesService, SalesService>();
    return services;
  }

}
