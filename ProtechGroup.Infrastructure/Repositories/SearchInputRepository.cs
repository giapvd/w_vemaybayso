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
    public class SearchInputRepository : ISearchInputRepository
    {
        private readonly ApplicationDbContext _searchInputContext;
        private readonly IMapper _searchInputmapper;
        public SearchInputRepository(ApplicationDbContext context, IMapper mapper)
        {
            _searchInputContext = context;
            _searchInputmapper = mapper;
        }
        public SearchInputMod Insert(SearchInputMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _searchInputmapper.Map<SearchInput>(entity);
                _searchInputContext.SearchInputs.Add(infraEntity);
                _searchInputContext.SaveChanges();
                return _searchInputmapper.Map<SearchInputMod>(infraEntity);
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

        // Cập nhật
        public void Update(SearchInputMod entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                var infraEntity = _searchInputmapper.Map<SearchInput>(entity);
                _searchInputContext.Entry(infraEntity).State = EntityState.Modified;
                _searchInputContext.SaveChanges();
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

        // Xoá
        public void Delete(int id)
        {
            try
            {
                var entity = _searchInputContext.SearchInputs.Find(id);
                if (entity != null)
                {
                    _searchInputContext.SearchInputs.Remove(entity);
                    _searchInputContext.SaveChanges();
                }
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

        public SearchInputMod GetByKeySessionId(int sessionId)
        {
            try
            {
                var infraEntity = _searchInputContext.SearchInputs.FirstOrDefault(x => x.SessionId == sessionId);
                return _searchInputmapper.Map<SearchInputMod>(infraEntity);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Insert OrderBooking failed: {innerMessage}");
            }
        }
        public int GetNextSessionId()
        {
            int maxSessionId = 0;

            if (_searchInputContext.SearchInputs.Any())
            {
                maxSessionId = _searchInputContext.SearchInputs.Max(x => x.SessionId);
            }

            return maxSessionId + 1;
        }

    }
}
