using Microsoft.EntityFrameworkCore;

namespace BusinessControlApp.Models.DB
{
    public class BusinessControlDBContext : DbContext
    {
        public BusinessControlDBContext(DbContextOptions<BusinessControlDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Business>().ToTable("business");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<MenuItem>().ToTable("menu_items");
            modelBuilder.Entity<UserType>().ToTable("user_types");

            // agregar datos iniciales
            modelBuilder.Entity<UserType>().HasData(
                new UserType { Id = 1, Type = "Admin" },
                new UserType { Id = 2, Type = "User" }
            );
            modelBuilder.Entity<User>().HasData(
                // usuario admin
                new User
                {
                    Id = 1,
                    Names = "admin",
                    Lastnames = "admin",
                    UserTypeId = 1,
                    Password = "123",
                    Email = "admin@test.com"
                },
                // usuario user
                new User
                {
                    Id = 2,
                    Names = "user",
                    Lastnames = "user",
                    UserTypeId = 2,
                    Password = "123",
                    Email = "user@test.com"
                }

            );
            // agregar un negocio
            modelBuilder.Entity<Business>().HasData(
                new Business
                {
                    Id = 1,
                    Name = "Negocio 1",
                    Description = "Descripcion del negocio 1",
                    CreationDate = System.DateTime.Now,
                    UserId = 1
                }
            );

        }
    }
}