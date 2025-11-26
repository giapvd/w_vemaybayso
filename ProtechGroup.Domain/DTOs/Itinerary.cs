using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.DTOs
{
    public class Itinerary
    {
        public bool IsRoundTrip;
        public string RecommendationNumber;
        public string RefNumberOutBound;
        public string RefNumberInBound;
        public GroupFlight FlightOutBound;
        public GroupFlight FlightInBound;
        public decimal TotalPrice;
        public decimal Tax;
        public DateTime LastTicketDate;
    }
}

