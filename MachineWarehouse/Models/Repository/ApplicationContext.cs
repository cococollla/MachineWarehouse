using MachineWarehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineWarehouse.Models.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarColor> Colors { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role[]
                {
                    new Role {Id = 1, RoleName = "Admin" },
                    new Role {Id = 2, RoleName = "Manager" },
                    new Role {Id = 3, RoleName = "User" }
                }) ;

            modelBuilder.Entity<CarColor>().HasData(
                new CarColor[]
                {
                    new CarColor {Id = 1, Color = "Red"},
                    new CarColor {Id = 2, Color = "Blue"},
                    new CarColor {Id = 3, Color = "Silver"}
                });

            modelBuilder.Entity<CarBrand>().HasData(
                new CarBrand[] 
                { 
                    new CarBrand {Id = 1, Brand = "BMW"},
                    new CarBrand {Id = 2, Brand = "Nissan"},
                    new CarBrand {Id = 3, Brand = "Lexus"}
                });
        }
    }
}
