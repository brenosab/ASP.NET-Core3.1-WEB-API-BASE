using Microsoft.EntityFrameworkCore;

namespace APIorm.Models.Context
{
    public class CompraContext : DbContext
    {
        public CompraContext(DbContextOptions<CompraContext> options)
            : base(options)
        {

        }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ItensCompra> ItensCompras { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItensCompra>()
                .HasOne(p => p.Compra)
                .WithMany(b => b.ItensCompra)
                .HasForeignKey(p => p.IdCompra);

            modelBuilder.Entity<ItensCompra>()
                .HasOne(p => p.Produto)
                .WithMany(b => b.ItensCompra)
                .HasForeignKey(p => p.IdProduto);
        }
      
    }
}