using System.Collections.Generic;
using System.Linq;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Contexts;
using System.Data.Entity;
using AutoMapper;

namespace ProtechGroup.Infrastructure.Repositories
{
    public class ServiceFeeRepository : IServiceFeeRepository
    {
        private readonly ApplicationDbContext _serviceFeeContext;
        private readonly IMapper _serviceFeemapper;
        public ServiceFeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _serviceFeeContext = context;
            _serviceFeemapper = mapper;
        }
        public ServiceFeeMod GetServiceFeeByAgBfdDo(bool isDomestric, int agencyId, int beforeFlightDay)
        {
            var infraEntity = _serviceFeeContext.ServiceFees.FirstOrDefault(x=>x.IsDomestric == isDomestric &&
                                                                                x.AgencyId == agencyId &&
                                                                                x.BeforeFlightDay == beforeFlightDay);
            return _serviceFeemapper.Map<ServiceFeeMod>(infraEntity);
        }
    }
}
