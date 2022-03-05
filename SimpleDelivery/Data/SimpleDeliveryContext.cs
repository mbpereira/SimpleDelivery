using Data.Entities.Catalog;
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
    }
}
