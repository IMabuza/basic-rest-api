using Microsoft.EntityFrameworkCore;
using simpleRestApi.Models;

namespace simpleRestApi.Data
{
    public class DataContextEF : DbContext
    {

        private readonly IConfiguration _config;
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if(!optionsBuilder.IsConfigured){
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                 optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.Id);

            modelBuilder.Entity<Organisation>()
            .ToTable("Organisations")
            .HasKey(u => u.Id);
        }
    }
}