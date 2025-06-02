using Microsoft.EntityFrameworkCore;
using Orbis.Domain.Entities;
using Orbis.Domain.Repositories;
using Orbis.Infrastructure.Data;

namespace Orbis.Infrastructure.Repositories
{
    public class PedidoAjudaRepository : IPedidoAjudaRepository
    {
        private readonly OrbisDbContext _context;

        public PedidoAjudaRepository(OrbisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoAjuda>> GetAllAsync()
        {
            return await _context.PedidosAjuda.AsNoTracking().Include(p => p.Usuario).ToListAsync();
        }

        public async Task<PedidoAjuda> GetByIdAsync(int id)
        {
            return await _context.PedidosAjuda.Include(p => p.Usuario)
                      .FirstOrDefaultAsync(p => p.PedidoId == id);
        }

        public async Task AddAsync(PedidoAjuda pedido)
        {
            _context.PedidosAjuda.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PedidoAjuda pedido)
        {
            var tracked = await _context.PedidosAjuda.FindAsync(pedido.PedidoId);
            if (tracked != null)
            {
                _context.Entry(tracked).CurrentValues.SetValues(pedido);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var pedido = await _context.PedidosAjuda.FindAsync(id);
            if (pedido != null)
            {
                _context.PedidosAjuda.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}