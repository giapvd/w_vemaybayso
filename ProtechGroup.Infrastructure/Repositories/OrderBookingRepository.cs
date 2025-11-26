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
    public class OrderBookingRepository: IOrderBookingRepository
    {
        private readonly ApplicationDbContext _orderBookingContext;
        private readonly IMapper _orderBookingmapper;
        public OrderBookingRepository(ApplicationDbContext orderBookingContext, IMapper orderBookingmapper)
        {
            _orderBookingContext = orderBookingContext;
            _orderBookingmapper = orderBookingmapper;
        }
        public OrderBookingMod GetOrderBookingByOrderId(int orderId)
        {
            var infraEntity = _orderBookingContext.OrderBookings.FirstOrDefault(x => x.OrderId == orderId);
            return _orderBookingmapper.Map<OrderBookingMod>(infraEntity);
        }
        public OrderBookingMod Insert(OrderBookingMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _orderBookingmapper.Map<OrderBooking>(entity);
                _orderBookingContext.OrderBookings.Add(infraEntity);
                _orderBookingContext.SaveChanges();
                return _orderBookingmapper.Map<OrderBookingMod>(infraEntity); ;
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
        public void Update(OrderBookingMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = _orderBookingContext.OrderBookings.Find(entity.OrderId);
                if (existingEntity == null)
                    throw new Exception($"OrderBooking with ID {entity.OrderId} not found.");
                _orderBookingmapper.Map(entity, existingEntity);
                _orderBookingContext.SaveChanges();
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
    }
}
