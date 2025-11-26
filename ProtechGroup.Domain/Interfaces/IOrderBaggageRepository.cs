using System.Collections.Generic;
using ProtechGroup.Domain.Entities;


namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderBaggageRepository
    {
        OrderBaggageMod Insert(OrderBaggageMod entity);
        IEnumerable<OrderBaggageMod> GetOrderBaggaeByOrderId(int orderId);
        OrderBaggageMod GetOrderBaggaeByTravellerId(int traId);
    }
}
