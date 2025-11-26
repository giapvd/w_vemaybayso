using ProtechGroup.Domain.Entities;


namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderFlightRepository
    {
        OrderFlightMod Insert(OrderFlightMod entity);
        void Update(OrderFlightMod entity);
        OrderFlightMod GetOrderFlightByOrderId(int orderId);
        OrderFlightMod GetOrderFlightBySessionId(int sessionId);
    }
}
