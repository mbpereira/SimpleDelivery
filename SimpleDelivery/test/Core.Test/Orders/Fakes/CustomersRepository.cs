using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Test.Orders.Fakes
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IList<Customer> _customers;

        public CustomersRepository(IList<Customer> customers)
        {
            _customers = customers;
        }

        public Task Add(Customer entity)
        {
            return Task.Run(() => _customers.Add(entity));
        }

        public Task DeleteByKey(params object[] key)
        {
            return Task.Run(() =>
            {
                var id = (int)key[0];
                var index = _customers.ToList().FindIndex(p => p.Id.Equals(id));
                if (index <= 0) return;
                _customers.RemoveAt(index);
            });
        }

        public Task<IList<Customer>> GetAll()
        {
            return Task.Run(() => _customers);
        }

        public Task<Customer> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            var entity = _customers.FirstOrDefault(p => p.Id.Equals(id));
            return Task.Run(() => entity);
        }

        public Task Update(Customer entity)
        {
            return Task.Run(() =>
            {
                var index = _customers.ToList().FindIndex(p => p.Id.Equals(entity.Id));
                if (index <= 0)
                    return;
                _customers[index] = entity;
            });
        }
    }
}
