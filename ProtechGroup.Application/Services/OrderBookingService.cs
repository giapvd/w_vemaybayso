using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace ProtechGroup.Application.Services
{
    public class OrderBookingService : IOrderBookingService
    {
        private readonly IOrderBookingRepository _orderBookingRepository;
        public OrderBookingService(IOrderBookingRepository orderBookingRepository)
        {
            _orderBookingRepository = orderBookingRepository;
        }
        public OrderBookingMod GetOrderBookingByOrderId(int orderId)
        {
            return _orderBookingRepository.GetOrderBookingByOrderId(orderId);
        }
        public OrderBookingMod Insert(OrderBookingMod entity)
        {
            return _orderBookingRepository.Insert(entity);
        }
        public void Update(OrderBookingMod entity)
        {
            _orderBookingRepository.Update(entity);
        }
    }
}
