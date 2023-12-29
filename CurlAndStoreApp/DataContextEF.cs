using Azure.Security.KeyVault.Secrets;
using CurlAndStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace CurlAndStoreApp
{
    public class DataContextEF : DbContext
    {
        private readonly string _dbString;
        public DataContextEF(string dbString)
        {
            _dbString = dbString;
        }
        public DbSet<Article>? Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer($"{_dbString}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("NewsDb");
            modelBuilder.Entity<Article>().ToTable("Articles");
        }

    }
}