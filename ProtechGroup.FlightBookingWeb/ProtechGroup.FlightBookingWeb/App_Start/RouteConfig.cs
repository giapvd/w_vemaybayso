using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProtechGroup.FlightBookingWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Timkiemchuyenbay",
                url: "Timkiemchuyenbay/{data}",
                defaults: new { controller = "SearchFlight", action = "ResultsFlightDomestic", data = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Timkiemsanbay",
                url: "Timkiemsanbay/{data}",
                defaults: new { controller = "Airport", action = "SearchByKey", data = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Thongtinchuyenbay",
                url: "Thongtinchuyenbay/{data}",
                defaults: new { controller = "SearchFlight", action = "GetFilghtInfor", data = UrlParameter.Optional }
            );
            
            routes.MapRoute(
               name: "Trangchu",
               url: "Trangchu",
               defaults: new { controller = "Home", action = "Index" }
           );
           routes.MapRoute(
               name: "Kiemtrathongtinsanbay",
               url: "Kiemtrathongtinsanbay",
               defaults: new { controller = "Base", action = "EncryptQuery" }
           );
           routes.MapRoute(
              name: "Kiemtrathongtintimkiemchuyenbay",
              url: "Kiemtrathongtintimkiemchuyenbay",
              defaults: new { controller = "Airport", action = "Cheksearchflightinput" }
            );
            routes.MapRoute(
              name: "Thaydoingaybay",
              url: "Thaydoingaybay",
              defaults: new { controller = "Airport", action = "ChangeDatesearchflight" }
            );
            routes.MapRoute(
              name: "Chonchuyenbay",
              url: "Chonchuyenbay",
              defaults: new { controller = "OrderBooking", action = "ChekSelectFlight" }
            );
            routes.MapRoute(
              name: "Xacnhanthongtinkhachhang",
              url: "Xacnhanthongtinkhachhang",
              defaults: new { controller = "OrderBooking", action = "ConfirmCusInformation"}
            );
            routes.MapRoute(
              name: "Checkcreatebooking",
              url: "Checkcreatebooking",
              defaults: new { controller = "Payment", action = "CreateBookingOrder"}
            );
            routes.MapRoute(
              name: "Thanhtoandonhang",
              url: "Thanhtoandonhang/{data}",
              defaults: new { controller = "Payment", action = "PaymentMethodOrder", data = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Ketquatimkiem",
              url: "Ketquatimkiem",
              defaults: new { controller = "SearchFlight", action = "ResultsFlightDomestic"}
            );
            routes.MapRoute(
                 name: "Default",
                 url: "",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}
