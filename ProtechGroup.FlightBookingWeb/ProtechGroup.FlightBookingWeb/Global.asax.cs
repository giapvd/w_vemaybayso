using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProtechGroup.FlightBookingWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        /// <summary>
        /// Chặn lộ thông tin phiên bản server/framework
        /// </summary>
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
        /// <summary>
        /// Bắt buộc HTTPS
        /// </summary>
        protected void Application_BeginRequest()
        {
            if (!Context.Request.IsSecureConnection)
            {
                string redirectUrl = Context.Request.Url.ToString().Replace("http:", "https:");
                Response.Redirect(redirectUrl, true);
            }
        }
        /// <summary>
        /// Bảo mật Session & Cookie
        /// </summary>
        protected void Session_Start(Object sender, EventArgs e)
        {
            // Đặt timeout session
            Session.Timeout = 20; // 20 phút

            // Thiết lập cookie an toàn
            if (Response.Cookies[".ASPXAUTH"] != null)
            {
                Response.Cookies[".ASPXAUTH"].HttpOnly = true;
                Response.Cookies[".ASPXAUTH"].Secure = true; // Chỉ cho HTTPS
                Response.Cookies[".ASPXAUTH"].SameSite = SameSiteMode.Strict;
            }
        }
    }
    /// <summary>
    /// Filter bảo mật bổ sung chống XSS, Clickjacking, MIME sniffing
    /// </summary>
    
}
