using Data.Entities.Catalog;
using Data.Entities.Reports;
using Data.Entities.Sale;
using Data.Entities.Store;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SimpleDeliveryContext : DbContext
    {
        public SimpleDeliveryContext()
        {
        }

        public SimpleDeliveryContext(DbContextOptions<SimpleDeliveryContext> opt)
            : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ProductSold>()
                .HasNoKey()
                .ToView(null);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<StoreInfo> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductSold> ProductsSold { get; set; }
    }
}
