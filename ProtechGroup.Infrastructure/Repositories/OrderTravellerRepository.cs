using System;
using System.Collections.Generic;
using System.Linq;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;
using ProtechGroup.Infrastructure.Contexts;
using System.Data.Entity;
using AutoMapper;
using ProtechGroup.Infrastructure.Entities;
using System.Data.Entity.Validation;

namespace ProtechGroup.Infrastructure.Repositories
{
    public class OrderTravellerRepository : IOrderTravellerRepository
    {
        private readonly ApplicationDbContext _orderTravellerContext;
        private readonly IMapper _orderTravellermapper;
        public OrderTravellerRepository(ApplicationDbContext orderTravellerContext, IMapper orderTravellermapper)
        {
            _orderTravellerContext = orderTravellerContext;
            _orderTravellermapper = orderTravellermapper;
        }
        public OrderTravellerMod Insert (OrderTravellerMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _orderTravellermapper.Map<OrderTraveller>(entity);
                _orderTravellerContext.OrderTravellers.Add(infraEntity);
                _orderTravellerContext.SaveChanges();
                return _orderTravellermapper.Map<OrderTravellerMod>(infraEntity); ;
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
        public void Update(OrderTravellerMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = _orderTravellerContext.OrderTravellers.Find(entity.Id);
                if (existingEntity == null)
                    throw new Exception($"OrderBooking with ID {entity.OrderId} not found.");
                _orderTravellermapper.Map(entity, existingEntity);
                _orderTravellerContext.SaveChanges();
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
        public IEnumerable<OrderTravellerMod> GetTravellerByOrderId(int orderId)
        {
            var infraEntity = _orderTravellerContext.OrderTravellers.Where(f => f.OrderId == orderId);
            return _orderTravellermapper.Map<IEnumerable<OrderTravellerMod>>(infraEntity);
        }
        public OrderTravellerMod GetTravellerByTravellerId(int travellId)
        {
            var infraEntity = _orderTravellerContext.OrderTravellers.Find(travellId);
            return _orderTravellermapper.Map<OrderTravellerMod>(infraEntity);
        }
    }
}
