using Microsoft.EntityFrameworkCore;
using Orbis.Domain.Entities;
using Orbis.Domain.Repositories;
using Orbis.Infrastructure.Data;

namespace Orbis.Infrastructure.Repositories
{
    public class OngParceiraRepository : IOngParceiraRepository
    {
        private readonly OrbisDbContext _context;

        public OngParceiraRepository(OrbisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OngParceira>> GetAllAsync()
        {
            return await _context.OngsParceiras.AsNoTracking().ToListAsync();
        }

        public async Task<OngParceira> GetByIdAsync(int id)
        {
            return await _context.OngsParceiras.AsNoTracking().FirstOrDefaultAsync(o => o.OngId == id);
        }

        public async Task AddAsync(OngParceira ong)
        {
            // Caso o banco gere automaticamente o id, zere para evitar conflito
            ong.OngId = 0;
            _context.OngsParceiras.Add(ong);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OngParceira ong)
        {
            // Buscar entidade existente para evitar rastreamento duplicado
            var existing = await _context.OngsParceiras.FindAsync(ong.OngId);
            if (existing == null)
                throw new KeyNotFoundException($"OngParceira com Id {ong.OngId} não encontrada.");

            // Atualizar campos manualmente
            existing.Nome = ong.Nome;
            existing.Localidade = ong.Localidade;
            existing.TipoOng = ong.TipoOng;
            existing.Telefone = ong.Telefone;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ong = await _context.OngsParceiras.FindAsync(id);
            if (ong != null)
            {
                _context.OngsParceiras.Remove(ong);
                await _context.SaveChangesAsync();
            }
        }
    }
}
