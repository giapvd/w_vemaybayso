using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Concurrent;

namespace ProtechGroup.FlightBookingWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Xử lý Exception toàn cục
            filters.Add(new HandleErrorAttribute());

            // Thêm filter bảo mật
            filters.Add(new SecurityFilter());

            // Có thể thêm Logging filter nếu muốn
            filters.Add(new LoggingFilter());
            // Thêm RateLimit filter chống DoS (mỗi IP tối đa 100 request / phút)
            filters.Add(new RateLimitFilter(limit: 100, seconds: 60));

            filters.Add(new HandleErrorAttribute
            {
                View = "Error"  // Bắt mọi exception chuyển về trang Error.cshtml
            });
        }
    }
    /// <summary>
    /// Filter bảo mật: thêm các header chống XSS, Clickjacking, MIME sniffing
    /// </summary>
    public class SecurityFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            // Chống Clickjacking
            if (!response.Headers.AllKeys.Contains("X-Frame-Options"))
                response.Headers.Add("X-Frame-Options", "DENY");

            // Chống MIME sniffing
            if (!response.Headers.AllKeys.Contains("X-Content-Type-Options"))
                response.Headers.Add("X-Content-Type-Options", "nosniff");

            // Chống XSS
            if (!response.Headers.AllKeys.Contains("X-XSS-Protection"))
                response.Headers.Add("X-XSS-Protection", "1; mode=block");

            base.OnResultExecuting(filterContext);
        }
    }

    /// <summary>
    /// Filter logging đơn giản ghi log request và error
    /// </summary>
    public class LoggingFilter : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            string log = $"[{System.DateTime.Now}] Request: {request.HttpMethod} {request.RawUrl}";
            System.Diagnostics.Debug.WriteLine(log);

            base.OnActionExecuting(filterContext);
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                string errorLog = $"[{System.DateTime.Now}] ERROR: {filterContext.Exception.Message}";
                System.Diagnostics.Debug.WriteLine(errorLog);

                // Có thể lưu vào DB hoặc file log tại đây
            }
        }
    }
    /// <summary>
    /// Filter giới hạn số request để chống DoS
    /// </summary>
    public class RateLimitFilter : ActionFilterAttribute
    {
        private static readonly ConcurrentDictionary<string, RequestCounter> Requests =
            new ConcurrentDictionary<string, RequestCounter>();

        private readonly int _limit;
        private readonly int _seconds;

        public RateLimitFilter(int limit = 60, int seconds = 60)
        {
            _limit = limit;     // số request tối đa
            _seconds = seconds; // trong bao nhiêu giây
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var counter = Requests.GetOrAdd(ip, new RequestCounter());

            lock (counter)
            {
                if ((DateTime.UtcNow - counter.Start).TotalSeconds > _seconds)
                {
                    counter.Count = 0;
                    counter.Start = DateTime.UtcNow;
                }

                counter.Count++;

                if (counter.Count > _limit)
                {
                    filterContext.Result = new ContentResult
                    {
                        Content = "Too many requests. Please try again later."
                    };
                    filterContext.HttpContext.Response.StatusCode = 429; // Too Many Requests
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private class RequestCounter
        {
            public int Count { get; set; } = 0;
            public DateTime Start { get; set; } = DateTime.UtcNow;
        }
    }
}
