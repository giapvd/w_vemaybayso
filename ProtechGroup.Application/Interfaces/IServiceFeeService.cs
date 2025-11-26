using ProtechGroup.Domain.Entities;

namespace ProtechGroup.Application.Interfaces
{
    public interface IServiceFeeService
    {
        ServiceFeeMod GetServiceFeeByAgBfdDo(bool isDomestric, int agencyId, int beforeFlightDay);
    }
}
