using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain
{
    public class Segment
    {
        public string FlightNumber;
        public string AirlineCode;
        public string AirlineName;
        public string Duration;
        public string OperatingAirlineCode;
        public string OperatingAirlineName;
        public string DepartureAirportCode;
        public string DepartureAirportName;
        public string DepartureTerminal;
        public DateTime DepartureDate;
        public string DepartureTime;
        public string DepartureCity;
        public string DepartureCountry;
        public string ArrivalAirportCode;
        public string ArrivalAirportName;
        public string ArrivalTerminal;
        public DateTime ArrivalDate;
        public string ArrivalTime;
        public string ArrivalCity;
        public string ArrivalCountry;
        public string TicketClass;
        public string AircraftCode;
        public string AircraftName;
        public int SeatRemain;
        public string SegmentStop = string.Empty;
        public SegmentType SegmentType;
    }
}
