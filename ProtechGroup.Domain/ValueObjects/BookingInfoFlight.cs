using ProtechGroup.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain
{
    public class BookingInfoFlight
    {
        public string PnrNumber { get; set; }
        public DateTime ExpiryTime { get; set; }
        public decimal PaymentToAirline { get; set; }
        public string AirlineCode {  get; set; }
        public int SegmentId {  get; set; }
    }
}