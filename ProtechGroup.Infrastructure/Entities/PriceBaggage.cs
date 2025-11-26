using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("Airport")]
    public class PriceBaggage
    {
        [Key]
        public int Id { get; set; }
        [StringLength(5)]
        public string Airline { get; set; }
        public int TotalKg { get; set; }
        public decimal Price { get; set; }
        public decimal ServiceFee { get; set; }

    }
}
