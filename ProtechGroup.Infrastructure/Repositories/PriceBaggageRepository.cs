using System.Collections.Generic;
using System.Linq;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Contexts;
using System.Data.Entity;
using AutoMapper;

namespace ProtechGroup.Infrastructure.Repositories
{
    public class PriceBaggageRepository: IPriceBaggageRepository
    {
        private readonly ApplicationDbContext _priceBaggageContext;
        private readonly IMapper _priceBaggagemapper;
        public PriceBaggageRepository(ApplicationDbContext priceBaggageContext, IMapper priceBaggagemapper)
        {
            _priceBaggageContext = priceBaggageContext;
            _priceBaggagemapper = priceBaggagemapper;
        }
        public IEnumerable<PriceBaggageMod> GetPriceBaggageByAirlineCode(string airLineCode)
        {
            var infraEntity = _priceBaggageContext.PriceBaggages.Where(p => p.Airline == airLineCode)
                                            .OrderBy(p => p.TotalKg);
            return _priceBaggagemapper.Map<IEnumerable<PriceBaggageMod>>(infraEntity);
        }
        public PriceBaggageMod GetBaggageById(int bagId)
        {
            var infraEntity = _priceBaggageContext.PriceBaggages.Find(bagId);
            return _priceBaggagemapper.Map<PriceBaggageMod>(infraEntity);
        }
    }
}
