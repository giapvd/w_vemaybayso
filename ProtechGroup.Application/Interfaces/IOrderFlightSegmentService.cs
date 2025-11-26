using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IOrderFlightSegmentService
    {
        OrderFlightSegmentMod Insert(OrderFlightSegmentMod entity);
        void Update(OrderFlightSegmentMod entity);
        IEnumerable<OrderFlightSegmentMod> GetOrderFlightSegmentByOrderId(int orderId);
        OrderFlightSegmentMod GetOrderFlightSegmentById(int segId);
    }
}
