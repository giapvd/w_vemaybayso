using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System.Collections.Generic;

namespace ProtechGroup.Application.Services
{
    public class PriceBaggageService : IPriceBaggageService
    {
        private readonly IPriceBaggageRepository _priceBaggageRepository;
        public PriceBaggageService(IPriceBaggageRepository priceBaggageRepository)
        {
            _priceBaggageRepository = priceBaggageRepository;
        }
        public IEnumerable<PriceBaggageMod> GetPriceBaggageByAirlineCode(string airLineCode)
        {
            return _priceBaggageRepository.GetPriceBaggageByAirlineCode(airLineCode);
        }
        public PriceBaggageMod GetBaggageById(int bagId)
        {
            return _priceBaggageRepository.GetBaggageById(bagId);
        }
    }
}
