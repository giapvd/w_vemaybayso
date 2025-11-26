using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("ServiceFee")]
    public class ServiceFee
    {
        [Key]
        public int Id { get; set; }
        public bool? IsDomestric { get; set; }
        public decimal? Price { get; set; }
        public int? AgencyId { get; set; }
        public int? BeforeFlightDay { get; set; }
        public bool? Status { get; set; }
    }
}
