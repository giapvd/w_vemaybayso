using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtechGroup.FlightBookingWeb.Models
{
    public class FlightSearchRequest
    {
        public string departure { get; set; }
        public string arrival { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime? returnDate { get; set; }
        public int roundType { get; set; }
        public byte countAdt { get; set; }
        public byte countChd { get; set; }
        public byte countInf { get; set; }
    }
    public class SelectFlightRequest
    {
        public string AirlineCode { get; set; }
        public string SessionId { get; set; }
        public string BookingKey { get; set; }
        public int WayType {  get; set; }
    }
    public class ChangDateSearchInput
    {
        public string SessionChange { get; set; }
        public string DateChange { get; set; }
        public int WayTypeChange { get; set; }
    }
}