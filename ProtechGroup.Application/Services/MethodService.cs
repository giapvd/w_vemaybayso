using Newtonsoft.Json;
using ProtechGroup.Application.Common;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ProtechGroup.Application.Services
{
   
    public class MethodService: IMethodService
    {
        private readonly ISearchInputService _searchInputService;
        private readonly IServiceFeeService _serviceFeeService;
        private readonly IBamBooAirWaysMethod _bamBooAirWaysMethod;
        private readonly IBambooAirwaysService _bamBooAirWaysService;
        private readonly IVietJetsMethod _vietJetsMethod;
        private readonly IVietJetsService _vietJetsService;
        private readonly IVietNamAirLinesMethod _vietNamAirLinesMethod;
        private readonly IVietNamAirLinesService _vietNamAirLinesService;
        private readonly IOrderFlightService _orderFlightService;
        private readonly IOrderContactInfoService _orderContactInfo;
        private readonly IOrderFlightSegmentService _orderFlightSegmentService;
        private readonly IOrderTravellerService _orderTravellerService;
        private readonly IOrderBaggageService _orderBaggageService;
        public MethodService(ISearchInputService searchInputService,
                             IServiceFeeService serviceFeeService,
                             IBamBooAirWaysMethod bamBooAirWaysMethod, 
                             IBambooAirwaysService bamBooAirWaysService,
                             IVietJetsMethod vietJetsMethod,
                             IVietJetsService vietJetsService,
                             IVietNamAirLinesService vietNamAirLinesService,
                             IVietNamAirLinesMethod vietNamAirLinesMethod,
                             IOrderFlightService orderFlightService,
                             IOrderContactInfoService orderContactInfo,
                             IOrderFlightSegmentService orderFlightSegmentService,
                             IOrderTravellerService orderTravellerService,
                             IOrderBaggageService orderBaggageService
                             )
        {
            _searchInputService = searchInputService;
            _bamBooAirWaysMethod = bamBooAirWaysMethod;
            _bamBooAirWaysService = bamBooAirWaysService;
            _vietJetsMethod = vietJetsMethod;
            _vietJetsService = vietJetsService;
            _vietNamAirLinesMethod = vietNamAirLinesMethod;
            _vietNamAirLinesService = vietNamAirLinesService;
            _serviceFeeService = serviceFeeService;
            _orderFlightService = orderFlightService;
            _orderContactInfo = orderContactInfo;
            _orderFlightSegmentService = orderFlightSegmentService;
            _orderTravellerService = orderTravellerService;
            _orderBaggageService = orderBaggageService;
        }
        public GroupFlight GetGroupFlightVNARow(int sessionId, string bookingKey, int wayType)
        {
            var searchInput = _searchInputService.GetByKeySessionId(sessionId);
            if (!CoreUtils.IsFileEmptyOrDoesntExist(CoreUtils.GetVietNamAirLinesResponseFilePath(sessionId)))
            {
                var rootVNA = JsonConvert.DeserializeObject<RootVNA>(CoreUtils.GetContentFromFile(CoreUtils.GetVietNamAirLinesResponseFilePath(sessionId)));
                int airlineOptionId = Convert.ToInt32(bookingKey.Split('_')[1]);
                int flightOptionId = Convert.ToInt32(bookingKey.Split('_')[2]);
                int fareOptionId = Convert.ToInt32(bookingKey.Split('_')[3]);
                ListAirOptionVNA airLineVN = new ListAirOptionVNA();
                if (wayType == (int)WayType.OutBound)
                    airLineVN = rootVNA.ListGroup[0].ListAirOption.FirstOrDefault(f => f.OptionId == airlineOptionId);
                else
                    airLineVN = rootVNA.ListGroup[1].ListAirOption.FirstOrDefault(f => f.OptionId == airlineOptionId);
                var result = _vietNamAirLinesMethod.GetGroupFlightVietNamAirLines(airLineVN, wayType, rootVNA.Session, searchInput.IsSearchDomestic, searchInput.TotalPax);
                var priceBreakDown = result.PriceBreakDowns.FirstOrDefault(f => f.BookingKey == bookingKey);
                result.PriceBreakDowns = new List<PriceBreakDownFlight>();
                result.PriceBreakDowns.Add(priceBreakDown);
                return result;
            }
            else
                return null;
        }
        public GroupFlight GetGroupFlightVJRow(int sessionId, string bookingKey, int wayType)
        {
            var searchInput = _searchInputService.GetByKeySessionId(sessionId);
            if (!CoreUtils.IsFileEmptyOrDoesntExist(CoreUtils.GetVietJetResponseFilePath(sessionId)))
            {
                var rootVJ = JsonConvert.DeserializeObject<RootVietJets[]>(CoreUtils.GetContentFromFile(CoreUtils.GetVietJetResponseFilePath(sessionId)));
                string keyFlight = bookingKey.Split('_')[0];
                var airlineVj = rootVJ.ToList().FirstOrDefault(f => f.key == keyFlight);
                var result = _vietJetsMethod.GetGroupFlightVietJets(airlineVj, 0, wayType, searchInput.TotalPax, searchInput.IsSearchDomestic);
                var priceBreakDown = result.PriceBreakDowns.FirstOrDefault(p => p.BookingKey == bookingKey);
                result.PriceBreakDowns = new List<PriceBreakDownFlight>();
                result.PriceBreakDowns.Add(priceBreakDown);
                return result;
            }
            else
                return null;
        }
        public GroupFlight GetGroupFlightQHRow(int sessionId, string bookingKey, int wayType)
        {
            var searchInput = _searchInputService.GetByKeySessionId(sessionId);
            if (!CoreUtils.IsFileEmptyOrDoesntExist(CoreUtils.GetBamBooResponseFilePath(sessionId)))
            {
                var rootQH = JsonConvert.DeserializeObject<RootBamBoo>(CoreUtils.GetContentFromFile(CoreUtils.GetBamBooResponseFilePath(sessionId)));
                int flightSegmentGroupId = Convert.ToInt32(bookingKey.Split('_')[0]);
                string fareClass = bookingKey.Split('_')[1];
                string fareBasis = bookingKey.Split('_')[2];
                string searchId = bookingKey.Split('_')[3];
                var airlineQH = new TripInfoBamBoo();
                if (wayType == (int)WayType.OutBound)
                    airlineQH = rootQH.data[0].trip_info.FirstOrDefault(f => f.flight_segment_group_id == flightSegmentGroupId);
                else
                    airlineQH = rootQH.data[1].trip_info.FirstOrDefault(f => f.flight_segment_group_id == flightSegmentGroupId);
                var result = _bamBooAirWaysMethod.GetGroupFlightBamBoo(airlineQH, wayType, searchInput.TotalPax,
                                                                    searchInput.IsSearchDomestic, searchId);
                var priceBreakDown = result.PriceBreakDowns.FirstOrDefault(p => p.BookingKey == bookingKey);
                result.PriceBreakDowns = new List<PriceBreakDownFlight>();
                result.PriceBreakDowns.Add(priceBreakDown);
                return result;
            }
            else
                return null;
        }
        public async Task<FlightResultOutput> GetFlightDomestic(int sessionId)
        {
            var result = new FlightResultOutput();
            RootBamBoo alinesBB = new RootBamBoo();
            RootVietJets[] alineVJ = new RootVietJets[100];
            RootVNA alinesVN = new RootVNA();
            var searchInput = _searchInputService.GetByKeySessionId(sessionId);
            if (CoreUtils.IsFileEmptyOrDoesntExist(CoreUtils.GetBamBooResponseFilePath(sessionId)))
            {
                try
                {
                    string bodypost = _bamBooAirWaysMethod.GetBodyAirAvailability(searchInput);
                    alinesBB = await _bamBooAirWaysService.GetAlinesBamBoo(bodypost);
                    CoreUtils.WriteToFile(CoreUtils.GetBamBooResponseFilePath(sessionId), JsonConvert.SerializeObject(alinesBB));
                }
                catch(Exception ex)
                {
                    alinesBB = new RootBamBoo();
                    CoreUtils.WriteToFile(CoreUtils.GetBamBooResponseFilePath(sessionId), ex.Message);
                }
            }
            else
            {
                try
                {
                    alinesBB = JsonConvert.DeserializeObject<RootBamBoo>(CoreUtils.GetContentFromFile(CoreUtils.GetBamBooResponseFilePath(sessionId)));
                }
                catch
                {
                    alinesBB = new RootBamBoo();
                }
            }
            if (CoreUtils.IsFileEmptyOrDoesntExist(CoreUtils.GetVietJetResponseFilePath(sessionId)))
            {
                try
                {
                    string strRequest = _vietJetsMethod.GetStringRequestAlinesVietJet(searchInput);
                    alineVJ = await _vietJetsService.GetAlinesVietJets(strRequest);
                    CoreUtils.WriteToFile(CoreUtils.GetVietJetResponseFilePath(sessionId), JsonConvert.SerializeObject(alineVJ));
                }
                catch (Exception ex)
                {
                    alineVJ = new RootVietJets[100];
                    CoreUtils.WriteToFile(CoreUtils.GetVietJetResponseFilePath(sessionId), ex.Message);
                }
            }
            else
            {
                try
                {
                    alineVJ = JsonConvert.DeserializeObject<RootVietJets[]>(CoreUtils.GetContentFromFile(CoreUtils.GetVietJetResponseFilePath(sessionId)));
                }
                catch {
                    alineVJ = new RootVietJets[100];
                }
            }
            if (CoreUtils.IsFileEmptyOrDoesntExist(CoreUtils.GetVietNamAirLinesResponseFilePath(sessionId)))
            {
                try
                {
                    string bodyPost = _vietNamAirLinesMethod.GetBodyPostSearchFlightVietNamAirLine(searchInput);
                    alinesVN = await _vietNamAirLinesService.SearchFlightVietNamAirLines(bodyPost);
                    CoreUtils.WriteToFile(CoreUtils.GetVietNamAirLinesResponseFilePath(sessionId), JsonConvert.SerializeObject(alinesVN));
                }
                catch (Exception ex)
                {
                    alinesVN = new RootVNA();
                    CoreUtils.WriteToFile(CoreUtils.GetVietNamAirLinesResponseFilePath(sessionId), ex.Message);
                }
            }
            else
            {
                try
                {
                    alinesVN = JsonConvert.DeserializeObject<RootVNA>(CoreUtils.GetContentFromFile(CoreUtils.GetVietNamAirLinesResponseFilePath(sessionId)));
                }
                catch
                {
                    alinesVN = new RootVNA();
                }
            }
            var flightResultBB = _bamBooAirWaysMethod.BuildFlightResultBamBoo(alinesBB, searchInput.TotalPax, searchInput.IsSearchDomestic);
            var flightResultVJ = _vietJetsMethod.BuildFlightResultVietJets(alineVJ, searchInput.TotalPax, searchInput.IsSearchDomestic);
            var flightResultVN = _vietNamAirLinesMethod.BuildFlightResultVietNamAirLines(alinesVN, searchInput.TotalPax, searchInput.IsSearchDomestic);
            result.BlockItems = new List<BlockItem>();
            result.Airlines = new List<FlightResultOutput.Airline>();
            result.BlockItems.Add(new BlockItem());
            result.BlockItems[0].FlightOutBounds = new List<GroupFlight>();
            result.BlockItems[0].FlightInBounds = new List<GroupFlight>();
            if (flightResultBB != null && flightResultBB.BlockItems.Count > 0)
            {
                var airline = new FlightResultOutput.Airline();
                airline.AirlineName = "BambooAirWays";
                airline.AirlineCode = "QH";
                result.Airlines.Add(airline);
                flightResultBB.BlockItems[0].FlightOutBounds.ForEach(f => {
                    result.BlockItems[0].FlightOutBounds.Add(f);
                });
                if (flightResultBB.BlockItems[0].FlightInBounds !=null && flightResultBB.BlockItems[0].FlightInBounds.Count > 0)
                {
                    flightResultBB.BlockItems[0].FlightInBounds.ForEach(f => {
                        result.BlockItems[0].FlightInBounds.Add(f);
                    });
                }
            }
            if(flightResultVJ != null && flightResultVJ.BlockItems.Count > 0)
            {
                var airline = new FlightResultOutput.Airline();
                airline.AirlineName = "Vietjet Air";
                airline.AirlineCode = "VJ";
                result.Airlines.Add(airline);
                flightResultVJ.BlockItems[0].FlightOutBounds.ForEach(f => {
                    result.BlockItems[0].FlightOutBounds.Add(f);
                });
                if (flightResultVJ.BlockItems[0].FlightInBounds != null && flightResultVJ.BlockItems[0].FlightInBounds.Count > 0)
                {
                    flightResultVJ.BlockItems[0].FlightInBounds.ForEach(f => {
                        result.BlockItems[0].FlightInBounds.Add(f);
                    });
                }
            }
            if(flightResultVN != null && flightResultVN.BlockItems.Count > 0)
            {
                var airline = new FlightResultOutput.Airline();
                airline.AirlineName = "VietNam AirLines";
                airline.AirlineCode = "VN";
                result.Airlines.Add(airline);
                flightResultVN.BlockItems[0].FlightOutBounds.ForEach(f => {
                    result.BlockItems[0].FlightOutBounds.Add(f);
                });
                if (flightResultVN.BlockItems[0].FlightInBounds != null && flightResultVN.BlockItems[0].FlightInBounds.Count > 0)
                {
                    flightResultVN.BlockItems[0].FlightInBounds.ForEach(f => {
                        result.BlockItems[0].FlightInBounds.Add(f);
                    });
                }
            }
            return result;
        }
        public async Task<List<BookingInfoFlight>> FlightBooking(int orderId, int sessionId, string payMethod)
        {
            var result = new List<BookingInfoFlight>();
            var orderFlightSegment = _orderFlightSegmentService.GetOrderFlightSegmentByOrderId(orderId).ToList();
            var segmentVJ = new List<OrderFlightSegmentMod>();
            var segmentQH = new List<OrderFlightSegmentMod>();
            var segmentVN = new List<OrderFlightSegmentMod>();
            foreach (var segment in orderFlightSegment.Where(s => s.SegmentType != (int)SegmentType.NotApply).ToList())
            {
                if (segment.AirlineCode.Equals("VJ"))
                    segmentVJ.Add(segment);
                if(segment.AirlineCode.Equals("QH"))
                    segmentQH.Add(segment);
                if (segment.AirlineCode.Equals("VN"))
                    segmentVN.Add(segment);
            }
            if(segmentVJ.Count > 0)
            {
                var BookingVj = await GetBookingVietJet(orderId, sessionId, segmentVJ, payMethod);
                foreach(var segment in segmentVJ)
                {
                    var bookMod = new BookingInfoFlight
                    {
                        PnrNumber = BookingVj.locator,
                        ExpiryTime = Convert.ToDateTime(BookingVj.bookingInformation.hold.expiryTime),
                        PaymentToAirline = Convert.ToDecimal(BookingVj.paymentTransactions[0].currencyAmounts[0].totalAmount),
                        AirlineCode = segment.AirlineCode,
                        SegmentId = segment.Id
                    };
                    result.Add(bookMod);
                }
            }
            return result;
        }
        public async Task<RootBookingVietJet> GetBookingVietJet(int orderId, int sessionId, List<OrderFlightSegmentMod> orderFlightSegment, string payMethod)
        {
            var searchInput = _searchInputService.GetByKeySessionId(sessionId);
            var orderContactl = _orderContactInfo.GetOrderContactlInfoByOrderId(orderId);
            var orderFlight = _orderFlightService.GetOrderFlightByOrderId(orderId);
            var orderTraveller = _orderTravellerService.GetTravellerByOrderId(orderId).ToList();
            var orderBaggage = _orderBaggageService.GetOrderBaggaeByOrderId(orderId).ToList();
            string bodyPost = await _vietJetsMethod.GetBodyPostBookingVietJet(searchInput, orderContactl, orderFlight,
                              orderFlightSegment, orderTraveller, orderBaggage, payMethod);
            //return await _vietJetsService.GetBookingVietJet(bodyPost);
            return new RootBookingVietJet();
        }
    }
}
