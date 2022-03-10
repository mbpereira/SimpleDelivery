namespace Core.Orders.Shipment.Models
{
    public class ShipmentInfo
    {
        public AddressInfo Origin { get; set; }
        public AddressInfo Destinantion { get; set; }
        public decimal ShipmentValue { get; set; }
    }
}
