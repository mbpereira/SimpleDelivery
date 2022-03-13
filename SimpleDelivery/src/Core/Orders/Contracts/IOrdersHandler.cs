using Data.Entities.Sale;
using System.Threading.Tasks;

namespace Core.Orders.Contracts
{
    public interface IOrdersHandler
    {
        Task Create(Order orde);
    }
}
