using Core.Integration.Address.Contracts;
using Core.Orders.Shipment.Contracts;
using Core.Orders.Shipment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Orders.Shipment
{
    public class ShipmentCalculator : IShipmentCalculator
    {
        private readonly IAddressGetter _addressGetter;

        public ShipmentCalculator(IAddressGetter addressGetter)
        {
            _addressGetter = addressGetter;
        }

        public async Task<ShipmentInfo> Calculate(string fromPostalCode, string toPostalCode)
        {
            var tasks = new List<Task<AddressInfo>>()
            {
                _addressGetter.GetAddress(fromPostalCode),
                _addressGetter.GetAddress(toPostalCode)
            };

            var addresses = await Task.WhenAll(tasks);

            var info = new ShipmentInfo()
            {
                Origin = addresses[0],
                Destinantion = addresses[1]
            };

            return GetValue(info);
        }

        private ShipmentInfo GetValue(ShipmentInfo info)
        {
            if (SameCity(info))
                info.ShipmentValue = 10M;
            else if (SameUf(info))
                info.ShipmentValue = 20M;
            else
                info.ShipmentValue = 40M;

            return info;
        }

        private bool SameUf(ShipmentInfo info)
        {
            return Same(info.Origin.Uf, info.Destinantion.Uf);
        }

        private bool SameCity(ShipmentInfo info)
        {
            var sameUf = SameUf(info);
            var sameCity = Same(info.Origin.City, info.Destinantion.City);

            return (sameUf && sameCity);
        }

        private bool Same(string a, string b)
        {
            return a.Equals(b, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
