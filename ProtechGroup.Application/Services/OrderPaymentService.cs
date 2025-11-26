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
    public class OrderPaymentService: IOrderPaymentService
    {
        private readonly IOrderPaymentRepositorie _orderPaymentRepositorie;
        public OrderPaymentService(IOrderPaymentRepositorie orderPaymentRepositorie)
        {
            _orderPaymentRepositorie = orderPaymentRepositorie;
        }
        public OrderPaymentMod Insert(OrderPaymentMod entity)
        {
            return _orderPaymentRepositorie.Insert(entity);
        }
        public void Update(OrderPaymentMod entity)
        {
            _orderPaymentRepositorie.Update(entity);
        }
        public OrderPaymentMod GetOrderPayMentByOrderId(int orderId)
        {
            return _orderPaymentRepositorie.GetOrderPayMentByOrderId(orderId);
        }
    }
}
