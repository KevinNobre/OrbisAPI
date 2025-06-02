using Orbis.Domain.Entities;

namespace Orbis.Domain.Repositories
{
    public interface IPedidoAjudaRepository
    {
        Task<IEnumerable<PedidoAjuda>> GetAllAsync();
        Task<PedidoAjuda> GetByIdAsync(int id);
        Task AddAsync(PedidoAjuda pedido);
        Task UpdateAsync(PedidoAjuda pedido);
        Task DeleteAsync(int id);
    }
}
