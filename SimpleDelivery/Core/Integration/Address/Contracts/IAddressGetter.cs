using Core.Orders.Shipment.Models;
using System.Threading.Tasks;

namespace Core.Integration.Address.Contracts
{
    public interface IAddressGetter
    {
        Task<AddressInfo> GetAddress(string postalCode);
    }
}
