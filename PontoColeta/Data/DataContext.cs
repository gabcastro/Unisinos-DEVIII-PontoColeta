using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PontoColeta.Data.Maps;
using PontoColeta.Models;

namespace PontoColeta.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CoordinateMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}