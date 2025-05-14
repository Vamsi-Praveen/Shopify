using Microsoft.EntityFrameworkCore;
using Shopify.Core.Entities;

namespace Shopify.Core.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> users {  get; set; }

        public DbSet<Orders> orders { get; set; }

        public DbSet<Product> product { get; set; }

        public DbSet<OrderItem> orderItem { get; set; }

        public DbSet<ProductImage> productImage { get; set; }

        public DbSet<Address> addresses { get; set; }
    }
}
