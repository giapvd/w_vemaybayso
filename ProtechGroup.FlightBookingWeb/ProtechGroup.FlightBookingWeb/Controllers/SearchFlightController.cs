using ProtechGroup.FlightBookingWeb.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using ProtechGroup.Application.Interfaces;
using System.Threading.Tasks;
using ProtechGroup.Domain;

namespace ProtechGroup.FlightBookingWeb.Controllers
{
    public class SearchFlightController : BaseController
    {
        private readonly IMethodService _methodServie;
        private readonly ISearchInputService _searchInputService;
        public SearchFlightController(IMethodService methodServie,
                                      ISearchInputService searchInputService
                                     )
        {
            _methodServie = methodServie;
            _searchInputService = searchInputService;
        }
        // GET: SearchFlight
        public async Task<ActionResult> ResultsFlightDomestic(string data)
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
                var sessionId = values["sessionId"];
                var searchInput = _searchInputService.GetByKeySessionId(Convert.ToInt32(sessionId));
                if (searchInput.IPAddress.Equals(clientInfo.IpAddress))
                {
                    if (searchInput.IsSearchDomestic)
                    {
                        var flightSearch = await _methodServie.GetFlightDomestic(Convert.ToInt32(sessionId));
                        ViewBag.SearchInput = searchInput;
                        return View(flightSearch);
                    }
                    else
                    {
                        return RedirectToAction("/Ketquachuyenbayquocte");
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
        [HttpGet]
        public JsonResult GetFilghtInfor(string airLineCode, int sessionId, string bookingKey, int WayType)
        {
            var groupFlight = new GroupFlight();
            if(airLineCode.Equals("VN") || airLineCode.Equals("BL"))
                groupFlight = _methodServie.GetGroupFlightVNARow(sessionId, bookingKey, WayType);
            if (airLineCode.Equals("VJ"))
                groupFlight = _methodServie.GetGroupFlightVJRow(sessionId, bookingKey, WayType);
            if(airLineCode.Equals("QH"))
                groupFlight = _methodServie.GetGroupFlightQHRow(sessionId, bookingKey, WayType);
            return Json(groupFlight, JsonRequestBehavior.AllowGet);
        }
    }
}