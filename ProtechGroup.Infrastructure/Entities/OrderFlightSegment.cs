using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;


namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderFlightSegment")]
    public class OrderFlightSegment
    {
        [Key]
        public int? Id { get; set; }

        public int? OrderId { get; set; }

        public int? OrderFlightId { get; set; }

        [StringLength(2)]
        public string AirlineCode { get; set; }

        public int? FlightNumber { get; set; }

        [StringLength(50)]
        public string TicketClass { get; set; }

        [StringLength(50)]
        public string TicketClassName { get; set; }

        [StringLength(200)]
        public string DepartureAirport { get; set; }

        [StringLength(200)]
        public string ArrivalAirport { get; set; }

        public DateTime? DepartureDate { get; set; }

        public TimeSpan? DepartureTime { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public TimeSpan? ArrivalTime { get; set; }
        public decimal? SegmentPrice { get; set; }

        [StringLength(10)]
        public string PNRNumber { get; set; }

        [StringLength(50)]
        public string EticketNumber { get; set; }

        public byte? PNRStatus { get; set; }

        [StringLength(50)]
        public string Timelimit { get; set; }

        public byte? SegmentType { get; set; }

        public DateTime? TimeRemindCustomer { get; set; }

        public byte? RemindCustomerStatus { get; set; }

        public int? GroupIndex { get; set; }

        [StringLength(50)]
        public string SI { get; set; }
        public decimal? BasePrice { get; set; }
        [StringLength(4000)]
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
