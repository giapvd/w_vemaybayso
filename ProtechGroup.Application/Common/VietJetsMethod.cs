using Newtonsoft.Json;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Common
{
    public class VietJetsMethod : IVietJetsMethod
    {
        private readonly IAirportService _iairportService;
        private readonly IServiceFeeService _iserviceFeeService;
        private readonly IVietJetsService _ivietJetsService;
        public VietJetsMethod(IAirportService iairportService, IServiceFeeService iserviceFeeService, IVietJetsService ivietJetsService)
        {
            _iairportService = iairportService;
            _iserviceFeeService = iserviceFeeService;
            _ivietJetsService = ivietJetsService;
        }
        public FlightResultOutput BuildFlightResultVietJets(RootVietJets[] alineVJ, int countPax, bool isDomestric)
        {
            try
            {
                var flightResultOutput = new FlightResultOutput();
                flightResultOutput.IsFlightDomestic = true;
                flightResultOutput.BlockItems = new List<BlockItem>();
                flightResultOutput.Airlines = new List<FlightResultOutput.Airline>();
                var airline = new FlightResultOutput.Airline();
                airline.AirlineName = "VietJetAir";
                airline.AirlineCode = "VJ";

                flightResultOutput.Airlines.Add(airline);
                BlockItem blockItem = new BlockItem();
                blockItem.FlightOutBounds = new List<GroupFlight>();
                blockItem.FlightInBounds = new List<GroupFlight>();
                if (alineVJ != null && alineVJ.Length > 0)
                {
                    string departureAirport = alineVJ[0].cityPair.identifier.Split('-')[0];
                    string ArrivalAirport = alineVJ[0].cityPair.identifier.Split('-')[1];
                    for (int i = 0; i < alineVJ.Length; i++)
                    {

                        if (alineVJ[i].cityPair.identifier.Equals(departureAirport + "-" + ArrivalAirport))
                        {
                            var gf = GetGroupFlightVietJets(alineVJ[i], i, 0, countPax, isDomestric);
                            if (gf != null)
                                blockItem.FlightOutBounds.Add(gf);
                        }
                        else
                        {
                            var gf = GetGroupFlightVietJets(alineVJ[i], i, 1, countPax, isDomestric);
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
        public GroupFlight GetGroupFlightVietJets(RootVietJets root, int fareId, int wattype, int countPax, bool isDomestric)
        {
            GroupFlight result = new GroupFlight();
            string depDate = root.flights[0].departure.scheduledTime.Split(' ')[0].Split('-')[2] + "/" +
                             root.flights[0].departure.scheduledTime.Split(' ')[0].Split('-')[1] + "/" +
                             root.flights[0].departure.scheduledTime.Split(' ')[0].Split('-')[0] + " " +
                             root.flights[0].departure.scheduledTime.Split(' ')[1].Split(':')[0] + ":" +
                             root.flights[0].departure.scheduledTime.Split(' ')[1].Split(':')[1] + ":00";
            double timecheck = CoreUtils.GetHourDifference(depDate);
            int beforday = 0;
            if (timecheck <= 24)
            {
                beforday = 1;
            }
            var serviceFee = _iserviceFeeService.GetServiceFeeByAgBfdDo(isDomestric, 0, beforday);
            var priceBreakDownFlightVJ = GetPriceBreakDownFlightVJ(root.fareOptions, countPax, serviceFee.Price, root.key);
            if (priceBreakDownFlightVJ != null && priceBreakDownFlightVJ.Count >0)
            {
                result.FareDataId = fareId;
                result.BookingKey = priceBreakDownFlightVJ[0].BookingKey;
                result.FlightServiceSearch = FlightServiceSearch.VietjetAir;
                result.PriceDomestic = priceBreakDownFlightVJ[0].TotallPriceAdt;
                result.BgRow = string.Empty;
                result.PriceBreakDowns = new List<PriceBreakDownFlight>();
                result.PriceBreakDowns = priceBreakDownFlightVJ;
                result.TicketClassDomestic = priceBreakDownFlightVJ[0].FareClass;
                result.FlightRef = int.Parse(root.flights[0].flightNumber);
                result.ListSegment = new List<Segment>();
                result.ListSegment = GetListSegmentVJ(root.flights, priceBreakDownFlightVJ[0].FareClass, wattype);
                result.MainFlightNumber = "VJ" + root.flights[0].flightNumber;
                result.MainAirlineCode = "VJ";
                result.MainAirlineName = "VietJet Air";
                result.MainDepartureAirportCode = root.flights[0].departure.airport.code;
                result.MainDepartureAirportName = root.flights[0].departure.airport.name;
                var departureAirportRow = _iairportService.GetAirportByCode(root.flights[0].departure.airport.code);
                result.MainDepartureCity = departureAirportRow.CityName;
                result.MainDepartureCountry = departureAirportRow.CountryName;
                result.MainDepartureTime = Convert.ToDateTime(root.flights[0].departure.scheduledTime).ToString("HH:mm");
                result.Plane = root.flights[0].aircraftModel.name;
                result.MainDepartureDate = Convert.ToDateTime(root.flights[0].departure.scheduledTime);
                result.MainArrivalAirportCode = root.flights[root.flights.Count - 1].arrival.airport.code;
                result.MainArrivalAirportName = root.flights[root.flights.Count - 1].arrival.airport.name;
                var arrivalAirportRow = _iairportService.GetAirportByCode(root.flights[root.flights.Count - 1].arrival.airport.code);
                result.MainArrivalCity = arrivalAirportRow.CityName;
                result.MainArrivalCountry = arrivalAirportRow.CountryName;
                result.MainArrivalTime = Convert.ToDateTime(root.flights[root.flights.Count - 1].arrival.scheduledTime).ToString("HH:mm");
                result.MainArrivalDate = Convert.ToDateTime(root.flights[root.flights.Count - 1].arrival.scheduledTime);

                var h = Convert.ToInt16(root.enRouteHours);
                var m = Convert.ToInt16((Convert.ToDecimal(root.enRouteHours) - h) * 60);
                result.Duration = h + "h" + m + "m";
                result.TotalMinute = Convert.ToInt16(Convert.ToDecimal(root.enRouteHours) * 60);
                result.Stop = Convert.ToInt16(root.numberOfStops) - 1 ;
                if (wattype == 0)
                    result.WayType = WayType.OutBound;
                else
                    result.WayType = WayType.InBound;
                return result;
            }
            else
                return null;
        }
        public List<PriceBreakDownFlight> GetPriceBreakDownFlightVJ(List<FareOption> lFareot, int countPax, decimal serviceFee,string flightKey)
        {
            var result = new List<PriceBreakDownFlight>();
            foreach (var f in lFareot)
            {
                if (f.fareValidity.valid && !f.fareValidity.soldOut && !f.fareValidity.noFare)
                {
                    if (f.availability >= countPax)
                    {
                        var price = new PriceBreakDownFlight();
                        price.BookingKey = flightKey + "_" + f.bookingKey;
                        price.FareClass = f.fareClass.description;
                        price.FareBasis = f.fareClass.code;
                        price.ClassName = f.bookingCode.description;
                        price.CabinClass = f.cabinClass.description;
                        price.SeatAvailablity = f.availability;
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
                        foreach (var faOp in f.fareCharges)
                        {
                            if (faOp.chargeType.code.Equals("FA"))
                            {
                                if (faOp.passengerApplicability.adult)
                                {
                                    price.TotallPriceAdt += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount) + serviceFee;
                                    price.DiscountAdt += Convert.ToDecimal(faOp.currencyAmounts[0].discountAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.FareBaseAdt += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.child)
                                {
                                    price.TotallPriceChd += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount) + serviceFee;
                                    price.DiscountChd += Convert.ToDecimal(faOp.currencyAmounts[0].discountAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.FareBaseChd += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.infant)
                                {
                                    price.TotallPriceInf += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount) + serviceFee;
                                    price.DiscountInf += Convert.ToDecimal(faOp.currencyAmounts[0].discountAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.FareBaseInf += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                            }
                            if (faOp.chargeType.code.Equals("AM"))
                            {
                                if (faOp.passengerApplicability.adult)
                                {
                                    price.TotallPriceAdt += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                    
                                }
                                if (faOp.passengerApplicability.child)
                                {
                                    price.TotallPriceChd += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.infant)
                                {
                                    price.TotallPriceInf += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                            }
                            if (faOp.chargeType.code.Equals("MF"))
                            {
                                if (faOp.passengerApplicability.adult)
                                {
                                    price.TotallPriceAdt += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);

                                }
                                if (faOp.passengerApplicability.child)
                                {
                                    price.TotallPriceChd += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.infant)
                                {
                                    price.TotallPriceInf += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                            }
                            if (faOp.chargeType.code.Equals("AS"))
                            {
                                if (faOp.passengerApplicability.adult)
                                {
                                    price.TotallPriceAdt += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);

                                }
                                if (faOp.passengerApplicability.child)
                                {
                                    price.TotallPriceChd += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.infant)
                                {
                                    price.TotallPriceInf += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                            }
                            if (faOp.chargeType.code.Equals("AI"))
                            {
                                if (faOp.passengerApplicability.adult)
                                {
                                    price.TotallPriceAdt += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);

                                }
                                if (faOp.passengerApplicability.child)
                                {
                                    price.TotallPriceChd += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.infant)
                                {
                                    price.TotallPriceInf += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                            }
                            if (faOp.chargeType.code.Equals("IF"))
                            {
                                if (faOp.passengerApplicability.adult)
                                {
                                    price.TotallPriceAdt += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxAdt += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);

                                }
                                if (faOp.passengerApplicability.child)
                                {
                                    price.TotallPriceChd += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxChd += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                                if (faOp.passengerApplicability.infant)
                                {
                                    price.TotallPriceInf += Convert.ToDecimal(faOp.currencyAmounts[0].totalAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].taxAmount);
                                    price.TaxInf += Convert.ToDecimal(faOp.currencyAmounts[0].baseAmount);
                                }
                            }
                            if (price.TotallPriceAdt == 0)
                                price.TotallPriceAdt += serviceFee;
                            if (price.TotallPriceChd == 0)
                                price.TotallPriceChd += serviceFee;
                            if (price.TotallPriceInf == 0)
                                price.TotallPriceInf += serviceFee;

                        }
                        switch (f.fareClass.code.Split('_')[1])
                        {
                            case "ECO":
                                price.RecommendationNumber = "Hành lý xách tay 7kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 0kg";
                                price.Condition = "<ul class=\"conditions\">";
                                price.Condition += "<li>Hành lý xách tay 7kg</li>";
                                price.Condition += "<li>Không có hành lý ký gửi</li>";
                                price.Condition += "<li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) mất phí + chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "<li>Không được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                                price.Condition += "<li>Không được phép Đổi tên</li>";
                                price.Condition += "<li>Được Hoàn bảo lưu định danh Tiền Vé. Thông báo trước ít nhất 24 giờ so với giờ khởi hành dự kiến. Bảo lưu tiền vé tối đa 01 năm kể từ ngày khởi hành của chuyến bay.</li>";                          
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Không hoàn hủy";
                                break;
                            case "DLX":
                                price.RecommendationNumber = "Hành lý xách tay 7kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 20kg";
                                price.Condition = "<ul class=\"conditions\">";
                                price.Condition += "<li>Hành lý xách tay: 07kg</li>";
                                price.Condition += "<li>Hành lý ký gửi: 20Kg</li>";
                                price.Condition += "<li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) miễn phí + chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "<li>Không được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                                price.Condition += "<li>Không được phép Đổi tên</li>";
                                price.Condition += "<li>Được Hoàn bảo lưu định danh Tiền Vé. Thông báo trước ít nhất 24 giờ so với giờ khởi hành dự kiến. Bảo lưu tiền vé tối đa 01 năm kể từ ngày khởi hành của chuyến bay</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Không hoàn hủy";
                                break;
                            case "SBoss":
                                price.RecommendationNumber = "Hành lý xách tay 10kg";
                                price.AllowanceBaggage = "Hành lý ký gửi 30kg";
                                price.Condition = "<ul class=\"conditions\">";
                                price.Condition += "<li>Hành lý xách tay: 10kg</li>";
                                price.Condition += "<li>Hành lý ký gửi: 30kg và 01 bộ dụng cụ chơi golf (nếu có)</li>";
                                price.Condition += "<li>Phòng chờ sang trọng</li>";
                                price.Condition += "<li>Được phép Thay đổi chuyến bay/ hành trình (Trước giờ khởi hành tối thiểu 03 tiếng) miễn phí + chênh lệch giá vé (nếu có)</li>";
                                price.Condition += "<li>Không được phép Thay đổi chuyến bay/ hành trình (Trong vòng 03 tiếng trước giờ khởi hành và sau giờ khởi hành)</li>";
                                price.Condition += "<li>Được phép đổi tên mất phí + chênh lệch Giá Vé (nếu có). Chỉ áp dụng đối với Vé chưa sử dụng. Phải đổi tên cho toàn bộ hành trình trong Vé</li>";
                                price.Condition += "<li>Được Hoàn bảo lưu định danh Tiền Vé. Thông báo trước ít nhất 24 giờ so với giờ khởi hành dự kiến. Bảo lưu tiền vé tối đa 02 năm kể từ ngày khởi hành của chuyến bay</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Không hoàn hủy";
                                break;
                            default:
                                price.RecommendationNumber = "0 Kg hành lý xách tay";
                                price.AllowanceBaggage = "0 Kg hành lý ký gửi";
                                price.Condition = "<ul class=\"conditions\">";
                                price.Condition += "<li>0 Kg hành lý xách tay</li>";
                                price.Condition += "<li>0 Kg hành lý ký gửi</li>";
                                price.Condition += "<li>Không được thay đổi chuyến bay, chặng bay, ngày bay</li>";
                                price.Condition += "</ul>";
                                price.ReturnTicket = "Không hoàn hủy";
                                break;
                        }
                        result.Add(price);
                    }
                }
            }
            return result;
        }
        public List<Segment> GetListSegmentVJ(List<Flight> flights, string ticketClass, int wayType)
        {

            List<Segment> result = new List<Segment>();
            int segIndex = 0;
            flights.ForEach(f => {
                Segment s = new Segment();
                s.FlightNumber = f.flightNumber;
                s.AirlineCode = f.airlineCode.code;
                s.AirlineName = "VietJetAir";
                DateTime departureTime = Convert.ToDateTime(f.departure.scheduledTime);
                DateTime arrivalTime = Convert.ToDateTime(f.arrival.scheduledTime);
                TimeSpan beweenTime = arrivalTime - departureTime;
                double TotalMinute = beweenTime.TotalMinutes;
                var h = Convert.ToInt16(TotalMinute / 60);
                var m = Convert.ToInt16(TotalMinute - h * 60);
                s.Duration = h + "h" + m + "m";
                s.OperatingAirlineCode = "VJ";
                s.OperatingAirlineName = "VietJetAir";
                var departureAirportRow = _iairportService.GetAirportByCode(f.departure.airport.code);
                s.DepartureAirportCode = f.departure.airport.code;
                s.DepartureAirportName = f.departure.airport.name;
                s.DepartureTerminal = string.Empty;
                s.DepartureDate = departureTime;
                s.DepartureTime = departureTime.ToString("HH:mm");
                s.DepartureCity = departureAirportRow.CityName;
                s.DepartureCountry = departureAirportRow.CountryName;
                s.ArrivalAirportCode = f.arrival.airport.code;
                var arrivalAirportRow = _iairportService.GetAirportByCode(f.arrival.airport.code);
                s.ArrivalAirportName = f.arrival.airport.name;
                s.ArrivalTerminal = string.Empty;
                s.ArrivalDate = arrivalTime;
                s.ArrivalTime = arrivalTime.ToString("HH:mm");
                s.ArrivalCity = arrivalAirportRow.CityName;
                s.ArrivalCountry = arrivalAirportRow.CountryName;
                s.TicketClass = ticketClass;
                s.AircraftCode = f.aircraftModel.name.Length < 4 ? "A" + f.aircraftModel.name : f.aircraftModel.name;
                s.AircraftName = f.aircraftModel.name.Length < 4 ? "A" + f.aircraftModel.name : f.aircraftModel.name;
                s.SeatRemain = 0;
                s.SegmentStop = "1";
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
        public RootVietJets GetAirlineResponseRowVJ(string jsonContent, string bookingKey)
        {
            RootVietJets result = new RootVietJets();
            var root = JsonConvert.DeserializeObject<RootVietJets[]>(jsonContent);
            foreach (var r in root)
            {
                foreach (var f in r.fareOptions)
                {
                    if (f.bookingKey.ToLower().Equals(bookingKey))
                    {
                        result = r;
                        return result;
                    }
                }
            }
            return result;
        }
        public string GetStringRequestAlinesVietJet(SearchInputMod searchInput)
        {
            string strRequest = "?cityPair=" + searchInput.DepartureAirport + "-" + searchInput.ArrivalAirport;
            strRequest += "&departure=" + searchInput.DepartureDate.ToString("yyyy-MM-dd");
            strRequest += "&cabinClass=Y";
            strRequest += "&currency=VND";
            strRequest += "&adultCount=" + searchInput.AdultNumber;
            strRequest += "&childCount=" + searchInput.ChildNumber;
            strRequest += "&infantCount=" + searchInput.InfantNumber;
            if (searchInput.IsRoundTrip)
                strRequest += "&return=" + searchInput.ReturnDate?.ToString("yyyy-MM-dd");
            strRequest += "&company=i5M1ALKO4jozmgXmq3Cp8cSS56eS3V1GxLk1n¥I69CE=";
            return strRequest;
        }
        public async Task<string> GetBodyPostBookingVietJet(SearchInputMod searchInput, OrderContactlInfoMod orderContactInfo, OrderFlightMod orderFlight,
                                                List<OrderFlightSegmentMod> orderFlightSegment, List<OrderTravellerMod> orderTraveller,
                                                List<OrderBaggageMod> orderBaggage, string payMethod)
        {
            string result = string.Empty;
            decimal totalOrder = (decimal)orderFlight.TotalPrice + (decimal)orderFlight.TotalTaxAndFee + (decimal)orderFlight.TotalAddOnFee;
            List<OrderTravellerMod> infants = new List<OrderTravellerMod>();
            foreach (var oderTravel in orderTraveller)
            {
                if (oderTravel.TravellerType == (int)TravellerType.Infant)
                    infants.Add(oderTravel);
            }
            result = @"{" +
                        "\"bookingInformation\": {" +
                                                "\"contactInformation\": {" +
                                                "\"name\": \"CTY CP VE MAY BAY SO\"," +
                                                "\"phoneNumber\": \"0919005320\"," +
                                                "\"extension\": \"\"," +
                                                "\"email\": \"contact@vemaybayso.vn\"," +
                                                "\"language\": {" +
                                                "\"href\": \"/languages/en\"," +
                                                "\"code\": \"en\"," +
                                                "\"name\": \"English\"" +
                                                                        "}" +
                                                                    "}" +
                                                "}," +
                         "\"passengers\": [";
            int index = 1;
            foreach (var oderTravel in orderTraveller)
            {
                if (oderTravel.TravellerType != (int)TravellerType.Infant)
                {
                    if (index == 1)
                        result += "{";
                    else
                        result += ",{";
                    result += "\"index\": " + index + "," +
                              "\"fareApplicability\": {";
                    if (oderTravel.TravellerType == (int)TravellerType.Child)
                        result += "\"child\": true,\"adult\": false";
                    else
                        result += "\"child\": false,\"adult\": true";
                    result += "}," +
                    "\"reservationProfile\": {" +
                    "\"lastName\": \"" + oderTravel.LastName + "\"," +
                    "\"firstName\": \"" + oderTravel.FirstName + "\",";
                    if (oderTravel.Gender == (int)Gender.Nam)
                    {
                        result += "\"title\": \"Mr\"," +
                                "\"gender\": \"Male\",";
                    }
                    else
                    {
                        result += "\"title\": \"Ms\"," +
                                "\"gender\": \"Female\",";
                    }
                    try
                    {
                        if (oderTravel.DateOfBirth != null)
                            result += "\"birthDate\":\"" + oderTravel.DateOfBirth?.ToString("yyyy-MM-dd") + "\",";
                    }
                    catch { }
                    result += "\"personalContactInformation\": {" +
                                                     "\"phoneNumber\": \"\"," +
                                                     "\"mobileNumber\": \"" + orderContactInfo.MobilePhone + "\"," +
                                                     "\"email\": \"" + orderContactInfo.Email + "\"" +
                             "},";
                    if (!string.IsNullOrEmpty(oderTravel.PaxNote))
                    {
                        var passportInfo = oderTravel.PaxNote.Split(';');
                        if (passportInfo.Length > 1)
                        {
                            result += "\"passport\":{" +
                            "\"number\":\"" + passportInfo[0] + "\"," +
                            "\"expiryDate\":\"" + DateTime.Parse(passportInfo[2]).ToString("yyyy-MM-dd") + "\"," +
                            "\"issuingCountry\":{" +
                                    "\"code\":\"" + passportInfo[1] + "\"" +
                                              "}," +
                           "\"issuingCity\":\"\"," +
                           "\"issuingDate\":\"\"},";
                        }
                        else
                        {
                            result += "\"passport\":{" +
                            "\"number\":\"" + oderTravel.MilesCardNumber + "\"," +
                            "\"expiryDate\":\"\"," +
                            "\"issuingCountry\":{" +
                                    "\"code\":\"\"" +
                                              "}," +
                           "\"issuingCity\":\"\"," +
                           "\"issuingDate\":\"\"},";
                        }
                    }
                    result += "\"loyaltyProgram\":null," +
                           "\"preBoard\":false," +
                           "\"status\":{" +
                                       "\"active\":true," +
                                       "\"inactive\":false," +
                                       "\"denied\":false" +
                                       "}," +
                            "\"reference1\":\"\"," +
                            "\"reference2\":\"\"," +
                            "\"notes\": \"\"}," +
                           "\"weight\": null," +
                         "\"notes\": \"\"";
                    if (index <= infants.Count)
                    {
                        var inf = infants[index - 1];
                        result += ",\"infants\": [{" +
                            "\"index\":" + index + "," +
                            "\"reservationProfile\": {" +
                            "\"lastName\": \"" + inf.LastName + "\"," +
                        "\"firstName\": \"" + inf.FirstName + "\"," +
                        "\"title\": \"Infant\",";
                        if (inf.Gender == (int)Gender.Nam)
                            result += "\"gender\": \"Male\",";
                        else
                            result += "\"gender\": \"Female\",";
                        result += "\"birthDate\": \"" + inf.DateOfBirth?.ToString("yyyy-MM-dd") + "\"," +
                        "\"reference1\": \"\"," +
                        "\"reference2\": \"\"," +
                        "\"notes\": \"\"" +
                        "}," +
                        "\"AdvancePassengerInformation\": null," +
                        "\"notes\": \"\"" +
                        "}]";
                    }

                    result += "}";
                    index++;
                }
            }

            result += "]," +
            "\"journeys\": [" +
            "{" +
                "\"index\": 1," +
                "\"passengerJourneyDetails\": [";
            index = 1;
            string ancillaryPurchases = string.Empty;
            foreach (var oderTravel in orderTraveller)
            {
                if (oderTravel.TravellerType != (int)TravellerType.Infant)
                {
                    if (index > 1)
                        result += ",";
                    result += "{" +
                    "\"passenger\": {" +
                                "\"index\":" + index +
                    "}," +
                    "\"segment\": null," +
                    "\"bookingKey\":\"" + orderFlightSegment.ToList().FirstOrDefault(s => s.SegmentType != (int)SegmentType.NotApply).FareRule + "\"," +
                    "\"reservationStatus\": {" +
                        "\"confirmed\": true," +
                        "\"waitlist\": false," +
                        "\"standby\": false," +
                        "\"cancelled\": false," +
                        "\"open\": false," +
                        "\"finalized\": false," +
                        "\"external\": false" +
                    "}}";
                    var baggageOrder = orderBaggage.FirstOrDefault(x=>x.OrderTravellerId == oderTravel.Id);
                    if (baggageOrder != null && baggageOrder.KgOutBound > 0)
                    {
                        if (ancillaryPurchases.Equals(string.Empty))
                            ancillaryPurchases = await GetStringAncillaryOptions(index, orderFlightSegment.ToList().FirstOrDefault(s => s.GroupIndex == 1 
                                                                                                                    && s.SegmentType != (int)SegmentType.NotApply).FareRule, baggageOrder.KgOutBound, 1);
                        else
                            ancillaryPurchases += "," + await GetStringAncillaryOptions(index, orderFlightSegment.ToList().FirstOrDefault(s => s.GroupIndex == 1 
                                                                                                                    && s.SegmentType != (int)SegmentType.NotApply).FareRule, baggageOrder.KgOutBound, 1);
                        totalOrder += baggageOrder.PriceOutBound;
                    }
                    index++;
                }
            }
            result += "]}";
            if (orderFlightSegment.Count > 1)
            {
                index = 1;
                result += ",{" +
               "\"index\": 2," +
               "\"passengerJourneyDetails\": [";
                foreach (var oderTravel in orderTraveller)
                {
                    if (oderTravel.TravellerType != (int)TravellerType.Infant)
                    {
                        if (index > 1)
                            result += ",";
                        result += "{" +
                       "\"passenger\": {" +
                                   "\"index\": " + index +
                       "}," +
                       "\"segment\": null," +
                       "\"bookingKey\":\"" + orderFlightSegment.ToList().FirstOrDefault(s => s.GroupIndex == 2 &&
                                                                            s.SegmentType != (int)SegmentType.NotApply).FareRule + "\"," +
                       "\"reservationStatus\": {" +
                           "\"confirmed\": true," +
                           "\"waitlist\": false," +
                           "\"standby\": false," +
                           "\"cancelled\": false," +
                           "\"open\": false," +
                           "\"finalized\": false," +
                           "\"external\": false" +
                       "}}";
                        var baggageOrder = orderBaggage.FirstOrDefault(x => x.OrderTravellerId == oderTravel.Id);
                        if (baggageOrder != null && baggageOrder.KgInBound > 0)
                        {
                            if (ancillaryPurchases.Equals(string.Empty))
                                ancillaryPurchases = await GetStringAncillaryOptions(index, orderFlightSegment.ToList().FirstOrDefault(s => s.GroupIndex == 2 
                                                                                                            && s.SegmentType != (int)SegmentType.NotApply).FareRule, baggageOrder.KgInBound, 2);
                            else
                                ancillaryPurchases += "," + await GetStringAncillaryOptions(index, orderFlightSegment.ToList().FirstOrDefault(s => s.GroupIndex == 2 
                                                                                                            && s.SegmentType != (int)SegmentType.NotApply).FareRule, baggageOrder.KgInBound, 2);
                            totalOrder += baggageOrder.PriceInBound;
                        }
                        index++;
                    }
                }
                result += "]}";
            }
            result += "],";
            if (!ancillaryPurchases.Equals(string.Empty))
                result += "\"ancillaryPurchases\":[" + ancillaryPurchases + "],";
            result += "\"reservationStatus\": {" +
            "\"cancelled\": false," +
            "\"open\": false," +
            "\"finalized\": false," +
            "\"external\": false" +
    "},";
            if (payMethod == "PL")
            {
                result += "\"paymentTransactions\": [ " +
                        "{" +
                        "\"paymentMethod\": { " +
                        "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/paymentMethods/tfCeB5%C2%A5mircWvs2C4HkDdOXNJf%C6%92NFOopDW2yQCBh2p1rOTwFA5LN6VUgknLR%C2%A5uSRURzqRAo79Q%C2%A5yB9ni61HUMA==\"," +
                        "\"key\": \"tfCeB5¥mircWvs2C4HkDdOXNJfƒNFOopDW2yQCBh2p1rOTwFA5LN6VUgknLR¥uSRURzqRAo79Q¥yB9ni61HUMA==\"," +
                        "\"identifier\": \"PL\"," +
                        "\"description\": \"Pay Later\"" +
                        "}," +
                        "\"paymentMethodCriteria\": {" +
                        "\"thirdParty\": {" +
                        "\"clientIP\": \"192.168.0.0\"," +
                        "\"language\": {" +
                        "\"href\": \"https://intelisys-api.intelisys.ca/RESTv1/languages/EN\"," +
                        "\"code\": \"EN\"," +
                        "\"name\": \"English CN\"" +
                        "}," +
                        "\"applicationIdentifier\": \"\"," +
                        "\"redirectURL\": \"\"," +
                        "\"postURL\": \"\"," +
                        "\"postData\": \"\"," +
                        "\"reference\": \"\"" +
                        "}" +
               "}," +
               "\"currencyAmounts\": [" +
              "{" +
              "\"totalAmount\": " + totalOrder + "," +
              "\"currency\": {" +
                    "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/currencies/VND\"," +
                    "\"code\": \"VND\"," +
                    "\"description\": \"Vietnam Dong\"," +
                    "\"baseCurrency\": true" +
                "}," +
                "\"exchangeRate\": 1" +
                     "}" +
                "]," +
                "\"processingCurrencyAmounts\": [" +
                    "{" +
                        "\"totalAmount\": 0," +
                        "\"currency\": {" +
                            "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/currencies/VND\"," +
                            "\"code\": \"VND\"," +
                            "\"description\": \"Vietnam Dong\"," +
                            "\"baseCurrency\": true" +
                        "}," +
                        "\"exchangeRate\": 1" +
                    "}" +
                "]," +
                "\"payerDescription\": null," +
                "\"receiptNumber\": null," +
                "\"payments\": null," +
                "\"refundTransactions\": null," +
                "\"notes\": null}]";
            }
            else
            {
                var companie = await _ivietJetsService.GetCompanies();
                result += "\"paymentTransactions\": [{" +
                          "\"paymentMethod\": {" +
                          "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/paymentMethods/tfCeB5%C2%A5mircWvs2CC2%A59VaH1zFawFw==\"," +
                          "\"key\": \"tfCeB5¥mircWvs2C4HkDdOXNJfƒNFOopDW2yQCBh2p1¥CcncCLQNu3uhZGWzJkJUbmKK13BpWK¥9VaH1zFawFw==\"," +
                          "\"identifier\": \"AG\"," +
                          "\"description\": \"Agency Credit\"" +
                           "}," +
                           "\"paymentMethodCriteria\": {" +
                           "\"account\": {" +
                           "\"company\": {" +
                           "\"href\": \"https://vietjet-api.intelisys.ca/RESTv1/companies/i5M1ALKO4jozmgXmq3Cp8cSS...A5I69CE=\"," +
                           "\"key\": \"" + companie.key + "\"," +
                           "\"identifier\": null," +
                           "\"name\": null," +
                           "\"status\": null," +
                           "\"specifiedTaxConfiguration\": null," +
                           "\"taxLicense1\": null," +
                           "\"taxLicense2\": \"null\"," +
                           "\"purchaseOrderRequired\": null," +
                           "\"address\": null," +
                           "\"contactInformation\": null," +
                           "\"notes\": null," +
                           "\"account\": {" +
                           "\"accountNumber\": \"" + companie.account.accountNumber + "\"," +
                           "\"creditLimit\": 0," +
                           "\"creditAvailable\": 20717340.0," +
                           "\"currency\": {" +
                           "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/currencies/USD\"," +
                           "\"code\": \"VND\"," +
                           "\"description\": \"Vietnam Dong\"," +
                           "\"baseCurrency\": false," +
                           "\"currentExchangeRate\": 0.0," +
                           "\"format\": null" +
                           "},\"startDate\": null, \"endDate\": null, \"lastInvoiceDate\": null,\"privateFares\": null, \"timestamp\": null" +
                        "}" +
                     "}" +
                  "}" +
                "}," +
            "\"currencyAmounts\": [{" +
            "\"totalAmount\": " + totalOrder + "," +
            "\"currency\": {" +
            "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/currencies/VND\"," +
            "\"code\": \"VND\"," +
            "\"description\": \"Vietnam Dong\"," +
            "\"baseCurrency\": true" +
                "}," +
                "\"exchangeRate\": 1" +
                "}]," +
                "\"processingCurrencyAmounts\": [{" +
                                    "\"totalAmount\": 0," +
                "\"currency\": {" +
                "\"href\": \"https://vietjet-api.intelisystraining.ca/RESTv1/currencies/VND\"," +
                "\"code\": \"VND\"," +
                "\"description\": \"Vietnam Dong\"," +
                "\"baseCurrency\": true" +
                "}," +
                "\"exchangeRate\": 1" +
                "}]," +
                "\"payerDescription\": null," +
                "\"receiptNumber\": null," +
                "\"payments\": null," +
                "\"refundTransactions\": null," +
                "\"notes\": null" +
                "}]";
            }
            result += "}";
            return result;
        }
        private async Task<string> GetStringAncillaryOptions(int index, string bookingkey, int begkg, int outIn)
        {
            string result = string.Empty;
            string bagName = "Bag " + begkg + "kgs";
            var ancillaryOptions = await _ivietJetsService.GetAncillaryOptions(bookingkey);
            for (int i = 0; i < ancillaryOptions.Length; i++)
            {
                if (ancillaryOptions[i].ancillaryItem.name.Equals(bagName))
                {
                    result = "{\"purchaseKey\": \"" + ancillaryOptions[i].purchaseKey + "\"," +
                             "\"passenger\": {" +
                                        "\"index\":" + index + "}," +
                             "\"journey\": {" +
                             "\"index\":" + outIn + "}" +
                        "}";
                    return result;
                }
            }
            return result;
        }
    }
}
