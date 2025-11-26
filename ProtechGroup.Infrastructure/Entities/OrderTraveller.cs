using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderTraveller")]
    public class OrderTraveller
    {
        [Key]
        public int? Id { get; set; }
        public int? SessionId { get; set; }
        public int? UserId { get; set; }
        public byte? UserRole { get; set; }
        public int? OrderId { get; set; }
        public int? OrderFlightId { get; set; }
        public int? OrderPackageHotelRoomId { get; set; }
        public int? OrderHotelRoomId { get; set; }
        public byte? TravellerType { get; set; }
        public byte? Gender { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsMilesCard { get; set; }
        [StringLength(50)]
        public string MilesCardNumber { get; set; }
        [StringLength(50)]
        public string AirlineCode { get; set; }
        public decimal? PaxGiaCoBan { get; set; }
        public decimal? PaxTaxAndFee { get; set; }
        public decimal? PaxAddOnFee { get; set; }
        public decimal? PaxServiceFee { get; set; }
        public decimal? PaxPrice { get; set; }
        [StringLength(250)]
        public string PaxNote { get; set; }
        public decimal? TaxAndFeeExcludeDiscount { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? Discount { get; set; }
        public bool? Active { get; set; }

        [StringLength(50)]
        public string EticketNumberOutBound { get; set; }

        [StringLength(50)]
        public string EticketNumberInBound { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string PNROutBound { get; set; }

        [StringLength(50)]
        public string PNRInBound { get; set; }
        public int? OrderTourId { get; set; }
    }
}
