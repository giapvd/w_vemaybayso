using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Infrastructure.Contexts; 
using ProtechGroup.Infrastructure.Entities;

namespace ProtechGroup.FlightBookingWeb.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}