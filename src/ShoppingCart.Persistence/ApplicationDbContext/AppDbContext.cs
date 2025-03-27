using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Persistence.ApplicationDbContext
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasOne(ci => ci.User).WithMany(u => u.CartItems);
            modelBuilder.Entity<CartItem>().HasOne(ci => ci.Product);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "user1", PasswordHash = "hashedpassword1" },
                new User { Id = 2, Username = "user2", PasswordHash = "hashedpassword2" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                new Product { Id = 2, Name = "Smartphone", Price = 499.99m },
                new Product { Id = 3, Name = "Headphones", Price = 79.99m }
            );
        }
    }

}
