using System.Collections.Generic;
using ProtechGroup.Domain.Entities;


namespace ProtechGroup.Domain.Interfaces
{
    public interface IServiceFeeRepository
    {
        ServiceFeeMod GetServiceFeeByAgBfdDo(bool isDomestric, int agencyId, int beforeFlightDay);
    }
}
