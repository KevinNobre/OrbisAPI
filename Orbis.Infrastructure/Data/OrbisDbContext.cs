using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Infrastructure.Data
{
    public class OrbisDbContext : DbContext
    {
        public OrbisDbContext(DbContextOptions<OrbisDbContext> options) : base(options)
        {
        }
    }
}
