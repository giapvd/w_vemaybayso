using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderBooking")]
    public class OrderBooking
    {
        [Key]
        public int OrderId { get; set; }
        public byte? StatusAvailable { get; set; }
        public byte? FinnalStatus { get; set; }
        public int? UserId { get; set; }
        [StringLength(50)]
        public string CompanyCode { get; set; }
        public bool? IsHotel { get; set; }
        public bool? IsFlight { get; set; }
        public decimal? FinalPrice { get; set; }
        public bool? IsRequirePayment { get; set; }
        public byte? PaymentMethodSelected { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte? SupplierHotel { get; set; }
        public byte? SupplierFlight { get; set; }
        public byte? OrderStep { get; set; }
        [StringLength(50)]
        public string BookingReference { get; set; }
        [StringLength(2000)]
        public string OrderErrorMessage { get; set; }
        [StringLength(2000)]
        public string Note { get; set; }
        public bool? IsShowFrontEnd { get; set; }
        [StringLength(20)]
        public string IPAddress { get; set; }
        [StringLength(100)]
        public string Domain { get; set; }
        public decimal? PriceFirstPaid { get; set; }
        public decimal? ExtraFee { get; set; }
        public byte? ExtraFeeType { get; set; }
        public byte? PaymentStatusFinnal { get; set; }
        [StringLength(100)]
        public string UserCity { get; set; }
        [StringLength(100)]
        public string UserCountry { get; set; }
        public DateTime? LastTimeValidBankOrCashOrigin { get; set; }
        public DateTime? LastTimeValidBankOrCashCurrent { get; set; }
        public DateTime? LastTimeValidBank { get; set; }
        public DateTime? LastTimeValidCash { get; set; }
        public bool? IsMobile { get; set; }
        public bool? IsTracking { get; set; }
        public bool? IsBooking { get; set; }
        public int? UserIdProcess { get; set; }
        public bool? Active { get; set; }
        public byte? CallPaxStatus { get; set; }
        public DateTime? CallPaxFromTime { get; set; }
        public DateTime? CallPaxToTime { get; set; }
        public int? CallPaxUser { get; set; }
        public DateTime? DateTimeProcess { get; set; }
        public byte? Priority { get; set; }
        public int? UserIdAssignedBooking { get; set; }
        public byte? UserAssingedBookingStatus { get; set; }
        public int? UserIdAssingedCallPax { get; set; }
        public byte? UserAssingedCallPaxStatus { get; set; }
        public bool? IsKeepSeat { get; set; }
        public bool? IsDone { get; set; }
        public int? KeepSeatByUserId { get; set; }
        public DateTime? AssignedBookingDate { get; set; }
        public DateTime? DateTimeKeepSeat { get; set; }
        public DateTime? TimeLimit { get; set; }
        public int? UserIdUpdateStatus { get; set; }
        public bool? IsRecallPax { get; set; }
        public bool? XiCangDan { get; set; }
        public int? LogClientId { get; set; }
        public DateTime? DateTimeClickPayment { get; set; }
        public DateTime? DateTimeDelivery { get; set; }
        public DateTime? DateTimeRecall { get; set; }
        public int? UserRecall { get; set; }
        public byte? StatusRecall { get; set; }
        public DateTime? DateTimeDoneRecall { get; set; }
        public int? UserIdDeactive { get; set; }
        public DateTime? DateTimeDeactive { get; set; }
        public bool? NeedToResell { get; set; }
        public byte? InvoiceStatus { get; set; }
        public int? UserInvoiceRequest { get; set; }
        public int? UserInvoiceIssued { get; set; }
        public DateTime? UserInvoiceRequestTime { get; set; }
        public DateTime? UserInvoiceIssuedTime { get; set; }
        public bool? NeedToBeIssued { get; set; }
        public bool? Issued { get; set; }
        public bool? Paid { get; set; }
        public byte? OriginOnlinePaymentStatus { get; set; }
        public bool? Refunded { get; set; }
        public bool? IsDebt { get; set; }
        public string DebtReason { get; set; }
        public bool? NeedToSell { get; set; }
        public bool? CanExploited { get; set; }
        [StringLength(50)]
        public string PayAtBank { get; set; }
        public bool? HasLoss { get; set; }
        public decimal? LostAmount { get; set; }
        public bool? HasSMSBank { get; set; }
        public decimal? DepositAmmount { get; set; }
        public byte? KeepSeatStatus { get; set; }
        public byte? RemindBeforeFlyStatus { get; set; }
        public byte? RemindBeforeFlyQuality { get; set; }
        public byte? RemindBeforeFlyUser { get; set; }
        public DateTime? RemindBeforeFlyCallDate { get; set; }
        public string DeactiveReason { get; set; }
        public byte? DeactiveStatus { get; set; }
        public bool? NeedToDelivery { get; set; }
        public string DeliveryAddess { get; set; }
        public bool? Delivered { get; set; }
        public bool? IsTour { get; set; }
        public bool? IsBlockFlight { get; set; }
        [StringLength(50)]
        public string VoucherCode { get; set; }
    }
}
