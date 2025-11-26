using System;
using ProtechGroup.Infrastructure.Setting;
using ProtechGroup.Infrastructure.HttpClients;
using Newtonsoft.Json;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtechGroup.Infrastructure.FlightProviders
{
    public class BambooAirwaysProvider : IBambooAirwaysProvider
    {
        public async Task<RootLoginBamBoo> GetUserSessionsBamBoo()
        {
            try
            {
                var headers = new Dictionary<string, string>
                            {
                                { "x-api-key", ApiBambooAirwaysSetting.x_api_key },
                                { "x-api-secret", ApiBambooAirwaysSetting.x_api_secret }
                            };

                string urlPost = ApiBambooAirwaysSetting.urlBamBoo + "/login";
                string body = "{"
                    + "\"email\":\"" + ApiBambooAirwaysSetting.body_email + "\","
                    + "\"password\":\"" + ApiBambooAirwaysSetting.body_password + "\","
                    + "\"iata_code\":\"" + ApiBambooAirwaysSetting.body_iata_code + "\""
                    + "}";

                string response = await ApiClient.PostMethodHttpClientAddHeader(urlPost, body, headers);

                return JsonConvert.DeserializeObject<RootLoginBamBoo>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gọi API Bamboo: " + ex.Message, ex);
            }

        }
        public async Task<RootBamBoo> GetAlinesBamBoo(string postBody, string token)
        {
            try
            {
                var headers = new Dictionary<string, string>
                                {
                                    { "x-api-key",ApiBambooAirwaysSetting.x_api_key },
                                    { "x-api-secret", ApiBambooAirwaysSetting.x_api_secret},
                                    { "Authorization", "Bearer " + token}
                                };
                string urlPost = ApiBambooAirwaysSetting.urlBamBoo + "/flight-normal/air-availability";
                string response = await ApiClient.PostMethodHttpClientAddHeader(urlPost, postBody, headers);
                return JsonConvert.DeserializeObject<RootBamBoo>(response);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
