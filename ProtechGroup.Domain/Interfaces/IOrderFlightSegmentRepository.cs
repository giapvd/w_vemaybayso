using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderFlightSegmentRepository
    {
        OrderFlightSegmentMod Insert(OrderFlightSegmentMod entity);
        void Update(OrderFlightSegmentMod entity);
        IEnumerable<OrderFlightSegmentMod> GetOrderFlightSegmentByOrderId(int orderId);
        OrderFlightSegmentMod GetOrderFlightSegmentById(int segId);
    }
}
