using ProtechGroup.Application.Interfaces;
using System.Web.Mvc;
using System.Linq;
using System;
using ProtechGroup.Domain.Entities;
using ProtechGroup.FlightBookingWeb.Helpers;
using ProtechGroup.FlightBookingWeb.Models;
using ProtechGroup.Domain;

namespace ProtechGroup.FlightBookingWeb.Controllers
{
    public class AirportController : BaseController
    {
        private readonly IAirportService _airportService;
        private readonly ISearchInputService _searchInputService;

        public AirportController(IAirportService airportService, 
                                ISearchInputService searchInputService)
        {
            _airportService = airportService;
            _searchInputService = searchInputService;
        }

        [HttpGet]
        public JsonResult SearchByKey(string data)
        {
            var para = GetValueParaEncryptHelper(data);
            string keyword = para["keywordSearch"];
            var airports = _airportService.SearchAirports(keyword, 30)
                .Where(a => a.CityName != "")
                .Select(a => new
                {
                    a.AirportCode,
                    a.AirportName,
                    a.CityName,
                    a.CountryName
                }).ToList();
            return Json(airports, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangeDatesearchflight(ChangDateSearchInput request)
        {
            var searchInput = _searchInputService.GetByKeySessionId(Convert.ToInt32(request.SessionChange));
            searchInput.SessionId = _searchInputService.GetNextSessionId();
            var departureAirport = _airportService.GetAirportByCode(searchInput.DepartureAirport);
            var arrivalAirport = _airportService.GetAirportByCode(searchInput.ArrivalAirport);
            if (request.WayTypeChange == (int)WayType.OutBound)
            {
                searchInput.DepartureDate = Convert.ToDateTime(request.DateChange);
            }   
            else
            {
                searchInput.ReturnDate = Convert.ToDateTime(request.DateChange);
            }
            var searchInputMod = _searchInputService.Insert(searchInput);
            var dataEncrypt = GetEncryptQuery("sessionId=" + searchInputMod.SessionId);
            return Json(new { dataResult = dataEncrypt }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Cheksearchflightinput(FlightSearchRequest request)
        {
            var validationResult = ValidateFlightRequest(request);
            if (validationResult.IsValid)
            {
                var searchInputMod = new SearchInputMod();
                searchInputMod.SessionId = _searchInputService.GetNextSessionId();
                var departureAirport = _airportService.GetAirportByCode(request.departure);
                var arrivalAirport = _airportService.GetAirportByCode(request.arrival);
                if (request.roundType == 0)
                    searchInputMod.IsRoundTrip = false;
                else
                {
                    searchInputMod.IsRoundTrip = true;
                    searchInputMod.ReturnDate = Convert.ToDateTime(request.returnDate);
                }
                searchInputMod.DepartureAirport = request.departure.Trim();
                searchInputMod.ArrivalAirport = request.arrival.Trim();
                searchInputMod.DepartureCity = departureAirport.CityName;
                searchInputMod.ArrivalCity = arrivalAirport.CityName;
                searchInputMod.DepartureDate = Convert.ToDateTime(request.departureDate);
                searchInputMod.RequestMaxResult = 0;
                searchInputMod.AdultNumber = request.countAdt;
                searchInputMod.ChildNumber = request.countChd;
                searchInputMod.InfantNumber = request.countInf;
                byte totalPax = (byte)(request.countAdt + request.countChd + request.countInf);
                searchInputMod.TotalPax = totalPax;
                searchInputMod.DateTimeInsert = DateTime.Now;
                if (departureAirport.CountryCode.Equals("VN") && arrivalAirport.CountryCode.Equals("VN"))
                    searchInputMod.IsSearchDomestic = true;
                else
                    searchInputMod.IsSearchDomestic = false;
                searchInputMod.UserId = 0;
                searchInputMod.IPAddress = clientInfo.IpAddress;
                searchInputMod.SearchIndividual = false;
                searchInputMod.AirlineCode = clientInfo.UserAgent;
                searchInputMod.Price = 0;
                var searchInput = _searchInputService.Insert(searchInputMod);
                var dataEncrypt = GetEncryptQuery("sessionId=" + searchInput.SessionId);
                return Json(new { dataResult = dataEncrypt }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { dataResult = string.Empty }, JsonRequestBehavior.AllowGet);
        }
        private ValidationResult ValidateFlightRequest(FlightSearchRequest request)
        {
            var result = new ValidationResult();

            // 1. Check departure airport
            var depAirport = _airportService.GetAirportByCode(request.departure);
            if (depAirport == null)
                result.Errors.Add("Mã sân bay đi không đúng.");

            // 2. Check arrival airport
            var arrAirport = _airportService.GetAirportByCode(request.arrival);
            if (arrAirport == null)
                result.Errors.Add("Mã sân bay đến không đúng.");

            // 3. Check departureDate
            if (request.departureDate == default(DateTime))
                result.Errors.Add("Ngày tháng đi không đúng.");

            // 4. Check returnDate (null hoặc DateTime hợp lệ)
            if (request.returnDate.HasValue && request.returnDate.Value == default(DateTime))
                result.Errors.Add("Ngày tháng về không đúng.");

            // 5. Check số nguyên: roundType, countAdt, countChd, countInf
            if (request.roundType < 0)
                result.Errors.Add("Round type is invalid.");

            if (request.countAdt < 0)
                result.Errors.Add("Adult count is invalid.");

            if (request.countChd < 0)
                result.Errors.Add("Child count is invalid.");

            if (request.countInf < 0)
                result.Errors.Add("Infant count is invalid.");

            result.IsValid = !result.Errors.Any();
            return result;
        }
    }
}