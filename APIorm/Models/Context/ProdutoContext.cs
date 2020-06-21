using Microsoft.EntityFrameworkCore;

namespace APIorm.Models.Context
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options)
            : base(options)
        {

        }
        public DbSet<Produto> Produtos { get; set; }
       /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasData(
                    new Produto
                    {
                        Id = 1,
                        Descricao = "ALCOOL EM GEL 1L",
                        Codigo = 1
                    },
                    new Produto
                    {
                        Id = 2,
                        Descricao = "MÁSCARA",
                        Codigo = 22
                    }
                );
        }
        */
    }
}