using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProtechGroup.FlightBookingWeb.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString EncryptedActionLink(this HtmlHelper htmlHelper,
            string linkText, string routeName, object routeValues)
        {
            // Serialize & encrypt routeValues
            var queryString = string.Join("&",
                routeValues.GetType().GetProperties()
                    .Select(p => $"{p.Name}={p.GetValue(routeValues)}"));

            var encrypted = UrlEncryptHelper.Encrypt(queryString);

            // Tạo url theo route name
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = urlHelper.RouteUrl(routeName, new { data = encrypted });

            var builder = new TagBuilder("a");
            builder.InnerHtml = linkText;
            builder.Attributes["href"] = url;

            return MvcHtmlString.Create(builder.ToString());
        }
        /// <summary>
        /// Trả về chuỗi mã hóa từ query string để dùng trong Ajax (ví dụ: keywordSearch=abc)
        /// </summary>
        public static string EncryptQuery(this HtmlHelper htmlHelper, string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return string.Empty;

            return UrlEncryptHelper.Encrypt(queryString);
        }
    }
}