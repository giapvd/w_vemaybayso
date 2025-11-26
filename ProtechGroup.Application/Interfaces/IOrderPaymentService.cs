using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IOrderPaymentService
    {
        OrderPaymentMod Insert(OrderPaymentMod entity);
        void Update(OrderPaymentMod entity);
        OrderPaymentMod GetOrderPayMentByOrderId(int orderId);
    }
}
