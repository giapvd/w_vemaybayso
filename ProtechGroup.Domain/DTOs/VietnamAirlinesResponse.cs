using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.DTOs
{
    public class LoginVNA
    {
        public string accessToken { get; set; }
        public string expried { get; set; }
        public string success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
    public class CurrencyInfoVNA
    {
        public string OriginCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public int Rate { get; set; }
        public int RoundUnit { get; set; }
    }

    public class ListAirOptionVNA
    {
        public int OptionId { get; set; }
        public int Leg { get; set; }
        public string Airline { get; set; }
        public string System { get; set; }
        public string Currency { get; set; }
        public object Remark { get; set; }
        public List<ListFlightOptionVNA> ListFlightOption { get; set; }
        public List<ListFareOptionVNA> ListFareOption { get; set; }
    }

    public class ListFareInfoVNA
    {
        public int SegmentId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string FareClass { get; set; }
        public string FareBasis { get; set; }
        public string FareFamily { get; set; }
        public string CabinCode { get; set; }
        public string CabinName { get; set; }
        public string HandBaggage { get; set; }
        public string FreeBaggage { get; set; }
        public int Availability { get; set; }
    }

    public class ListFareItemVNA
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class ListFareOptionVNA
    {
        public int OptionId { get; set; }
        public bool Refundable { get; set; }
        public string FareClass { get; set; }
        public int Availability { get; set; }
        public object ExpiryDate { get; set; }
        public int TotalFare { get; set; }
        public int TotalTax { get; set; }
        public int TotalPrice { get; set; }
        public object Remark { get; set; }
        public List<ListFarePaxVNA> ListFarePax { get; set; }
    }

    public class ListFarePaxVNA
    {
        public string PaxType { get; set; }
        public int PaxNumb { get; set; }
        public int TotalFare { get; set; }
        public int BaseFare { get; set; }
        public int Taxes { get; set; }
        public List<ListFareItemVNA> ListFareItem { get; set; }
        public List<ListFareInfoVNA> ListFareInfo { get; set; }
        public List<object> ListTaxDetail { get; set; }
    }

    public class ListFlightVNA
    {
        public int Leg { get; set; }
        public string FlightId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string FlightNumber { get; set; }
        public int StopNum { get; set; }
        public int Duration { get; set; }
        public List<ListSegmentVNA> ListSegment { get; set; }
    }

    public class ListFlightOptionVNA
    {
        public int OptionId { get; set; }
        public List<ListFlightVNA> ListFlight { get; set; }
    }

    public class ListGroupVNA
    {
        public int Leg { get; set; }
        public string Journey { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public List<ListAirOptionVNA> ListAirOption { get; set; }
        public CurrencyInfoVNA CurrencyInfo { get; set; }
    }

    public class ListSegmentVNA
    {
        public int SegmentId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string FlightNumber { get; set; }
        public string Equipment { get; set; }
        public int Duration { get; set; }
        public bool HasStop { get; set; }
        public object StopPoint { get; set; }
        public int StopTime { get; set; }
        public object TechnicalStop { get; set; }
        public bool DayChange { get; set; }
        public bool StopOvernight { get; set; }
        public bool ChangeStation { get; set; }
        public bool ChangeAirport { get; set; }
        public object MarriageGrp { get; set; }
        public int FlightsMiles { get; set; }
        public string Status { get; set; }
    }

    public class RootVNA
    {
        public string Session { get; set; }
        public object Remark { get; set; }
        public List<ListGroupVNA> ListGroup { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public string Language { get; set; }
        public string RequestID { get; set; }
        public List<object> ApiQueries { get; set; }
    }
    #region Hold vé VNA
    //--Verify Flight VNA
    public class InputVerifyFlight
    {
        public string Session { get; set; }
        public int DataOptionId { get; set; }
        public int FareOptionId { get; set; }
        public int FlightOptionId { get; set; }
    }
    public class FareInfoVerifyFlight
    {
        public int OptionId { get; set; }
        public bool Refundable { get; set; }
        public string FareClass { get; set; }
        public int Availability { get; set; }
        public object ExpiryDate { get; set; }
        public int TotalFare { get; set; }
        public int TotalTax { get; set; }
        public int TotalPrice { get; set; }
        public object Remark { get; set; }
        public List<ListFarePaxVerifyFlight> ListFarePax { get; set; }
    }

    public class FlightFareVerifyFlight
    {
        public string Session { get; set; }
        public object Leg { get; set; }
        public int Itinerary { get; set; }
        public string Airline { get; set; }
        public string System { get; set; }
        public string Currency { get; set; }
        public string Remark { get; set; }
        public FareInfoVerifyFlight FareInfo { get; set; }
        public List<ListFlightVerifyFlight> ListFlight { get; set; }
    }

    public class ListFareInfoVerifyFlight
    {
        public int SegmentId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string FareClass { get; set; }
        public string FareBasis { get; set; }
        public string FareFamily { get; set; }
        public string CabinCode { get; set; }
        public string CabinName { get; set; }
        public string HandBaggage { get; set; }
        public string FreeBaggage { get; set; }
        public int Availability { get; set; }
    }

    public class ListFareItemVerifyFlight
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class ListFarePaxVerifyFlight
    {
        public string PaxType { get; set; }
        public int PaxNumb { get; set; }
        public int TotalFare { get; set; }
        public int BaseFare { get; set; }
        public int Taxes { get; set; }
        public List<ListFareItemVerifyFlight> ListFareItem { get; set; }
        public List<ListFareInfoVerifyFlight> ListFareInfo { get; set; }
        public List<ListTaxDetailVerifyFlight> ListTaxDetail { get; set; }
    }

    public class ListFlightVerifyFlight
    {
        public int Leg { get; set; }
        public string FlightId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string FlightNumber { get; set; }
        public int StopNum { get; set; }
        public int Duration { get; set; }
        public List<ListSegmentVerifyFlight> ListSegment { get; set; }
    }

    public class ListSegmentVerifyFlight
    {
        public int SegmentId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string FlightNumber { get; set; }
        public string Equipment { get; set; }
        public int Duration { get; set; }
        public bool HasStop { get; set; }
        public object StopPoint { get; set; }
        public int StopTime { get; set; }
        public object TechnicalStop { get; set; }
        public bool DayChange { get; set; }
        public bool StopOvernight { get; set; }
        public bool ChangeStation { get; set; }
        public bool ChangeAirport { get; set; }
        public object MarriageGrp { get; set; }
        public int FlightsMiles { get; set; }
        public string Status { get; set; }
    }

    public class ListTaxDetailVerifyFlight
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class RootVerifyFlight
    {
        public FlightFareVerifyFlight FlightFare { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public string Language { get; set; }
        public string RequestID { get; set; }
        public List<object> ApiQueries { get; set; }
    }
    //--End VerifyFlight
    //--Book Flight VNA
    public class ContactInputBookFlightVna
    {
        public int Gender { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Remark { get; set; }
        public bool ReceiveEmail { get; set; }
    }

    public class ListAirOptionInputBookFlightVna
    {
        public string Session { get; set; }
        public int DataOptionId { get; set; }
        public int FlightOptionId { get; set; }
        public int FareOptionId { get; set; }
    }

    public class ListPassengerInputBookFlightVna
    {
        public int Index { get; set; }
        public string NameId { get; set; }
        public int ParentId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Gender { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public object ListPreSeat { get; set; }
        public List<ListBaggageInputBookFlightVna> ListBaggage { get; set; }
        public object ListService { get; set; }
        public object ListFareInfo { get; set; }
        public MembershipInfoInputBookFlightVna MembershipInfo { get; set; }
        public object DocumentType { get; set; }
        public object DocumentCode { get; set; }
        public object DocumentExpiry { get; set; }
        public object Nationality { get; set; }
        public object IssueCountry { get; set; }
    }
    public class ListBaggageInputBookFlightVna
    {
        public int Index { get; set; }
        public string Session { get; set; }
        public string Airline { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public object Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public int Leg { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public object StatusCode { get; set; }
        public bool Confirmed { get; set; }
    }
    public class MembershipInfoInputBookFlightVna
    {
        public string Index { get; set; }
        public string Membership { get; set; }
    }

    public class InputBookFlightVna
    {
        public ContactInputBookFlightVna Contact { get; set; }
        public List<ListPassengerInputBookFlightVna> ListPassenger { get; set; }
        public List<ListAirOptionInputBookFlightVna> ListAirOption { get; set; }
        public List<string> ListOSI { get; set; }
        public string BookType { get; set; }
        public string VerifySession { get; set; }
        public string Tourcode { get; set; }
        public string CACode { get; set; }
    }

    public class BookingOutputBookFlightVna
    {
        public string Status { get; set; }
        public string System { get; set; }
        public string Airline { get; set; }
        public string BookingCode { get; set; }
        public object BookingImage { get; set; }
        public object ExpirationDate { get; set; }
        public object TimePurchase { get; set; }
        public TotalPriceOutputBookFlightVna TotalPrice { get; set; }
        public FlightFareOutputBookFlightVna FlightFare { get; set; }
        public List<ListPassengerOutputBookFlightVna> ListPassenger { get; set; }
        public object ListTicket { get; set; }
        public ContactInfoOutputBookFlightVna ContactInfo { get; set; }
        public object ListSK { get; set; }
        public List<ListOSIOutputBookFlightVna> ListOSI { get; set; }
        public object ListRemark { get; set; }
        public object ListSplitPNR { get; set; }
    }

    public class ContactInfoOutputBookFlightVna
    {
        public int Gender { get; set; }
        public object Title { get; set; }
        public object Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public object Address { get; set; }
        public object City { get; set; }
        public object Country { get; set; }
        public object Remark { get; set; }
        public bool ReceiveEmail { get; set; }
    }

    public class FareInfoOutputBookFlightVna
    {
        public int OptionId { get; set; }
        public bool Refundable { get; set; }
        public object FareClass { get; set; }
        public int Availability { get; set; }
        public object ExpiryDate { get; set; }
        public int TotalFare { get; set; }
        public int TotalTax { get; set; }
        public int TotalPrice { get; set; }
        public object Remark { get; set; }
        public List<ListFarePaxOutputBookFlightVna> ListFarePax { get; set; }
    }

    public class FlightFareOutputBookFlightVna
    {
        public object Session { get; set; }
        public object Leg { get; set; }
        public int Itinerary { get; set; }
        public string Airline { get; set; }
        public string System { get; set; }
        public string Currency { get; set; }
        public object Remark { get; set; }
        public FareInfoOutputBookFlightVna FareInfo { get; set; }
        public List<ListFlightOutputBookFlightVna> ListFlight { get; set; }
    }

    public class ListFareInfoOutputBookFlightVna
    {
        public int SegmentId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string FareClass { get; set; }
        public string FareBasis { get; set; }
        public object FareFamily { get; set; }
        public object CabinCode { get; set; }
        public object CabinName { get; set; }
        public object HandBaggage { get; set; }
        public string FreeBaggage { get; set; }
        public int Availability { get; set; }
    }

    public class ListFareItemOutputBookFlightVna
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class ListFarePaxOutputBookFlightVna
    {
        public string PaxType { get; set; }
        public int PaxNumb { get; set; }
        public int TotalFare { get; set; }
        public int BaseFare { get; set; }
        public int Taxes { get; set; }
        public List<ListFareItemOutputBookFlightVna> ListFareItem { get; set; }
        public List<ListFareInfoOutputBookFlightVna> ListFareInfo { get; set; }
        public List<ListTaxDetailOutputBookFlightVna> ListTaxDetail { get; set; }
    }

    public class ListFlightOutputBookFlightVna
    {
        public int Leg { get; set; }
        public string FlightId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string FlightNumber { get; set; }
        public int StopNum { get; set; }
        public int Duration { get; set; }
        public List<ListSegmentOutputBookFlightVna> ListSegment { get; set; }
    }

    public class ListOSIOutputBookFlightVna
    {
        public string Index { get; set; }
        public string OSI { get; set; }
    }

    public class ListPassengerOutputBookFlightVna
    {
        public int Index { get; set; }
        public string NameId { get; set; }
        public int ParentId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Gender { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public object BirthDate { get; set; }
        public List<object> ListPreSeat { get; set; }
        public List<object> ListBaggage { get; set; }
        public List<object> ListService { get; set; }
        public object ListFareInfo { get; set; }
        public object MembershipInfo { get; set; }
        public object DocumentInfo { get; set; }
    }

    public class ListSegmentOutputBookFlightVna
    {
        public int SegmentId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string FlightNumber { get; set; }
        public string Equipment { get; set; }
        public int Duration { get; set; }
        public bool HasStop { get; set; }
        public object StopPoint { get; set; }
        public int StopTime { get; set; }
        public object TechnicalStop { get; set; }
        public bool DayChange { get; set; }
        public bool StopOvernight { get; set; }
        public bool ChangeStation { get; set; }
        public bool ChangeAirport { get; set; }
        public object MarriageGrp { get; set; }
        public int FlightsMiles { get; set; }
        public string Status { get; set; }
    }

    public class ListTaxDetailOutputBookFlightVna
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class RootOutputBookFlightVna
    {
        public BookingOutputBookFlightVna Booking { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public string Language { get; set; }
        public string RequestID { get; set; }
        public List<object> ApiQueries { get; set; }
    }

    public class TotalPriceOutputBookFlightVna
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
    }
    //--End Book Flight VNA

    #endregion Hold vé VNA
    #region GetService
    public class ListBaggageGetAncillary
    {
        public object Index { get; set; }
        public object Session { get; set; }
        public string Airline { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public object Description { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public int Leg { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public object StatusCode { get; set; }
        public bool Confirmed { get; set; }
        public string Source { get; set; }
        public string NameId { get; set; }
    }

    public class ListServiceGetAncillary
    {
        public object Index { get; set; }
        public object Session { get; set; }
        public string Airline { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public object Description { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public int Leg { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public object StatusCode { get; set; }
        public bool Confirmed { get; set; }
        public string Source { get; set; }
        public string NameId { get; set; }
    }

    public class RootGetAncillary
    {
        public string Session { get; set; }
        public List<ListBaggageGetAncillary> ListBaggage { get; set; }
        public List<ListServiceGetAncillary> ListService { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public string Language { get; set; }
        public string RequestID { get; set; }
        public List<object> ApiQueries { get; set; }
    }
    #endregion EndGetService

    #region ReviewPNR
    public class BookingReviewPNR
    {
        public string Status { get; set; }
        public string System { get; set; }
        public string Airline { get; set; }
        public string BookingCode { get; set; }
        public object BookingImage { get; set; }
        public string ExpirationDate { get; set; }
        public string TimePurchase { get; set; }
        public TotalPriceReviewPNR TotalPrice { get; set; }
        public FlightFareReviewPNR FlightFare { get; set; }
        public List<ListPassengerReviewPNR> ListPassenger { get; set; }
        public object ListTicket { get; set; }
        public ContactInfoReviewPNR ContactInfo { get; set; }
        public object ListSK { get; set; }
        public object ListOSI { get; set; }
        public object ListRemark { get; set; }
        public object ListSplitPNR { get; set; }
    }

    public class ContactInfoReviewPNR
    {
        public int Gender { get; set; }
        public object Title { get; set; }
        public object Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public object Address { get; set; }
        public object City { get; set; }
        public object Country { get; set; }
        public object Remark { get; set; }
        public bool ReceiveEmail { get; set; }
    }

    public class FareInfoReviewPNR
    {
        public int OptionId { get; set; }
        public bool Refundable { get; set; }
        public object FareClass { get; set; }
        public int Availability { get; set; }
        public object ExpiryDate { get; set; }
        public int TotalFare { get; set; }
        public int TotalTax { get; set; }
        public int TotalPrice { get; set; }
        public object Remark { get; set; }
        public List<ListFarePaxReviewPNR> ListFarePax { get; set; }
    }

    public class FlightFareReviewPNR
    {
        public object Session { get; set; }
        public object Leg { get; set; }
        public int Itinerary { get; set; }
        public string Airline { get; set; }
        public string System { get; set; }
        public string Currency { get; set; }
        public object Remark { get; set; }
        public FareInfoReviewPNR FareInfo { get; set; }
        public List<ListFlightReviewPNR> ListFlight { get; set; }
    }

    public class ListBaggageReviewPNR
    {
        public string Index { get; set; }
        public object Session { get; set; }
        public string Airline { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public object Description { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public int Leg { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string StatusCode { get; set; }
        public bool Confirmed { get; set; }
        public string Source { get; set; }
        public string NameId { get; set; }
    }

    public class ListFareInfoReviewPNR
    {
        public int SegmentId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string FareClass { get; set; }
        public string FareBasis { get; set; }
        public object FareFamily { get; set; }
        public object CabinCode { get; set; }
        public object CabinName { get; set; }
        public object HandBaggage { get; set; }
        public string FreeBaggage { get; set; }
        public int Availability { get; set; }
    }

    public class ListFareItemReviewPNR
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class ListFarePaxReviewPNR
    {
        public string PaxType { get; set; }
        public int PaxNumb { get; set; }
        public int TotalFare { get; set; }
        public int BaseFare { get; set; }
        public int Taxes { get; set; }
        public List<ListFareItemReviewPNR> ListFareItem { get; set; }
        public List<ListFareInfoReviewPNR> ListFareInfo { get; set; }
        public List<ListTaxDetailReviewPNR> ListTaxDetail { get; set; }
    }

    public class ListFlightReviewPNR
    {
        public int Leg { get; set; }
        public string FlightId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string FlightNumber { get; set; }
        public int StopNum { get; set; }
        public int Duration { get; set; }
        public List<ListSegmentReviewPNR> ListSegment { get; set; }
    }

    public class ListPassengerReviewPNR
    {
        public int Index { get; set; }
        public string NameId { get; set; }
        public int ParentId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Gender { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public List<object> ListPreSeat { get; set; }
        public List<ListBaggageReviewPNR> ListBaggage { get; set; }
        public List<ListServiceReviewPNR> ListService { get; set; }
        public object ListFareInfo { get; set; }
        public object MembershipInfo { get; set; }
        public object DocumentInfo { get; set; }
    }

    public class ListSegmentReviewPNR
    {
        public int SegmentId { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public string ArriveDate { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string FlightNumber { get; set; }
        public string Equipment { get; set; }
        public int Duration { get; set; }
        public bool HasStop { get; set; }
        public object StopPoint { get; set; }
        public int StopTime { get; set; }
        public object TechnicalStop { get; set; }
        public bool DayChange { get; set; }
        public bool StopOvernight { get; set; }
        public bool ChangeStation { get; set; }
        public bool ChangeAirport { get; set; }
        public object MarriageGrp { get; set; }
        public int FlightsMiles { get; set; }
        public string Status { get; set; }
    }

    public class ListServiceReviewPNR
    {
        public object Index { get; set; }
        public object Session { get; set; }
        public string Airline { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public int Leg { get; set; }
        public object StartPoint { get; set; }
        public object EndPoint { get; set; }
        public string StatusCode { get; set; }
        public bool Confirmed { get; set; }
        public string Source { get; set; }
        public string NameId { get; set; }
    }

    public class ListTaxDetailReviewPNR
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class RootReviewPNR
    {
        public BookingReviewPNR Booking { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public string Language { get; set; }
        public string RequestID { get; set; }
        public List<object> ApiQueries { get; set; }
    }

    public class TotalPriceReviewPNR
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
    }

    #endregion EndReviewPNR

    #region IssueTicke
    public class CouponInfoIssueTicke
    {
        public string ItemNumber { get; set; }
        public string Airline { get; set; }
        public string Operator { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartDate { get; set; }
        public object ArriveDate { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string FareBasis { get; set; }
        public string CouponNumber { get; set; }
        public string CouponStatus { get; set; }
        public string BaggageInfo { get; set; }
    }

    public class ListTicketIssueTicke
    {
        public int Index { get; set; }
        public string BookingCode { get; set; }
        public string TicketNumber { get; set; }
        public string TicketType { get; set; }
        public string ServiceType { get; set; }
        public object ServiceCode { get; set; }
        public object RelatedNumber { get; set; }
        public object RelatedType { get; set; }
        public string IssueDate { get; set; }
        public string PaxIndex { get; set; }
        public string FullName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public object PaxType { get; set; }
        public int TotalFare { get; set; }
        public int BaseFare { get; set; }
        public int Taxes { get; set; }
        public string Currency { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Info { get; set; }
        public List<TaxDetailIssueTicke> TaxDetails { get; set; }
        public List<CouponInfoIssueTicke> CouponInfos { get; set; }
        public string Endorsement { get; set; }
        public object TicketImage { get; set; }
    }

    public class RootIssueTicke
    {
        public List<ListTicketIssueTicke> ListTicket { get; set; }
        public string StatusCode { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public string Language { get; set; }
        public string RequestID { get; set; }
        public List<object> ApiQueries { get; set; }
    }

    public class TaxDetailIssueTicke
    {
        public string Code { get; set; }
        public object Name { get; set; }
        public int Amount { get; set; }
    }

    #endregion  IssueTicke
}
