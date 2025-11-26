using System.Collections.Generic;
using ProtechGroup.Domain.Entities;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IAirportRepository
    {
        IEnumerable<AirportView> SearchAirports(string keyword, int maxResults = 10);
        AirportMod GetAirportByCode(string airportCode);
    }
}
