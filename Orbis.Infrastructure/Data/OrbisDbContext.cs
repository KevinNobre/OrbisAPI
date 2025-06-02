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
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIO");
            modelBuilder.Entity<CheckSeguranca>().ToTable("TB_CHECK_SEGURANCA");
            modelBuilder.Entity<PedidoAjuda>().ToTable("TB_PEDIDO_AJUDA");
            modelBuilder.Entity<OfertaDoacao>().ToTable("TB_OFERTA_DOACAO");
            modelBuilder.Entity<MatchAjuda>().ToTable("TB_MATCH_AJUDA");
            modelBuilder.Entity<OngParceira>().ToTable("TB_ONG_PARCEIRAS");

            modelBuilder.Entity<PedidoAjuda>(entity =>
            {
                entity.ToTable("TB_PEDIDO_AJUDA");

                entity.HasOne(p => p.Usuario)
                    .WithMany()
                    .HasForeignKey(p => p.UsuarioId)
                    .HasConstraintName("FK_PEDIDO_AJUDA_USUARIO")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OngParceira>(entity =>
            {
                entity.ToTable("TB_ONG_PARCEIRAS");

                entity.HasKey(e => e.OngId);

                entity.Property(e => e.OngId)
                      .HasColumnName("ONG_ID")
                      .ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
