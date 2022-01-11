using Microsoft.EntityFrameworkCore;

namespace APIorm.Models.Context
{
    public class CompraContext : DbContext
    {
        public CompraContext(DbContextOptions<CompraContext> options) : base(options) { }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<ItensCompra> ItensCompra { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.IdUsuario);
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.DataNascimento).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Email).IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(p => p.Cpf).IsRequired();
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