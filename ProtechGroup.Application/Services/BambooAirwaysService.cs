using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Services
{
    public class BambooAirwaysService : IBambooAirwaysService
    {
        private readonly IBambooAirwaysProvider _bambooAirwaysProvider;
        private readonly ISearchWSHistoryRepository _searchWSHistoryRepository;

        public BambooAirwaysService(IBambooAirwaysProvider bambooAirwaysProvider,
                                    ISearchWSHistoryRepository searchWSHistoryRepository)
        {
            _bambooAirwaysProvider = bambooAirwaysProvider;
            _searchWSHistoryRepository = searchWSHistoryRepository;
        }

        public async Task<RootLoginBamBoo> GetUserSessionsBamBoo()
        {
            return await _bambooAirwaysProvider.GetUserSessionsBamBoo();
        }
        public async Task<RootBamBoo> GetAlinesBamBoo(string postBody)
        {
            var searchHis = _searchWSHistoryRepository.GetSearchWSHistoryByAirlineCode("QH");
            string accessToken = string.Empty;
            if(searchHis != null && !string.IsNullOrEmpty(searchHis.AccessToken))
            {
                accessToken = searchHis.AccessToken;
            }
            else
            {
                var userQh = await GetUserSessionsBamBoo();
                var enty = new SearchWSHistoryMod()
                {
                    AccessToken = userQh.data.access_token,
                    DateTimeBlock = DateTime.Now.AddMinutes(3),
                    AirlineCode = "QH"
                };
                var tokenAcc = _searchWSHistoryRepository.Insert(enty);
                accessToken = tokenAcc.AccessToken;
            }
            return await _bambooAirwaysProvider.GetAlinesBamBoo(postBody, accessToken);
        }
    }
}
