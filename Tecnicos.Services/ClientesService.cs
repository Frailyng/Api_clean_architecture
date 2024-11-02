using Api_clean_architecture.Context;
using Api_clean_architecture.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tecnicos.Abstractions;
using Tecnicos.Domain.DTOS;

namespace Tecnicos.Services;

public class ClientesService(IDbContextFactory<TecnicosContext> DbFactory) : IClientesService
{
    private async Task<bool> Existe(int ClienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(e => e.ClienteId == ClienteId);
    }

    private async Task<bool> Insertar(ClientesDto clienteDto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = new Clientes()
        {
            Nombres = clienteDto.Nombres,
            WhatsApp = clienteDto.WhatsApp
        };
        contexto.Clientes.Add(cliente);
        var guardo = await contexto.SaveChangesAsync() > 0;
        clienteDto.ClienteId = cliente.ClienteId;
        return guardo;
    }

    private async Task<bool> Modificar(ClientesDto clienteDto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = new Clientes()
        {
            ClienteId = clienteDto.ClienteId,
            Nombres = clienteDto.Nombres,
            WhatsApp = clienteDto.WhatsApp
        };
        contexto.Update(cliente);
        var modificado = await contexto.SaveChangesAsync() > 0;
        return modificado;
    }

    public async Task<bool> Guardar(ClientesDto cliente)
    {
        if (!await Existe(cliente.ClienteId))
        {
            return await Insertar(cliente);
        }
        else
        {
            return await Modificar(cliente);
        }
    }

    public async Task<bool> Eliminar(int ClienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(e => e.ClienteId == ClienteId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<ClientesDto> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = await contexto.Clientes
            .Where(e => e.ClienteId ==  id)
            .Select(p => new ClientesDto()
            {
                ClienteId = p.ClienteId,
                Nombres = p.Nombres,
                WhatsApp = p.WhatsApp
            }).FirstOrDefaultAsync();

        return cliente ?? new ClientesDto();
    }

    public async Task<List<ClientesDto>> Listar(Expression<Func<ClientesDto, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Select(p => new ClientesDto()
            {
                ClienteId = p.ClienteId,
                Nombres = p.Nombres,
                WhatsApp = p.WhatsApp
            })
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteCliente(int id, string nombres, string whatsapp)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(e => e.ClienteId != id
            && e.Nombres.ToLower().Equals(nombres.ToLower())
            || e.WhatsApp.ToLower().Equals(whatsapp.ToLower()));
    }


}
