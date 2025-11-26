using System.Collections.Generic;
using ProtechGroup.Domain.Entities;

namespace ProtechGroup.Application.Interfaces
{
    public interface ISearchInputService
    {
        SearchInputMod Insert(SearchInputMod entity);
        void Update(SearchInputMod entity);
        void Delete(int id);
        SearchInputMod GetByKeySessionId(int id);
        int GetNextSessionId();
    }   
}
