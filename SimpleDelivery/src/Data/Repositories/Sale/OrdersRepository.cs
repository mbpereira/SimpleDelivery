using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Sale
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly SimpleDeliveryContext _context;

        public OrdersRepository(SimpleDeliveryContext context)
        {
            _context = context;
        }

        public async Task Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
        }

        public async Task DeleteByKey(params object[] key)
        {
            var entity = await GetByKey(key);
            _context.Orders.Remove(entity);
        }

        public async Task<IList<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            return await _context.Orders.FirstOrDefaultAsync(f => f.Id.Equals(id));
        }

        public Task Update(Order entity)
        {
            return Task.Run(() => _context.Orders.Update(entity));
        }
    }
}
