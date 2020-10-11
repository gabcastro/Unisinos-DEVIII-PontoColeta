using Microsoft.EntityFrameworkCore;

namespace PontoColeta.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Items> Items { get; set; }

        public DbSet<Coordinates> Coordinates { get; set; }
    }
}
