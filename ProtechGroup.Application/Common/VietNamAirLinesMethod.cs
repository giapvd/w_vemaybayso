using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using ProtechGroup.Domain.Entities;

namespace ProtechGroup.Application.Common
{
    public class VietNamAirLinesMethod : IVietNamAirLinesMethod
    {
        private readonly IAirportService _iairportService;
        private readonly IServiceFeeService _iserviceFeeService;
        public VietNamAirLinesMethod(IAirportService iairportService, IServiceFeeService iserviceFeeService)
        {
            _iairportService = iairportService;
            _iserviceFeeService = iserviceFeeService;
        }
        public FlightResultOutput BuildFlightResultVietNamAirLines(RootVNA alineVNA, int countPax, bool isDomestric)
        {
            var flightResultOutput = new FlightResultOutput();
            flightResultOutput.IsFlightDomestic = true;
            flightResultOutput.BlockItems = new List<BlockItem>();
            flightResultOutput.Airlines = new List<FlightResultOutput.Airline>();
            var airline = new FlightResultOutput.Airline();
            airline.AirlineName = "VietNamAirLines";
            airline.AirlineCode = "VN";
            flightResultOutput.Airlines.Add(airline);
            BlockItem blockItem = new BlockItem();
            blockItem.FlightOutBounds = new List<GroupFlight>();
            blockItem.FlightInBounds = new List<GroupFlight>();
            if (alineVNA != null)
            {
                if (alineVNA.ListGroup != null && alineVNA.ListGroup.Count > 0)
                {
                    foreach (var avn in alineVNA.ListGroup[0].ListAirOption)
                    {
                        var grvna = GetGroupFlightVietNamAirLines(avn, 0, alineVNA.Session, isDomestric, countPax);
                        if (grvna != null)
                            blockItem.FlightOutBounds.Add(grvna);
                    }
                    if (alineVNA.ListGroup.Count > 1)
                    {
                        foreach (var avn in alineVNA.ListGroup[1].ListAirOption)
                        {
                            var grvna = GetGroupFlightVietNamAirLines(avn, 1, alineVNA.Session, isDomestric, countPax);
                            if (grvna != null)
                                blockItem.FlightInBounds.Add(grvna);
                        }
                        blockItem.IsRoundTrip = true;
                    }
                }

            }
            flightResultOutput.BlockItems.Add(blockItem);   
            return flightResultOutput;
        }
        public string GetBodyPostSearchFlightVietNamAirLine(SearchInputMod searchInput)
        {
            string bodyPost = "{" +
                                       "\"adt\": " + searchInput.AdultNumber + "," +
                                       "\"chd\": " + searchInput.ChildNumber + "," +
                                       "\"inf\": " + searchInput.InfantNumber + "," +
                                       "\"listRoute\": [" +
                                               "{" +
                                                   "\"leg\": 0," +
                                                   "\"startPoint\": \"" + searchInput.DepartureAirport + "\"," +
                                                   "\"endPoint\": \"" + searchInput.ArrivalAirport + "\"," +
                                                   "\"departDate\": \"" + searchInput.DepartureDate.ToString("ddMMyyyy") + "\"" +
                                                "}";
            if (searchInput.IsRoundTrip)
            {
                bodyPost += ",{" +
                        "\"leg\": 1," +
                        "\"startPoint\": \"" + searchInput.ArrivalAirport + "\"," +
                        "\"endPoint\": \"" + searchInput.DepartureAirport + "\"," +
                        "\"departDate\": \"" + searchInput.ReturnDate?.ToString("ddMMyyyy") + "\"" +
                    "}";
            }
            bodyPost += "]," +
                    "\"Option\": { " +
                    "\"DirectOnly\": true " +
                    "}" +
            "}";
            return bodyPost;
        }
        public GroupFlight GetGroupFlightVietNamAirLines(ListAirOptionVNA airOption, int waytype, 
                                                        string sesionId, bool isDomestric, int countPax)
        {
            
            GroupFlight result = new GroupFlight();
            string mDepTime = airOption.ListFlightOption[0].ListFlight[0].DepartDate.Substring(0, 2) + "/" + airOption.ListFlightOption[0].ListFlight[0].DepartDate.Substring(2, 2) + "/" + airOption.ListFlightOption[0].ListFlight[0].DepartDate.Substring(4, 4) + " " + airOption.ListFlightOption[0].ListFlight[0].DepartDate.Substring(9, 2) + ":" + airOption.ListFlightOption[0].ListFlight[0].DepartDate.Substring(11, 2) + ":00";
            string mArrTime = airOption.ListFlightOption[0].ListFlight[0].ArriveDate.Substring(0, 2) + "/" + airOption.ListFlightOption[0].ListFlight[0].ArriveDate.Substring(2, 2) + "/" + airOption.ListFlightOption[0].ListFlight[0].ArriveDate.Substring(4, 4) + " " + airOption.ListFlightOption[0].ListFlight[0].ArriveDate.Substring(9, 2) + ":" + airOption.ListFlightOption[0].ListFlight[0].ArriveDate.Substring(11, 2) + ":00";
            double timecheck = CoreUtils.GetHourDifference(mDepTime);
            int beforday = 0;
            if (timecheck <= 24)
            {
                beforday = 1;
            }
            var serviceFee = _iserviceFeeService.GetServiceFeeByAgBfdDo(isDomestric, 0, beforday);
            var priceBreakDownFlightVN = GetPriceBreakDownFlightVN(airOption.ListFareOption, serviceFee.Price, countPax, sesionId, 
                                                                    airOption.OptionId, airOption.ListFlightOption[0].OptionId);
            if (priceBreakDownFlightVN != null && priceBreakDownFlightVN.Count>0)
            {
                result.FareDataId = airOption.OptionId;
                result.BookingKey = priceBreakDownFlightVN[0].BookingKey;
                result.FlightServiceSearch = FlightServiceSearch.VietnamAirline;
                result.BgRow = string.Empty;
                result.PriceBreakDowns = new List<PriceBreakDownFlight>();
                result.PriceBreakDowns = priceBreakDownFlightVN;
                result.TicketClassDomestic = string.Empty;
                result.FlightRef = int.Parse(airOption.ListFlightOption[0].ListFlight[0].FlightNumber);
                var listChangbay = GetListChangBayVN(airOption.ListFlightOption[0].ListFlight[0].ListSegment, priceBreakDownFlightVN[0].ClassName, waytype);
                result.ListSegment = new List<Segment>();
                result.ListSegment = listChangbay;
                result.MainFlightNumber = "VN" + airOption.ListFlightOption[0].ListFlight[0].FlightNumber;
                result.MainAirlineCode = airOption.ListFlightOption[0].ListFlight[0].Operator;
                if (result.MainAirlineCode.Equals("BL"))
                    result.MainAirlineName = "Pacific Airlines";
                else
                    result.MainAirlineName = "Vietnam Airlines";
                result.MainDepartureAirportCode = airOption.ListFlightOption[0].ListFlight[0].StartPoint;
                var departureAirportRow = _iairportService.GetAirportByCode(airOption.ListFlightOption[0].ListFlight[0].StartPoint);
                result.MainDepartureAirportName = departureAirportRow.AirportNameVN;
                result.MainDepartureCity = departureAirportRow.CityName;
                result.MainDepartureCountry = departureAirportRow.CountryName;
                
                result.MainDepartureTime = Convert.ToDateTime(mDepTime).ToString("HH:mm");
                result.Plane = listChangbay[0].AircraftName;
                result.MainDepartureDate = Convert.ToDateTime(mDepTime);
                result.MainArrivalAirportCode = airOption.ListFlightOption[0].ListFlight[0].EndPoint;
                var arrivalAirportRow = _iairportService.GetAirportByCode(airOption.ListFlightOption[0].ListFlight[0].EndPoint);
                result.MainArrivalAirportName = arrivalAirportRow.AirportNameVN;
                result.MainArrivalCity = arrivalAirportRow.CityName;
                result.MainArrivalCountry = arrivalAirportRow.CountryName;
                result.MainArrivalTime = Convert.ToDateTime(mArrTime).ToString("HH:mm");
                result.MainArrivalDate = Convert.ToDateTime(mArrTime);

                TimeSpan beweenTime = result.MainArrivalDate - result.MainDepartureDate;
                double TotalMinute = beweenTime.TotalMinutes;
                var h = Convert.ToInt16(TotalMinute / 60);
                var m = Convert.ToInt16(TotalMinute - h * 60);
                result.Duration = h + "h" + m + "m"; ;
                result.TotalMinute = Convert.ToInt16(TotalMinute);
                result.Stop = Convert.ToInt16(airOption.ListFlightOption[0].ListFlight[0].ListSegment.Count - 1);
                if (waytype == 0)
                    result.WayType = WayType.OutBound;
                else
                    result.WayType = WayType.InBound;
               
            }
            return result;
        }
        public List<PriceBreakDownFlight> GetPriceBreakDownFlightVN(List<ListFareOptionVNA> fareOpt, decimal serviceFee,
                                                                    int countPax, string sesionId, int airlineOptionId, int flightOptionId)

