using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("SearchInput")]
    public class SearchInput
    {
        [Key]
        public int Id { get; set; }

        public int SessionId { get; set; }

        public bool IsRoundTrip { get; set; }

        [Required]
        [StringLength(5)]
        public string DepartureAirport { get; set; }

        [Required]
        [StringLength(5)]
        public string ArrivalAirport { get; set; }

        [Required]
        [StringLength(50)]
        public string DepartureCity { get; set; }

        [Required]
        [StringLength(50)]
        public string ArrivalCity { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int RequestMaxResult { get; set; }

        public byte AdultNumber { get; set; }

        public byte ChildNumber { get; set; }

        public byte InfantNumber { get; set; }

        public byte TotalPax { get; set; }

        public DateTime DateTimeInsert { get; set; }

        public bool IsSearchDomestic { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }

        public bool SearchIndividual { get; set; }

        [StringLength(3000)]
        public string AirlineCode { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
