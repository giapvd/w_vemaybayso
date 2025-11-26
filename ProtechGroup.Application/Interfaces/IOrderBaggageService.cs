using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IOrderBaggageService
    {
        OrderBaggageMod Insert(OrderBaggageMod entity);
        IEnumerable<OrderBaggageMod> GetOrderBaggaeByOrderId(int orderId);
        OrderBaggageMod GetOrderBaggaeByTravellerId(int traId);
    }
}
