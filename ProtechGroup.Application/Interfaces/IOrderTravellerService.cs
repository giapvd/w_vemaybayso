using ProtechGroup.Domain.Entities;
using ProtechGroup.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IOrderTravellerService
    {
        OrderTravellerMod Insert(OrderTravellerMod entity);
        void Update(OrderTravellerMod entity);
        IEnumerable<OrderTravellerMod> GetTravellerByOrderId(int orderId);
        OrderTravellerMod GetTravellerByTravellerId(int travellId);
    }
}
