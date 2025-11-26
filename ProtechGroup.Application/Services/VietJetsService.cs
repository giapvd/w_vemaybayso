using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.FlightProviders;
using System;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Services
{
    public class VietJetsService : IVietJetsService
    {
        private readonly IVietJetsProvider _vietjetsProvider;
        private readonly ISearchWSHistoryRepository _searchWSHistoryRepository;
        public VietJetsService(IVietJetsProvider vietjetsProvider, 
                                ISearchWSHistoryRepository searchWSHistoryRepository)
        {
            _vietjetsProvider = vietjetsProvider;
            _searchWSHistoryRepository = searchWSHistoryRepository;
        }
        public async Task<UserSessionVJ> GetUserSessionsVietJets()
        {
            return await _vietjetsProvider.GetUserSessionsVietJets();
        }
        public async Task<string> GetAccessTokenVJ()
        {
            string resutl = string.Empty;
            var searchHis = _searchWSHistoryRepository.GetSearchWSHistoryByAirlineCode("VJ");
            string accessToken = string.Empty;
            if (searchHis != null && !string.IsNullOrEmpty(searchHis.AccessToken))
            {
                resutl = searchHis.AccessToken;
            }
            else
            {
                var userVj = await GetUserSessionsVietJets();
                var enty = new SearchWSHistoryMod()
                {
                    AccessToken = userVj.accessToken,
                    DateTimeBlock = DateTime.Now.AddMinutes(3),
                    AirlineCode = "VJ"
                };
                var tokenAcc = _searchWSHistoryRepository.Insert(enty);
                resutl = tokenAcc.AccessToken;
            }
            return resutl;
        }
        public async Task<RootVietJets[]> GetAlinesVietJets(string strRequest)
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetAlinesVietJets(strRequest, accessToken);
        }
        public async Task<RootBookingVietJet> GetBookingVietJet(string bodypost)
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetBookingVietJet(accessToken, bodypost);
        }
        public async Task<RootAncillaryOptions[]> GetAncillaryOptions(string bookingKey)
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetAncillaryOptions(accessToken, bookingKey);
        }
        public async Task<Companies> GetCompanies()
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetCompanies(accessToken);
        }
        public async Task<RootBokingIFTR> GetBookingInfomation(string pnr)
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetBookingInfomation(accessToken, pnr);
        }
        public async Task<RootAgencies> GetAgenciesVJ()
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetAgenciesVJ(accessToken);
        }
        public async Task<RootPayMentTransaction> GetPaymentTransaction(string reservationkey, string bodyPost)
        {
            string accessToken = await GetAccessTokenVJ();
            return await _vietjetsProvider.GetPaymentTransaction(accessToken, reservationkey, bodyPost);
        }
    }
}
