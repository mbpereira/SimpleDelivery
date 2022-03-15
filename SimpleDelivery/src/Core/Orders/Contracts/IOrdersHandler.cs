using Data.Entities.Sale;
using System.Threading.Tasks;

namespace Core.Orders.Contracts
{
    public interface IOrdersHandler
    {
        Task Cancel(int idOrder);
        Task Create(Order orde);
        Task Delete(int idOrder);
        Task Update(Order updatedOrder);
        Task Deliver(int id);
        Task Prepare(int id);
        Task Approve(int id);
    }
}
