using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Store
{
    public class StoreInfo
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        [Required, MaxLength(9)]
        public string PostalCode { get; set; }
        [Required, MaxLength(255)]
        public string Address { get; set; }
        public int Number { get; set; }
        [Required, MaxLength(50)]
        public string State { get; set; }
        [Required, MaxLength(2)]
        public string Uf { get; set; }
        [Required, MaxLength(50)]
        public string Country { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
    }
}
