using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Infrastructure.Setting
{
    public class ApiBambooAirwaysSetting
    {
        public const string urlBamBoo = "https://efc-agency.bambooairways.com/v3";
        public const string x_api_key = "00206043";
        public const string x_api_secret = "a05f18e43db1d29c9584190a05eb7810";
        public const string body_email = "giapvd@protechgroup.vn";
        public const string body_password = "Gi@p&1981";
        public static string body_iata_code = "00206043";
    }
    public class ApiVietJetsSetting
    {
        public const string urlVietjets = "https://apigw-prod.vietjetair.com";//Prod
        public const string username = "OTAAG486276A18KXM";//Prod
        public const string password = "Gi@hwng05T@anh!!06";//Prod 
        public const string apikey = "gMx7ZCX3TiR7XbkyGkCAGRPKi0BOn4aSFFAtTw9sfRkiTuAi";//Prod
    }
    public class ApiVietNamAirLinesSetting
    {
        public const string urlEndpoint = "https://prod-api.muadi.vn";
        public const string User = "jmw_user_api_vmb";
        public const string Password = "63Qu)K2xmQnnaQs%xut(83Jv5MmGxSYsIhp9V7a2M*EsS7G2GxM+z$vd+&eZDUTr";
        public const string SecurityCode = "K2SYDtPJMR(ETWac^HhE8kB3NgkMSm&9V+QbGaEPgrRWBN8ZAnypn^!w2LzFzLtd";
    }
}
