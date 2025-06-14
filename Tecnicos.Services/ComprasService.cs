using Api_clean_architecture.Context;
using Tecnicos.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tecnicos.Abstractions;
using Tecnicos.Domain.DTOS;

namespace Tecnicos.Services;

public class ComprasService(IDbContextFactory<TecnicosContext> DbFactory) : IComprasService
{
    private async Task<bool> Existe(int CompraId, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Compras.AnyAsync(e => e.CompraId == CompraId, cancellationToken);
    }

    private async Task<bool> Insertar(ComprasDto compraDto, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        var compra = new Compras()
        {
            Descripcion = compraDto.Descripcion,
            Monto = compraDto.Monto
        };
        contexto.Compras.Add(compra);
        var guardo = await contexto.SaveChangesAsync(cancellationToken) > 0;
        compraDto.CompraId = compra.CompraId; // Ajuste para asignar el nuevo ID
        return guardo;
    }

    private async Task<bool> Modificar(ComprasDto compraDto, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        var cliente = new Compras()
        {
            CompraId = compraDto.CompraId,
            Descripcion = compraDto.Descripcion,
            Monto = compraDto.Monto
        };
        contexto.Update(cliente);
        var modificado = await contexto.SaveChangesAsync(cancellationToken) > 0;
        return modificado;
    }

    public async Task<bool> Guardar(ComprasDto compra, CancellationToken cancellationToken = default)
    {
        if (!await Existe(compra.CompraId, cancellationToken))
        {
            return await Insertar(compra, cancellationToken);
        }
        else
        {
            return await Modificar(compra, cancellationToken);
        }
    }

    public async Task<bool> Eliminar(int CompraId, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Compras
            .Where(e => e.CompraId == CompraId)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<ComprasDto> Buscar(int id, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        var cliente = await contexto.Compras
            .Where(e => e.CompraId == id)
            .Select(p => new ComprasDto()
            {
                CompraId = p.CompraId,
                Descripcion = p.Descripcion,
                Monto = p.Monto
            }).FirstOrDefaultAsync(cancellationToken);

        return cliente ?? new ComprasDto();
    }

    public async Task<List<ComprasDto>> Listar(Expression<Func<ComprasDto, bool>> criterio, CancellationToken cancellationToken = default)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync(cancellationToken);
        return await contexto.Compras
            .Select(p => new ComprasDto()
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
        return await contexto.Compras
            .AnyAsync(e => e.CompraId != id
            && e.Descripcion.ToLower().Equals(descripcion.ToLower())
            || e.Monto == monto, cancellationToken);
    }
}
