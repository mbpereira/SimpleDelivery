using Data.Entities.Sale;

namespace Data.Entities.Reports
{
    public class OrdersInProgress
    {
        public OrderStatus Status { get; set; }
        public string StatusDescription => Status.ToString();
        public int Quantity { get; set; }
    }
}
