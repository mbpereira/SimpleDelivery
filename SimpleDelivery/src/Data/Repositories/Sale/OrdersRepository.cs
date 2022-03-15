using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Sale
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly SimpleDeliveryContext _context;
        private IQueryable<Order> _query => _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Itens)
                .ThenInclude(i => i.Product);

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
            return await _query.ToListAsync();
        }

        public async Task<Order> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            return await _query.FirstOrDefaultAsync(f => f.Id.Equals(id));
        }

        public Task Update(Order entity)
        {
            return Task.Run(() => _context.Orders.Update(entity));
        }

        public async Task<IList<Order>> GetByInterval(System.DateTime from, System.DateTime to)
        {
            return await _query.Where(o => o.Date >= from && o.Date <= to).ToListAsync();
        }
    }
}
