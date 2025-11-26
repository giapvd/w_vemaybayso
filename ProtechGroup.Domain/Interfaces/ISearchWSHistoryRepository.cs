using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface ISearchWSHistoryRepository
    {
        SearchWSHistoryMod GetSearchWSHistoryByAirlineCode(string airlineCode);
        SearchWSHistoryMod Insert(SearchWSHistoryMod entity);
    }
}
