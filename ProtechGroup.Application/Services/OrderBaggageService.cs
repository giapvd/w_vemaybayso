using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ProtechGroup.Application.Services
{
    public class OrderBaggageService : IOrderBaggageService
    {
        private readonly IOrderBaggageRepository _orderBaggageRepository;
        public OrderBaggageService(IOrderBaggageRepository orderBaggageRepository)
        {
            _orderBaggageRepository = orderBaggageRepository;
        }
        public OrderBaggageMod Insert(OrderBaggageMod entity)
        {
            return _orderBaggageRepository.Insert(entity);
        }
        public IEnumerable<OrderBaggageMod> GetOrderBaggaeByOrderId(int orderId)
        {
            return _orderBaggageRepository.GetOrderBaggaeByOrderId(orderId);
        }
        public OrderBaggageMod GetOrderBaggaeByTravellerId(int traId)
        {
            return _orderBaggageRepository.GetOrderBaggaeByTravellerId(traId);
        }
    }
}
