using ProtechGroup.Domain;
using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace ProtechGroup.Application.Common
{
    public partial class CoreUtils
    {
        public static string SendNotifyTelegram(string messager)
        {
            string token = "5441729164:AAEk9Rh0j3cIsoHUF9echctLUz9xsLu41W4";
            //string idGroup = "-460593983";//old
            string idGroup = "-1001913132852";
            string urlsend = "https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={destID}&text={text}";

            urlsend = urlsend.Replace("{apilToken}", token);
            urlsend = urlsend.Replace("{destID}", idGroup);
            urlsend = urlsend.Replace("{text}", messager);
            using (var webClient = new WebClient())
            {
                try
                {
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    return webClient.DownloadString(urlsend);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

        }
        public static string FormatPrice(object price)
        {
            if (Convert.ToDecimal(price) == 0) return "0";
            if (GetCurrencyCurrent() + "" == "VND")
                return String.Format("{0:0,0}", price);
            else
                return price + "";
        }
        public static string GetCurrencyCurrent()
        {
            return "VND";
        }

        public static string GetComputerName()
        {
            return Environment.MachineName;
        }
        
        public static void RefreshCurrentPage()
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }

        public static bool IsLocalHostByUrl()
        {
            bool isLocalHost = false;
            string url = GetUrl().ToLower();
            if (url.IndexOf("localhost") >= 0 || url.IndexOf("pc") >= 0 || url.IndexOf("192.168.") >= 0 || url.IndexOf("10.0.") >= 0)
                isLocalHost = true;
            return isLocalHost;
        }


        #region URL

        public static object GetUrlReferrer()
        {
            try
            {
                return HttpContext.Current.Request.UrlReferrer;
            }
            catch
            {
                return null;
            }
        }

        public static void SaveUrlReferrer()
        {
            HttpContext.Current.Session["UrlReferrer"] = GetUrlReferrer();
        }

        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static string GetPageNameRawUrl()
        {
            string rawUrl = HttpContext.Current.Request.Url.AbsolutePath; //.RawUrl;
            string pageName = rawUrl;
            return pageName;
        }

        #endregion

        #region Redirect

        /// <summary>
        /// Do not forget call SaveUrlReferrer(); in if (!IsPostBack)
        /// </summary>

        public static void RedirectToCorrectPage()
        {
            string url = HttpContext.Current.Request.Url.ToString().Trim().ToLower();
            int index = url.IndexOf("gclid=");
            if (index > 0)
            {
                url = url.Substring(0, index);
                HttpContext.Current.Response.Redirect(url);
            }

        }

        public static void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

        public static void RedirectToCurrentPage()
        {
            Redirect(HttpContext.Current.Request.Url.ToString());
        }

        #endregion



        #region Get Value From HttpRequest

        public static string GetValueFormByKey(string keyname)
        {
            string value = HttpContext.Current.Request.Form[keyname];
            return value;
        }

        public static string GetValueFormByKeyContain(string serverControlId)
        {
            HttpRequest request = HttpContext.Current.Request;
            string temp = null;
            string clientName = String.Empty;

            int countkey = request.Form.Count;
            for (int i = 0; i < countkey; i++)
            {
                string key = request.Form.GetKey(i);
                if (key.IndexOf(serverControlId) >= 0)
                {
                    clientName = key;
                    break;
                }
            }
            if (!clientName.Equals(string.Empty))
                temp = request.Form.Get(clientName);
            return temp;
        }

        #endregion

        #region Cookie

        public static void ClearAllCookies()
        {
            string[] cookies = HttpContext.Current.Request.Cookies.AllKeys;
            var response = HttpContext.Current.Response;
            foreach (string cookie in cookies)
            {
                RemoveCookie(cookie);
            }
            RefreshCurrentPage();
        }

        public static object GetCookieValue(string cookieName)
        {
            object cookievalue = null;
            if (HttpContext.Current.Request.Cookies.Get(cookieName) != null)
            {
                var httpCookie = HttpContext.Current.Request.Cookies.Get(cookieName);
                if (httpCookie != null)
                    cookievalue = HttpContext.Current.Server.UrlDecode(httpCookie.Value);
            }
            return cookievalue;
        }

        public static void SetCookie(string cookieName, string cookievalue, int totalDayExpire)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpContext.Current.Server.UrlEncode(cookievalue);
            cookie.Expires = DateTime.Today.AddDays(totalDayExpire);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void SetCookieBySeconds(string cookieName, string cookievalue, int totalSecExpire)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpContext.Current.Server.UrlEncode(cookievalue);
            cookie.Expires = DateTime.Today.AddSeconds(totalSecExpire);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void SetCookieByMinutes(string cookieName, string cookievalue, int totalMinExpire)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpContext.Current.Server.UrlEncode(cookievalue);
            cookie.Expires = DateTime.Now.AddMinutes(totalMinExpire);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void RemoveCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                var c = new HttpCookie(cookieName);
                c.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(c);
            }

        }
        #endregion

        #region Other
        public static string SetFirstCharUpper(string content, bool isToLowerTheRest)
        {
            string temp = content.Trim();
            if (temp.Length > 0)
            {
                if (isToLowerTheRest)
                    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1).ToLower();
                else
                    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1);
            }
            return temp;
        }
        public static int GetOrderIdReal(int orderId)
        {
            if (orderId.ToString().Length < 9)
                return orderId;
            return Convert.ToInt32(orderId.ToString().Substring(4));
        }

        public static bool IsAdminArea()
        {
            bool isBackEnd = false;
            string url = GetUrl().ToLower().Replace("\\", "/");
            if (url.IndexOf("tkt.vemaybayso.vn") >= 0)
                isBackEnd = true;
            return isBackEnd;
        }

        public static bool IsPublicWebsite()
        {
            return !IsAdminArea();
        }


        public static string GetFileNameWithExtentionFromFilePath(string path)
        {
            string temp = string.Empty;
            path = path.Replace("/", "\\");
            int lastIndex = path.LastIndexOf("\\");
            if (lastIndex > 0)
                temp = path.Substring(lastIndex + 1);
            return temp;
        }

        public static string GetMathRound(Decimal number, int digit)
        {
            string temp = "#0.";
            if (digit > 1)
            {
                for (int i = 1; i <= digit; i++)
                {
                    temp += "0";
                }
                return number.ToString(temp);
            }
            else
            {
                return Convert.ToInt32(number).ToString();
            }
        }

        public static string GetUserAgent()
        {
            return HttpContext.Current.Request.UserAgent;
        }


        public static string GetIpAddress()
        {
            try
            {
                return HttpContext.Current.Request.UserHostAddress;
                //string ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //if (ipaddress == "" || ipaddress == null)
                //{
                //    ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //}
                //return ipaddress;
            }
            catch (Exception)
            {
                return "1.1.1.1";
            }
        }

        /// <summary>
        /// Ví dụ trả về: http://localhost:8089, http://www.vemaybayso.vn
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetDomainCurrent()
        {
            return HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host +
                   (HttpContext.Current.Request.Url.Port != 80 ? ":" + HttpContext.Current.Request.Url.Port : "");
        }

        #endregion
       
        public static string GetLangTravellerNoTuoi(TravellerType travellerType)
        {
            switch (travellerType)
            {
                case TravellerType.Adult:
                    return "Người lớn";
                case TravellerType.Child:
                    return "Trẻ em";
                case TravellerType.Infant:
                    return "Em bé";
                default:
                    return string.Empty;
            }
        }
        public static string GetLangTraveller(TravellerType travellerType)
        {
            switch (travellerType)
            {
                case TravellerType.Adult:
                    return "NGƯỜI LỚN(TỪ 12 TUỔI TRỞ LÊN)";
                case TravellerType.Child:
                    return "TRẺ EM(TỪ 2 ĐẾN DƯỚI 12 TUỔI)";
                case TravellerType.Infant:
                    return "EM BÉ(DƯỚI 2 TUỔI)";
                default:
                    return string.Empty;
            }
        }

        public static string GetLangDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Thứ 2";
                case DayOfWeek.Tuesday:
                    return "Thứ 3";
                case DayOfWeek.Wednesday:
                    return "Thứ 4";
                case DayOfWeek.Thursday:
                    return "Thứ 5";
                case DayOfWeek.Friday:
                    return "Thứ 6";
                case DayOfWeek.Saturday:
                    return "Thứ 7";
                case DayOfWeek.Sunday:
                    return "Chủ Nhật";
                default:
                    return string.Empty;
            }
        }

        public static string sVnVowelsTone = "đăâêôơưáắấéếíóốớúứýàằầèềìòồờùừỳảẳẩẻểỉỏổởủửỷãẵẫẽễĩõỗỡũữỹạặậẹệịọộợụựỵ";
        public static string sVnVowelsNoTone = "daaeoouaaaeeiooouuyaaaeeiooouuyaaaeeiooouuyaaaeeiooouuyaaaeeiooouuy";

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static string ConvertDecimalToString(decimal number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return str + "đồng chẵn";
        }
        //---Chuyển dương lịch sang âm lịch VN
        public static DateTime ConvertSolarToLunarVN(DateTime solarDate)
        {
            int timeZone = 7; // Việt Nam UTC+7
            int day = solarDate.Day;
            int month = solarDate.Month;
            int year = solarDate.Year;

            int jd = ConvertToJulius(solarDate);

            int k = (int)((jd - 2415021.076998695) / 29.530588853);
            int monthStart = GetNewMoonDay(k + 1, timeZone);

            if (monthStart > jd)
                monthStart = GetNewMoonDay(k, timeZone);

            int a11 = GetLunarMonth11(year, timeZone);
            int b11 = GetLunarMonth11(year + 1, timeZone);

            int lunarYear = 0;
            int lunarMonth = 0;

            if (monthStart < a11)
            {
                lunarYear = year;
                a11 = GetLunarMonth11(year - 1, timeZone);
            }
            else
            {
                lunarYear = year + 1;
            }

            int lunarDay = jd - monthStart + 1;
            int diff = (int)Math.Round((monthStart - a11) / 29.530588853);
            lunarMonth = diff + 11;

            if (lunarMonth > 12)
                lunarMonth -= 12;

            return new DateTime(lunarYear, lunarMonth, lunarDay);
        }
        private static double NewMoon(int k)
        {
            double T = k / 1236.85;
            double T2 = T * T;
            double T3 = T2 * T;
            double dr = Math.PI / 180;
            double Jd1 = 2415020.75933 + 29.53058868 * k +
                0.0001178 * T2 - 0.000000155 * T3 +
                0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr);
            double M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3;
            double Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3;
            double F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3;

            double C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr)
                + 0.0021 * Math.Sin(2 * M * dr)
                - 0.4068 * Math.Sin(Mpr * dr)
                + 0.0161 * Math.Sin(2 * Mpr * dr)
                - 0.0004 * Math.Sin(3 * Mpr * dr)
                + 0.0104 * Math.Sin(2 * F * dr)
                - 0.0051 * Math.Sin((M + Mpr) * dr)
                - 0.0074 * Math.Sin((M - Mpr) * dr)
                + 0.0004 * Math.Sin((2 * F + M) * dr)
                - 0.0004 * Math.Sin((2 * F - M) * dr)
                - 0.0006 * Math.Sin((2 * F + Mpr) * dr)
                + 0.0010 * Math.Sin((2 * F - Mpr) * dr)
                + 0.0005 * Math.Sin((2 * Mpr + M) * dr);

            double JdNew = Jd1 + C1;
            return JdNew;
        }

        private static double SunLongitude(double jdn)
        {
            double T = (jdn - 2451545.0) / 36525;
            double T2 = T * T;
            double dr = Math.PI / 180;

            double M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2;
            double L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2;

            double DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M)
                + (0.019993 - 0.000101 * T) * Math.Sin(dr * (2 * M))
                + 0.000290 * Math.Sin(dr * (3 * M));

            double L = L0 + DL;
            L = (L + 360) % 360;
            return L;
        }

        private static int GetSunLongitudeSector(double jdn)
        {
            return (int)(SunLongitude(jdn) / 30);
        }

        private static int GetNewMoonDay(int k, int timeZone)
        {
            return (int)(NewMoon(k) + 0.5 + timeZone / 24.0);
        }

        private static int GetLunarMonth11(int year, int timeZone)
        {
            DateTime off = new DateTime(year, 12, 31);
            int jd = ConvertToJulius(off);
            int k = (int)((jd - 2415021.076998695) / 29.530588853);
            int nm = GetNewMoonDay(k, timeZone);
            int sun = GetSunLongitudeSector(nm);

            if (sun >= 9)
                nm = GetNewMoonDay(k - 1, timeZone);

            return nm;
        }

        private static int ConvertToJulius(DateTime date)
        {
            int a = (14 - date.Month) / 12;
            int y = date.Year + 4800 - a;
            int m = date.Month + 12 * a - 3;

            return date.Day + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 + y / 400 - 32045;
        }
        //----End Chuyển dương lịch sang âm lịch VN
        public static bool IsFileEmptyOrDoesntExist(string virtualFilePath)
        {
            return GetFileSize(virtualFilePath) == 0;
        }
        public static long GetFileSize(string virtualFilePath)
        {
            if (string.IsNullOrEmpty(virtualFilePath))
                return 0;

            string physicalFilePath = GetPhysicalFilePath(virtualFilePath);
            if (!File.Exists(physicalFilePath))
                return 0;

            FileInfo info = new FileInfo(physicalFilePath);
            return info.Length;
        }
        protected static string GetPhysicalFilePath(string virtualFilePath)
        {
            if (!string.IsNullOrEmpty(virtualFilePath))
            {
                virtualFilePath = virtualFilePath.Replace("/", "\\").Trim();
                if (virtualFilePath.StartsWith("\\"))
                    virtualFilePath = virtualFilePath.Substring(1);
                return Path.Combine(HttpRuntime.AppDomainAppPath, virtualFilePath);
            }
            else
                return string.Empty;
        }
        public static string GetBamBooResponseFilePath(object sessionId)
        {
            return "SessionId/" + sessionId + "/Response.FlightBamBoo.txt";
        }
        public static string GetVietJetResponseFilePath(object sessionId)
        {
            return "SessionId/" + sessionId + "/Response.FlightVietJet.txt";
        }
        public static string GetVietNamAirLinesResponseFilePath(object sessionId)
        {
            return "SessionId/" + sessionId + "/Response.FlightVNA.txt";
        }
        public static void WriteToFile(string virtualFilePath, string contentToWrite)
        {
            WriteToPhysicalFile(GetPhysicalFilePath(virtualFilePath), contentToWrite);
        }
        protected static void WriteToPhysicalFile(string physicalFilePath, string contentToWrite)
        {
            CreateFolderIfNotExistByFilePath(physicalFilePath);
            if (physicalFilePath.ToLower().Trim().IndexOf(".xml") > -1 && contentToWrite != null && contentToWrite.Trim() != "" &&
                contentToWrite.IndexOf("<") >= 0)
            {
                var xmlDoc = new XmlDocument();
                var sRead = new StringReader(contentToWrite);
                xmlDoc.Load(sRead);
                xmlDoc.Save(physicalFilePath);
            }
            else
            {
                using (var objWriter = new StreamWriter(physicalFilePath, false, Encoding.Unicode))
                {
                    objWriter.WriteLine(contentToWrite);
                }
            }
        }
        protected static void CreateFolderIfNotExistByFilePath(string filePath)
        {
            filePath = filePath.Replace("/", "\\");
            if (!File.Exists(filePath))
            {
                int temp = filePath.Trim().LastIndexOf("\\");
                string folderPath = filePath.Trim().Substring(0, temp);
                CreateFolderIfNotExistByFolderPath(folderPath);
            }
        }
        protected static void CreateFolderIfNotExistByFolderPath(string folderPathPhysical)
        {
            folderPathPhysical = folderPathPhysical.Replace("/", "\\");
            if (!Directory.Exists(folderPathPhysical))
            {
                Directory.CreateDirectory(folderPathPhysical);
            }
        }
        public static string GetContentFromFile(string virtualFilePath)
        {
            return GetContentFromPhysicalFile(GetPhysicalFilePath(virtualFilePath));
        }
        protected static string GetContentFromPhysicalFile(string physicalFilePath)
        {
            string content = string.Empty;
            if (File.Exists(physicalFilePath))
                content = File.ReadAllText(physicalFilePath);
            return content;
        }
        public static object GetQueryString(string queryName)
        {
            object value = HttpContext.Current.Request.QueryString[queryName];
            if (value == null || value.ToString() == string.Empty)
                value = null;
            else
                value =
                    value.ToString().Replace("'", "").Replace("\"", "").Replace("--", "").ToLower().Replace("select", "")
                        .
                        Replace("update", "").Replace("delete", "").Replace("drop", "").Replace("truncate", "").Replace(
                            ";", "").Replace("xp_", "").Replace("<", "").Replace(">", "");
            return value;
        }
        public static double GetHourDifference(string inputTimeStr)
        {
            string format = "dd/MM/yyyy HH:mm:ss";
            DateTime inputTime = DateTime.ParseExact(inputTimeStr, format, CultureInfo.InvariantCulture);
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime nowVN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
            TimeSpan diff = inputTime - nowVN;
            return diff.TotalHours;
        }
        public static string GetBookingReference(OrderBookingMod rowOrder)
        {
            string temp = "";
            string year = rowOrder.OrderDate?.Year.ToString().Substring(2, 2);
            string month = rowOrder.OrderDate?.Month.ToString();
            if (month.Length == 1)
                month = "0" + month;
            string seq = rowOrder.OrderId.ToString();
            switch (seq.Length)
            {
                case 1:
                    seq = "0000" + seq;
                    break;
                case 2:
                    seq = "000" + seq;
                    break;
                case 3:
                    seq = "00" + seq;
                    break;
                case 4:
                    seq = "0" + seq;
                    break;
            }
            temp = year + month + seq;
            return temp;
        }
        public static bool IsMobileDevice()
        {
            try
            {
                var ua = HttpContext.Current.Request.UserAgent ?? "";

                string[] mobileTokens = {
            "iphone", "android", "phone", "mobile", "ipad", "ipod",
            "blackberry", "windows phone", "opera mini", "opera mobi"
        };

                ua = ua.ToLower();

                return mobileTokens.Any(token => ua.Contains(token));
            }
            catch
            {
                return false;
            }
        }
    }
}
