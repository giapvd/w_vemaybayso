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
    public class OrderPaymentRepositorie : IOrderPaymentRepositorie
    {
        private readonly ApplicationDbContext _orderPaymentContext;
        private readonly IMapper _orderPaymentmapper;
        public OrderPaymentRepositorie(ApplicationDbContext orderPaymentContext, IMapper orderPaymentmapper)
        {
            _orderPaymentContext = orderPaymentContext;
            _orderPaymentmapper = orderPaymentmapper;
        }
        public OrderPaymentMod Insert(OrderPaymentMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _orderPaymentmapper.Map<OrderPayment>(entity);
                _orderPaymentContext.OrderPayments.Add(infraEntity);
                _orderPaymentContext.SaveChanges();
                return _orderPaymentmapper.Map<OrderPaymentMod>(infraEntity); ;
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
        public void Update(OrderPaymentMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = _orderPaymentContext.OrderPayments.Find(entity.Id);
                if (existingEntity == null)
                    throw new Exception($"Có lỗi xảy ra.");
                _orderPaymentmapper.Map(entity, existingEntity);
                _orderPaymentContext.SaveChanges();
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
        public OrderPaymentMod GetOrderPayMentByOrderId(int orderId)
        {
            var infraEntity = _orderPaymentContext.OrderPayments.FirstOrDefault(f => f.OrderId == orderId);
            return _orderPaymentmapper.Map<OrderPaymentMod>(infraEntity);
        }
    }
}
