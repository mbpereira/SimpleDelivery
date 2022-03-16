using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Sale
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required, MaxLength(9)]
        public string PostalCode { get; set; }
        [Required, MaxLength(255)]
        public string Address { get; set; }
        public int Number { get; set; }
        [Required, MaxLength(2)]
        public string Uf { get; set; }
        [Required, MaxLength(50)]
        public string Country { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
    }
}
