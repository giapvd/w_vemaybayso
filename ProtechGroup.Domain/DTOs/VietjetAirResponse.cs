using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.DTOs
{
    public class AircraftModel
    {
        public string href { get; set; }
        public string key { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public object type { get; set; }
        public int seatingCapacity { get; set; }
        public object timestamp { get; set; }
    }

    public class AirlineCode
    {
        public string href { get; set; }
        public string code { get; set; }
        public object name { get; set; }
        public object description { get; set; }
        public object active { get; set; }
        public object parent { get; set; }
        public object timestamp { get; set; }
    }

    public class AirportVJ
    {
        public string href { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object timezone { get; set; }
        public UtcOffset utcOffset { get; set; }
        public object secure { get; set; }
    }

    public class Arrival
    {
        public string scheduledTime { get; set; }
        public object localScheduledTime { get; set; }
        public object utcScheduledShortTime { get; set; }
        public object localScheduledShortTime { get; set; }
        public object estimatedTime { get; set; }
        public object utcEstimatedShortTime { get; set; }
        public object utcActualOutShortTime { get; set; }
        public object utcActualOffShortTime { get; set; }
        public object utcActualOnShortTime { get; set; }
        public object utcActualInShortTime { get; set; }
        public AirportVJ airport { get; set; }
    }

    public class BookingApplicability
    {
        public bool allPassengers { get; set; }
        public bool primaryPassenger { get; set; }
        public bool optional { get; set; }
    }

    public class BookingCode
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object cabinClass { get; set; }
        public object nesting { get; set; }
        public object published { get; set; }
        public object fareClassDefaultCriteria { get; set; }
        public object seatSelectionCharge { get; set; }
        public object timestamp { get; set; }
    }

    public class CabinClass
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

    public class ChargeType
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object saleCode { get; set; }
        public object usageCode { get; set; }
        public object feeCategory { get; set; }
        public int index { get; set; }
        public object timestamp { get; set; }
    }

    public class CityPair
    {
        public string href { get; set; }
        public string identifier { get; set; }
        public object departure { get; set; }
        public object arrival { get; set; }
        public object validConnectionAirports { get; set; }
        public object fareStatuses { get; set; }
        public object chargeStatuses { get; set; }
        public object taxConfiguration { get; set; }
        public int loyaltyPointsEarned { get; set; }
        public object groupBookingCount { get; set; }
        public object routeType { get; set; }
        public object travelOptionCriteria { get; set; }
        public object fares { get; set; }
        public object charges { get; set; }
        public object timestamp { get; set; }
    }

    public class Currency
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool baseCurrency { get; set; }
        public double currentExchangeRate { get; set; }
        public object format { get; set; }
    }

    public class CurrencyAmount
    {
        public double baseAmount { get; set; }
        public double discountAmount { get; set; }
        public double taxAmount { get; set; }
        public List<TaxRateAmount> taxRateAmounts { get; set; }
        public double totalAmount { get; set; }
        public Currency currency { get; set; }
        public double exchangeRate { get; set; }
    }

    public class Departure
    {
        public string scheduledTime { get; set; }
        public object localScheduledTime { get; set; }
        public object utcScheduledShortTime { get; set; }
        public object localScheduledShortTime { get; set; }
        public object estimatedTime { get; set; }
        public object utcEstimatedShortTime { get; set; }
        public object utcActualOutShortTime { get; set; }
        public object utcActualOffShortTime { get; set; }
        public object utcActualOnShortTime { get; set; }
        public object utcActualInShortTime { get; set; }
        public AirportVJ airport { get; set; }
    }

    public class FareCharge
    {
        public string description { get; set; }
        public BookingApplicability bookingApplicability { get; set; }
        public PassengerApplicability passengerApplicability { get; set; }
        public ChargeType chargeType { get; set; }
        public List<CurrencyAmount> currencyAmounts { get; set; }
        public TaxConfiguration taxConfiguration { get; set; }
    }

    public class FareClass
    {
        public string href { get; set; }
        public string key { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object bookingCode { get; set; }
        public object bookingCodeDefault { get; set; }
        public object secondarySelectionDisplay { get; set; }
        public object fareType { get; set; }
        public object bookingStatus { get; set; }
        public object fareRules { get; set; }
        public object fareRestrictions { get; set; }
        public bool seatSale { get; set; }
        public object autoApplyCharges { get; set; }
        public object nonRevenue { get; set; }
        public bool allowWaitlist { get; set; }
        public object lateBookingOverride { get; set; }
        public PassengerApplicability passengerApplicability { get; set; }
        public object advancedBookingDays { get; set; }
        public object journeyApplicability { get; set; }
        public object stayOverCriteria { get; set; }
        public SeatSelectionChargeApplicability seatSelectionChargeApplicability { get; set; }
        public object loyaltyMultiplier { get; set; }
        public object specifiedCommission { get; set; }
        public object distributionChannels { get; set; }
        public object timestamp { get; set; }
    }

    public class FareOption
    {
        public string bookingKey { get; set; }
        public FareValidity fareValidity { get; set; }
        public FareClass fareClass { get; set; }
        public BookingCode bookingCode { get; set; }
        public CabinClass cabinClass { get; set; }
        public FareType fareType { get; set; }
        public int availability { get; set; }
        public bool cheapestFareType { get; set; }
        public bool cheapestFareOption { get; set; }
        public List<FareCharge> fareCharges { get; set; }
        public bool promoCodeApplied { get; set; }
    }

    public class FareType
    {
        public string href { get; set; }
        public string identifier { get; set; }
        public string description { get; set; }
        public int index { get; set; }
    }

    public class FareValidity
    {
        public bool valid { get; set; }
        public bool soldOut { get; set; }
        public bool noFare { get; set; }
        public bool invalidAdultAvailability { get; set; }
        public bool invalidChildAvailability { get; set; }
        public bool invalidAvailability { get; set; }
        public bool invalidLayover { get; set; }
        public bool invalidStayover { get; set; }
    }

    public class Flight
    {
        public string href { get; set; }
        public string key { get; set; }
        public AirlineCode airlineCode { get; set; }
        public string flightNumber { get; set; }
        public object operatingPartnerCarrier { get; set; }
        public object flightType { get; set; }
        public AircraftModel aircraftModel { get; set; }
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }
        public object status { get; set; }
        public object flightStatus { get; set; }
        public object schedule { get; set; }
        public object legs { get; set; }
        public object timestamp { get; set; }
    }

    public class PassengerApplicability
    {
        public bool child { get; set; }
        public bool adult { get; set; }
        public bool infant { get; set; }
    }

    public class PromoCodeApplicability
    {
        public bool promoCodeRequested { get; set; }
        public PromoCodeValidity promoCodeValidity { get; set; }
        public string promoCode { get; set; }
    }

    public class PromoCodeValidity
    {
        public bool valid { get; set; }
        public bool notApplicable { get; set; }
        public bool noMarket { get; set; }
        public bool invalidFlightDate { get; set; }
        public bool notAvailable { get; set; }
        public bool invalidAvailability { get; set; }
    }

    public class AirlineVietJets
    {
        public List<RootVietJets> rootVietJets { get; set; }
    }
    public class RootVietJets
    {
        public string href { get; set; }
        public string key { get; set; }
        public CityPair cityPair { get; set; }
        public string departureDate { get; set; }
        public double enRouteHours { get; set; }
        public int numberOfStops { get; set; }
        public int numberOfChanges { get; set; }
        public List<Flight> flights { get; set; }
        public List<FareOption> fareOptions { get; set; }
        public PromoCodeApplicability promoCodeApplicability { get; set; }
    }

    public class SeatSelectionChargeApplicability
    {
        public bool bookingCode { get; set; }
        public bool seatType { get; set; }
    }

    public class TaxConfiguration
    {
        public object feeCategory { get; set; }
    }

    public class TaxRateAmount
    {
        public string name { get; set; }
        public double amount { get; set; }
    }

    public class UtcOffset
    {
        public string iso { get; set; }
        public double hours { get; set; }
        public int minutes { get; set; }
    }

    public class UserSessionVJ
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string accountActionMessage { get; set; }
        public string daysUntilExpiry { get; set; }
        public bool isPasswordExpiryEnabled { get; set; }
    }
    #region Booking VJ
    public class RootBookingVietJet
    {
        public string href { get; set; }
        public string key { get; set; }
        public int number { get; set; }
        public string locator { get; set; }
        public object directParentReservation { get; set; }
        public object subsidiaryReservations { get; set; }
        public BookingInformation bookingInformation { get; set; }
        public object reservationSummary { get; set; }
        public List<Passenger> passengers { get; set; }
        public List<Journey> journeys { get; set; }
        public object insurancePolicies { get; set; }
        public List<object> ancillaryPurchases { get; set; }
        public List<object> seatSelections { get; set; }
        public List<PassengerLegDetail> passengerLegDetails { get; set; }
        public List<Charge> charges { get; set; }
        public List<PaymentTransaction> paymentTransactions { get; set; }
        public List<object> eTickets { get; set; }
    }
    public class PaymentTransaction
    {
        public object href { get; set; }
        public object key { get; set; }
        public object paymentTime { get; set; }
        public object description { get; set; }
        public PaymentMethodVJ paymentMethod { get; set; }
        public PaymentMethodCriteria paymentMethodCriteria { get; set; }
        public List<CurrencyAmountVJ> currencyAmounts { get; set; }
        public object processingCurrencyAmounts { get; set; }
        public object payerDescription { get; set; }
        public object receiptNumber { get; set; }
        public object allPassengers { get; set; }
        public object payments { get; set; }
        public object refundTransactions { get; set; }
        public object notes { get; set; }
    }
    public class CurrencyAmountVJ
    {
        public double baseAmount { get; set; }
        public double discountAmount { get; set; }
        public double taxAmount { get; set; }
        public List<TaxRateAmountVJ> taxRateAmounts { get; set; }
        public double totalAmount { get; set; }
        public CurrencyVJ currency { get; set; }
        public double exchangeRate { get; set; }
    }
    public class TaxRateAmountVJ
    {
        public string name { get; set; }
        public double amount { get; set; }
    }
    public class PaymentMethodCriteria
    {
        public object account { get; set; }
        public object creditCard { get; set; }
        public object thirdParty { get; set; }
        public object voucher { get; set; }
    }
    public class PaymentMethodVJ
    {
        public string href { get; set; }
        public string key { get; set; }
        public string identifier { get; set; }
        public object description { get; set; }
        public object type { get; set; }
        public object receiptRequired { get; set; }
        public object ticketRequired { get; set; }
        public object processingFee { get; set; }
        public object saleCode { get; set; }
        public object usageCode { get; set; }
        public object timestamp { get; set; }
    }

    public class Charge
    {
        public string href { get; set; }
        public string key { get; set; }
        public Passenger passenger { get; set; }
        public Journey journey { get; set; }
        public string chargeTime { get; set; }
        public string description { get; set; }
        public ChargeTypeVJ chargeType { get; set; }
        public Surcharge surcharge { get; set; }
        public List<CurrencyAmountVJ> currencyAmounts { get; set; }
        public TaxConfigurationVJ taxConfiguration { get; set; }
        public Status status { get; set; }
        public object discount { get; set; }
        public bool refundable { get; set; }
        public bool privateFares { get; set; }
        public string notes { get; set; }
        public object paymentTransactions { get; set; }
        public string timestamp { get; set; }
    }
    public class TaxConfigurationVJ
    {
        public string href { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public object taxRates { get; set; }
        public object timestamp { get; set; }
    }
    public class Surcharge
    {
        public string href { get; set; }
        public string key { get; set; }
        public string identifier { get; set; }
        public string description { get; set; }
        public object chargeAmount { get; set; }
        public object chargeType { get; set; }
        public object specifiedTaxConfiguration { get; set; }
        public object usage { get; set; }
        public object passengerApplicability { get; set; }
        public object refundable { get; set; }
        public object distributionChannels { get; set; }
        public object index { get; set; }
        public object timestamp { get; set; }
    }
    public class ChargeTypeVJ
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object saleCode { get; set; }
        public object usageCode { get; set; }
        public object feeCategory { get; set; }
        public int index { get; set; }
        public object timestamp { get; set; }
    }
    public class PassengerLegDetail
    {
        public string href { get; set; }
        public string key { get; set; }
        public Passenger passenger { get; set; }
        public Journey journey { get; set; }
        public SegmentVJ segment { get; set; }
        public Leg leg { get; set; }
        public ReservationStatus reservationStatus { get; set; }
        public TravelStatus travelStatus { get; set; }
        public object boardingSequenceNumber { get; set; }
        public bool thru { get; set; }
        public string timestamp { get; set; }
    }
    public class TravelStatus
    {
        public bool notCheckedIn { get; set; }
        public bool checkedIn { get; set; }
        public bool boarded { get; set; }
        public bool noshow { get; set; }
    }

    public class Journey
    {
        public string href { get; set; }
        public string key { get; set; }
        public object index { get; set; }
        public DepartureVJ departure { get; set; }
        public List<SegmentVJ> segments { get; set; }
        public List<PassengerJourneyDetail> passengerJourneyDetails { get; set; }
        public ReservationStatus reservationStatus { get; set; }
        public object ancillaryPurchases { get; set; }
        public object seatSelections { get; set; }
        public object paymentTransactions { get; set; }
    }
    public class PassengerJourneyDetail
    {
        public string href { get; set; }
        public string key { get; set; }
        public Passenger passenger { get; set; }
        public object segment { get; set; }
        public string bookingKey { get; set; }
        public FareClassVJ fareClass { get; set; }
        public BookingCodeVJ bookingCode { get; set; }
        public bool realizedRevenue { get; set; }
        public bool shuttle { get; set; }
        public bool privateFares { get; set; }
        public string ticketNumber { get; set; }
        public string notes { get; set; }
        public ReservationStatus reservationStatus { get; set; }
        public string timestamp { get; set; }
    }
    public class BookingCodeVJ
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object cabinClass { get; set; }
        public object nesting { get; set; }
        public object published { get; set; }
        public object fareClassDefaultCriteria { get; set; }
        public object seatSelectionCharge { get; set; }
        public object timestamp { get; set; }
    }
    public class FareClassVJ
    {
        public string href { get; set; }
        public string key { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public object bookingCode { get; set; }
        public object bookingCodeDefault { get; set; }
        public object secondarySelectionDisplay { get; set; }
        public object fareType { get; set; }
        public object bookingStatus { get; set; }
        public object fareRules { get; set; }
        public object fareRestrictions { get; set; }
        public object seatSale { get; set; }
        public object autoApplyCharges { get; set; }
        public object nonRevenue { get; set; }
        public object allowWaitlist { get; set; }
        public object lateBookingOverride { get; set; }
        public object passengerApplicability { get; set; }
        public object advancedBookingDays { get; set; }
        public object journeyApplicability { get; set; }
        public object stayOverCriteria { get; set; }
        public object seatSelectionChargeApplicability { get; set; }
        public object loyaltyMultiplier { get; set; }
        public object specifiedCommission { get; set; }
        public object distributionChannels { get; set; }
        public object timestamp { get; set; }
    }
    public class SegmentVJ
    {
        public string key { get; set; }
        public object index { get; set; }
        public Flight flight { get; set; }
        public DepartureVJ departure { get; set; }
        public Arrival arrival { get; set; }
        public List<Leg> legs { get; set; }
        public ReservationStatus reservationStatus { get; set; }
    }
    public class Leg
    {
        public string key { get; set; }
        public object index { get; set; }
        public DepartureVJ departure { get; set; }
        public ArrivalVJ arrival { get; set; }
    }
    public class ArrivalVJ
    {
        public string scheduledTime { get; set; }
        public string localScheduledTime { get; set; }
        public object utcScheduledShortTime { get; set; }
        public object localScheduledShortTime { get; set; }
        public object estimatedTime { get; set; }
        public object utcEstimatedShortTime { get; set; }
        public object utcActualOutShortTime { get; set; }
        public object utcActualOffShortTime { get; set; }
        public object utcActualOnShortTime { get; set; }
        public object utcActualInShortTime { get; set; }
        public AirportVJ1 airport { get; set; }
    }
    public class AirportVJ1
    {
        public string href { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object timezone { get; set; }
        public UtcOffsetVJ utcOffset { get; set; }
        public object secure { get; set; }
    }
    public class UtcOffsetVJ
    {
        public string iso { get; set; }
        public double hours { get; set; }
        public int minutes { get; set; }
    }
    public class DepartureVJ
    {
        public string scheduledTime { get; set; }
        public string localScheduledTime { get; set; }
        public object utcScheduledShortTime { get; set; }
        public object localScheduledShortTime { get; set; }
        public object estimatedTime { get; set; }
        public object utcEstimatedShortTime { get; set; }
        public object utcActualOutShortTime { get; set; }
        public object utcActualOffShortTime { get; set; }
        public object utcActualOnShortTime { get; set; }
        public object utcActualInShortTime { get; set; }
        public AirportVJ airport { get; set; }
    }
    public class BookingInformation
    {
        public string href { get; set; }
        public string key { get; set; }
        public string sessionIdentifier { get; set; }
        public DistributionChannel distributionChannel { get; set; }
        public object externalLocators { get; set; }
        public Agency agency { get; set; }
        public object company { get; set; }
        public CurrencyVJ currency { get; set; }
        public ContactInformation contactInformation { get; set; }
        public Creation creation { get; set; }
        public Hold hold { get; set; }
        public object cancellation { get; set; }
        public BookingType bookingType { get; set; }
        public object frequentFlyer { get; set; }
        public string notes { get; set; }
        public object paymentTransactions { get; set; }
        public string timestamp { get; set; }
    }
    public class DistributionChannel
    {
        public string href { get; set; }
        public string identifier { get; set; }
        public string description { get; set; }
        public object index { get; set; }
    }
    public class Agency
    {
        public string href { get; set; }
        public string key { get; set; }
        public object agencyType { get; set; }
        public string iataNumber { get; set; }
        public string name { get; set; }
        public object status { get; set; }
        public object masterParentAgency { get; set; }
        public object directParentAgency { get; set; }
        public object specifiedTaxConfiguration { get; set; }
        public object address { get; set; }
        public object contactInformation { get; set; }
        public object notes { get; set; }
        public object markupCommission { get; set; }
        public object specifiedHoldTime { get; set; }
        public object specifiedGroupBookingCount { get; set; }
        public object agencyAccount { get; set; }
        public object gdsAccount { get; set; }
        public object timestamp { get; set; }
    }
    public class CurrencyVJ
    {
        public string href { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool baseCurrency { get; set; }
        public double currentExchangeRate { get; set; }
        public object format { get; set; }
    }
    public class ContactInformation
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public object mobileNumber { get; set; }
        public object faxNumber { get; set; }
        public string extension { get; set; }
        public string email { get; set; }
    }
    public class Creation
    {
        public string time { get; set; }
    }

    public class Hold
    {
        public bool overridden { get; set; }
        public string expiryTime { get; set; }
    }
    public class BookingType
    {
        public bool eticketed { get; set; }
        public bool groupReserved { get; set; }
        public bool frequentFlyerRedeemed { get; set; }
    }
    public class Passenger
    {
        public string href { get; set; }
        public string key { get; set; }
        public object index { get; set; }
        public bool reservationOwner { get; set; }
        public ReservationStatus reservationStatus { get; set; }
        public FareApplicability fareApplicability { get; set; }
        public ReservationProfile reservationProfile { get; set; }
        public object frequentFlyer { get; set; }
        public object advancePassengerInformation { get; set; }
        public List<object> passengerServiceRequests { get; set; }
        public object passengerCostCenter { get; set; }
        public List<object> infants { get; set; }
        public object weight { get; set; }
        public string notes { get; set; }
        public object journeys { get; set; }
        public object paymentTransactions { get; set; }
        public string timestamp { get; set; }
    }
    public class ReservationStatus
    {
        public bool confirmed { get; set; }
        public bool waitlist { get; set; }
        public bool standby { get; set; }
        public bool cancelled { get; set; }
        public bool noshow { get; set; }
        public bool open { get; set; }
        public bool pending { get; set; }
        public bool finalized { get; set; }
        public bool external { get; set; }
    }
    public class FareApplicability
    {
        public bool child { get; set; }
        public bool adult { get; set; }
        public bool infant { get; set; }
    }
    public class ReservationProfile
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string title { get; set; }
        public string gender { get; set; }
        public Address address { get; set; }
        public object birthDate { get; set; }
        public NationCountry nationCountry { get; set; }
        public PersonalContactInformation personalContactInformation { get; set; }
        public BusinessContactInformation businessContactInformation { get; set; }
        public object destinationContactInformation { get; set; }
        public object passport { get; set; }
        public object loyaltyProgram { get; set; }
        public bool preBoard { get; set; }
        public Status status { get; set; }
        public string reference1 { get; set; }
        public string reference2 { get; set; }
        public string notes { get; set; }
        public object timestamp { get; set; }
    }
    public class Status
    {
        public bool active { get; set; }
        public object inactive { get; set; }
        public object denied { get; set; }
        public object historical { get; set; }
        public object pending { get; set; }
    }
    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public Location location { get; set; }
        public string postalCode { get; set; }
    }
    public class Location
    {
        public Country country { get; set; }
        public object province { get; set; }
    }
    public class Country
    {
        public string href { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public object provinces { get; set; }
        public object timestamp { get; set; }
    }
    public class NationCountry
    {
        public string href { get; set; }
        public string code { get; set; }
        public object name { get; set; }
        public object provinces { get; set; }
        public object timestamp { get; set; }
    }
    public class PersonalContactInformation
    {
        public object name { get; set; }
        public string phoneNumber { get; set; }
        public string mobileNumber { get; set; }
        public object faxNumber { get; set; }
        public object extension { get; set; }
        public string email { get; set; }
    }
    public class BusinessContactInformation
    {
        public object name { get; set; }
        public string phoneNumber { get; set; }
        public object mobileNumber { get; set; }
        public string faxNumber { get; set; }
        public string extension { get; set; }
        public object email { get; set; }
    }

    #endregion End Booking VJ

    #region Root Ancillary Options
    public class RootAncillaryOptions
    {
        public string href { get; set; }
        public string key { get; set; }
        public string purchaseKey { get; set; }
        public AncillaryItem ancillaryItem { get; set; }
        public RequirementLocation requirementLocation { get; set; }
        public object availability { get; set; }
        public PurchaseApplicability purchaseApplicability { get; set; }
        public List<AncillaryCharge> ancillaryCharges { get; set; }
    }
    public class AncillaryCharge
    {
        public string description { get; set; }
        public ChargeType chargeType { get; set; }
        public List<CurrencyAmount> currencyAmounts { get; set; }
        public TaxConfiguration taxConfiguration { get; set; }
    }
    public class PurchaseApplicability
    {
        public bool available { get; set; }
        public bool package { get; set; }
        public bool unavailable { get; set; }
    }
    public class RequirementLocation
    {
        public string date { get; set; }
        public object airport { get; set; }
        public CityPair cityPair { get; set; }
        public object flight { get; set; }
    }
    public class AncillaryItem
    {
        public object href { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int index { get; set; }
        public object status { get; set; }
        public AncillaryCategory ancillaryCategory { get; set; }
        public AncillaryOptionType ancillaryOptionType { get; set; }
        public object chargeAmount { get; set; }
        public object chargeOverrides { get; set; }
        public object fareClassOverrides { get; set; }
        public PurchaseConditions purchaseConditions { get; set; }
        public object inventoryCriteria { get; set; }
        public object serviceRequest { get; set; }
        public object serviceRequestNote { get; set; }
        public object translations { get; set; }
        public object requestedTranslation { get; set; }
        public object timestamp { get; set; }
    }
    public class AncillaryOptionType
    {
        public bool airportOption { get; set; }
        public bool cityPairOption { get; set; }
        public bool flightOption { get; set; }
    }
    public class AncillaryCategory
    {
        public object href { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int index { get; set; }
        public object status { get; set; }
        public object ancillaryType { get; set; }
        public PurchaseConditions purchaseConditions { get; set; }
        public object translations { get; set; }
        public RequestedTranslation requestedTranslation { get; set; }
        public object timestamp { get; set; }
    }
    public class RequestedTranslation
    {
        public string key { get; set; }
        public Language language { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class Language
    {
        public string href { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
    public class PurchaseConditions
    {
        public string allowMultipleItems { get; set; }
        public object priorDepartureLockMinutes { get; set; }
        public object maximumPerPassengerCount { get; set; }
        public object enforcedDuringBooking { get; set; }
        public object specifyDate { get; set; }
    }
    #endregion End Root Ancillary Options

    #region Companies
    public class Companies
    {
        public string href { get; set; }
        public string key { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public Accountcps account { get; set; }
    }
    public class Currencycps
    {
        public string code { get; set; }
        public string description { get; set; }
        public bool baseCurrency { get; set; }
        public double currentExchangeRate { get; set; }
    }
    public class Accountcps
    {
        public string accountNumber { get; set; }
        public double creditLimit { get; set; }
        public double creditAvailable { get; set; }
        public Currencycps currency { get; set; }
    }

    #endregion End Companies

    #region RootBokingIFTR
    public class AgencyIFTR
    {
        public string href { get; set; }
        public string key { get; set; }
        public string iataNumber { get; set; }
        public string name { get; set; }
    }

    public class BookingInformationIFTR
    {
        public string href { get; set; }
        public string key { get; set; }
        public string sessionIdentifier { get; set; }
        public DistributionChannelIFTR distributionChannel { get; set; }
        public AgencyIFTR agency { get; set; }
        public CurrencyIFTR currency { get; set; }
        public CreationIFTR creation { get; set; }
        public HoldIFTR hold { get; set; }
        public BookingTypeIFTR bookingType { get; set; }
    }

    public class BookingTypeIFTR
    {
        public bool eticketed { get; set; }
        public bool groupReserved { get; set; }
        public bool frequentFlyerRedeemed { get; set; }
    }

    public class ChargesIFTR
    {
        public double baseAmount { get; set; }
        public double discountAmount { get; set; }
        public double taxAmount { get; set; }
        public double totalAmount { get; set; }
        public double exchangeRate { get; set; }
    }

    public class CreationIFTR
    {
        public string time { get; set; }
    }

    public class CurrencyIFTR
    {
        public string code { get; set; }
        public string description { get; set; }
        public bool baseCurrency { get; set; }
        public double currentExchangeRate { get; set; }
    }

    public class DistributionChannelIFTR
    {
        public string href { get; set; }
        public string identifier { get; set; }
        public string description { get; set; }
    }

    public class HoldIFTR
    {
        public bool overridden { get; set; }
        public string expiryTime { get; set; }
    }

    public class PassengerIFTR
    {
        public string href { get; set; }
        public string key { get; set; }
        public bool reservationOwner { get; set; }
        public ReservationProfileIFTR reservationProfile { get; set; }
    }

    public class PaymentsIFTR
    {
        public double baseAmount { get; set; }
        public double discountAmount { get; set; }
        public double taxAmount { get; set; }
        public double totalAmount { get; set; }
        public double exchangeRate { get; set; }
    }

    public class RefundsIFTR
    {
        public double baseAmount { get; set; }
        public double discountAmount { get; set; }
        public double taxAmount { get; set; }
        public double totalAmount { get; set; }
        public double exchangeRate { get; set; }
    }

    public class ReservationProfileIFTR
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
    }

    public class ReservationSummaryIFTR
    {
        public PassengerIFTR passenger { get; set; }
        public ChargesIFTR charges { get; set; }
        public int seatedPassengerCount { get; set; }
        public PaymentsIFTR payments { get; set; }
        public RefundsIFTR refunds { get; set; }
        public string itinerary { get; set; }
    }

    public class RootBokingIFTR
    {
        public string href { get; set; }
        public string key { get; set; }
        public int number { get; set; }
        public string locator { get; set; }
        public BookingInformationIFTR bookingInformation { get; set; }
        public ReservationSummaryIFTR reservationSummary { get; set; }
    }
    #endregion End RootBokingIFTR

    #region RootAgencies
    public class RootAgencies
    {
        public string href { get; set; }
        public string key { get; set; }
        public AgencyTypeAgc agencyType { get; set; }
        public string iataNumber { get; set; }
        public string name { get; set; }
        public Status status { get; set; }
        public SpecifiedTaxConfigurationAgc specifiedTaxConfiguration { get; set; }
        public Address address { get; set; }
        public ContactInformation contactInformation { get; set; }
        public string notes { get; set; }
        public SpecifiedHoldTimeAgc specifiedHoldTime { get; set; }
        public SpecifiedGroupBookingCountAgc specifiedGroupBookingCount { get; set; }
        public AgencyAccountAgc agencyAccount { get; set; }
    }
    public class AgencyAccountAgc
    {
        public string key { get; set; }
        public bool agencyAccount { get; set; }
        public bool gdsAccount { get; set; }
        public CompanyAgc company { get; set; }
    }
    public class CompanyAgc
    {
        public string href { get; set; }
        public string key { get; set; }
        public AccountAgc account { get; set; }
    }
    public class AccountAgc
    {
        public string accountNumber { get; set; }
        public double creditLimit { get; set; }
        public double creditAvailable { get; set; }
        public CurrencyAgc currency { get; set; }
    }
    public class CurrencyAgc
    {
        public string code { get; set; }
        public string description { get; set; }
        public bool baseCurrency { get; set; }
        public double currentExchangeRate { get; set; }
    }
    public class AgencyTypeAgc
    {
        public string href { get; set; }
        public string key { get; set; }
        public string name { get; set; }
    }

    public class SpecifiedGroupBookingCountAgc
    {
        public bool overridden { get; set; }
    }

    public class SpecifiedHoldTimeAgc
    {
        public bool overridden { get; set; }
        public HoldTimeAgc holdTime { get; set; }
    }
    public class HoldTimeAgc
    {
        public string key { get; set; }
        public int holdTimeMinutes { get; set; }
    }
    public class SpecifiedTaxConfigurationAgc
    {
        public bool overridden { get; set; }
    }

    public class StatusAgc
    {
        public bool active { get; set; }
        public bool inactive { get; set; }
    }
    #endregion End RootAgencies

    #region RootPayMentTransaction
    public class RootPayMentTransaction
    {
        public string href { get; set; }
        public string key { get; set; }
        public string paymentTime { get; set; }
        public string description { get; set; }
        public PaymentMethodPTr paymentMethod { get; set; }
        public PaymentMethodCriteriaPTr paymentMethodCriteria { get; set; }
        public List<CurrencyAmountPTr> currencyAmounts { get; set; }
        public string payerDescription { get; set; }
        public int receiptNumber { get; set; }
        public List<PaymentPtr> payments { get; set; }
        public List<object> refundTransactions { get; set; }
        public string notes { get; set; }
    }
    public class PaymentPtr
    {
        public string key { get; set; }
        public List<CurrencyAmountPTr> currencyAmounts { get; set; }
        public PassengerPTr passenger { get; set; }
    }
    public class PassengerPTr
    {
        public string href { get; set; }
        public string key { get; set; }
        public bool reservationOwner { get; set; }
    }
    public class PaymentMethodPTr
    {
        public string href { get; set; }
        public string key { get; set; }
        public string identifier { get; set; }
    }
    public class PaymentMethodCriteriaPTr
    {
        public AccountPTr account { get; set; }
    }
    public class CurrencyAmountPTr
    {
        public double baseAmount { get; set; }
        public double discountAmount { get; set; }
        public double taxAmount { get; set; }
        public double totalAmount { get; set; }
        public Currency currency { get; set; }
        public double exchangeRate { get; set; }
    }
    public class AccountPTr
    {
        public CompanyPTr company { get; set; }
        public string purchaseOrder { get; set; }
    }
    public class CompanyPTr
    {
        public string href { get; set; }
        public string key { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
    }

    #endregion
}
