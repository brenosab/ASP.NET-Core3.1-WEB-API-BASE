using Microsoft.EntityFrameworkCore;

namespace APIorm.Models.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }
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
        }
    }
}