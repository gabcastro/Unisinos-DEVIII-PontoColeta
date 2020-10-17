using Microsoft.EntityFrameworkCore;
using PontoColeta.Data.Maps;
using PontoColeta.Models;

namespace PontoColeta.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=pontocoleta;User ID=SA;Password=Yhsf1oI_+FJG*&S#Jss");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CoordinateMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}