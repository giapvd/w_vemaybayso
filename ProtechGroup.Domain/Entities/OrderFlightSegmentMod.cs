using System;


namespace ProtechGroup.Domain.Entities
{

    public class OrderFlightSegmentMod
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? OrderFlightId { get; set; }
        public string AirlineCode { get; set; }
        public int? FlightNumber { get; set; }
        public string TicketClass { get; set; }
        public string TicketClassName { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime? DepartureDate { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public decimal? SegmentPrice { get; set; }
        public string PNRNumber { get; set; }
        public string EticketNumber { get; set; }
        public byte? PNRStatus { get; set; }
        public string Timelimit { get; set; }
        public byte? SegmentType { get; set; }
        public DateTime? TimeRemindCustomer { get; set; }
        public byte? RemindCustomerStatus { get; set; }
        public int? GroupIndex { get; set; }
        public string SI { get; set; }
        public decimal? BasePrice { get; set; }
        public string FareRule { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? PriceShownOnTicket { get; set; }
        public byte? RecheckStatus { get; set; }
        public bool? Active { get; set; }
        public int? UserIdRecheck { get; set; }
        public string ARC_FullBookingDetail { get; set; }
        public int? ARC_BookStatus { get; set; }
        public string ContentIssue { get; set; }
    }
}
