using ProtechGroup.Domain.Entities;
using System.Collections.Generic;
namespace ProtechGroup.Application.Interfaces
{
    public interface IAirportService
    {
        IEnumerable<AirportView> SearchAirports(string keyword, int maxResults = 10);
        AirportMod GetAirportByCode(string airportCode);
    }
}
