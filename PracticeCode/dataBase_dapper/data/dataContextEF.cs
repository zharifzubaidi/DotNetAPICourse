using System.Security.Cryptography;
using database.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace database.data
{
    // Dapper class
    public class DataContextEF : DbContext // Inherit from DbContext class
    {
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
                options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;", 
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
