using System;
using ProtechGroup.Infrastructure.Setting;
using ProtechGroup.Infrastructure.HttpClients;
using ProtechGroup.Domain.Interfaces;
using Newtonsoft.Json;
using ProtechGroup.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using ProtechGroup.Domain;
using RestSharp;
using System.Net;


namespace ProtechGroup.Infrastructure.FlightProviders
{
    public class VietJetsProvider : IVietJetsProvider
    {
        public async Task<UserSessionVJ> GetUserSessionsVietJets()
        {
            try
            {
                string author = "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(ApiVietJetsSetting.username + 
                                                                                        ":" + ApiVietJetsSetting.password));
                var headers = new Dictionary<string, string>
                            {
                                { "apikey", ApiVietJetsSetting.apikey },
                                { "Authorization", author }
                            };

                string urlPost = ApiVietJetsSetting.urlVietjets + "/flight/userSessions";
                string response = await ApiClient.PostMethodHttpClientAddHeader(urlPost, string.Empty, headers);
                return JsonConvert.DeserializeObject<UserSessionVJ>(response);
            }
            catch
            {
                throw new Exception("Lỗi khi tìm vé!", null);
            }
        }
        public async Task<RootVietJets[]> GetAlinesVietJets(string strRequest, string accessToken)
        {
            try
            {
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey }
                            };
                string strGet = ApiVietJetsSetting.urlVietjets + "/flight/travelOptions" + strRequest;
                string response = await ApiClient.GetMethodHttpClientAddHeader(strGet, headers);
                return JsonConvert.DeserializeObject<RootVietJets[]>(response);
            }
            catch { 
                throw new Exception("Lỗi khi tìm vé!", null);
            }
               
        }
        public async Task<RootBookingVietJet> GetBookingVietJet(string accessToken, string bodypost)
        {
            try
            {
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey },
                                { "Content-Type", "application/json" },
                            };
                string urlPost = ApiVietJetsSetting.urlVietjets + "/flight/reservations";
                string response = await ApiClient.PostMethodHttpClientAddHeader(urlPost, bodypost, headers);
                return JsonConvert.DeserializeObject<RootBookingVietJet>(response);
            }
            catch
            { 
                throw new Exception("Lỗi khi đặt vé!", null); 
            }
        }
        public async Task<RootAncillaryOptions[]> GetAncillaryOptions(string accessToken, string bookingKey)
        {
            try {
                string strGet = ApiVietJetsSetting.urlVietjets + "/flight/ancillaryOptions" + "?bookingKey=" + bookingKey;
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey }
                            };
                string response = await ApiClient.GetMethodHttpClientAddHeader(strGet, headers);
                return JsonConvert.DeserializeObject<RootAncillaryOptions[]>(response);
            }
            catch
            {
                throw new Exception("Lỗi khi đặt vé!", null);
            }
        }
        public async Task<Companies> GetCompanies(string accessToken)
        {
            try
            {
                
                string strGet = ApiVietJetsSetting.urlVietjets + "flight/comPanies";
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey }
                            };
                string response = await ApiClient.GetMethodHttpClientAddHeader(strGet, headers);
                var result = JsonConvert.DeserializeObject<Companies[]>(response);
                return result[0];
            }
            catch
            {
                return null;
            }
        }
        public async Task<RootBokingIFTR> GetBookingInfomation(string accessToken, string pnr)
        {
            try
            {
                string strGet = ApiVietJetsSetting.urlVietjets + "/flight/reservations?reservationLocator=" + pnr;
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey }
                            };
                string response = await ApiClient.GetMethodHttpClientAddHeader(strGet, headers);
                var result = JsonConvert.DeserializeObject<RootBokingIFTR[]>(response);
                return result[0];
            }
            catch
            {
                return null;
            }
        }
        public async Task<RootAgencies> GetAgenciesVJ(string accessToken)
        {
            try
            {
                string strGet = ApiVietJetsSetting.urlVietjets + "/flight/agencies";
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey }
                            };
                string response = await ApiClient.GetMethodHttpClientAddHeader(strGet, headers);
                var result = JsonConvert.DeserializeObject<RootAgencies[]>(response);
                    return result[0];

            }
            catch
            {
                return null;
            }
        }
        public async Task<RootPayMentTransaction> GetPaymentTransaction(string accessToken, string reservationkey, string bodyPost)
        {
            try
            {
                string strPost = ApiVietJetsSetting.urlVietjets + "/flight/reservations/"+ reservationkey + "/paymentTransactions";
                var headers = new Dictionary<string, string>
                            {
                                { "Authorization", "Bearer " + accessToken},
                                { "apikey", ApiVietJetsSetting.apikey }
                            };
                string response = await ApiClient.PostMethodHttpClientAddHeader(strPost, bodyPost, headers);
                return JsonConvert.DeserializeObject<RootPayMentTransaction>(response);
            }
            catch
            {
                return new RootPayMentTransaction();
            }

        }
    }
}
