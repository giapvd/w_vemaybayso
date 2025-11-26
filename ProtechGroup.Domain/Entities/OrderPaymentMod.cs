using System;

namespace ProtechGroup.Domain.Entities
{
    public class OrderPaymentMod
    {
       
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string TransactionId { get; set; }
        public string CreatedOn { get; set; }
        public int? PaymentType { get; set; }
        public string TransactionStatus { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        public string MerchantId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string VerifySign { get; set; }
        public string Resend { get; set; }
        public DateTime? DateTimeInsert { get; set; }
        public string StrResponse { get; set; }
        public string OrderIdResponse { get; set; }
        public string TransactionIdResponse { get; set; }
        public string TransactionStatusResponse { get; set; }
        public string OrderDescriptionResponse { get; set; }
        public DateTime? DateTimeResponse { get; set; }
    }
}
