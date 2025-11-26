using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderTravellerRepository
    {
        OrderTravellerMod Insert(OrderTravellerMod entity);
        void Update(OrderTravellerMod entity);
        IEnumerable<OrderTravellerMod> GetTravellerByOrderId(int orderId);
        OrderTravellerMod GetTravellerByTravellerId(int travellId);
    }
}
