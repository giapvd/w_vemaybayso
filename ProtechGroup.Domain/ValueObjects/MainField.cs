using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain
{
    public class MainField
    {
        public string DepartureAirport;
        public string ArrivalAirport;
        public string DepartureCity;
        public string ArrivalCity;
        public string DepartureCountry;
        public string ArrivalCountry;
        public bool IsRoundTrip;
        public bool IsSearchDomestic;
        public int Adults;
        public int Childs;
        public int Infants;
        public DateTime DepartureDate;
        public DateTime ReturnDate;
        public int SessionId;
        public string XmlResultFilePath;
    }
}
