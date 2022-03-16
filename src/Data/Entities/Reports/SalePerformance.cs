using Data.Entities.Sale;
using System;

namespace Data.Entities.Reports
{
    public class SalePerformance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public decimal ShipmentValue { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusDescription => Status.ToString();
        public decimal GrossTotal { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Profit { get; set; }
        public decimal PercProfit { get; set; }
    }
}
