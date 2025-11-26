using ProtechGroup.Application.Interfaces;
using System.Web.Mvc;
using System.Linq;
using ProtechGroup.FlightBookingWeb.Helpers;
using ProtechGroup.FlightBookingWeb.Models;
using System.Collections.Generic;
using ProtechGroup.Domain;
using System.Web;
using System;
using ProtechGroup.Application.Services;


namespace ProtechGroup.FlightBookingWeb.Controllers
{
    public class OrderBookingController : BaseController
    {
        // GET: OrderBooking
        private readonly IMethodService _methodServie;
        private readonly ISearchInputService _searchInputService;
        private readonly IPriceBaggageService _priceBaggageService;
        public OrderBookingController(IMethodService methodServie,
                            ISearchInputService searchInputService,
                            IPriceBaggageService priceBaggageService)
        {
            _methodServie = methodServie;
            _searchInputService = searchInputService;
            _priceBaggageService = priceBaggageService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCusInformation(string data)
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
                string airCodeOut = values["airCodeOut"];
                string booKingOut = values["booKingOut"];
                string airCodeIn = values["airCodeIn"];
                string booKingIn = values["booKingIn"];
                var searchInput = _searchInputService.GetByKeySessionId(sessionId);
                if (searchInput.IPAddress.Equals(clientInfo.IpAddress))
                {
                    var modelView = new List<GroupFlight>();
                    switch (airCodeOut.ToUpper())
                    {
                        case "VN":
                        case "BL":
                            var groupFlightVN = _methodServie.GetGroupFlightVNARow(sessionId, booKingOut, (int)WayType.OutBound);
                            modelView.Add(groupFlightVN);
                            break;
                        case "VJ":
                            var groupFlightVJ = _methodServie.GetGroupFlightVJRow(sessionId, booKingOut, (int)WayType.OutBound);
                            modelView.Add(groupFlightVJ);
                            break;
                        case "QH":
                            var groupFlightQH = _methodServie.GetGroupFlightQHRow(sessionId, booKingOut, (int)WayType.OutBound);
                            modelView.Add(groupFlightQH);
                            break;
                    }
                    if (searchInput.IsRoundTrip)
                    {
                        switch (airCodeIn.ToUpper())
                        {
                            case "VN":
                            case "BL":
                                var groupFlightVN = _methodServie.GetGroupFlightVNARow(sessionId, booKingIn, (int)WayType.InBound);
                                modelView.Add(groupFlightVN);
                                break;
                            case "VJ":
                                var groupFlightVJ = _methodServie.GetGroupFlightVJRow(sessionId, booKingIn, (int)WayType.InBound);
                                modelView.Add(groupFlightVJ);
                                break;
                            case "QH":
                                var groupFlightQH = _methodServie.GetGroupFlightQHRow(sessionId, booKingIn, (int)WayType.InBound);
                                modelView.Add(groupFlightQH);
                                break;
                        }
                    }
                    if (modelView != null && modelView.Count > 0)
                    {
                        ViewBag.SearchInput = searchInput;
                        ViewBag.ListBagOut = _priceBaggageService.GetPriceBaggageByAirlineCode(airCodeOut.ToUpper());
                        if (searchInput.IsRoundTrip)
                            ViewBag.ListBagIn = _priceBaggageService.GetPriceBaggageByAirlineCode(airCodeIn.ToUpper());
                        return View(modelView);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Truy cập không hợp lệ";
                        return View("Error");
                    }
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
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChekSelectFlight(List<SelectFlightRequest> request)
        {
            var validateSelectFlightRequest = ValidateSelectFlightRequest(request);
            if (validateSelectFlightRequest.IsValid)
            {
                string strRequest = "sessionId={Session}&airCodeOut={AirCodeOut}&booKingOut={BooKingOut}"+
                                    "&airCodeIn={AirCodeIn}&booKingIn={BooKingIn}";
                foreach (var req in request) {
                    var searchIput = _searchInputService.GetByKeySessionId(Convert.ToInt32(req.SessionId));
                    if (searchIput.IPAddress.Equals(clientInfo.IpAddress))
                    {
                        strRequest = strRequest.Replace("{Session}", req.SessionId);
                        if (req.WayType == (int)WayType.OutBound)
                        {
                            strRequest = strRequest.Replace("{AirCodeOut}", req.AirlineCode);
                            strRequest = strRequest.Replace("{BooKingOut}", req.BookingKey);
                        }
                        else
                        {
                            strRequest = strRequest.Replace("{AirCodeIn}", req.AirlineCode);
                            strRequest = strRequest.Replace("{BooKingIn}", req.BookingKey);
                        }
                    }
                    else
                    {
                        return Json(new { status = false, dataResult = string.Empty }, JsonRequestBehavior.AllowGet);
                    }
                }
                var dataEncrypt = GetEncryptQuery(strRequest);
                return Json(new { status = true, dataResult = dataEncrypt }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = false, dataResult = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        private ValidationResult ValidateSelectFlightRequest(List<SelectFlightRequest> request)
        {
            ValidationResult result = new ValidationResult();
            foreach (var item in request) {
                if(item.SessionId == null || string.IsNullOrEmpty(item.SessionId)) 
                    result.Errors.Add("Invalid SessionId.");
                if(item.AirlineCode == null || string.IsNullOrEmpty(item.AirlineCode))
                    result.Errors.Add("Invalid AirlineCode.");
                if (item.BookingKey == null || string.IsNullOrEmpty(item.BookingKey))
                    result.Errors.Add("Invalid BookingKey.");
                if (item.WayType < 0 || item.WayType > 1)
                    result.Errors.Add("Invalid BookingKey.");
                result.IsValid = !result.Errors.Any();
                if(!result.IsValid)
                    return result;
            }
            return result;
        }
    }
}