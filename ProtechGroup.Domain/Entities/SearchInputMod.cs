using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Entities
{
    public class SearchInputMod
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public bool IsRoundTrip { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int RequestMaxResult { get; set; }
        public byte AdultNumber { get; set; }
        public byte ChildNumber { get; set; }
        public byte InfantNumber { get; set; }  
        public byte TotalPax { get; set; }  
        public DateTime DateTimeInsert { get; set; }
        public bool IsSearchDomestic { get; set; }
        public int UserId { get; set; }
        public string IPAddress { get; set; }       
        public bool SearchIndividual { get; set; }
        public string AirlineCode { get; set; }
        public decimal Price { get; set; }
    }
}
