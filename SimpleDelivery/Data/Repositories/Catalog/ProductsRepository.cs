using Data.Entities.Catalog;
using Data.Repositories.Catalog.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Catalog
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly SimpleDeliveryContext _context;

        public ProductsRepository(SimpleDeliveryContext context)
        {
            _context = context;
        }

        public async Task Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }

        public async Task DeleteByKey(params object[] id)
        {
            var entity = await GetByKey(id);
            _context.Products.Remove(entity);
        }

        public async Task<IList<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IList<Product>> GetAllByDescription(string description)
        {
            return await _context.Products.Where(p => EF.Functions.ILike(p.Description, $"%{description}%")).ToListAsync();
        }

        public Task<Product> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            return _context.Products.FirstAsync(p => p.Id.Equals(id));
        }

        public Task Update(Product entity)
        {
            return Task.Run(() => _context.Products.Update(entity));
        }
    }
}
