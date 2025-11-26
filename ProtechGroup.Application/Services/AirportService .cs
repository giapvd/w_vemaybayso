using System.Collections.Generic;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;

namespace ProtechGroup.Application.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public IEnumerable<AirportView> SearchAirports(string keyword, int maxResults = 10)
        {
            return _airportRepository.SearchAirports(keyword, maxResults);
        }
        public AirportMod GetAirportByCode(string airportCode)
        {
            return _airportRepository.GetAirportByCode(airportCode);
        }
    }
}
