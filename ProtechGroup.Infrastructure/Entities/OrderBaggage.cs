using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderBaggage")]
    public class OrderBaggage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderTravellerId { get; set; }
        [StringLength(10)]
        public string AirlineOutBound { get; set; }
        public decimal PriceOutBound { get; set; }
        public decimal BasePriceOutBound { get; set; }
        public decimal ServiceOutBound { get; set; }
        public int KgOutBound { get; set; }
        [StringLength(10)]
        public string AirlineInBound { get; set; }
        public decimal PriceInBound { get; set; }
        public decimal BasePriceInBound { get; set; }
        public decimal ServiceInBound { get; set; }   
        public int KgInBound { get; set; }
    }
}
