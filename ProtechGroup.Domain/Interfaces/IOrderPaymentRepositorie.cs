using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderPaymentRepositorie
    {
        OrderPaymentMod Insert(OrderPaymentMod entity);
        void Update(OrderPaymentMod entity);
        OrderPaymentMod GetOrderPayMentByOrderId(int orderId);
    }
}
