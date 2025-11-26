using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderFlight")]
    public class OrderFlight
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [StringLength(250)]
        public string OrderCode { get; set; }
        public byte? OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ProcessedBy { get; set; }
        public byte? Triptype { get; set; }
        [StringLength(250)]
        public string ItineraryDesc { get; set; }
        public byte? PaymentMethod { get; set; }
        public decimal? TotalAdult { get; set; }
        public decimal? TotalChild { get; set; }
        public decimal? TotalInfant { get; set; }
        public decimal? TotalBasePrice { get; set; }
        public decimal? TotalTaxAndFee { get; set; }
        public decimal? TotalAddOnFee { get; set; }
        public decimal? TotalServiceFee { get; set; }
        public decimal? Commission { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Balance { get; set; }
        [StringLength(500)]
        public string PriceNote { get; set; }
        public byte? OptionGetBetterFare { get; set; }
        public decimal? TotalDiscount { get; set; }
        public int? SessionId { get; set; }
        public byte? Supplier { get; set; }
        public byte? KeepSeat15minStatus { get; set; }
        [StringLength(50)]
        public string SessionIdGDS { get; set; }
        public DateTime? DateTimeKeepSeat { get; set; }
        [StringLength(50)]
        public string PnrFake { get; set; }
        public byte? PnrKeepStatus { get; set; }
        public DateTime? DateTimeCreatePnr { get; set; }
        public bool? IsShowFlagSNB { get; set; }
        public int? NoFlagSNB { get; set; }
        public bool? IsShowFlagTML { get; set; }
        public decimal? OtherFee { get; set; }
        [StringLength(50)]
        public string PNRNumber { get; set; }
        public byte? Enviroment { get; set; }
        public bool? IsRoundTrip { get; set; }
        [StringLength(50)]
        public string DepartureAirport { get; set; }
        [StringLength(50)]
        public string ArrivalAirport { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [StringLength(250)]
        public string DepartureCity { get; set; }
        [StringLength(250)]
        public string ArrivalCity { get; set; }
        public int? RecommendationNumber { get; set; }
        [StringLength(100)]
        public string OutBoundRefNumber { get; set; }
        [StringLength(100)]
        public string InBoundRefNumber { get; set; }
        public byte? Adult { get; set; }
        public byte? Child { get; set; }
        public byte? Infant { get; set; }
        public bool? IsIssuedEticket { get; set; }
        [StringLength(200)]
        public string TicketClass { get; set; }
        [StringLength(10)]
        public string AirlineCode { get; set; }
        [StringLength(10)]
        public string AirlineCodeInBound { get; set; }
    }
}
