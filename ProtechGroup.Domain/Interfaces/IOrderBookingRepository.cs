using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderBookingRepository
    {
        OrderBookingMod GetOrderBookingByOrderId(int orderId);
        OrderBookingMod Insert(OrderBookingMod entity);
        void Update(OrderBookingMod entity);
    }
}
