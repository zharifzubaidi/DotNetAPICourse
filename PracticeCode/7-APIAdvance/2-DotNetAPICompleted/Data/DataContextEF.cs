using DotNetAPI.UserJobInfoModels;
using DotNetAPI.UserModels;
using DotNetAPI.UserSalaryModels;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // We need DbSet to map our models to the database
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        
        // Configure the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        // Need to tell EF Core how to connect to the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema"); // Take to the correct table schema

            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")  // Needed because different name
                .HasKey(u => u.UserId);                 // Set UserId as the primary key. Autopopulated by SQL Server

            modelBuilder.Entity<UserJobInfo>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId);
        }


    }
}