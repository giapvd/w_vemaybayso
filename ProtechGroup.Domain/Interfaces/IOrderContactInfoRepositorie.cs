using ProtechGroup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IOrderContactInfoRepositorie
    {
        OrderContactlInfoMod Insert(OrderContactlInfoMod entity);
        void Update(OrderContactlInfoMod entity);
        OrderContactlInfoMod GetOrderContactlInfoByOrderId(int orderId);
    }
}
