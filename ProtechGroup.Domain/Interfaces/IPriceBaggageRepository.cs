using System.Collections.Generic;
using ProtechGroup.Domain.Entities;


namespace ProtechGroup.Domain.Interfaces
{
    public interface IPriceBaggageRepository
    {
        IEnumerable<PriceBaggageMod> GetPriceBaggageByAirlineCode(string airLineCode);
        PriceBaggageMod GetBaggageById(int bagId);
    }
}
