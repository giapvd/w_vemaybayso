using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderPayment")]
    public class OrderPayment
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [StringLength(1500)]
        public string TransactionId { get; set; }
        [StringLength(50)]
        public string CreatedOn { get; set; }
        public int? PaymentType { get; set; }
        [StringLength(50)]
        public string TransactionStatus { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        [StringLength(250)]
        public string MerchantId { get; set; }
        [StringLength(500)]
        public string CustomerName { get; set; }
        [StringLength(150)]
        public string CustomerEmail { get; set; }
        [StringLength(50)]
        public string CustomerPhone { get; set; }
        [StringLength(500)]
        public string CustomerAddress { get; set; }
        [StringLength(50)]
        public string VerifySign { get; set; }
        [StringLength(50)]
        public string Resend { get; set; }
        public DateTime? DateTimeInsert { get; set; }
        [StringLength(3000)]
        public string StrResponse { get; set; }
        [StringLength(50)]
        public string OrderIdResponse { get; set; }
        [StringLength(50)]
        public string TransactionIdResponse { get; set; }
        [StringLength(50)]
        public string TransactionStatusResponse { get; set; }
        [StringLength(1000)]
        public string OrderDescriptionResponse { get; set; }
        public DateTime? DateTimeResponse { get; set; }
    }
}
