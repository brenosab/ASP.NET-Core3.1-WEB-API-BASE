using GestaoCompras.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoCompras.Infra.Context
{
    public class CompraContext : DbContext
    {
        public CompraContext(DbContextOptions<CompraContext> options) : base(options) { }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<ItemCompra> ItensCompra { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.IdUsuario);
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Nome).HasMaxLength(199).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.DataNascimento).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Email).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Cpf).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Sexo).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<Usuario>()
               .Property(p => p.TipoUsuario).IsRequired();

            modelBuilder.Entity<Produto>()
                .HasKey(p => p.IdProduto);
            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.Codigo)
                .IsUnique();
            modelBuilder.Entity<Produto>()
             .Property(p => p.DataHoraCadastro).IsRequired();
            modelBuilder.Entity<Produto>()
             .Property(p => p.Descricao).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Compra>()
                .HasKey(p => p.IdCompra);
            modelBuilder.Entity<Compra>()
                .HasOne(p => p.Usuario)
                .WithMany(b => b.Compra)
                .HasForeignKey(p => p.UsuarioSolicitante);
            modelBuilder.Entity<ItemCompra>()
                .HasOne(p => p.Compra)
                .WithMany(b => b.ItensCompra)
                .HasForeignKey(p => p.IdCompra);
        }      
    }
}