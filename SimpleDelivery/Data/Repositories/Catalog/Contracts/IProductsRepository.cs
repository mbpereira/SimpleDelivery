using Data.Entities.Catalog;
using Data.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Catalog.Contracts
{
    public interface IProductsRepository : IBasicRepository<Product>
    {
        Task<IList<Product>> GetAllByDescription(string description);
    }
}
