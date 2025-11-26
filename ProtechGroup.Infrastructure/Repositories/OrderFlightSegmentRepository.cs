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
    public  class OrderFlightSegmentRepository: IOrderFlightSegmentRepository
    {
        private readonly ApplicationDbContext _orderFlightSegmentContext;
        private readonly IMapper _orderFlightSegmentmapper;
        public OrderFlightSegmentRepository(ApplicationDbContext orderFlightSegmentContext, IMapper orderFlightSegmentmapper)
        {
            _orderFlightSegmentContext = orderFlightSegmentContext;
            _orderFlightSegmentmapper = orderFlightSegmentmapper;
        }
        public OrderFlightSegmentMod Insert(OrderFlightSegmentMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _orderFlightSegmentmapper.Map<OrderFlightSegment>(entity);
                _orderFlightSegmentContext.OrderFlightSegments.Add(infraEntity);
                _orderFlightSegmentContext.SaveChanges();
                return _orderFlightSegmentmapper.Map<OrderFlightSegmentMod>(infraEntity); ;
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
        public void Update(OrderFlightSegmentMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = _orderFlightSegmentContext.OrderFlightSegments.Find(entity.Id);
                if (existingEntity == null)
                    throw new Exception($"OrderBooking with ID {entity.Id} not found.");
                _orderFlightSegmentmapper.Map(entity, existingEntity);
                _orderFlightSegmentContext.SaveChanges();
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
        public IEnumerable<OrderFlightSegmentMod> GetOrderFlightSegmentByOrderId(int orderId)
        {
            var infraEntity = _orderFlightSegmentContext.OrderFlightSegments.Where(f => f.OrderId == orderId);
            return _orderFlightSegmentmapper.Map<IEnumerable<OrderFlightSegmentMod>>(infraEntity);
        }
        public OrderFlightSegmentMod GetOrderFlightSegmentById(int segId)
        {
            var infraEntity = _orderFlightSegmentContext.OrderFlightSegments.Find(segId);
            return _orderFlightSegmentmapper.Map<OrderFlightSegmentMod>(infraEntity);
        }
    }
}
