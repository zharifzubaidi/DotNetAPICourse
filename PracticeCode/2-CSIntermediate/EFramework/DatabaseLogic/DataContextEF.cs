
using EFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EFramework.DatabaseLogic
{
    public class DataContextEFramework : DbContext //Inheritance from DbContext
    {
        /*
            Properties for the DbContext related models
            DbSet represents a collection of entities of a specific type
            This allows Entity Framework to map the model to the database table
        */
        public DbSet<Computer>? Computer { get; set; } // DbSet for Computer model

        // Relevant context for the database connection
        // Override the OnConfiguring method to set up the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* 
                Check if the optionsBuilder is not configured
                If not configured, set the connection string for the SQL Server database
                Use the EnableRetryOnFailure option to handle transient errors
                This is useful for applications that require high availability
                and need to retry operations that fail due to temporary issues.
            */
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=CSD-ZHARIF\\SQLEXPRESS;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;",
                    options => options.EnableRetryOnFailure());
            }
        }

        // Override the OnModelCreating method to configure the model 
        // Refer to our Computer model and set the default schema for the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema"); // Set the default schema for the database

            // Configure the Computer entity
            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId); // Set the primary key for the Computer entity
            //  .HasNoKey(); // Disable the primary key for the Computer entity;
            //  .HasKey(c => c.Motherboard);
            //  .ToTable("TableName", "SchemaName")
        }
    }
}