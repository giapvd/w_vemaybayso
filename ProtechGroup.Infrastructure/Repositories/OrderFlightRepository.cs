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
    public class OrderFlightRepository: IOrderFlightRepository
    {
        private readonly ApplicationDbContext _orderFlightContext;
        private readonly IMapper _orderFlightmapper;
        public OrderFlightRepository(ApplicationDbContext orderFlightContext, IMapper orderFlightmapper)
        {
            _orderFlightContext = orderFlightContext;
            _orderFlightmapper = orderFlightmapper;
        }
        public OrderFlightMod Insert(OrderFlightMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _orderFlightmapper.Map<OrderFlight>(entity);
                _orderFlightContext.OrderFlights.Add(infraEntity);
                _orderFlightContext.SaveChanges();
                return _orderFlightmapper.Map<OrderFlightMod>(infraEntity); ;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Có lỗi xảy ra.");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"Có lỗi xảy ra.");
                    }
                }
                throw;
            }
        }
        public void Update(OrderFlightMod entity) {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = _orderFlightContext.OrderFlights.Find(entity.Id);
                if (existingEntity == null)
                    throw new Exception($"Có lỗi xảy ra.");
                _orderFlightmapper.Map(entity, existingEntity);
                _orderFlightContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Có lỗi xảy ra.");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"Có lỗi xảy ra.");
                    }
                }
                throw;
            }
        }
        public OrderFlightMod GetOrderFlightByOrderId(int orderId)
        {
            var infraEntity = _orderFlightContext.OrderFlights.FirstOrDefault(f => f.OrderId == orderId);
            return _orderFlightmapper.Map<OrderFlightMod>(infraEntity);
        }
        public OrderFlightMod GetOrderFlightBySessionId(int sessionId)
        {
            var infraEntity = _orderFlightContext.OrderFlights.FirstOrDefault(f => f.SessionId == sessionId && f.OrderStatus == 0);
            return _orderFlightmapper.Map<OrderFlightMod>(infraEntity);
        }
    }
}
