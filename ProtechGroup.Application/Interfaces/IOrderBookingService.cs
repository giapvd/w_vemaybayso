using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IOrderBookingService
    {
        OrderBookingMod GetOrderBookingByOrderId(int orderId);
        OrderBookingMod Insert(OrderBookingMod entity);
        void Update(OrderBookingMod entity);
    }
}
