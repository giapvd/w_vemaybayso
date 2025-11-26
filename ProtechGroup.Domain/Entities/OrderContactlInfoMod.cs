using System;

namespace ProtechGroup.Domain.Entities
{
    public class OrderContactlInfoMod
    {
        
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public byte? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public bool? IsInvoice { get; set; }
        public string NameInvoice { get; set; }
        public string ProfessionInvoice { get; set; }
        public string VATInvoice { get; set; }
        public string AddressInvoice { get; set; }
        public string CityInvoice { get; set; }
        public string TaxOffice { get; set; }
        public bool? IsReciveInformation { get; set; }
        public string FullName { get; set; }
        public string AddressReciveInvoice { get; set; }
    }
}
