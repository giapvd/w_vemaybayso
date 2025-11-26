using Newtonsoft.Json.Linq;
using ProtechGroup.FlightBookingWeb.Helpers;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace ProtechGroup.FlightBookingWeb.Controllers
{
    public class BaseController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public ClientInfo clientInfo = new ClientInfo();
        public BaseController()
        {
            clientInfo = ClientInfoHelper.GetClientInfo();
        }

        // GET: Base
        public NameValueCollection GetValueParaEncryptHelper(string data)
        {
            var decrypted = UrlEncryptHelper.Decrypt(data);
            return HttpUtility.ParseQueryString(decrypted);
        }
        public string GetEncryptQuery(string plainText)
        {
            return UrlEncryptHelper.Encrypt(plainText);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EncryptQuery(string plainText)
        {
            
            if (!string.IsNullOrEmpty(plainText))
            {
                plainText = "keywordSearch=" + plainText;
                return Content(GetEncryptQuery(plainText));
            }
            return Content(string.Empty);
        }
       
        public static async Task<(string CountryCode, string City)> GetLocationFromIpAsync(string ip)
        {
            try
            {
                string url = $"http://ip-api.com/json/{ip}?fields=status,countryCode,city";

                var response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);

                if (json["status"]?.ToString() == "success")
                {
                    string countryCode = json["countryCode"]?.ToString();
                    string city = json["city"]?.ToString();
                    return (countryCode, city);
                }

                return (string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                return (string.Empty, string.Empty);
            }
        }
    }
}