using Core.Orders.Shipment.Models;
using System.Threading.Tasks;

namespace Core.Orders.Shipment.Contracts
{
    public interface IShipmentCalculator
    {
        Task<ShipmentInfo> Calculate(string fromPostalCode, string toPostalCode);
    }
}
