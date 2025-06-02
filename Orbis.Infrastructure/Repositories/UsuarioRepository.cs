using Microsoft.EntityFrameworkCore;
using Orbis.Domain.Entities;
using Orbis.Domain.Repositories;
using Orbis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly OrbisDbContext _context;
        public UsuarioRepository(OrbisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync() =>
            await _context.Usuarios.ToListAsync();

        public async Task<Usuario> GetByIdAsync(int id) =>
            await _context.Usuarios.FindAsync(id);

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Usuario usuario)
        {
            // Verifique se o usuário paciente existe
            var existingUsuario = await GetByIdAsync(usuario.UsuarioId);

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}