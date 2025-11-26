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
    public class OrderFlightSegmentService : IOrderFlightSegmentService
    {
        private readonly IOrderFlightSegmentRepository _orderFlightSegmentRepository;
        public OrderFlightSegmentService(IOrderFlightSegmentRepository orderFlightSegmentRepository)
        {
            _orderFlightSegmentRepository = orderFlightSegmentRepository;
        }
        public OrderFlightSegmentMod Insert(OrderFlightSegmentMod entity)
        {
            return _orderFlightSegmentRepository.Insert(entity);
        }
        public void Update(OrderFlightSegmentMod entity) 
        {
            _orderFlightSegmentRepository.Update(entity);
        }
        public IEnumerable<OrderFlightSegmentMod> GetOrderFlightSegmentByOrderId(int orderId)
        {
            return _orderFlightSegmentRepository.GetOrderFlightSegmentByOrderId(orderId);
        }
        public OrderFlightSegmentMod GetOrderFlightSegmentById(int segId)
        {
            return _orderFlightSegmentRepository.GetOrderFlightSegmentById(segId);
        }
    }
}
