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
    }
}