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
                .HasKey(p => p.IdProduto);

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
            )
            
        }*/

    }
}