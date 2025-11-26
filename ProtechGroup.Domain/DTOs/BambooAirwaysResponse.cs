using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.DTOs
{
    #region LogingBamBoo
    public class DataLoginBamBoo
    {
        public string access_token { get; set; }
        public ExpireAtLoginBamBoo expire_at { get; set; }
    }

    public class ExpireAtLoginBamBoo
    {
        public int seconds { get; set; }
        public int nanos { get; set; }
    }

    public class MessageLoginBamBoo
    {
        public string message { get; set; }
    }

    public class RootLoginBamBoo
    {
        public DataLoginBamBoo data { get; set; }
        public string id { get; set; }
        public List<MessageLoginBamBoo> message { get; set; }
        public bool success { get; set; }
    }
    #endregion

    #region FlightInfo
    public class AircraftInfoBamBoo
    {
        public string type { get; set; }
    }

    public class AppliedFareBamBoo
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class ArrivalInfoBamBoo
    {
        public string airport_code { get; set; }
        public string terminal { get; set; }
        public string datetime { get; set; }
    }

    public class BaggageAllowanceBamBoo
    {
        public string weight { get; set; }
        public string unit { get; set; }
    }

    public class BaseFareBamBoo
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class BookingClassBamBoo
    {
        public int trip_index { get; set; }
        public string booking_class { get; set; }
        public int seat_availablity { get; set; }
        public string cabin_class { get; set; }
        public List<GroupFareBamBoo> group_fare { get; set; }
        public PricingBamBoo pricing { get; set; }
    }

    public class DatumBamBoo
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public string route { get; set; }
        public List<TripInfoBamBoo> trip_info { get; set; }
    }

    public class DepartureInfoBamBoo
    {
        public string airport_code { get; set; }
        public string terminal { get; set; }
        public string datetime { get; set; }
    }

    public class DiscountBamBoo
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class DisplayFareBamBoo
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class FlightInfoBamBoo
    {
        public string carrier_code { get; set; }
        public string flight_number { get; set; }
        public string flight_date { get; set; }
    }

    public class GroupFareBamBoo
    {
        public int group_segment_id { get; set; }
        public int segment_id { get; set; }
        public string fare_class { get; set; }
        public string fare_basis { get; set; }
    }

    public class MessageBamBoo
    {
        public string message { get; set; }
    }

    public class PaxPricingInfoBamBoo
    {
        public string pax_type { get; set; }
        public BaggageAllowanceBamBoo baggage_allowance { get; set; }
        public DisplayFareBamBoo display_fare { get; set; }
        public DiscountBamBoo discount { get; set; }
        public AppliedFareBamBoo applied_fare { get; set; }
        public TaxBamBoo tax { get; set; }
        public BaseFareBamBoo base_fare { get; set; }
        public TotalBamBoo total { get; set; }
    }

    public class PricingBamBoo
    {
        public string fare_type { get; set; }
        public List<PaxPricingInfoBamBoo> pax_pricing_info { get; set; }
    }

    public class RootBamBoo
    {
        public List<DatumBamBoo> data { get; set; }
        public string id { get; set; }
        public List<MessageBamBoo> message { get; set; }
        public bool success { get; set; }
    }

    public class SegmentInfoBamBoo
    {
        public int group_segment_id { get; set; }
        public int segment_id { get; set; }
        public FlightInfoBamBoo flight_info { get; set; }
        public DepartureInfoBamBoo departure_info { get; set; }
        public ArrivalInfoBamBoo arrival_info { get; set; }
        public AircraftInfoBamBoo aircraft_info { get; set; }
        public bool is_international { get; set; }
    }

    public class TaxBamBoo
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class TotalBamBoo
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class TripInfoBamBoo
    {
        public int flight_segment_group_id { get; set; }
        public bool is_transit { get; set; }
        public List<SegmentInfoBamBoo> segment_info { get; set; }
        public List<BookingClassBamBoo> booking_class { get; set; }
    }
    #endregion

    #region Confirm Price
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddressConfirmPrice
    {
        public string email { get; set; }
        public string phone_number { get; set; }
    }

    public class AppliedFareDetailType
    {
        public int base_fare { get; set; }
        public string base_fare_currency { get; set; }
        public int applied_fare { get; set; }
        public int display_fare_amount { get; set; }
        public string currency { get; set; }
    }

    public class CouponDetail
    {
        public string baggage_allowance_weight { get; set; }
        public string baggage_allowance_unit { get; set; }
        public int segment_id { get; set; }
    }

    public class DataConfirmPrice
    {
        public object error { get; set; }
        public bool pnr_on_hold { get; set; }
        public string last_modfied_datetime { get; set; }
        public int number_of_seats { get; set; }
        public List<ItineraryConfirmPrice> itinerary { get; set; }
        public List<GuestDetaiConfirmPrice> guest_details { get; set; }
        public List<object> ssr_details { get; set; }
        public List<object> seat_details { get; set; }
        public object fee_details { get; set; }
        public PnrContact pnr_contact { get; set; }
        public ItinPrice itin_price { get; set; }
        public TotalAmountPaid total_amount_paid { get; set; }
        public TotalAmountTobePaid total_amount_tobe_paid { get; set; }
        public RefundAmount refund_amount { get; set; }
    }

    public class FareDetailForGuestType
    {
        public string fare_basis { get; set; }
        public string fare_type { get; set; }
        public List<int> segment_id { get; set; }
        public int fare_component_id { get; set; }
    }

    public class GuestAmountConfirmPrice
    {
        public int guest_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string guest_type { get; set; }
    }

    public class GuestDetaiConfirmPrice
    {
        public string given_name { get; set; }
        public string sur_name { get; set; }
        public string name_prefix { get; set; }
        public string type { get; set; }
        public int guest_id { get; set; }
        public List<TicketDetailConfirmPrice> ticket_details { get; set; }
    }

    public class GuestPriceBreakDownConfirmPrice
    {
        public List<PriceBreakDownConfirmPrice> price_break_down { get; set; }
        public List<TaxConfirmPrice> tax { get; set; }
        public string guest_type { get; set; }
    }

    public class ItineraryConfirmPrice
    {
        public int flight_segment_group_id { get; set; }
        public int segment_id { get; set; }
        public string carrier_code { get; set; }
        public int flight_number { get; set; }
        public string flight_date { get; set; }
        public string board_point { get; set; }
        public string off_point { get; set; }
        public string scheduled_departure_datetime { get; set; }
        public string scheduled_arrival_time { get; set; }
        public string departure_terminal { get; set; }
        public string arrival_terminal { get; set; }
        public string cabin_class { get; set; }
        public string flight_status { get; set; }
        public string aircraft_type { get; set; }
        public string booking_class { get; set; }
        public bool is_international { get; set; }
    }

    public class ItinPrice
    {
        public List<GuestPriceBreakDownConfirmPrice> guest_price_break_down { get; set; }
    }

    public class MessageConfirmPrice
    {
        public string message { get; set; }
    }

    public class PnrContact
    {
        public AddressReviewBooking address { get; set; }
        public string name_prefix { get; set; }
        public string language { get; set; }
        public string sur_name { get; set; }
        public string given_name { get; set; }
    }

    public class PriceBreakDownConfirmPrice
    {
        public AppliedFareDetailType applied_fare_detail_type { get; set; }
        public List<FareDetailForGuestType> fare_detail_for_guest_type { get; set; }
    }

    public class RefundAmount
    {

    }

    public class RootConfirmPrice
    {
        public DataConfirmPrice data { get; set; }
        public string id { get; set; }
        public List<MessageConfirmPrice> message { get; set; }
        public bool success { get; set; }
    }

    public class TaxConfirmPrice
    {
        public string code { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string iata_code { get; set; }
    }

    public class TicketDetailConfirmPrice
    {
        public List<CouponDetail> coupon_details { get; set; }
    }

    public class TotalAmountPaid
    {
    }

    public class TotalAmountTobePaid
    {
        public string currency { get; set; }
        public int amount { get; set; }
        public List<GuestAmountConfirmPrice> guest_amount { get; set; }
    }

    #endregion

    #region Add Passport
    public class DataAddPassport
    {
        public object error { get; set; }
        public string pnr_status { get; set; }
        public bool pnr_on_hold { get; set; }
        public string creation_datetime { get; set; }
        public string last_modfied_datetime { get; set; }
        public int number_of_seats { get; set; }
        public List<ItineraryConfirmPrice> itinerary { get; set; }
        public List<GuestDetailAddPassport> guest_details { get; set; }
        public List<object> ssr_details { get; set; }
        public List<object> seat_details { get; set; }
        public object fee_details { get; set; }
        public PnrContact pnr_contact { get; set; }
        public ItinPrice itin_price { get; set; }
        public TotalAmountPaid total_amount_paid { get; set; }
        public TotalAmountTobePaid total_amount_tobe_paid { get; set; }
        public TimeLimitDetailAddPassport time_limit_detail { get; set; }
        public RefundAmount refund_amount { get; set; }
    }

    public class DocumentAddPassport
    {
        public string number { get; set; }
        public string date_of_birth { get; set; }
        public string expiry_date { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
    }

    public class GuestDetailAddPassport
    {
        public string given_name { get; set; }
        public string sur_name { get; set; }
        public string name_prefix { get; set; }
        public string type { get; set; }
        public int guest_id { get; set; }
        public DocumentAddPassport document { get; set; }
        public List<TicketDetailConfirmPrice> ticket_details { get; set; }
    }
    public class RootAddPassport
    {
        public DataAddPassport data { get; set; }
        public string id { get; set; }
        public List<MessageConfirmPrice> message { get; set; }
        public bool success { get; set; }
    }
    public class TimeLimitDetailAddPassport
    {
        public string time_limit_action { get; set; }
        public string time_limit_ltc { get; set; }
        public string time_limit_utc { get; set; }
        public string note { get; set; }
    }
    #endregion

    #region Addl Loyalty
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DataAddLoyalty
    {
        public object error { get; set; }
        public string pnr_status { get; set; }
        public bool pnr_on_hold { get; set; }
        public string creation_datetime { get; set; }
        public string last_modfied_datetime { get; set; }
        public int number_of_seats { get; set; }
        public List<Itinerary> itinerary { get; set; }
        public List<GuestDetailAddPassport> guest_details { get; set; }
        public List<object> ssr_details { get; set; }
        public List<object> seat_details { get; set; }
        public object fee_details { get; set; }
        public PnrContact pnr_contact { get; set; }
        public ItinPrice itin_price { get; set; }
        public TotalAmountPaid total_amount_paid { get; set; }
        public TotalAmountTobePaid total_amount_tobe_paid { get; set; }
        public TimeLimitDetailAddPassport time_limit_detail { get; set; }
        public RefundAmount refund_amount { get; set; }
    }

    public class DocumentAddLoyalty
    {
        public string type { get; set; }
        public string country { get; set; }
        public string number { get; set; }
        public string nationality { get; set; }
        public string date_of_birth { get; set; }
        public string expiry_date { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
    }
    public class RootAddLoyalty
    {
        public DataAddLoyalty data { get; set; }
        public string id { get; set; }
        public List<MessageConfirmPrice> message { get; set; }
        public bool success { get; set; }
    }

    #endregion

    #region Create Booking
    public class DataCreateBooking
    {
        public object error { get; set; }
        public string pnr_number { get; set; }
        public string pnr_status { get; set; }
        public bool pnr_on_hold { get; set; }
        public string creation_datetime { get; set; }
        public string last_modfied_datetime { get; set; }
        public int number_of_seats { get; set; }
        public List<ItineraryConfirmPrice> itinerary { get; set; }
        public List<GuestDetaiConfirmPrice> guest_details { get; set; }
        public List<object> ssr_details { get; set; }
        public List<object> seat_details { get; set; }
        public object fee_details { get; set; }
        public PnrContact pnr_contact { get; set; }
        public ItinPrice itin_price { get; set; }
        public TotalAmountPaid total_amount_paid { get; set; }
        public TotalAmountTobePaid total_amount_tobe_paid { get; set; }
        public TimeLimitDetailCreateBooking time_limit_detail { get; set; }
        public RefundAmount refund_amount { get; set; }
    }
    public class TimeLimitDetailCreateBooking
    {
        public string time_limit_action { get; set; }
        public string time_limit_ltc { get; set; }
        public string time_limit_utc { get; set; }
        public string note { get; set; }
    }
    public class RootCreateBooking
    {
        public DataCreateBooking data { get; set; }
        public string id { get; set; }
        public List<MessagePayNow> message { get; set; }
        public bool success { get; set; }
    }

    #endregion

    #region Booking PayNow
    public class RootBookingPayNow
    {
        public DataBookingPayNow data { get; set; }
        public string id { get; set; }
        public List<MessagePayNow> message { get; set; }
        public bool success { get; set; }
    }
    public class MessagePayNow
    {
        public string message { get; set; }
    }
    public class CouponDetailBookingPayNow
    {
        public string number { get; set; }
        public string status { get; set; }
        public string baggage_allowance_weight { get; set; }
        public string baggage_allowance_unit { get; set; }
        public int segment_id { get; set; }
    }
    public class DataBookingPayNow
    {
        public object error { get; set; }
        public string pnr_number { get; set; }
        public string pnr_status { get; set; }
        public bool pnr_on_hold { get; set; }
        public string creation_datetime { get; set; }
        public string last_modfied_datetime { get; set; }
        public int number_of_seats { get; set; }
        public List<ItineraryConfirmPrice> itinerary { get; set; }
        public List<GuestDetailBookingPayNow> guest_details { get; set; }
        public List<object> ssr_details { get; set; }
        public List<object> seat_details { get; set; }
        public object fee_details { get; set; }
        public PnrContact pnr_contact { get; set; }
        public ItinPrice itin_price { get; set; }
        public TotalAmountPaidBookingPayNow total_amount_paid { get; set; }
        public TotalAmountTobePaidBookingPayNow total_amount_tobe_paid { get; set; }
        public RefundAmount refund_amount { get; set; }
    }
    public class GuestDetailBookingPayNow
    {
        public string given_name { get; set; }
        public string sur_name { get; set; }
        public string name_prefix { get; set; }
        public string type { get; set; }
        public int guest_id { get; set; }
        public TicketDetailBookingPayNow ticket_detail { get; set; }
        public List<TicketDetailBookingPayNow> ticket_details { get; set; }
    }
    public class TicketDetailBookingPayNow
    {
        public string number { get; set; }
    }
    public class TicketDetailBookingPayNow2
    {
        public string number { get; set; }
        public List<CouponDetail> coupon_details { get; set; }
    }
    public class TotalAmountPaidBookingPayNow
    {
        public string currency { get; set; }
        public int amount { get; set; }
        public string form_of_payment { get; set; }
        public List<GuestAmountConfirmPrice> guest_amount { get; set; }
    }

    public class TotalAmountTobePaidBookingPayNow
    {
    }

    #endregion
    #region ReviewBooking
    public class AddressReviewBooking
    {
        public string email { get; set; }
        public string phone_number { get; set; }
    }

    public class AppliedFareDetailTypeReviewBooking
    {
        public int base_fare { get; set; }
        public string base_fare_currency { get; set; }
        public int applied_fare { get; set; }
        public int display_fare_amount { get; set; }
        public string currency { get; set; }
    }

    public class CouponDetailReviewBooking
    {
        public string baggage_allowance_weight { get; set; }
        public string baggage_allowance_unit { get; set; }
        public int segment_id { get; set; }
    }

    public class DataReviewBooking
    {
        public object error { get; set; }
        public string user_book { get; set; }
        public string user_issue { get; set; }
        public string source { get; set; }
        public string pnr_number { get; set; }
        public string pnr_status { get; set; }
        public bool pnr_on_hold { get; set; }
        public string creation_datetime { get; set; }
        public string last_modfied_datetime { get; set; }
        public int number_of_seats { get; set; }
        public List<ItineraryReviewBooking> itinerary { get; set; }
        public List<GuestDetailReviewBooking> guest_details { get; set; }
        public List<SsrDetailReviewBooking> ssr_details { get; set; }
        public List<object> seat_details { get; set; }
        public object fee_details { get; set; }
        public List<object> emd_deposits { get; set; }
        public PnrContactReviewBooking pnr_contact { get; set; }
        public ItinPriceReviewBooking itin_price { get; set; }
        public TotalAmountPaidReviewBooking total_amount_paid { get; set; }
        public TotalAmountTobePaidReviewBooking total_amount_tobe_paid { get; set; }
        public TimeLimitDetailReviewBooking time_limit_detail { get; set; }
        public RefundAmountReviewBooking refund_amount { get; set; }
        public List<string> remarks { get; set; }
    }

    public class FareDetailForGuestTypeReviewBooking
    {
        public string fare_basis { get; set; }
        public string fare_type { get; set; }
        public List<int> segment_id { get; set; }
        public int fare_component_id { get; set; }
    }

    public class GuestAmountReviewBooking
    {
        public int guest_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string guest_type { get; set; }
    }

    public class GuestDetailReviewBooking
    {
        public string given_name { get; set; }
        public string sur_name { get; set; }
        public string name_prefix { get; set; }
        public string type { get; set; }
        public int guest_id { get; set; }
        public List<TicketDetailReviewBooking> ticket_details { get; set; }
    }

    public class GuestPriceBreakDownReviewBooking
    {
        public List<PriceBreakDownReviewBooking> price_break_down { get; set; }
        public List<TaxReviewBooking> tax { get; set; }
        public string guest_type { get; set; }
    }

    public class ItineraryReviewBooking
    {
        public int flight_segment_group_id { get; set; }
        public int segment_id { get; set; }
        public string carrier_code { get; set; }
        public int flight_number { get; set; }
        public string flight_date { get; set; }
        public string board_point { get; set; }
        public string off_point { get; set; }
        public string scheduled_departure_datetime { get; set; }
        public string scheduled_arrival_time { get; set; }
        public string departure_terminal { get; set; }
        public string arrival_terminal { get; set; }
        public string cabin_class { get; set; }
        public string flight_status { get; set; }
        public string aircraft_type { get; set; }
        public string booking_class { get; set; }
        public bool is_international { get; set; }
    }

    public class ItinPriceReviewBooking
    {
        public List<GuestPriceBreakDownReviewBooking> guest_price_break_down { get; set; }
    }

    public class Message
    {
        public string message { get; set; }
    }

    public class PnrContactReviewBooking
    {
        public AddressReviewBooking address { get; set; }
        public string name_prefix { get; set; }
        public string language { get; set; }
        public string sur_name { get; set; }
        public string given_name { get; set; }
    }

    public class PriceBreakDownReviewBooking
    {
        public AppliedFareDetailTypeReviewBooking applied_fare_detail_type { get; set; }
        public List<FareDetailForGuestTypeReviewBooking> fare_detail_for_guest_type { get; set; }
        public TotalTaxReviewBooking total_tax { get; set; }
    }

    public class RefundAmountReviewBooking
    {
    }

    public class RootReviewBooking
    {
        public DataReviewBooking data { get; set; }
        public string id { get; set; }
        public List<Message> message { get; set; }
        public bool success { get; set; }
    }

    public class SsrDetailReviewBooking
    {
        public string code { get; set; }
        public string status { get; set; }
        public int guest_id { get; set; }
        public int segment_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public int quantity { get; set; }
        public string weight { get; set; }
    }

    public class TaxReviewBooking
    {
        public string code { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string iata_code { get; set; }
    }

    public class TicketDetailReviewBooking
    {
        public List<CouponDetailReviewBooking> coupon_details { get; set; }
    }

    public class TimeLimitDetailReviewBooking
    {
        public string time_limit_action { get; set; }
        public string time_limit_ltc { get; set; }
        public string time_limit_tst { get; set; }
        public string time_limit_tsm { get; set; }
        public string note { get; set; }
    }

    public class TotalAmountPaidReviewBooking
    {
    }

    public class TotalAmountTobePaidReviewBooking
    {
        public string currency { get; set; }
        public int amount { get; set; }
        public List<GuestAmountReviewBooking> guest_amount { get; set; }
    }

    public class TotalTaxReviewBooking
    {
    }
    #endregion ReviewBooking
}
