using Data.Entities.Sale;
using Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Sale.Contracts
{
    public interface IOrdersRepository : IBasicRepository<Order>
    {
        Task<IList<Order>> GetByInterval(DateTime from, DateTime to, OrderStatus[] status = null);
    }
}
