using System;
using System.Collections.Generic;
using ProtechGroup.Domain;

namespace ProtechGroup.Domain
{
    public class GroupFlight : IEquatable<GroupFlight>
    {
        public int FareDataId;
        public string BookingKey;
        public FlightServiceSearch FlightServiceSearch;
        public decimal PriceDomestic;

        public string BgRow;
        public string Plane;
        public List<PriceBreakDownFlight> PriceBreakDowns;

        public string ReturnTicket;
        public string TicketClassDomestic;
        public decimal FlightRef;
        public List<Segment> ListSegment;
        public string MainFlightNumber;
        public string MainAirlineCode;
        public string MainAirlineName;

        public string MainDepartureAirportCode;
        public string MainDepartureAirportName;
        public string MainDepartureCity;
        public string MainDepartureCountry;
        public string MainDepartureTime;
        public DateTime MainDepartureDate;

        public string MainArrivalAirportCode;
        public string MainArrivalAirportName;
        public string MainArrivalCity;
        public string MainArrivalCountry;
        public string MainArrivalTime;
        public DateTime MainArrivalDate;
        public string AllowanceBaggage;

        public string Duration; // Duration in XML file (for display)
        public int TotalMinute; // Duration in INT (for sort)
        public int Stop;
        public WayType WayType;
        public bool IsSelected = false;
        public string RecommendationNumber = "0";
        public string Note;
        public bool IsMarkupPrivate = false;
        public bool Equals(GroupFlight other)
        {
            //            if (this.MainFlightNumber.Equals(other.MainFlightNumber) || this.FlightRef == other.FlightRef)
            //                                    return true;
            //if (FlightServiceSearch != FlightServiceSearch.Amadeus)
            //{
            if (this.MainFlightNumber.Equals(other.MainFlightNumber))
                return true;
            //}
            //else
            //    if (this.FlightRef == other.FlightRef)
            //        return true;
            return false;
        }
    }
    public class PriceBreakDownFlight
    {
        public string BookingKey { get; set; }
        public string FareClass { get; set; }
        public string FareBasis { get; set; }
        public string ClassName { get; set; }
        public decimal TotallPriceAdt { get; set; }
        public decimal DiscountAdt { get; set; }
        public decimal TaxAdt { get; set; }
        public decimal FareBaseAdt { get; set; }
        public decimal FeeAdt { get; set; }
        public decimal TotallPriceChd { get; set; }
        public decimal DiscountChd { get; set; }
        public decimal TaxChd { get; set; }
        public decimal FareBaseChd { get; set; }
        public decimal FeeChd { get; set; }
        public decimal TotallPriceInf { get; set; }
        public decimal DiscountInf { get; set; }
        public decimal TaxInf { get; set; }
        public decimal FareBaseInf { get; set; }
        public decimal FeeInf { get; set; }
        public string Condition { get; set; }
        public string ReturnTicket { get; set; }
        public string RecommendationNumber { get; set; }
        public string AllowanceBaggage { get; set; }
        public int SeatAvailablity { get; set; }
        public string CabinClass { get; set; }
    }
}
