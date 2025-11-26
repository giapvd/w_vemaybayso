using ProtechGroup.Domain.Entities;


namespace ProtechGroup.Application.Interfaces
{
    public interface IOrderFlightService
    {
        OrderFlightMod Insert(OrderFlightMod entity);
        void Update(OrderFlightMod entity);
        OrderFlightMod GetOrderFlightByOrderId(int orderId);
        OrderFlightMod GetOrderFlightBySessionId(int sessionId);
    }
}
