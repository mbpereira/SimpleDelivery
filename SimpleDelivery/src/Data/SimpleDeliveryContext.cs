using Data.Entities.Catalog;
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

        public DbSet<Product> Products { get; set; }
        public DbSet<StoreInfo> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
