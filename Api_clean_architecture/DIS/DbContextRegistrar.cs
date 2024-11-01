using Api_clean_architecture.Context;
using Microsoft.EntityFrameworkCore;

namespace Api_clean_architecture.DIS;

public static class DbContextRegistrar
{
    public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services)
    {
        services.AddDbContextFactory<TecnicosContext>(o => o.UseSqlServer("Name=SqlConStr"));
        return services;
    }
}
