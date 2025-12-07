using ProtechGroup.Application.Interfaces;
using System.Web.Mvc;
using System.Linq;
using ProtechGroup.FlightBookingWeb.Helpers;
using ProtechGroup.FlightBookingWeb.Models;
using System.Collections.Generic;
using ProtechGroup.Domain;
using ProtechGroup.Domain.Entities;
using System.Web;
using System;
using ProtechGroup.Application.Services;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Application.Common;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using ProtechGroup.Infrastructure.Entities;
using System.EnterpriseServices;
using System.Web.Helpers;
using System.Text.RegularExpressions;
using AutoMapper;
using System.Diagnostics.Contracts;
using Unity;

namespace ProtechGroup.FlightBookingWeb.Controllers
{
    
    public class PaymentController : BaseController
    {
        private readonly IMethodService _methodService;
        private readonly ISearchInputService _searchInputService;
        private readonly IPriceBaggageService _priceBaggageService;
        private readonly IOrderBookingService _orderBookingService;
        private readonly IOrderFlightService _orderFlightService;
        private readonly IOrderTravellerService _orderTravellerService;
        private readonly IOrderBaggageService _orderBaggageService;
        private readonly IOrderFlightSegmentService _orderFlightSegmentService;
        private readonly IOrderContactInfoService _orderContactInfoService;
        public PaymentController(IMethodService methodServie, ISearchInputService searchInputService,
            IPriceBaggageService priceBaggageService, IOrderBookingService orderBookingService,
            IOrderFlightService orderFlightService, IOrderTravellerService orderTravellerService, 
            IOrderBaggageService orderBaggageService, IOrderFlightSegmentService orderFlightSegmentService,
            IOrderContactInfoService orderContactInfoService)
        {
            _methodService = methodServie;
            _searchInputService = searchInputService;
            _priceBaggageService = priceBaggageService;
            _orderBookingService = orderBookingService;
            _orderFlightService = orderFlightService;
            _orderTravellerService = orderTravellerService;
            _orderBaggageService = orderBaggageService;
            _orderFlightSegmentService = orderFlightSegmentService;
            _orderContactInfoService = orderContactInfoService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateBookingOrder(BookingRequest request)
        {
            var validation = ValidateOrderBookingRequest(request);
            if (validation.IsValid)
            {
                var searchIput = _searchInputService.GetByKeySessionId(request.sessionId);
                int orderCheck = CheckDuplicateBooking(request);
                if (orderCheck == 0)
                {
                    var clientInfo = ClientInfoHelper.GetClientInfo();
                    if (searchIput.IPAddress.Equals(clientInfo.IpAddress))
                    {
                        var groupFlights = new List<GroupFlight>();
                        foreach (var flight in request.flightList)
                        {
                            switch (flight.airCode.ToUpper())
                            {
                                case "VN":
                                case "BL":
                                    var groupFlightVN = _methodService.GetGroupFlightVNARow(request.sessionId, flight.bookingKey, flight.wayType);
                                    groupFlights.Add(groupFlightVN);
                                    break;
                                case "VJ":
                                    var groupFlightVJ = _methodService.GetGroupFlightVJRow(request.sessionId, flight.bookingKey, flight.wayType);
                                    groupFlights.Add(groupFlightVJ);
                                    break;
                                case "QH":
                                    var groupFlightQH = _methodService.GetGroupFlightQHRow(request.sessionId, flight.bookingKey, flight.wayType);
                                    groupFlights.Add(groupFlightQH);
                                    break;
                            }
                        }
                        string userCity = "HN";
                        string userCountry = "VN";
                        var locationFromIp = await GetLocationFromIpAsync(clientInfo.IpAddress);
                        if (!string.IsNullOrEmpty(locationFromIp.CountryCode))
                            userCountry = locationFromIp.CountryCode;
                        if (!string.IsNullOrEmpty(locationFromIp.City))
                            userCity = locationFromIp.City;
                        try
                        {
                            var orderBooking = _orderBookingService.Insert(new OrderBookingMod
                            {
                                StatusAvailable = 1,
                                FinnalStatus = (int)FinnalStatus.NewOrderCreated,
                                UserId = 0,
                                CompanyCode = "VEMAYBAYSO",
                                IsHotel = false,
                                IsFlight = true,
                                FinalPrice = 0,
                                IsRequirePayment = true,
                                PaymentMethodSelected = 0,
                                OrderDate = DateTime.Now,
                                SupplierHotel = 0,
                                SupplierFlight = 0,
                                OrderStep = (int)OrderStep.PendingAcceptance,
                                BookingReference = "0",
                                Note = string.Empty,
                                IsShowFrontEnd = false,
                                IPAddress = clientInfo.IpAddress,
                                Domain = CoreUtils.GetDomainCurrent(),
                                PriceFirstPaid = 0,
                                ExtraFee = 0,
                                ExtraFeeType = 0,
                                PaymentStatusFinnal = 2,
                                UserCity = userCity,
                                UserCountry = userCountry,
                                LastTimeValidBankOrCashOrigin = DateTime.Now,
                                LastTimeValidBankOrCashCurrent = DateTime.Now,
                                LastTimeValidBank = DateTime.Now,
                                LastTimeValidCash = DateTime.Now,
                                IsMobile = CoreUtils.IsMobileDevice(),
                                IsTracking = false,
                                IsBooking = true,
                                UserIdProcess = 0,
                                Active = true,
                                CallPaxStatus = (int)CallPaxStatus.NotYetCalled,
                                CallPaxUser = 0,
                                IsKeepSeat = false,
                                IsDone = false,
                                NeedToBeIssued = false,
                                Issued = false
                            });
                            orderBooking.BookingReference = CoreUtils.GetBookingReference(orderBooking);
                            _orderBookingService.Update(orderBooking);
                            var orderFlight = InsertOrderFlight(orderBooking, searchIput, groupFlights);
                            InsertTraveller(request.pasgerList, orderFlight);
                            InsertFlightSegment(orderFlight, groupFlights);
                            await InsertContactl(orderBooking.OrderId, request.contaclOrder, request.invoiceOrder);
                            UpdatePriceOrder(orderBooking.OrderId, groupFlights);
                            //await BookingOrderFlight(searchIput.SessionId, orderBooking.OrderId);
                            var dataEncrypt = GetEncryptQuery("sessionId=" + searchIput.SessionId + "&orderId=" + orderBooking.OrderId);
                            return Json(new { status = true, dataResult = dataEncrypt }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            return Json(new { status = false, dataResult = ex.Message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { status = false, dataResult = validation.Errors.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var dataEncrypt = GetEncryptQuery("sessionId=" + searchIput.SessionId + "&orderId=" + orderCheck);
                    return Json(new { status = true, dataResult = dataEncrypt }, JsonRequestBehavior.AllowGet);
                }  
            }
            else
            {
                return Json(new { status = false, dataResult = validation.Errors.ToString() }, JsonRequestBehavior.AllowGet);
            }    
        }

        private async Task BookingOrderFlight(int sessionId, int orderId)
        {
            var bookings = await _methodService.FlightBooking(orderId, sessionId, "PL");
            foreach(var book in bookings)
            {
                var segmentMod = _orderFlightSegmentService.GetOrderFlightSegmentById(book.SegmentId);
                segmentMod.PNRNumber = book.PnrNumber;
                segmentMod.Timelimit = book.ExpiryTime.ToString("dd/MM/yyyy HH:mm");
                _orderFlightSegmentService.Update(segmentMod);
            }
        }

        private void UpdatePriceOrder(int OrderId, List<GroupFlight> groupFlights)
        {
            var orderBoking = _orderBookingService.GetOrderBookingByOrderId(OrderId);
            var orderFlight = _orderFlightService.GetOrderFlightByOrderId(OrderId);
            var orderFlightSegment = _orderFlightSegmentService.GetOrderFlightSegmentByOrderId(OrderId);
            var orderTraveller = _orderTravellerService.GetTravellerByOrderId(OrderId);
            var orderBaggage = _orderBaggageService.GetOrderBaggaeByOrderId(OrderId);
            decimal totallPriceAdt = 0, totallPriceChd = 0, totallPriceInf = 0;
            decimal totallPriceAdtIn = 0, totallPriceChdIn = 0, totallPriceInfIn = 0;
            decimal fareBaseAdt = 0, fareBaseChd = 0, fareBaseInf = 0;
            decimal fareBaseAdtIn = 0, fareBaseChdIn = 0, fareBaseInfIn = 0;
            decimal taxAdt = 0, taxChd = 0, taxInf = 0;
            decimal taxAdtIn = 0, taxChdIn = 0, taxInfIn = 0;
            decimal feeAdt = 0, feeChd = 0, feeInf = 0;
            decimal feeAdtIn = 0, feeChdIn = 0, feeInfIn = 0;
            decimal totalAddOnFee = 0, totalAddOnFeeIn = 0, totalBaseAddOnFee = 0, totalBaseAddOnFeeIn =0, totalServiceAddOnFee=0, totalServiceAddOnFeeIn = 0;
            decimal priceBaggageAdt = 0, priceBaggageChd = 0, priceBaggageInf = 0;


            foreach (var gf in groupFlights)
            {
                var priceBrak = gf.PriceBreakDowns[0];
                if(gf.WayType == WayType.OutBound)
                {
                    totallPriceAdt = priceBrak.TotallPriceAdt;
                    totallPriceChd = priceBrak.TotallPriceChd;
                    totallPriceInf = priceBrak.TotallPriceInf;
                    fareBaseAdt = priceBrak.FareBaseAdt;
                    fareBaseChd = priceBrak.FareBaseChd;
                    fareBaseInf = priceBrak.FareBaseInf;
                    taxAdt = priceBrak.TaxAdt;
                    taxChd = priceBrak.TaxChd;
                    taxInf = priceBrak.TaxInf;
                    feeAdt = priceBrak.FeeAdt;
                    feeChd = priceBrak.FeeChd;
                    feeInf = priceBrak.FeeInf;
                }
                else
                {
                    totallPriceAdtIn = priceBrak.TotallPriceAdt;
                    totallPriceChdIn = priceBrak.TotallPriceChd;
                    totallPriceInfIn = priceBrak.TotallPriceInf;
                    fareBaseAdtIn = priceBrak.FareBaseAdt;
                    fareBaseChdIn = priceBrak.FareBaseChd;
                    fareBaseInfIn = priceBrak.FareBaseInf;
                    taxAdtIn = priceBrak.TaxAdt;
                    taxChdIn = priceBrak.TaxChd;
                    taxInfIn = priceBrak.TaxInf;
                    feeAdtIn = priceBrak.FeeAdt;
                    feeChdIn = priceBrak.FeeChd;
                    feeInfIn = priceBrak.FeeInf;
                }
            }
            foreach(var bag in orderBaggage)
            {
                totalAddOnFee += bag.PriceOutBound;
                totalBaseAddOnFee += bag.BasePriceOutBound;
                totalServiceAddOnFee += bag.ServiceOutBound;
                totalAddOnFeeIn += bag.PriceInBound;
                totalBaseAddOnFeeIn += bag.BasePriceInBound;
                totalServiceAddOnFeeIn += bag.ServiceInBound;
                var traveller = _orderTravellerService.GetTravellerByTravellerId(bag.OrderTravellerId);
                if (traveller.TravellerType == (int)TravellerType.Adult)
                    priceBaggageAdt += bag.PriceOutBound + bag.PriceInBound;
                if (traveller.TravellerType == (int)TravellerType.Child)
                    priceBaggageChd += bag.PriceOutBound + bag.PriceInBound;
                if (traveller.TravellerType == (int)TravellerType.Infant)
                    priceBaggageInf += bag.PriceOutBound + bag.PriceInBound;
            }
            decimal finalPrice = (decimal)((totallPriceAdt + totallPriceAdtIn) * orderFlight.Adult + (totallPriceChd + totallPriceChdIn) * orderFlight.Child 
                                            + (totallPriceInf + totallPriceInfIn) * orderFlight.Infant);
            finalPrice += totalAddOnFee + totalAddOnFeeIn;
            orderBoking.FinalPrice = finalPrice;
            orderBoking.PriceFirstPaid = (decimal)((fareBaseAdt + fareBaseAdtIn + taxAdt + taxAdtIn) * orderFlight.Adult +
                                            (fareBaseChd + fareBaseChdIn + taxChd + taxChdIn) * orderFlight.Child +
                                            (fareBaseInf + fareBaseInfIn + taxInf + taxInfIn) * orderFlight.Infant + totalBaseAddOnFee + totalBaseAddOnFeeIn);
            orderBoking.ExtraFee = (feeAdt + feeAdtIn) * orderFlight.Adult + (feeChd + feeChdIn) * orderFlight.Child + (feeInf + feeInfIn) * orderFlight.Infant + 
                                            totalServiceAddOnFee + totalServiceAddOnFeeIn;
            _orderBookingService.Update(orderBoking);
            orderFlight.TotalPrice = finalPrice;
            orderFlight.TotalAdult = (totallPriceAdt + totallPriceAdtIn) * orderFlight.Adult + priceBaggageAdt;
            orderFlight.TotalChild = (totallPriceChd + totallPriceChdIn) * orderFlight.Child + priceBaggageChd;
            orderFlight.TotalInfant = (totallPriceInf + totallPriceInfIn) * orderFlight.Infant + priceBaggageInf;
            orderFlight.TotalBasePrice = (decimal)((fareBaseAdt + fareBaseAdtIn) * orderFlight.Adult + (fareBaseChd + fareBaseChdIn) * orderFlight.Child
                                            + (fareBaseInf + fareBaseInfIn) * orderFlight.Infant);
            orderFlight.TotalTaxAndFee = (decimal)((taxAdt + taxAdtIn) * orderFlight.Adult + (taxChd + taxChdIn) * orderFlight.Child
                                            + (taxInf + taxInfIn) * orderFlight.Infant);
            orderFlight.TotalAddOnFee = totalBaseAddOnFee + totalBaseAddOnFeeIn;
            orderFlight.TotalServiceFee = (decimal)((feeAdt + feeAdtIn) * orderFlight.Adult + (feeChd + feeChdIn) * orderFlight.Child
                                            + (feeInf + feeInfIn) * orderFlight.Infant + totalServiceAddOnFee + totalServiceAddOnFeeIn);
            _orderFlightService.Update(orderFlight);
            foreach(var segment in orderFlightSegment.Where(s=>s.SegmentType != (int)SegmentType.NotApply).ToList())
            {
                if(segment.SegmentType != (int)SegmentType.NotApply)
                {
                    if (segment.GroupIndex == (int)WayType.OutBound)
                    {
                        segment.TotalPrice = (decimal)(totallPriceAdt * orderFlight.Adult + totallPriceChd * orderFlight.Child
                                                + totallPriceInf * orderFlight.Infant);
                        segment.TotalPrice += totalAddOnFee;
                        segment.BasePrice = (fareBaseAdt + taxAdt) * orderFlight.Adult + (fareBaseChd + taxChd) * orderFlight.Child
                                            + (fareBaseInf + taxInf) * orderFlight.Infant + totalBaseAddOnFee;
                        segment.PriceShownOnTicket = segment.TotalPrice;
                    }
                    else
                    {
                        segment.TotalPrice = (decimal)(totallPriceAdtIn * orderFlight.Adult + totallPriceChdIn * orderFlight.Child
                                                + totallPriceInfIn * orderFlight.Infant);
                        segment.TotalPrice += totalAddOnFeeIn;
                        segment.BasePrice = (fareBaseAdtIn + taxAdtIn) * orderFlight.Adult + (fareBaseChdIn + taxChdIn) * orderFlight.Child
                                            + (fareBaseInfIn + taxInfIn) * orderFlight.Infant + totalBaseAddOnFeeIn;
                        segment.PriceShownOnTicket = segment.TotalPrice;
                    }
                    _orderFlightSegmentService.Update(segment);
                }
            }
            foreach(var traveller in orderTraveller)
            {
                var bagPax = _orderBaggageService.GetOrderBaggaeByTravellerId(traveller.Id);
                switch (traveller.TravellerType)
                {
                    case (int)TravellerType.Adult:
                        traveller.PaxGiaCoBan = fareBaseAdt + fareBaseAdtIn;
                        traveller.PaxTaxAndFee = taxAdt + taxAdtIn;
                        traveller.PaxAddOnFee = bagPax.BasePriceOutBound + bagPax.BasePriceInBound;
                        traveller.ServiceFee = bagPax.ServiceOutBound + bagPax.ServiceInBound;
                        traveller.PaxServiceFee = feeAdt + feeAdtIn;
                        traveller.PaxPrice = traveller.PaxGiaCoBan + traveller.PaxTaxAndFee + traveller.PaxAddOnFee + traveller.ServiceFee + traveller.PaxServiceFee;
                        break;
                    case (int)TravellerType.Child:
                        traveller.PaxGiaCoBan = fareBaseChd + fareBaseChdIn;
                        traveller.PaxTaxAndFee = taxChd + taxChdIn;
                        traveller.PaxAddOnFee = bagPax.BasePriceOutBound + bagPax.BasePriceInBound;
                        traveller.ServiceFee = bagPax.ServiceOutBound + bagPax.ServiceInBound;
                        traveller.PaxServiceFee = feeChd + feeChdIn;
                        traveller.PaxPrice = traveller.PaxGiaCoBan + traveller.PaxTaxAndFee + traveller.PaxAddOnFee + traveller.ServiceFee + traveller.PaxServiceFee;
                        break;
                    case (int)TravellerType.Infant:
                        traveller.PaxGiaCoBan = fareBaseInf + fareBaseInfIn;
                        traveller.PaxTaxAndFee = taxInf + taxInfIn;
                        traveller.PaxAddOnFee = bagPax.BasePriceOutBound + bagPax.BasePriceInBound;
                        traveller.ServiceFee = bagPax.ServiceOutBound + bagPax.ServiceInBound;
                        traveller.PaxServiceFee = feeInf + feeInfIn;
                        traveller.PaxPrice = traveller.PaxGiaCoBan + traveller.PaxTaxAndFee + traveller.PaxAddOnFee + traveller.ServiceFee + traveller.PaxServiceFee;
                        break;
                }
                _orderTravellerService.Update(traveller);
            }
        }
        private OrderFlightMod InsertOrderFlight(OrderBookingMod order, SearchInputMod searchInput, List<GroupFlight> groupFlights)
        {
            var groupflightOut = groupFlights.FirstOrDefault(f => f.WayType == WayType.OutBound);
            var groupflightIn = groupFlights.FirstOrDefault(f => f.WayType == WayType.InBound);

            return _orderFlightService.Insert(new OrderFlightMod
            {
                OrderId = (int)order.OrderId,
                OrderCode = order.BookingReference,
                OrderStatus = (int)FinnalStatus.NewOrderCreated,
                OrderDate = DateTime.Now,
                ProcessedBy = (int)OrderStep.PendingAcceptance,
                Triptype = (byte)(searchInput.IsRoundTrip ? (int)TripType.RoundTrip : (int)TripType.OneWay),
                ItineraryDesc = searchInput.DepartureCity + "-" + searchInput.ArrivalCity + (searchInput.IsRoundTrip ? searchInput.DepartureCity : string.Empty),
                PaymentMethod = (int)PaymentMethod.AtOffice,
                TotalAdult = 0,
                TotalChild = 0,
                TotalInfant = 0,
                TotalBasePrice = 0,
                TotalTaxAndFee = 0,
                TotalAddOnFee = 0,
                TotalServiceFee = 0,
                Commission = 0,
                TotalPrice = 0,
                Balance = 0,
                PriceNote = string.Empty,
                OptionGetBetterFare = 0,
                TotalDiscount = 0,
                SessionId = searchInput.SessionId,
                Supplier = (byte)(searchInput.IsSearchDomestic? Supplier.Domestic : Supplier.International),
                IsRoundTrip = searchInput.IsRoundTrip,
                DepartureAirport = searchInput.DepartureAirport,
                ArrivalAirport = searchInput.ArrivalAirport,
                DepartureDate = searchInput.DepartureDate,
                ReturnDate = searchInput.IsRoundTrip ? searchInput.ReturnDate : null,
                DepartureCity = searchInput.DepartureCity,
                ArrivalCity = searchInput.ArrivalCity,
                OutBoundRefNumber = groupflightOut.MainFlightNumber,
                InBoundRefNumber = searchInput.IsRoundTrip ? groupflightIn.MainFlightNumber : string.Empty,
                Adult = searchInput.AdultNumber,
                Child = searchInput.ChildNumber,
                Infant = searchInput.InfantNumber,
                TicketClass = groupflightOut.PriceBreakDowns[0].ClassName + (searchInput.IsRoundTrip ? "-" + groupflightIn.PriceBreakDowns[0].ClassName : string.Empty),
                AirlineCode = groupflightOut.MainAirlineCode,
                AirlineCodeInBound = searchInput.IsRoundTrip ? groupflightIn.MainAirlineCode : string.Empty,
            });
        }
        public void InsertFlightSegment(OrderFlightMod flightMod, List<GroupFlight> groupFlights)
        {
            foreach(var groupfight in groupFlights)
            {
                foreach(var gf in groupfight.ListSegment)
                {
                    var orderFlightSegment = _orderFlightSegmentService.Insert(new OrderFlightSegmentMod
                    {
                        OrderId = flightMod.OrderId,
                        OrderFlightId = flightMod.Id,
                        AirlineCode = gf.AirlineCode,
                        FlightNumber = Convert.ToInt32(gf.FlightNumber),
                        TicketClass = gf.TicketClass,
                        TicketClassName = gf.TicketClass,
                        DepartureAirport = gf.DepartureAirportCode,
                        ArrivalAirport = gf.ArrivalAirportCode,
                        DepartureDate = gf.DepartureDate,
                        DepartureTime = TimeSpan.Parse(gf.DepartureTime),
                        ArrivalDate = gf.ArrivalDate,
                        ArrivalTime = TimeSpan.Parse(gf.ArrivalTime),
                        SegmentPrice = 0,
                        PNRStatus = 0,
                        SegmentType = (byte)gf.SegmentType,
                        GroupIndex = (int)groupfight.WayType,
                        BasePrice = 0,
                        FareRule = groupfight.BookingKey,
                        TotalPrice = 0,
                        PriceShownOnTicket = 0,
                        Active = true,
                        UserIdRecheck = 0,
                        ARC_BookStatus = 0,
                    });
                }
            }   
        }
        private void InsertTraveller(List<PassengerInfo> pasgerList, OrderFlightMod flightMod)
        {
            foreach (var pass in pasgerList)
            {
                string fullName = pass.namePax;
                int firstIndexOfSpace = fullName.IndexOf(" ", System.StringComparison.Ordinal);
                string firstName = string.Empty;
                string lastName = string.Empty;
                if (firstIndexOfSpace > 0)
                {
                    lastName = fullName.Substring(0, firstIndexOfSpace);
                    firstName = fullName.Substring(firstIndexOfSpace);
                }
                else
                    lastName = fullName;
                var passMod = _orderTravellerService.Insert(new OrderTravellerMod
                {
                    SessionId = flightMod.SessionId,
                    UserId = 0,
                    UserRole = 0,
                    OrderId = flightMod.OrderId,
                    OrderFlightId = flightMod.Id,
                    TravellerType = pass.typePax.Equals("inf") ? (byte)TravellerType.Infant : pass.typePax.Equals("chd") ? (byte)TravellerType.Child : (byte)TravellerType.Adult,
                    Gender = pass.genderPax,
                    FirstName = firstName,
                    MiddleName = string.Empty,
                    LastName = lastName,
                    DateOfBirth = Convert.ToDateTime(pass.birthPax),
                    MilesCardNumber = pass.cardNum,
                    AirlineCode = flightMod.AirlineCode + "-" + flightMod.AirlineCodeInBound,
                    PaxGiaCoBan = 0,
                    PaxTaxAndFee = 0,
                    PaxAddOnFee = 0,
                    PaxServiceFee =0,
                    PaxPrice = 0,
                    PaxNote = pass.memberNum,
                    TaxAndFeeExcludeDiscount = 0,
                    ServiceFee = 0,
                    Discount = 0,
                    Active = true,
                    EticketNumberOutBound = string.Empty,
                    EticketNumberInBound = string.Empty,
                    FullName = fullName,
                    PNROutBound = string.Empty,
                    PNRInBound = string.Empty
                });
                InsertBaggage(passMod.Id, (int)flightMod.OrderId, flightMod.AirlineCode, flightMod.AirlineCodeInBound, pass);

            }    
        }
        private void InsertBaggage(int passId, int orderId, string airlineOut, string airlineIn,PassengerInfo passengerInfo)
        {
            PriceBaggageMod bagOut = new PriceBaggageMod();
            PriceBaggageMod bagIn = new PriceBaggageMod();
            bagOut = _priceBaggageService.GetBaggageById(passengerInfo.bagIdOut);
            if(passengerInfo.bagIdIn> 0)
            {
                bagIn = _priceBaggageService.GetBaggageById(passengerInfo.bagIdIn);
            }
            else
            {
                bagIn = null;
            }
            var orderBaggage = _orderBaggageService.Insert(new OrderBaggageMod
            {
                OrderId = orderId,
                OrderTravellerId = passId,
                AirlineOutBound = airlineOut,
                PriceOutBound = bagOut != null ? bagOut.Price + bagOut.ServiceFee : 0,
                BasePriceOutBound = bagOut != null ? bagOut.Price : 0,
                ServiceOutBound = bagOut != null ? bagOut.ServiceFee : 0,
                KgOutBound = bagOut != null ? bagOut.TotalKg : 0,
                AirlineInBound = airlineIn,
                PriceInBound = bagIn != null ? bagIn.Price + bagIn.ServiceFee : 0,
                BasePriceInBound = bagIn != null ? bagIn.Price : 0,
                ServiceInBound = bagIn != null ? bagIn.ServiceFee : 0,
                KgInBound = bagIn != null ? bagIn.TotalKg : 0
            });
        }
        private async Task InsertContactl(int orderId, ContactOrder contactl, InvoiceOrder invoice)
        {
            var clientInfo = ClientInfoHelper.GetClientInfo();
            string fullName = contactl.nameContactl;
            int firstIndexOfSpace = fullName.IndexOf(" ", System.StringComparison.Ordinal);
            string firstName = string.Empty;
            string lastName = string.Empty;
            string userCity = "HN";
            string userCountry = "VN";
            var locationFromIp = await GetLocationFromIpAsync(clientInfo.IpAddress);
            if (!string.IsNullOrEmpty(locationFromIp.CountryCode))
                userCountry = locationFromIp.CountryCode;
            if (!string.IsNullOrEmpty(locationFromIp.City))
                userCity = locationFromIp.City;

            if (firstIndexOfSpace > 0)
            {
                lastName = fullName.Substring(0, firstIndexOfSpace);
                firstName = fullName.Substring(firstIndexOfSpace);
            }
            else
                lastName = fullName;
            string emailInvoice = string.Empty;
            if(string.IsNullOrEmpty(invoice.emailInvoice))
                emailInvoice = contactl.emailContactl;
            else
                emailInvoice = invoice.emailInvoice;
            var contactlOrder = _orderContactInfoService.Insert(new OrderContactlInfoMod
            {
                OrderId = orderId,
                Title = contactl.titleContactl,
                FirstName = firstName.ToUpper(),
                LastName = lastName.ToUpper(),
                Street = contactl.addContactl,
                City = userCity,
                CountryCode = userCountry,
                PhoneNumber = contactl.phoneContactl,
                MobilePhone = contactl.phoneContactl,
                Email = contactl.emailContactl,
                IsInvoice = invoice.isInvoice,
                NameInvoice = invoice.nameInvoice,
                ProfessionInvoice = string.Empty,
                VATInvoice = string.Empty,
                AddressInvoice = invoice.addInvoice,
                IsReciveInformation = false,
                FullName = fullName.ToUpper(),
                AddressReciveInvoice = emailInvoice
            });
            var orderBooking = _orderBookingService.GetOrderBookingByOrderId(orderId);
            orderBooking.Note = contactl.otherRequirementsContactl;
            _orderBookingService.Update(orderBooking);
        }
        public ActionResult PaymentMethodOrder(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    ViewBag.ErrorMessage = "Truy cập không hợp lệ";
                    return View("Error");
                }
                var decrypted = UrlEncryptHelper.Decrypt(data);
                var values = HttpUtility.ParseQueryString(decrypted);
                int sessionId = Convert.ToInt32(values["sessionId"]);
                int orderId = Convert.ToInt32(values["orderId"]);
                var searchIput = _searchInputService.GetByKeySessionId(sessionId);
                if (searchIput.IPAddress.Equals(clientInfo.IpAddress))
                {

                }
                else
                {
                    ViewBag.ErrorMessage = "Truy cập không hợp lệ";
                    return View("Error");
                }

            }
            catch
            {
                ViewBag.ErrorMessage = "Truy cập không hợp lệ";
                return View("Error");
            }
            return View();
        }
        private ValidationResult ValidateOrderBookingRequest(BookingRequest request)
        {
            ValidationResult result = new ValidationResult();
            if (request == null)
            {
                result.Errors.Add("Request không được null.");
                result.IsValid = false;
                return result;
            }
            Regex dangerousPattern = new Regex(@"[;'\-#<>""\[\]\(\)=]|(drop|delete|insert|update|exec|union|select)", RegexOptions.IgnoreCase);

            if (request.sessionId <= 0)
                result.Errors.Add("SessionId không hợp lệ.");
            if (request.flightList == null || !request.flightList.Any())
            {
                result.Errors.Add("Danh sách chuyến bay (flightList) không được rỗng.");
            }
            else
            {
                if (request.flightList.Count > 2)
                    result.Errors.Add("Số lượng chuyến bay không được vượt quá 2 (chỉ cho phép chiều đi và chiều về).");

                foreach (var f in request.flightList)
                {
                    if (string.IsNullOrWhiteSpace(f.bookingKey))
                        result.Errors.Add("FlightInfo.bookingKey không được để trống.");
                    if (string.IsNullOrWhiteSpace(f.airCode))
                        result.Errors.Add("FlightInfo.airCode không được để trống.");
                    if (f.wayType < 0 || f.wayType > 2)
                        result.Errors.Add("FlightInfo.wayType phải là 0 (chiều đi), 1 (chiều về), hoặc 2 (khứ hồi).");
                }
            }
            if (request.pasgerList == null || !request.pasgerList.Any())
            {
                result.Errors.Add("Danh sách hành khách (pasgerList) không được rỗng.");
            }
            else
            {
                if (request.pasgerList.Count > 9)
                    result.Errors.Add("Không được phép đặt quá 9 hành khách trong 1 đơn.");

                var validType = new[] { "ADT", "CHD", "INF" };

                foreach (var p in request.pasgerList)
                {
                    if (string.IsNullOrWhiteSpace(p.namePax))
                        result.Errors.Add("Tên hành khách không được để trống.");
                    else if (p.namePax.Length > 100)
                        result.Errors.Add("Tên hành khách không được vượt quá 100 ký tự.");
                    else if (dangerousPattern.IsMatch(p.namePax))
                        result.Errors.Add("Tên hành khách chứa ký tự không hợp lệ.");

                    if (p.genderPax != 0 && p.genderPax != 1)
                        result.Errors.Add($"Giới tính của {p.namePax} phải là 0 (Nam) hoặc 1 (Nữ).");

                    if (string.IsNullOrWhiteSpace(p.typePax) || !validType.Contains(p.typePax.ToUpper()))
                        result.Errors.Add($"Loại hành khách của {p.namePax} phải là ADT (người lớn), CHD (trẻ em), hoặc INF (em bé).");

                    if (!string.IsNullOrWhiteSpace(p.birthPax) && !DateTime.TryParse(p.birthPax, out _))
                        result.Errors.Add($"Ngày sinh của {p.namePax} không hợp lệ.");

                    if (!string.IsNullOrWhiteSpace(p.cardNum) && dangerousPattern.IsMatch(p.cardNum))
                        result.Errors.Add($"Số giấy tờ của {p.namePax} chứa ký tự không hợp lệ.");
                }
            }
            if (request.contaclOrder == null)
            {
                result.Errors.Add("Thông tin người liên hệ (contaclOrder) không được null.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.contaclOrder.nameContactl))
                    result.Errors.Add("Tên người liên hệ không được để trống.");

                if (string.IsNullOrWhiteSpace(request.contaclOrder.phoneContactl))
                    result.Errors.Add("Số điện thoại liên hệ không được để trống.");
                else if (!Regex.IsMatch(request.contaclOrder.phoneContactl, @"^(0|\+84)[0-9]{9,10}$"))
                    result.Errors.Add("Số điện thoại liên hệ không hợp lệ.");

                if (!string.IsNullOrWhiteSpace(request.contaclOrder.emailContactl) &&
                    !Regex.IsMatch(request.contaclOrder.emailContactl, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    result.Errors.Add("Email liên hệ không hợp lệ.");
            }
            if (request.invoiceOrder != null && request.invoiceOrder.isInvoice)
            {
                if (string.IsNullOrWhiteSpace(request.invoiceOrder.nameInvoice))
                    result.Errors.Add("Tên hóa đơn không được để trống khi isInvoice = true.");

                if (string.IsNullOrWhiteSpace(request.invoiceOrder.taxCodeInvoice))
                    result.Errors.Add("Mã số thuế không được để trống khi isInvoice = true.");

                if (!string.IsNullOrWhiteSpace(request.invoiceOrder.emailInvoice) &&
                    !Regex.IsMatch(request.invoiceOrder.emailInvoice, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    result.Errors.Add("Email hóa đơn không hợp lệ.");
            }
            result.IsValid = !result.Errors.Any();
            return result;
        }
        private int CheckDuplicateBooking(BookingRequest request)
        {
            var result = 0;
            var orderFilght = _orderFlightService.GetOrderFlightBySessionId(request.sessionId);
            if (orderFilght == null || orderFilght.Id == 0 || orderFilght.OrderId == 0)
                return 0;
            else
            {
                var orderFlightSegment = _orderFlightSegmentService.GetOrderFlightSegmentByOrderId((int)orderFilght.OrderId);
                if(orderFlightSegment == null)
                {
                    return 0;
                }
                else {
                    foreach(var segment in orderFlightSegment)
                    {
                        var checksegment = request.flightList.Where(s => s.bookingKey == segment.FareRule);
                        if (checksegment == null)
                        {
                            orderFilght.OrderStatus = (byte)OrderStatus.Remove;
                            _orderFlightService.Update(orderFilght);
                            return 0;
                        }
                        else
                            result = (int)orderFilght.OrderId;
                    }
                }
            }
            var orderTraveller = _orderTravellerService.GetTravellerByOrderId((int)orderFilght.OrderId);
            if (orderTraveller == null)
                return 0;
            else
            {
                foreach(var travell in orderTraveller)
                {
                    var bag = _orderBaggageService.GetOrderBaggaeByTravellerId(travell.Id);
                    var pass = new PassengerInfo
                    {
                        genderPax = (byte)travell.Gender,
                        namePax = travell.FullName,
                        cardNum = travell.MilesCardNumber,
                        birthPax = travell.DateOfBirth?.ToString("dd/MM/yyyy"),
                        typePax = travell.TravellerType == (byte)TravellerType.Infant ? "inf" : travell.TravellerType == (byte)TravellerType.Child ? "chd" : "adt",
                        totalKgOut = bag.KgOutBound.ToString(),
                        totalKgIn = bag.KgInBound.ToString()
                    };
                    if (IsPassengerExists(request.pasgerList, pass))
                        result = (int)orderFilght.OrderId;
                    else
                    {
                        orderFilght.OrderStatus = (byte)OrderStatus.Remove;
                        _orderFlightService.Update(orderFilght);
                        return 0;
                    }
                        
                }
            }
            var contactlup = _orderContactInfoService.GetOrderContactlInfoByOrderId((int)orderFilght.OrderId);
            var contactl = request.contaclOrder;
            var invoice = request.invoiceOrder;
            string fullName = contactl.nameContactl;
            int firstIndexOfSpace = fullName.IndexOf(" ", System.StringComparison.Ordinal);
            string firstName = string.Empty;
            string lastName = string.Empty;
            if (firstIndexOfSpace > 0)
            {
                lastName = fullName.Substring(0, firstIndexOfSpace);
                firstName = fullName.Substring(firstIndexOfSpace);
            }
            else
                lastName = fullName;
            contactlup.Title = contactl.titleContactl;
            contactlup.FirstName = firstName.ToUpper();
            contactlup.LastName = lastName.ToUpper();
            contactlup.Street = contactl.addContactl;
            contactlup.PhoneNumber = contactl.phoneContactl;
            contactlup.MobilePhone = contactl.phoneContactl;
            contactlup.Email = contactl.emailContactl;
            contactlup.IsInvoice = invoice.isInvoice;
            contactlup.NameInvoice = invoice.nameInvoice;
            contactlup.ProfessionInvoice = string.Empty;
            contactlup.AddressInvoice = invoice.addInvoice;
            contactlup.FullName = fullName.ToUpper();
            contactlup.AddressReciveInvoice = invoice.emailInvoice;
            _orderContactInfoService.Update(contactlup);
            return result;
        }
        public bool IsPassengerExists(List<PassengerInfo> passengers, PassengerInfo item)
        {
            if (passengers == null || item == null)
                return false;

            return passengers.Any(p =>
                p.genderPax == item.genderPax &&
                string.Equals(p.namePax, item.namePax, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.cardNum, item.cardNum, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.birthPax, item.birthPax, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.typePax, item.typePax, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.totalKgOut, item.totalKgOut, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.totalKgIn, item.totalKgIn, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}