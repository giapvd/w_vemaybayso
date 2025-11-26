using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderContactInfo")]
    public class OrderContactlInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public byte Title { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Street { get; set; }
        [StringLength(250)]
        public string City { get; set; }
        [StringLength(10)]
        public string CountryCode { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(20)]
        public string MobilePhone { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        public bool IsInvoice { get; set; }
        [StringLength(250)]
        public string NameInvoice { get; set; }
        [StringLength(250)]
        public string ProfessionInvoice { get; set; }
        [StringLength(250)]
        public string VATInvoice { get; set; }
        [StringLength(250)]
        public string AddressInvoice { get; set; }
        [StringLength(250)]
        public string CityInvoice { get; set; }
        [StringLength(250)]
        public string TaxOffice { get; set; }
        public bool IsReciveInformation { get; set; }
        [StringLength(250)]
        public string FullName { get; set; }
        [StringLength(250)]
        public string AddressReciveInvoice { get; set; }
    }
}
