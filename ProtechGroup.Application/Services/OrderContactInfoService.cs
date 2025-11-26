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
    public class OrderContactInfoService : IOrderContactInfoService
    {
        private readonly IOrderContactInfoRepositorie _orderContactInfoRepositorie;
        public OrderContactInfoService(IOrderContactInfoRepositorie orderContactInfoRepositorie)
        {
            _orderContactInfoRepositorie = orderContactInfoRepositorie;
        }

        public OrderContactlInfoMod Insert(OrderContactlInfoMod entity)
        {
            return _orderContactInfoRepositorie.Insert(entity);
        }
        public void Update(OrderContactlInfoMod entity)
        {
            _orderContactInfoRepositorie.Update(entity);
        }
        public OrderContactlInfoMod GetOrderContactlInfoByOrderId(int orderId)
        {
            return _orderContactInfoRepositorie.GetOrderContactlInfoByOrderId(orderId);
        }
    }
}
