using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtechGroup.FlightBookingWeb.Models
{
    public class BookingRequest
    {
        public int sessionId { get; set; }
        public List<FlightInfo> flightList { get; set; }
        public List<PassengerInfo> pasgerList { get; set; }
        public ContactOrder contaclOrder { get; set; }
        public InvoiceOrder invoiceOrder { get; set; }
    }

    public class FlightInfo
    {
        public string bookingKey { get; set; }
        public string airCode { get; set; }
        public int wayType { get; set; }
    }

    public class PassengerInfo
    {
        public byte genderPax { get; set; }
        public string namePax { get; set; }
        public string cardNum { get; set; }
        public string birthPax { get; set; }
        public string typePax { get; set; }
        public int bagIdOut { get; set; }
        public string totalKgOut { get; set; }
        public int bagIdIn { get; set; }
        public string totalKgIn { get; set; }
    }

    public class ContactOrder
    {
        public byte titleContactl { get; set; }
        public string nameContactl { get; set; }
        public string phoneContactl { get; set; }
        public string emailContactl { get; set; }
        public string addContactl { get; set; }
        public string otherRequirementsContactl { get; set; }
    }

    public class InvoiceOrder
    {
        public bool isInvoice { get; set; }
        public string nameInvoice { get; set; }
        public string taxCodeInvoice { get; set; }
        public string emailInvoice { get; set; }
        public string addInvoice { get; set; }
    }
}