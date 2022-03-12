using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Sale
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly SimpleDeliveryContext _context;

        public CustomersRepository(SimpleDeliveryContext context)
        {
            _context = context;
        }

        public async Task Add(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
        }

        public async Task DeleteByKey(params object[] key)
        {
            var entity = await GetByKey(key);
            _context.Customers.Remove(entity);
        }

        public async Task<IList<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public Task Update(Customer entity)
        {
            return Task.Run(() => _context.Customers.Update(entity));
        }
    }
}
