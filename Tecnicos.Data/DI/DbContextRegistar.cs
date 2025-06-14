
using Api_clean_architecture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Tecnicos.Data.Context;

public static class DbContextRegistar
{
    public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<TecnicosContext>(o => o.UseSqlServer(configuration.GetConnectionString("SqlConStr")));
        return services;
    }
}
