using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Services
{
    public class OrderTravellerService : IOrderTravellerService
    {
        private readonly OrderTravellerRepository _orderTravellerRepository;
        public OrderTravellerService(OrderTravellerRepository orderTravellerRepository)
        {
            _orderTravellerRepository = orderTravellerRepository;
        }
        public OrderTravellerMod Insert(OrderTravellerMod entity)
        {
            return _orderTravellerRepository.Insert(entity);
        }
        public void Update(OrderTravellerMod entity)
        {
            _orderTravellerRepository.Update(entity);
        }
        public IEnumerable<OrderTravellerMod> GetTravellerByOrderId(int orderId)
        {
            return _orderTravellerRepository.GetTravellerByOrderId(orderId);
        }
        public OrderTravellerMod GetTravellerByTravellerId(int travellId)
        {
            return _orderTravellerRepository.GetTravellerByTravellerId(travellId);
        }
    }
}
