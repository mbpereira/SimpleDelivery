using Core.Orders.Shipment;
using Core.Test.Orders.Shipment.Fakes;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test.Orders.Shipment
{
    public class ShipmentCalculatorTest
    {
        [Fact]
        public async Task ShipmentValueShouldBe10OnDeliveryForSameCity()
        {
            var addressGetter = new AddressGetter();
            var shipmentCalculator = new ShipmentCalculator(addressGetter);

            var a = await shipmentCalculator.Calculate(fromPostalCode: "78058-160", toPostalCode: "78065-230");
            var b = await shipmentCalculator.Calculate(fromPostalCode: "20021-290", toPostalCode: "22220-001");

            Assert.Equal(10, a.ShipmentValue);
            Assert.Equal(10, b.ShipmentValue);
        }

        [Fact]
        public async Task ShipmentValueShouldBe20OnDeliveryForSameState()
        {
            var addressGetter = new AddressGetter();
            var shipmentCalculator = new ShipmentCalculator(addressGetter);

            var a = await shipmentCalculator.Calculate(fromPostalCode: "78058-160", toPostalCode: "78550-970");
            var b = await shipmentCalculator.Calculate(fromPostalCode: "20021-290", toPostalCode: "28930-970");

            Assert.Equal(20, a.ShipmentValue);
            Assert.Equal(20, b.ShipmentValue);
        }

        [Fact]
        public async Task ShipmentValueShouldBe40OnDeliveryForAnotherCityAndState()
        {
            var addressGetter = new AddressGetter();
            var shipmentCalculator = new ShipmentCalculator(addressGetter);

            var a = await shipmentCalculator.Calculate(fromPostalCode: "78058-160", toPostalCode: "28930-970");
            var b = await shipmentCalculator.Calculate(fromPostalCode: "20021-290", toPostalCode: "78550-970");

            Assert.Equal(40, a.ShipmentValue);
            Assert.Equal(40, b.ShipmentValue);
        }
    }
}
