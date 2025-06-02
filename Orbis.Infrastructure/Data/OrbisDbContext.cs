using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbis.Domain.Entities;

namespace Orbis.Infrastructure.Data
{
    public class OrbisDbContext : DbContext
    {
        public OrbisDbContext(DbContextOptions<OrbisDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CheckSeguranca> CheckSegurancas { get; set; }
        public DbSet<PedidoAjuda> PedidosAjuda { get; set; }
        public DbSet<OfertaDoacao> OfertasDoacao { get; set; }
        public DbSet<MatchAjuda> MatchesAjuda { get; set; }
        public DbSet<OngParceira> OngsParceiras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de entidades, relacionamentos e tabelas
            modelBuilder.Entity<OngParceira>().ToTable("tb_ong_parceiras");
            modelBuilder.Entity<MatchAjuda>().ToTable("tb_match_ajuda");
            modelBuilder.Entity<OfertaDoacao>().ToTable("tb_oferta_doacao");
            modelBuilder.Entity<PedidoAjuda>().ToTable("tb_pedido_ajuda");
            base.OnModelCreating(modelBuilder);
        }
    }
}
