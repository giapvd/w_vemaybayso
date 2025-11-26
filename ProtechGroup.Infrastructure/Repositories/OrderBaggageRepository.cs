using System.Collections.Generic;
using System.Linq;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Contexts;
using System.Data.Entity;
using AutoMapper;
using ProtechGroup.Infrastructure.Entities;
using System;
using System.Data.Entity.Validation;

namespace ProtechGroup.Infrastructure.Repositories
{
    public class OrderBaggageRepository: IOrderBaggageRepository
    {
        private readonly ApplicationDbContext _orderBaggageContext;
        private readonly IMapper _orderBaggagemapper;
        public OrderBaggageRepository(ApplicationDbContext orderBaggageContext, IMapper orderBaggagemapper)
        {
            _orderBaggageContext = orderBaggageContext;
            _orderBaggagemapper = orderBaggagemapper;
        }
        public OrderBaggageMod Insert(OrderBaggageMod entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                var infraEntity = _orderBaggagemapper.Map<OrderBaggage>(entity);
                _orderBaggageContext.OrderBaggages.Add(infraEntity);
                _orderBaggageContext.SaveChanges();
                return entity;
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
        public IEnumerable<OrderBaggageMod> GetOrderBaggaeByOrderId(int orderId)
        {
            var infraEntity = _orderBaggageContext.OrderBaggages.Where(p => p.OrderId == orderId);
            return _orderBaggagemapper.Map<IEnumerable<OrderBaggageMod>>(infraEntity);  
        }
        public OrderBaggageMod GetOrderBaggaeByTravellerId(int traId)
        {
            var infraEntity = _orderBaggageContext.OrderBaggages.FirstOrDefault(p => p.OrderTravellerId == traId);
            return _orderBaggagemapper.Map<OrderBaggageMod>(infraEntity);
        }
    }
}
