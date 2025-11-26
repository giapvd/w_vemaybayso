using System.Collections.Generic;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;


namespace ProtechGroup.Application.Services
{
    public class SearchWSHistoryService : ISearchWSHistoryService
    {
        private readonly ISearchWSHistoryRepository _searchWSHistoryRepository;
        public SearchWSHistoryService(ISearchWSHistoryRepository searchWSHistoryRepository)
        {
            _searchWSHistoryRepository = searchWSHistoryRepository;
        }

        public SearchWSHistoryMod GetSearchWSHistoryByAirlineCode(string airlineCode)
        {
           return _searchWSHistoryRepository.GetSearchWSHistoryByAirlineCode(airlineCode);
        }

        public SearchWSHistoryMod Insert(SearchWSHistoryMod entity)
        {
            return _searchWSHistoryRepository.Insert(entity);
        }
    }
}
