using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Test.Orders.Fakes
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IList<Order> _orders;

        public OrdersRepository(IList<Order> orders)
        {
            _orders = orders;
        }

        public Task Add(Order entity)
        {
            return Task.Run(() => _orders.Add(entity));
        }

        public Task DeleteByKey(params object[] key)
        {
            return Task.Run(() =>
            {
                var id = (int)key[0];
                var index = _orders.ToList().FindIndex(p => p.Id.Equals(id));
                if (index < 0) return;
                _orders.RemoveAt(index);
            });
        }

        public Task<IList<Order>> GetAll()
        {
            return Task.Run(() => _orders);
        }

        public Task<IList<Order>> GetByInterval(System.DateTime from, System.DateTime to, OrderStatus[] status)
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetByKey(params object[] key)
        {
            var id = (int)key[0];
            var entity = _orders.FirstOrDefault(p => p.Id.Equals(id));
            return Task.Run(() => entity);
        }

        public Task Update(Order entity)
        {
            return Task.Run(() =>
            {
                var index = _orders.ToList().FindIndex(p => p.Id.Equals(entity.Id));
                if (index < 0)
                    return;
                _orders[index] = entity;
            });
        }
    }
}
