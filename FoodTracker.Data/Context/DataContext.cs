namespace FoodTracker.Data.Context
{
    using System.IO;
    using Core.Entities;
    using FoodTracker.Data.Context.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class DataContext : DbContext
    {
        #region Constructors



        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultDatabase"));
            base.OnConfiguring(optionsBuilder);
        }

        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<UserWeight> UserWeight { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserWeightConfiguration());
        }
    }
}