﻿
using Api_clean_architecture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tecnicos.Data.Context;

public static class DbContextRegistar
{
    public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services)
    {
        services.AddDbContextFactory<TecnicosContext>(o => o.UseSqlServer("Name=SqlConStr"));
        return services;
    }
}
