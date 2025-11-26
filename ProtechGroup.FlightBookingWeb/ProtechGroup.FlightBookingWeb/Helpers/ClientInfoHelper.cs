using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtechGroup.FlightBookingWeb.Helpers
{
    public class ClientInfo
    {
        public string IpAddress { get; set; }
        public string UserAgent { get; set; } 
    }
    public static class ClientInfoHelper
    {
        public static ClientInfo GetClientInfo()
        {
            var request = HttpContext.Current?.Request;

            if (request == null)
                return new ClientInfo { IpAddress = "Unknown", UserAgent = "Unknown" };

            // Lấy IP
            string ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] addresses = ip.Split(',');
                if (addresses.Length > 0)
                {
                    ip = addresses[0];
                }
            }
            else
            {
                ip = request.ServerVariables["REMOTE_ADDR"];
            }

            string userAgent = request.UserAgent ?? "Unknown";

            return new ClientInfo
            {
                IpAddress = ip,
                UserAgent = userAgent
            };
        }
    }
}