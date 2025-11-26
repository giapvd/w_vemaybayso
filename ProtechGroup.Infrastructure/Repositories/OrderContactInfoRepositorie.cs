using AutoMapper;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Contexts;
using ProtechGroup.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Infrastructure.Repositories
{
    public class OrderContactInfoRepositorie : IOrderContactInfoRepositorie
    {
        private readonly ApplicationDbContext _orrderContactInfoContext;
        private readonly IMapper _orderContactInfomapper;
        public OrderContactInfoRepositorie(ApplicationDbContext orrderContactInfoContext, IMapper orderContactInfomapper)
        {
            _orrderContactInfoContext = orrderContactInfoContext;
            _orderContactInfomapper = orderContactInfomapper;
        }
        public OrderContactlInfoMod Insert(OrderContactlInfoMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _orderContactInfomapper.Map<OrderContactlInfo>(entity);
                _orrderContactInfoContext.OrderContactInfos.Add(infraEntity);
                _orrderContactInfoContext.SaveChanges();
                return _orderContactInfomapper.Map<OrderContactlInfoMod>(infraEntity); ;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }

                throw; // ném lại lỗi nếu cần
            }
        }
        public void Update(OrderContactlInfoMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = _orrderContactInfoContext.OrderContactInfos.Find(entity.Id);
                if (existingEntity == null)
                    throw new Exception($"OrderBooking with ID {entity.OrderId} not found.");
                _orderContactInfomapper.Map(entity, existingEntity);
                _orrderContactInfoContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }

                throw; // ném lại lỗi nếu cần
            }
        }
        public OrderContactlInfoMod GetOrderContactlInfoByOrderId(int orderId)
        {
            var infraEntity = _orrderContactInfoContext.OrderContactInfos.FirstOrDefault(f => f.OrderId == orderId);
            return _orderContactInfomapper.Map<OrderContactlInfoMod>(infraEntity);
        }
    }
}
