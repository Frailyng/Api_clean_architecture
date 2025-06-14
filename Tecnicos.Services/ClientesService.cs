using Api_clean_architecture.Context;
using Tecnicos.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tecnicos.Abstractions;
using Tecnicos.Domain.DTOS;

namespace Tecnicos.Services;

public class ClientesService(IDbContextFactory<TecnicosContext> DbFactory) : IClientesService
{
    private async Task<bool> Existe(int CompraId, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Clientes.AnyAsync(e => e.CompraId == CompraId, cancellationToken);
    }

    private async Task<bool> Insertar(ClientesDto clienteDto, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        var cliente = new Clientes()
        {
            Descripcion = clienteDto.Descripcion,
            Monto = clienteDto.Monto
        };
        contexto.Clientes.Add(cliente);
        var guardo = await contexto.SaveChangesAsync(cancellationToken) > 0;
        clienteDto.CompraId = cliente.CompraId; // Ajuste para asignar el nuevo ID
        return guardo;
    }

    private async Task<bool> Modificar(ClientesDto clienteDto, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        var cliente = new Clientes()
        {
            CompraId = clienteDto.CompraId,
            Descripcion = clienteDto.Descripcion,
            Monto = clienteDto.Monto
        };
        contexto.Update(cliente);
        var modificado = await contexto.SaveChangesAsync(cancellationToken) > 0;
        return modificado;
    }

    public async Task<bool> Guardar(ClientesDto cliente, CancellationToken cancellationToken = default)
    {
        if (!await Existe(cliente.CompraId, cancellationToken))
        {
            return await Insertar(cliente, cancellationToken);
        }
        else
        {
            return await Modificar(cliente, cancellationToken);
        }
    }

    public async Task<bool> Eliminar(int CompraId, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Clientes
            .Where(e => e.CompraId == CompraId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<ClientesDto> Buscar(int id, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        var cliente = await contexto.Clientes
            .Where(e => e.CompraId == id)
            .Select(p => new ClientesDto()
            {
                CompraId = p.CompraId,
                Descripcion = p.Descripcion,
                Monto = p.Monto
            }).FirstOrDefaultAsync(cancellationToken);

        return cliente ?? new ClientesDto();
    }

    public async Task<List<ClientesDto>> Listar(Expression<Func<ClientesDto, bool>> criterio, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Clientes
            .Select(p => new ClientesDto()
            {
                CompraId = p.CompraId,
                Descripcion = p.Descripcion,
                Monto = p.Monto
            })
            .Where(criterio)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExisteCliente(int id, string descripcion, double monto, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Clientes
            .AnyAsync(e => e.CompraId != id
            && e.Descripcion.ToLower().Equals(descripcion.ToLower())
            || e.Monto == monto, cancellationToken);
    }
}
