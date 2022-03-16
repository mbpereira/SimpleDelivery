using Data.Entities.Catalog;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Sale
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitDiscount { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Quantity { get; set; }
        public Product Product { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Order Order { get; set; }
        [Required]
        public int OrderId { get; set; }
        [NotMapped]
        public decimal TotalCost => UnitCost * Quantity;
        [NotMapped]
        public decimal TotalDiscount => UnitDiscount * Quantity;
        [NotMapped]
        public decimal GrossTotal => UnitPrice * Quantity;
        [NotMapped]
        public decimal NetTotal => GrossTotal - TotalDiscount;
        public OrderItem Clone()
        {
            return (OrderItem)MemberwiseClone();
        }
    }
}
