using System.Collections.Generic;
using ProtechGroup.Application.Interfaces;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Domain.Interfaces;

namespace ProtechGroup.Application.Services
{
    public class SearchInputService : ISearchInputService
    {
        private readonly ISearchInputRepository _searchInputRepository;

        public SearchInputService(ISearchInputRepository searchInputRepository)
        {
            _searchInputRepository = searchInputRepository;
        }

        public SearchInputMod Insert(SearchInputMod entity)
        {
            return _searchInputRepository.Insert(entity);
        }
        public void Update(SearchInputMod entity)
        {
            _searchInputRepository.Update(entity);
        }
        public void Delete(int id)
        {
            _searchInputRepository.Delete(id);
        }
        public SearchInputMod GetByKeySessionId(int sessionId)
        {
            return _searchInputRepository.GetByKeySessionId(sessionId);
        }
        public int GetNextSessionId()
        {
            return _searchInputRepository.GetNextSessionId();
        }
    }
}
