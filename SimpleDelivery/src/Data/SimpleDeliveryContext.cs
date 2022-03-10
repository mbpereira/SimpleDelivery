using Data.Entities.Catalog;
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
    }
}
