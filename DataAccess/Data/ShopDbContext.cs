using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;


namespace DataAccess.Data
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public ShopDbContext() 
        { 

        }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderDetails> OrderDetails { get; set; } = default!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopPv421;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration can go here
            modelBuilder.SeedCategories();
            modelBuilder.SeedProducts();

            modelBuilder.Entity<OrderDetails>().HasOne(x => x.Order)
                                .WithMany(x => x.Items)
                                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderDetails>().HasOne(x => x.Product)
                                .WithMany(x => x.Orders)
                                .HasForeignKey(x => x.ProductId);
        }

    }
}
