using Api_clean_architecture.Context;
using Microsoft.EntityFrameworkCore;
using Tecnicos.Abstractions;

namespace Tecnicos.Services;

public class ClientesService(IDbContextFactory<TecnicosContext> DbFactory) : IClientesService
{
    private async Task<bool> Existe(int ClienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(e => e.ClienteId == ClienteId);
    }

  

}
