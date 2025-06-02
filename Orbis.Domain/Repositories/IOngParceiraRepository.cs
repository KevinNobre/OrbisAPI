using Orbis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Repositories
{
    public interface IOngParceiraRepository
    {
        Task<IEnumerable<OngParceira>> GetAllAsync();
        Task<OngParceira> GetByIdAsync(int id);
        Task AddAsync(OngParceira ong);
        Task UpdateAsync(OngParceira ong);
        Task DeleteAsync(int id);
    }
}