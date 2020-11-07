using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PontoColeta.Data.Maps;
using PontoColeta.Models;

namespace PontoColeta.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // private AzureServiceTokenProvider _azureServiceTokenProvider;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }

        // public DataContext() : base()
        // {
        // }
        public DataContext (IConfiguration configuration) //, DbContextOptions<DataContext> options, AzureServiceTokenProvider azureServiceTokenProvider) : base(options)
        {
            _configuration = configuration;
            // _azureServiceTokenProvider = azureServiceTokenProvider;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = _configuration.GetConnectionString("SQLDBConnection")//,
                // AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync("https://pontocoleta.azurewebsites.net").Result
            };
            optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CoordinateMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}