        {
            var result = new List<PriceBreakDownFlight>();
            foreach (var fare in fareOpt)
            {
                if (countPax <= fare.Availability)
                {
                    var price = new PriceBreakDownFlight();
                    price.BookingKey = sesionId + "_" + airlineOptionId + "_" + flightOptionId + "_" + fare.OptionId.ToString();
                    price.FareClass = fare.FareClass;
                    price.FareBasis = fare.ListFarePax[0].ListFareInfo[0].FareBasis;
                    price.ClassName = fare.ListFarePax[0].ListFareInfo[0].FareFamily;
                    price.CabinClass = fare.ListFarePax[0].ListFareInfo[0].CabinName;
                    price.SeatAvailablity = fare.Availability;
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
                    foreach(var farePax in fare.ListFarePax)
                    {
                        if (farePax.PaxType.Equals("ADT"))
                        {
                            price.TotallPriceAdt = Convert.ToDecimal(farePax.TotalFare) + serviceFee;
                            price.DiscountAdt = 0;
                            price.TaxAdt = Convert.ToDecimal(farePax.Taxes);
                            price.FareBaseAdt = Convert.ToDecimal(farePax.BaseFare);
                        }
                        if (farePax.PaxType.Equals("CHD"))
                        {
                            price.TotallPriceChd = Convert.ToDecimal(farePax.TotalFare) + serviceFee;
                            price.DiscountChd = 0;
                            price.TaxChd = Convert.ToDecimal(farePax.Taxes);
                            price.FareBaseChd = Convert.ToDecimal(farePax.BaseFare);
                        }
                        if (farePax.PaxType.Equals("INF"))
                        {
                            price.TotallPriceInf = Convert.ToDecimal(farePax.TotalFare) + serviceFee;
                            price.DiscountInf = 0;
                            price.TaxInf = Convert.ToDecimal(farePax.Taxes);
                            price.FareBaseInf = Convert.ToDecimal(farePax.BaseFare);
                        }
                        switch (farePax.ListFareInfo[0].FareClass)
                        {
                            case "P":
                            case "A":
                            case "G":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 0kg";
                                price.Condition = "<ul class=\"my-1 text-sm\">";
                                price.Condition += "    <li>Không được phép thay đổi</li>";
                                price.Condition += "    <li>Không bao gồm quầy thủ tục ưu tiên</li>";
                                price.Condition += "    <li>Không được phép hoàn/hủy vé</li>";
                                price.Condition += "    <li>Tích lũy 10% số dặm</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Không hoàn hủy";
                                break;
                            case "Q":
                            case "N":
                            case "R":
                            case "T":
                            case "E":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 23kg";
                                price.Condition = "<ul class=\"my-1 text-sm\">";
                                price.Condition += "    <li>Được phép Thay đổi mất phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "    <li>Không bao gồm quầy thủ tục ưu tiên</li>";
                                price.Condition += "    <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "    <li>Tích lũy 60% số dặm</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "S":
                            case "H":
                            case "K":
                            case "L":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 23kg";
                                price.Condition = "<ul class=\"my-1 text-sm\">";
                                price.Condition += "    <li>Được phép Thay đổi mất phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "    <li>Không bao gồm quầy thủ tục ưu tiên</li>";
                                price.Condition += "    <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "    <li>Tích lũy 80% số dặm</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "B":
                            case "M":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 23kg";
                                price.Condition = "<ul class=\"my-1 text-sm\">";
                                price.Condition += "    <li>Được phép Thay đổi miễn phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "    <li>Được sử dụng quầy thủ tục ưu tiên</li>";
                                price.Condition += "    <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "    <li>Tích lũy 110% số dặm</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "W":
                            case "Z":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 32kg";
                                price.Condition += "<ul class=\"my-1 text-sm\">";
                                price.Condition += "    <li>Được phép Thay đổi miễn phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "    <li>Được sử dụng quầy thủ tục ưu tiên</li>";
                                price.Condition += "    <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "    <li>Tích lũy 130% số dặm</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "U":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 32kg";
                                price.Condition += "<ul class=\"my-1 text-sm\">";
                                price.Condition += "     <li>Được phép Thay đổi mất phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "     <li>Được sử dụng quầy thủ tục ưu tiên</li>";
                                price.Condition = "      <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "     <li>Tích lũy 120% số dặm</li>";
                                price.Condition += " </ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "D":
                            case "I":
                                price.RecommendationNumber = "Hành lý xách tay 18kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 32kg";
                                price.Condition += "<ul class=\"my-1 text-sm\">";
                                price.Condition += "     <li>Được phép Thay đổi mất phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "     <li>Được phép Đổi chuyến tại sân bay mất phí</li>";
                                price.Condition += "     <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "     <li>Tích lũy 150% số dặm</li>";
                                price.Condition += " </ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "C":
                                price.RecommendationNumber = "Hành lý xách tay 18kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 32kg";
                                price.Condition += "<ul class=\"my-1 text-sm\">";
                                price.Condition += "     <li>Được phép Thay đổi miễn phí + Chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "     <li>Được phép Đổi chuyến tại sân bay mất phí</li>";
                                price.Condition += "     <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "     <li>Tích lũy 200% số dặm</li>";
                                price.Condition += " </ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            case "J":
                                price.RecommendationNumber = "Hành lý xách tay 18kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 32kg";
                                price.Condition += "<ul class=\"my-1 text-sm\">";
                                price.Condition += "     <li>Được phép Thay đổi miễn phí</li>";
                                price.Condition += "     <li>Được phép Đổi chuyến tại sân bay mất phí</li>";
                                price.Condition += "      <li>Được phép Hoàn/hủy vé mất phí</li>";
                                price.Condition += "     <li>Tích lũy 200% số dặm</li>";
                                price.Condition += " </ul>";
                                price.ReturnTicket = "Có thể hoàn hủy";
                                break;
                            default:
                                price.RecommendationNumber = "Hành lý xách tay 0kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 0kg";
                                price.Condition = "<ul class=\"my-1 text-sm\">";
                                price.Condition += "<li>Không hoàn hủy</li>";
                                price.Condition += "<li>Không được thay đổi chuyến bay, chặng bay, ngày bay</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Không hoàn hủy";
                                break;
                        }
                    }
                    result.Add(price);
                }
            }
            return result;
        }
        public List<Segment> GetListChangBayVN(List<ListSegmentVNA> listSegment, string className, int wayType)
        {
            var result = new List<Segment>();
            int segIndex = 0;
            foreach (var seg in listSegment)
            {
                Segment s = new Segment();
                s.FlightNumber = seg.FlightNumber;

                s.AirlineCode = seg.Airline;
                s.AirlineName = "Vietnam Airlines";
                string depTime = seg.DepartDate.Substring(0, 2) + "/" + seg.DepartDate.Substring(2, 2) + "/" + seg.DepartDate.Substring(4, 4) + " " + seg.DepartDate.Substring(9, 2) + ":" + seg.DepartDate.Substring(11, 2) + ":00";
                string arrTime = seg.ArriveDate.Substring(0, 2) + "/" + seg.ArriveDate.Substring(2, 2) + "/" + seg.ArriveDate.Substring(4, 4) + " " + seg.ArriveDate.Substring(9, 2) + ":" + seg.ArriveDate.Substring(11, 2) + ":00";

                DateTime departureTime = Convert.ToDateTime(depTime);
                DateTime arrivalTime = Convert.ToDateTime(arrTime);
                double TotalMinute = Convert.ToDouble(seg.Duration);
                var h = Convert.ToInt16(TotalMinute / 60);
                var m = Convert.ToInt16(TotalMinute - h * 60);
                s.Duration = h + "h" + m + "m";
                s.OperatingAirlineCode = seg.Operator;
                if (seg.Operator.Equals("BL"))
                    s.OperatingAirlineName = "Pacific Airlines";
                else
                    s.OperatingAirlineName = "Vietnam Airlines";
                var departureAirportRow = _iairportService.GetAirportByCode(seg.StartPoint);
                s.DepartureAirportCode = seg.StartPoint;
                s.DepartureAirportName = departureAirportRow.AirportNameVN;
                s.DepartureTerminal = string.Empty;
                s.DepartureDate = departureTime;
                s.DepartureTime = departureTime.ToString("HH:mm");
                s.DepartureCity = departureAirportRow.CityName;
                s.DepartureCountry = departureAirportRow.CountryName;
                s.ArrivalAirportCode = seg.EndPoint;
                var arrivalAirportRow = _iairportService.GetAirportByCode(seg.EndPoint);
                s.ArrivalAirportName = arrivalAirportRow.AirportNameVN;
                s.ArrivalTerminal = string.Empty;
                s.ArrivalDate = arrivalTime;
                s.ArrivalTime = arrivalTime.ToString("HH:mm");
                s.ArrivalCity = arrivalAirportRow.CityName;
                s.ArrivalCountry = arrivalAirportRow.CountryName;
                s.TicketClass = className;
                s.AircraftCode = seg.Equipment;
                s.AircraftName = "A" + seg.Equipment;
                s.SeatRemain = 0;
                s.SegmentStop = seg.Equipment.ToString();
                if(segIndex == 0)
                {
                    if(wayType == (int)WayType.OutBound)
                        s.SegmentType = SegmentType.FirstSegmentOutBound;
                    else
                        s.SegmentType = SegmentType.FirstSegmentInBound;
                }
                else
                {
                    s.SegmentType = SegmentType.NotApply;
                }
                result.Add(s);
                segIndex ++;
            }
            return result;
        }
    }
}
