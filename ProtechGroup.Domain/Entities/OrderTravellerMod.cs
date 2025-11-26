using System;

namespace ProtechGroup.Domain.Entities
{
    public class OrderTravellerMod
    {
        public int Id { get; set; }
        public int? SessionId { get; set; }
        public int? UserId { get; set; }
        public byte? UserRole { get; set; }
        public int? OrderId { get; set; }
        public int? OrderFlightId { get; set; }
        public int? OrderPackageHotelRoomId { get; set; }
        public int? OrderHotelRoomId { get; set; }
        public byte? TravellerType { get; set; }
        public byte? Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsMilesCard { get; set; }
        public string MilesCardNumber { get; set; }
        public string AirlineCode { get; set; }
        public decimal? PaxGiaCoBan { get; set; }
        public decimal? PaxTaxAndFee { get; set; }
        public decimal? PaxAddOnFee { get; set; }
        public decimal? PaxServiceFee { get; set; }
        public decimal? PaxPrice { get; set; }
        public string PaxNote { get; set; }
        public decimal? TaxAndFeeExcludeDiscount { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? Discount { get; set; }
        public bool? Active { get; set; }
        public string EticketNumberOutBound { get; set; }
        public string EticketNumberInBound { get; set; }
        public string FullName { get; set; }
        public string PNROutBound { get; set; }
        public string PNRInBound { get; set; }
        public int? OrderTourId { get; set; }
    }
}
