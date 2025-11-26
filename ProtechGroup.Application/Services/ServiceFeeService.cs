using System.Collections.Generic;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;

namespace ProtechGroup.Application.Services
{
    public class ServiceFeeService : IServiceFeeService
    {
        private readonly IServiceFeeRepository _serviceFeeRepository;
        public ServiceFeeService (IServiceFeeRepository serviceFeeRepository)
        {
            _serviceFeeRepository = serviceFeeRepository;
        }
        public ServiceFeeMod GetServiceFeeByAgBfdDo(bool isDomestric, int agencyId, int beforeFlightDay)
        {
            return _serviceFeeRepository.GetServiceFeeByAgBfdDo(isDomestric, agencyId, beforeFlightDay);
        }
    }
}
