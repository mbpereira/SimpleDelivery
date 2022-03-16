using Data.Entities.Catalog;
using Data.Repositories.Catalog.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Test.Orders.Fakes
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IList<Product> _products;

        public ProductsRepository(IList<Product> products)
        {
            _products = products;
        }

        public Task Add(Product entity)
        {
            return Task.Run(() => _products.Add(entity));
        }

        public Task DeleteByKey(params object[] key)
        {
            return Task.Run(() =>
            {
                var id = (int)key[0];
                var index = _products.ToList().FindIndex(p => p.Id.Equals(id));
                if (index < 0) return;
                _products.RemoveAt(index);
            });
        }

        public Task<IList<Product>> GetAll()
        {
            return Task.Run(() => _products);
        }

        public Task<IList<Product>> GetAllByDescription(string description)
        {
            IList<Product> products = _products.Where(p => p.Description.Contains(description, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return Task.Run(() => products);
        }

        public Task<Product> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            var entity = _products.FirstOrDefault(p => p.Id.Equals(id));
            return Task.Run(() => entity);
        }

        public Task Update(Product entity)
        {
            return Task.Run(() =>
            {
                var index = _products.ToList().FindIndex(p => p.Id.Equals(entity.Id));
                if (index < 0)
                    return;
                _products[index] = entity;
            });
        }
    }
}
