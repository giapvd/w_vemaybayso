using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Services
{
    public class OrderFlightService : IOrderFlightService
    {
        private readonly IOrderFlightRepository _orderFlightRepository;
        public OrderFlightService(IOrderFlightRepository orderFlightRepository)
        {
            _orderFlightRepository = orderFlightRepository;
        }
        public OrderFlightMod Insert(OrderFlightMod entity)
        {
            return _orderFlightRepository.Insert(entity);
        }
        public void Update(OrderFlightMod entity)
        { 
            _orderFlightRepository.Update(entity);
        }
        public OrderFlightMod GetOrderFlightByOrderId(int orderId)
        {
            return _orderFlightRepository.GetOrderFlightByOrderId(orderId);
        }
        public OrderFlightMod GetOrderFlightBySessionId(int sessionId)
        {
            return _orderFlightRepository.GetOrderFlightBySessionId(sessionId);
        }
    }
}
