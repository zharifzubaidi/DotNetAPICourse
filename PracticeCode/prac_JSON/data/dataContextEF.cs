using System.Security.Cryptography;
using prac_JSON.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace prac_JSON.data
{
    // Dapper class
    public class DataContextEF : DbContext // Inherit from DbContext class
    {
        // Private field in class
        private IConfiguration _config;

        // Class constructor that allow DataContextDapper class to receive an object as argument
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // A method that connect into db set
        public DbSet<Computer>? Computer { get; set; }
        // Override parent class method
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Connecting to SQL server
            // Check if connection is already configured
            // Retry on failure
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"), 
                    options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
                //.HasNoKey();
                //.ToTable("Computer","TutorialAppSchema");
                //.ToTable("TableName","SchemaName");
        }
    }
}
