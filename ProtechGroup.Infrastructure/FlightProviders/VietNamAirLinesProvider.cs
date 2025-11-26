using System;
using ProtechGroup.Infrastructure.Setting;
using ProtechGroup.Infrastructure.HttpClients;
using ProtechGroup.Domain.Interfaces;
using Newtonsoft.Json;
using ProtechGroup.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Security;
using System.Runtime.Remoting.Messaging;

namespace ProtechGroup.Infrastructure.FlightProviders
{
    public class VietNamAirLinesProvider: IVietNamAirLinesProvider
    {
        public async Task<LoginVNA> GetUserSessionsVNA()
        {
            try
            {
                string body = body = "{\"username\": \"" + ApiVietNamAirLinesSetting.User +
                                   "\",\"password\": \"" + ApiVietNamAirLinesSetting.Password +
                                   "\",\"securityCode\": \"" + ApiVietNamAirLinesSetting.SecurityCode + "\"}";
                string urlPost = ApiVietNamAirLinesSetting.urlEndpoint + "/auth/login";
                string response = await ApiClient.PostMethodHttpClientAddHeader(urlPost, body, null);
                return JsonConvert.DeserializeObject<LoginVNA>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gọi API Bamboo: " + ex.Message, ex);
            }
        }
        public async Task<RootVNA> SearchFlightVietNamAirLines(string postBody, string token)
        {
            try
            {
                var headers = new Dictionary<string, string>
                                {
                                    { "Authorization", "Bearer " + token}
                                };
                string urlPost = ApiVietNamAirLinesSetting.urlEndpoint + "/booking/vna/SearchFlight";
                string response = await ApiClient.PostMethodHttpClientAddHeader(urlPost, postBody, headers);
                return JsonConvert.DeserializeObject<RootVNA>(response);
            }
            catch
            {
                return null;
            }

        }
    }
}
