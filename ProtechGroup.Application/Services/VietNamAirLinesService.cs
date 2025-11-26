using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.FlightProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Services
{
    public class VietNamAirLinesService : IVietNamAirLinesService
    {
        private readonly IVietNamAirLinesProvider _vietNamAirLinesProvider;
        private readonly ISearchWSHistoryRepository _searchWSHistoryRepository;
        public VietNamAirLinesService(IVietNamAirLinesProvider vietNamAirLinesProvider, 
                                      ISearchWSHistoryRepository searchWSHistoryRepository)
        {
            _vietNamAirLinesProvider = vietNamAirLinesProvider;
            _searchWSHistoryRepository = searchWSHistoryRepository;
        }
        public async Task<LoginVNA> GetUserSessionsVNA()
        {
            return await _vietNamAirLinesProvider.GetUserSessionsVNA();
        }
        public async Task<RootVNA> SearchFlightVietNamAirLines(string postBody)
        {
            var searchHis = _searchWSHistoryRepository.GetSearchWSHistoryByAirlineCode("VN");
            string accessToken = string.Empty;
            if (searchHis != null && !string.IsNullOrEmpty(searchHis.AccessToken))
            {
                accessToken = searchHis.AccessToken;
            }
            else
            {
                var userVN = await GetUserSessionsVNA();
                var enty = new SearchWSHistoryMod()
                {
                    AccessToken = userVN.accessToken,
                    DateTimeBlock = DateTime.Now.AddMinutes(1),
                    AirlineCode = "VN"
                };
                var tokenAcc = _searchWSHistoryRepository.Insert(enty);
                accessToken = tokenAcc.AccessToken;
            }
            return await _vietNamAirLinesProvider.SearchFlightVietNamAirLines(postBody, accessToken);
        }
    }
}
