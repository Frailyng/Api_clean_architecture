using System.Linq.Expressions;
using Tecnicos.Domain.DTOS;

namespace Tecnicos.Abstractions;

public interface IComprasService
{
    Task<bool> Guardar(ComprasDto compra, CancellationToken cancellationToken = default);
    Task<bool> Eliminar(int compraId, CancellationToken cancellationToken = default);
    Task<ComprasDto> Buscar(int id, CancellationToken cancellationToken = default);
    Task<List<ComprasDto>> Listar(Expression<Func<ComprasDto, bool>> criterio, CancellationToken cancellationToken = default);
    Task<bool> ExisteCliente(int id, string descripcion, double monto, CancellationToken cancellationToken = default);
}