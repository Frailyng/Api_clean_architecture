using Microsoft.Extensions.DependencyInjection;
using Tecnicos.Abstractions;
using Tecnicos.Data.Context;

namespace Tecnicos.Services.DI;

public static class ServicesRegistrar
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.RegisterDbContextFactory();
        services.AddScoped<IClientesService, ClientesService>();
        return services;
    }
    
}
