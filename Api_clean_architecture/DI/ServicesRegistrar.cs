using Api_clean_architecture.DIS;

namespace Api_clean_architecture.DI;

public static class ServicesRegistrar
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.RegisterDbContextFactory();
        services.AddScoped<IClientesServices, ClientesService>();
        return services;
    }
}
