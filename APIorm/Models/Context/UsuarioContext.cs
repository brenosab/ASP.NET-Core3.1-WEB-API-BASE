using Microsoft.EntityFrameworkCore;

namespace APIorm.Models.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
          : base(options)
        {

        }
        public DbSet<Usuario> Usuario { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.IdUsuario);
            
            modelBuilder.Entity<Usuario>()
                .HasData(
                new Usuario
                {
                    IdProduto = 1,
                    Valor = 8.20,
                    Descricao = "ALCOOL EM GEL 1L",
                    Codigo = 1
                },
                new Usuario
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