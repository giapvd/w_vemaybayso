using System;

namespace ProtechGroup.Domain.Entities
{
    
    public class OrderFlightMod
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string OrderCode { get; set; }
        public byte? OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ProcessedBy { get; set; }
        public byte? Triptype { get; set; }
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
        public string PriceNote { get; set; }
        public byte? OptionGetBetterFare { get; set; }
        public decimal? TotalDiscount { get; set; }
        public int? SessionId { get; set; }
        public byte? Supplier { get; set; }
        public byte? KeepSeat15minStatus { get; set; }
        public string SessionIdGDS { get; set; }
        public DateTime? DateTimeKeepSeat { get; set; }
        public string PnrFake { get; set; }
        public byte? PnrKeepStatus { get; set; }
        public DateTime? DateTimeCreatePnr { get; set; }
        public bool? IsShowFlagSNB { get; set; }
        public int? NoFlagSNB { get; set; }
        public bool? IsShowFlagTML { get; set; }
        public decimal? OtherFee { get; set; }
        public string PNRNumber { get; set; }
        public byte? Enviroment { get; set; }
        public bool? IsRoundTrip { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public int? RecommendationNumber { get; set; }
        public string OutBoundRefNumber { get; set; }
        public string InBoundRefNumber { get; set; }
        public byte? Adult { get; set; }
        public byte? Child { get; set; }
        public byte? Infant { get; set; }
        public bool? IsIssuedEticket { get; set; }
        public string TicketClass { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineCodeInBound { get; set; }
    }
}
