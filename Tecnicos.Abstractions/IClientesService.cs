using System.Linq.Expressions;
using Tecnicos.Domain.DTOS;

namespace Tecnicos.Abstractions;

public interface IClientesService
{
    Task<bool> Guardar(ClientesDto cliente, CancellationToken cancellationToken = default);
    Task<bool> Eliminar(int clienteId, CancellationToken cancellationToken = default);
    Task<ClientesDto> Buscar(int id, CancellationToken cancellationToken = default);
    Task<List<ClientesDto>> Listar(Expression<Func<ClientesDto, bool>> criterio, CancellationToken cancellationToken = default);
    Task<bool> ExisteCliente(int id, string descripcion, double monto, CancellationToken cancellationToken = default);
}