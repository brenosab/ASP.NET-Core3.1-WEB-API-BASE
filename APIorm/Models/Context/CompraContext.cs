using Microsoft.EntityFrameworkCore;

namespace APIorm.Models.Context
{
    public class CompraContext : DbContext
    {
        public CompraContext(DbContextOptions<CompraContext> options) : base(options) { }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ItensCompra> ItensCompras { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.IdProduto);
            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.Codigo)
                .IsUnique();
            modelBuilder.Entity<Produto>()
             .Property(p => p.DataHoraCadastro).IsRequired();
            modelBuilder.Entity<Produto>()
                .HasData(
                    new Produto
                    {
                        IdProduto = 1,
                        Valor = 8.20,
                        Descricao = "ALCOOL EM GEL 1L",
                        Codigo = 1
                    },
                    new Produto
                    {
                        IdProduto = 2,
                        Valor = 1.40,
                        Descricao = "MÁSCARA",
                        Codigo = 22
                    }
                );

            modelBuilder.Entity<Compra>()
                .HasKey(p => p.IdCompra);
            modelBuilder.Entity<Compra>()
                .HasOne(p => p.Usuario)
                .WithMany(b => b.Compra)
                .HasForeignKey(p => p.UsuarioSolicitante);
            modelBuilder.Entity<ItensCompra>()
                .HasOne(p => p.Compra)
                .WithMany(b => b.ItensCompra)
                .HasForeignKey(p => p.IdCompra);
        }      
    }
}