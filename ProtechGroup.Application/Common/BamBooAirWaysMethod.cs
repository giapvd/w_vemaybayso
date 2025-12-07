using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProtechGroup.Application.Common
{
    public class BamBooAirWaysMethod : IBamBooAirWaysMethod
    {
        private readonly IAirportService _iairportService;
        private readonly IServiceFeeService _iserviceFeeService;
        public BamBooAirWaysMethod(IAirportService iairportService, IServiceFeeService iserviceFeeService)
        {
            _iairportService = iairportService;
            _iserviceFeeService = iserviceFeeService;
        }
        public FlightResultOutput BuildFlightResultBamBoo(RootBamBoo rootBamBoo, int countPax, bool isDomestric)
        {
            try
            {
                var result = new FlightResultOutput();
                var flightResultOutput = new FlightResultOutput();
                flightResultOutput.IsFlightDomestic = true;
                flightResultOutput.BlockItems = new List<BlockItem>();
                flightResultOutput.Airlines = new List<FlightResultOutput.Airline>();
                var airline = new FlightResultOutput.Airline();
                airline.AirlineName = "BamBooAirways";
                airline.AirlineCode = "QH";
                flightResultOutput.Airlines.Add(airline);
                BlockItem blockItem = new BlockItem();
                blockItem.FlightOutBounds = new List<GroupFlight>();
                blockItem.FlightInBounds = new List<GroupFlight>();
                if (rootBamBoo != null && rootBamBoo.data !=null && rootBamBoo.data.Count > 0)
                {
                    for (int i = 0; i < rootBamBoo.data[0].trip_info.Count; i++)
                    {
                        var gf = GetGroupFlightBamBoo(rootBamBoo.data[0].trip_info[i], 0, countPax, isDomestric, rootBamBoo.id);
                        if (gf != null)
                            blockItem.FlightOutBounds.Add(gf);
                    }
                    if (rootBamBoo.data.Count > 1)
                    {
                        for (int i = 0; i < rootBamBoo.data[1].trip_info.Count; i++)
                        {
                            var gf = GetGroupFlightBamBoo(rootBamBoo.data[1].trip_info[i], 1, countPax, isDomestric, rootBamBoo.id);
                            if (gf != null)
                                blockItem.FlightInBounds.Add(gf);
                        }
                    }
                    if (blockItem.FlightInBounds.Count > 0)
                        blockItem.IsRoundTrip = true;
                }
                flightResultOutput.BlockItems.Add(blockItem);
                return flightResultOutput;
            }
            catch
            {
                return null;
            }
        }
        public GroupFlight GetGroupFlightBamBoo(TripInfoBamBoo tripInfo, int waytype, 
                                                int countPax, bool isDomestric, string IdSearch)
        {
            GroupFlight result = new GroupFlight();
            string depDate = tripInfo.segment_info[0].departure_info.datetime.Split(' ')[0].Split('-')[2] + "/" +
                             tripInfo.segment_info[0].departure_info.datetime.Split(' ')[0].Split('-')[1] + "/" +
                             tripInfo.segment_info[0].departure_info.datetime.Split(' ')[0].Split('-')[0] + " " +
                             tripInfo.segment_info[0].departure_info.datetime.Split(' ')[1].Split(':')[0] + ":" +
                             tripInfo.segment_info[0].departure_info.datetime.Split(' ')[1].Split(':')[1] + ":00";
            double timecheck = CoreUtils.GetHourDifference(depDate);
            int beforday = 0;
            if (timecheck <= 24) {
                beforday = 1;
            }
            var serviceFee = _iserviceFeeService.GetServiceFeeByAgBfdDo(isDomestric, 0, beforday);
            var priceBreakDownFlightQH = GetPriceBreakDownFlightQH(tripInfo.booking_class, serviceFee.Price, 
                                                                    countPax, IdSearch, tripInfo.flight_segment_group_id);
            if (priceBreakDownFlightQH != null && priceBreakDownFlightQH.Count > 0)
            {
                result.FareDataId = tripInfo.flight_segment_group_id;
                result.BookingKey = priceBreakDownFlightQH[0].BookingKey;
                result.FlightServiceSearch = FlightServiceSearch.BamBooAirWays;
                result.PriceBreakDowns = new List<PriceBreakDownFlight>();
                result.PriceBreakDowns = priceBreakDownFlightQH;
                result.TicketClassDomestic = string.Empty;
                result.FlightRef = int.Parse(tripInfo.segment_info[0].flight_info.flight_number);
                result.ListSegment = new List<Segment>();
                result.ListSegment = GetListSegmentQH(tripInfo.segment_info,waytype);
                result.MainFlightNumber = "QH" + tripInfo.segment_info[0].flight_info.flight_number;
                result.MainAirlineCode = "QH";
                result.MainAirlineName = "BamBooAirways";
                result.MainDepartureAirportCode = tripInfo.segment_info[0].departure_info.airport_code;
                var departureAirportRow = _iairportService.GetAirportByCode(tripInfo.segment_info[0].departure_info.airport_code);
                result.MainDepartureAirportName = departureAirportRow.AirportNameVN;
                result.MainDepartureCity = departureAirportRow.CityName;
                result.MainDepartureCountry = departureAirportRow.CountryName;
                result.MainDepartureTime = Convert.ToDateTime(tripInfo.segment_info[0].departure_info.datetime).ToString("HH:mm");
                result.Plane = tripInfo.segment_info[0].aircraft_info.type;
                result.MainDepartureDate = Convert.ToDateTime(tripInfo.segment_info[0].departure_info.datetime);
                result.MainArrivalAirportCode = tripInfo.segment_info[tripInfo.segment_info.Count - 1].arrival_info.airport_code;
                var arrivalAirportRow = _iairportService.GetAirportByCode(tripInfo.segment_info[tripInfo.segment_info.Count - 1].arrival_info.airport_code);
                result.MainArrivalAirportName = arrivalAirportRow.AirportNameVN;
                result.MainArrivalCity = arrivalAirportRow.CityName;
                result.MainArrivalCountry = arrivalAirportRow.CountryName;
                result.MainArrivalTime = Convert.ToDateTime(tripInfo.segment_info[tripInfo.segment_info.Count - 1].arrival_info.datetime).ToString("HH:mm");
                result.MainArrivalDate = Convert.ToDateTime(tripInfo.segment_info[tripInfo.segment_info.Count - 1].arrival_info.datetime);

                TimeSpan beweenTime = result.MainArrivalDate - result.MainDepartureDate;
                double TotalMinute = beweenTime.TotalMinutes;
                var h = Convert.ToInt16(TotalMinute / 60);
                var m = Convert.ToInt16(TotalMinute - h * 60);
                result.Duration = h + "h" + m + "m"; ;
                result.TotalMinute = Convert.ToInt16(TotalMinute);
                result.Stop = Convert.ToInt16(tripInfo.segment_info.Count - 1);
                if (waytype == 0)
                    result.WayType = WayType.OutBound;
                else
                    result.WayType = WayType.InBound;
                result.RecommendationNumber = string.Empty;
                result.AllowanceBaggage = string.Empty;
                result.Note = string.Empty;
                result.ReturnTicket = string.Empty;
                return result;
            }
            else
                return null;
        }
        public List<PriceBreakDownFlight> GetPriceBreakDownFlightQH(List<BookingClassBamBoo> booKings, decimal serviceFee,
                                                                    int countPax, string IdSearch, int flight_segment_group_id)
        {
            var result = new List<PriceBreakDownFlight>();
            foreach(var booking in booKings)
            {
                if(countPax <= booking.seat_availablity)
                {
                    var price = new PriceBreakDownFlight();
                    price.BookingKey = flight_segment_group_id + "_" + booking.group_fare[0].fare_class + "_" + booking.group_fare[0].fare_basis + "_" + IdSearch;
                    price.FareClass = booking.group_fare[0].fare_class;
                    price.FareBasis = booking.group_fare[0].fare_basis;
                    price.ClassName = booking.pricing.fare_type;
                    price.CabinClass = booking.cabin_class;
                    price.SeatAvailablity = booking.seat_availablity;
                    price.TotallPriceAdt = 0;
                    price.DiscountAdt = 0;
                    price.TaxAdt = 0;
                    price.FareBaseAdt = 0;
                    price.FeeAdt = serviceFee;
                    price.TotallPriceChd = 0;
                    price.DiscountChd = 0;
                    price.TaxChd = 0;
                    price.FareBaseChd = 0;
                    price.FeeChd = serviceFee;
                    price.TotallPriceInf = 0;
                    price.DiscountInf = 0;
                    price.TaxInf = 0;
                    price.FareBaseInf = 0;
                    price.FeeInf = serviceFee;
                    foreach (var pricing in booking.pricing.pax_pricing_info)
                    {
                        if (pricing.pax_type.Equals("ADULT"))
                        {
                            price.TotallPriceAdt = Convert.ToDecimal(pricing.total.amount) + serviceFee;
                            price.DiscountAdt = Convert.ToDecimal(pricing.discount.amount);
                            price.TaxAdt = Convert.ToDecimal(pricing.tax.amount);
                            price.FareBaseAdt = Convert.ToDecimal(pricing.base_fare.amount);
                        }
                        if (pricing.pax_type.Equals("CHILD"))
                        {
                            price.TotallPriceChd = Convert.ToDecimal(pricing.total.amount) + serviceFee;
                            price.DiscountChd = Convert.ToDecimal(pricing.discount.amount);
                            price.TaxChd = Convert.ToDecimal(pricing.tax.amount);
                            price.FareBaseChd = Convert.ToDecimal(pricing.base_fare.amount);
                        }
                        if (pricing.pax_type.Equals("INFANT"))
                        {
                            price.TotallPriceInf = Convert.ToDecimal(pricing.total.amount) + serviceFee;
                            price.DiscountInf = Convert.ToDecimal(pricing.discount.amount);
                            price.TaxInf = Convert.ToDecimal(pricing.tax.amount);
                            price.FareBaseInf = Convert.ToDecimal(pricing.base_fare.amount);
                        }
                    }
                    switch (booking.booking_class)
                    {
                        case "A":
                        case "V":
                        case "U":
                        case "R":
                        case "O":
                            price.RecommendationNumber = "Hành lý xách tay 7kg";
                            price.AllowanceBaggage = "Hành lý ký gửi 0kg";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition += "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition += "    <li>Không được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                            price.Condition += "    <li>Không được phép Đổi tên</li>";
                            price.Condition += "    <li>Không được phép Hoàn vé (Trước giờ khởi hành tối thiểu 03 tiếng)</li>";
                            price.Condition += "    <li>Không được phép Hoàn vé (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                            price.Condition += "    <li>Hệ số cộng điểm Bamboo Club 0.25</li>";
                            price.Condition += "</ul>";
                            price.ReturnTicket = "Không hoàn hủy";
                            break;
                        case "G":
                            price.RecommendationNumber = "Hành lý xách tay 7kg";
                            price.AllowanceBaggage = "Hành lý ký gửi 20kg";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition += "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition += "    <li>Không được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                            price.Condition += "    <li>Không được phép Đổi tên</li>";
                            price.Condition += "    <li>Không được phép Hoàn vé (Trước giờ khởi hành tối thiểu 03 tiếng)</li>";
                            price.Condition += "    <li>Không được phép Hoàn vé (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                            price.Condition += "    <li>Hệ số cộng điểm Bamboo Club 0.25</li>";
                            price.Condition += "</ul>";
                            price.ReturnTicket = "Không hoàn hủy";
                            break;
                        case "X":
                        case "T":
                        case "Q":
                        case "N":
                        case "M":
                        case "L":
                        case "K":
                        case "H":
                            price.RecommendationNumber = "Hành lý xách tay 7kg";
                            price.AllowanceBaggage = "Hành lý ký gửi 0kg";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition += "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition += "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) mất phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition += "    <li>Không được phép Đổi tên</li>";
                            price.Condition += "    <li>Được phép Hoàn vé (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí</li>";
                            price.Condition += "    <li>Được phép Hoàn vé (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) mất phí</li>";
                            price.Condition += "    <li>Hệ số cộng điểm Bamboo Club 0.5</li>";
                            price.Condition += "</ul>";
                            price.ReturnTicket = "Có thể hoàn hủy";
                            break;
                        case "B":
                        case "W":
                        case "Y":
                            price.RecommendationNumber = "Hành lý xách tay 7kg";
                            price.AllowanceBaggage = "Hành lý ký gửi 20kg";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition += "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) miễn phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition += "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) miễn phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition += "    <li>Không được phép Đổi tên</li>";
                            price.Condition += "    <li>Được phép Hoàn vé (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí</li>";
                            price.Condition += "   	<li>Được phép Hoàn vé (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) mất phí</li>";
                            price.Condition += "   	<li>Hệ số cộng điểm Bamboo Club 1.00</li>";
                            price.Condition += "</ul>";
                            price.ReturnTicket = "Có thể hoàn hủy";
                            break;
                        case "D":
                        case "I":
                        case "C":
                            price.RecommendationNumber = "Hành lý xách tay 2 gói 7kg";
                            price.AllowanceBaggage = "Hành lý ký gửi 40kg";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition = "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition = "    <li>Được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) mất phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition = "    <li>Không được phép Đổi tên</li>";
                            price.Condition = "    <li>Được phép Hoàn vé (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí</li>";
                            price.Condition = "    <li>Được phép Hoàn vé (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) mất phí</li>";
                            price.Condition = "    <li>Hệ số cộng điểm Bamboo Club 1.75</li>";
                            price.Condition = "</ul>";
                            price.ReturnTicket = "Có thể hoàn hủy";
                            break;
                        case "J":
                        case "F":
                            price.RecommendationNumber = "Hành lý xách tay 2 gói 7kg";
                            price.AllowanceBaggage = "Hành lý ký gửi 40kg";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition = "     <li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) miễn phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition = "     <li>Được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) miễn phí + chênh lệch giá vé (nếu có)</li>";
                            price.Condition = "     <li>Không được phép Đổi tên</li>";
                            price.Condition = "     <li>Được phép Hoàn vé (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí</li>";
                            price.Condition = "     <li>Được phép Hoàn vé (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành) mất phí</li>";
                            price.Condition = "     <li>Hệ số cộng điểm Bamboo Club 2.00</li>";
                            price.Condition = " </ul>";
                            price.ReturnTicket = "Có thể hoàn hủy";
                            break;
                        default:
                            price.RecommendationNumber = "0 Kg hành lý xách tay";
                            price.AllowanceBaggage = "0 Kg hành lý ký gửi";
                            price.Condition = "<ul class=\"my-1 text-sm\">";
                            price.Condition += "<li>Không được thay đổi chuyến bay, chặng bay, ngày bay</li>";
                            price.Condition += "</ul>";
                            price.ReturnTicket = "Không hoàn hủy";
                            break;
                    }
                    result.Add(price);
                }
            }
            return result;
        }
        public List<Segment> GetListSegmentQH(List<SegmentInfoBamBoo> segment_info, int wayType)
        {
            List<Segment> result = new List<Segment>();
            int segIndex = 0;
            segment_info.ForEach(f => {
                Segment s = new Segment();
                s.FlightNumber = f.flight_info.flight_number;
                s.AirlineCode = f.flight_info.carrier_code;
                s.AirlineName = "BamBooAirways";
                DateTime departureTime = Convert.ToDateTime(f.departure_info.datetime);
                DateTime arrivalTime = Convert.ToDateTime(f.arrival_info.datetime);
                TimeSpan beweenTime = arrivalTime - departureTime;
                double TotalMinute = beweenTime.TotalMinutes;
                var h = Convert.ToInt16(TotalMinute / 60);
                var m = Convert.ToInt16(TotalMinute - h * 60);
                s.Duration = h + "h" + m + "m";
                s.OperatingAirlineCode = "QH";
                s.OperatingAirlineName = "BamBooAirways";
                var departureAirportRow = _iairportService.GetAirportByCode(f.departure_info.airport_code);
                s.DepartureAirportCode = f.departure_info.airport_code;
                s.DepartureAirportName = departureAirportRow.AirportNameVN;
                s.DepartureTerminal = string.Empty;
                s.DepartureDate = departureTime;
                s.DepartureTime = departureTime.ToString("HH:mm");
                s.DepartureCity = departureAirportRow.CityName;
                s.DepartureCountry = departureAirportRow.CountryName;
                s.ArrivalAirportCode = f.arrival_info.airport_code;
                var arrivalAirportRow = _iairportService.GetAirportByCode(f.arrival_info.airport_code);
                s.ArrivalAirportName = arrivalAirportRow.AirportNameVN;
                s.ArrivalTerminal = string.Empty;
                s.ArrivalDate = arrivalTime;
                s.ArrivalTime = arrivalTime.ToString("HH:mm");
                s.ArrivalCity = arrivalAirportRow.CityName;
                s.ArrivalCountry = arrivalAirportRow.CountryName;
                s.TicketClass = f.arrival_info.terminal;
                s.AircraftCode = f.aircraft_info.type;
                s.AircraftName = f.aircraft_info.type;
                s.SeatRemain = 0;
                s.SegmentStop = segment_info.Count.ToString();
                if (segIndex == 0)
                {
                    if (wayType == (int)WayType.OutBound)
                        s.SegmentType = SegmentType.FirstSegmentOutBound;
                    else
                        s.SegmentType = SegmentType.FirstSegmentInBound;
                }
                else
                {
                    s.SegmentType = SegmentType.NotApply;
                }
                result.Add(s);
                segIndex++;
            });
            return result;
        }
        public string GetBodyAirAvailability(SearchInputMod searchInput)
        {
            string bodypost = "{" +
                                  "\"availability_searches\": [" +
                                      "{" +
                                         "\"origin\": \"" + searchInput.DepartureAirport.ToUpper() + "\"," +
                                         "\"destination\": \"" + searchInput.ArrivalAirport.ToUpper() + "\"," +
                                         "\"flight_date\": \"" + searchInput.DepartureDate.ToString("yyyy-MM-dd") + "\"" +
                                    "}";
            if (searchInput.IsRoundTrip)
                bodypost += ",{" +
                                "\"origin\": \"" + searchInput.ArrivalAirport.ToUpper() + "\"," +
                                "\"destination\": \"" + searchInput.DepartureAirport.ToUpper() + "\"," +
                                "\"flight_date\": \"" + searchInput.ReturnDate?.ToString("yyyy-MM-dd") + "\"" +
                            "}";
            bodypost += "]," +
                        "\"pax_types\": [";
            if (searchInput.AdultNumber > 0)
                bodypost += "{\"type\": \"ADULT\",\"count\": " + searchInput.AdultNumber + "}";
            if (searchInput.ChildNumber > 0)
                bodypost += ",{\"type\": \"CHILD\",\"count\": " + searchInput.ChildNumber + "}";
            if (searchInput.InfantNumber > 0)
                bodypost += ",{\"type\": \"INFANT\",\"count\": " + searchInput.InfantNumber + "}";
            bodypost += "],";
            if (searchInput.IsRoundTrip)
                bodypost += "\"trip_type\":\"RT\",";
            else
                bodypost += "\"trip_type\":\"OW\",";
            bodypost += "\"point_of_purchase\":\"VND\"}";
            return bodypost;
        }
    }
}
