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
    public class SearchWSHistoryRepository : ISearchWSHistoryRepository
    {
        private readonly ApplicationDbContext _searchWSHistoryContext;
        private readonly IMapper _searchWSHistorymapper;
        public SearchWSHistoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _searchWSHistoryContext = context;
            _searchWSHistorymapper = mapper;
        }
        public SearchWSHistoryMod GetSearchWSHistoryByAirlineCode(string airlineCode)
        {
            var infraEntity = _searchWSHistoryContext.SearchWSHistorys.FirstOrDefault(x => x.AirlineCode == airlineCode && x.DateTimeBlock > DateTime.Now);
            return _searchWSHistorymapper.Map<SearchWSHistoryMod>(infraEntity);
        }
        public SearchWSHistoryMod Insert(SearchWSHistoryMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _searchWSHistorymapper.Map<SearchWSHistory>(entity);
                _searchWSHistoryContext.SearchWSHistorys.Add(infraEntity);
                _searchWSHistoryContext.SaveChanges();
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
    }
}
