using System.Collections.Generic;
using System.Linq;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Contexts;
using System.Data.Entity;
using AutoMapper;


namespace ProtechGroup.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly ApplicationDbContext _ariportContext;
        private readonly IMapper _ariportmapper;
        public AirportRepository(ApplicationDbContext context, IMapper mapper)
        {
            _ariportContext = context;
            _ariportmapper = mapper;
        }

        public IEnumerable<AirportView> SearchAirports(string keyword, int maxResults = 10)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Enumerable.Empty<AirportView>();

            keyword = keyword.ToLower();

            return _ariportContext.Airports
                .Where(a =>a.Active == true &(a.AirportCode.ToLower().Contains(keyword)
                         || a.CityName.ToLower().Contains(keyword)
                         || a.CountryName.ToLower().Contains(keyword)
                         || a.AirportNameEN.ToLower().Contains(keyword)
                         || a.AirportNameVN.ToLower().Contains(keyword)))
                .OrderBy(a => a.OrderDisplay)
                .Take(maxResults)
                 .Select(a => new Domain.Entities.AirportView
                 {
                     AirportCode = a.AirportCode,
                     AirportName = a.AirportNameVN,
                     CityName = a.SuggestNormal,
                     CountryName = a.CountryName
                 })
                .ToList();
        }
        public AirportMod GetAirportByCode(string airportCode)
        {
            var infraEntity = _ariportContext.Airports.FirstOrDefault(x => x.AirportCode == airportCode && x.Active == true);
            return _ariportmapper.Map<AirportMod>(infraEntity);
        }
    }
}