using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Catalog
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Stock { get; set; }
    }
}
