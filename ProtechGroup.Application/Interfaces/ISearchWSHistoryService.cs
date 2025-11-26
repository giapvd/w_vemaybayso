using ProtechGroup.Domain.Entities;
using System.Collections.Generic;

namespace ProtechGroup.Application.Interfaces
{
    public interface ISearchWSHistoryService
    {
        SearchWSHistoryMod GetSearchWSHistoryByAirlineCode(string airlineCode);
        SearchWSHistoryMod Insert(SearchWSHistoryMod entity);
    }
}
