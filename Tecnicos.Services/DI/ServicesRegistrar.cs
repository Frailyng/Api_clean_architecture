using Api_clean_architecture.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Tecnicos.Abstractions;
using Tecnicos.Data.Context;

namespace Tecnicos.Services.DI;

public static class ServicesRegistrar
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDbContextFactory(configuration);
        services.AddScoped<IComprasService, ComprasService>();
        return services;
    }
}