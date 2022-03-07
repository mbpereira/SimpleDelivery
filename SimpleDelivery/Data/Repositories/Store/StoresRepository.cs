using Data.Entities.Store;
using Data.Repositories.Store.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Store
{
    public class StoresRepository : IStoresRepository
    {
        private readonly SimpleDeliveryContext _context;

        public StoresRepository(SimpleDeliveryContext context)
        {
            _context = context;
        }

        public async Task Add(StoreInfo entity)
        {
            await _context.Stores.AddAsync(entity);
        }

        public async Task DeleteByKey(params object[] key)
        {
            var store = await GetByKey(key);
            _context.Stores.Remove(store);
        }

        public async Task<IList<StoreInfo>> GetAll()
        {
            return await _context.Stores.ToListAsync();
        }

        public Task<StoreInfo> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            return _context.Stores.FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public Task Update(StoreInfo entity)
        {
            return Task.Run(() => _context.Stores.Update(entity));
        }
    }
}
