using ProtechGroup.Domain.Entities;

namespace ProtechGroup.Domain.Interfaces
{
    public interface ISearchInputRepository
    {
        SearchInputMod Insert(SearchInputMod input);
        void Update(SearchInputMod input);
        void Delete(int id);
        SearchInputMod GetByKeySessionId(int sessionId);
        int GetNextSessionId();
    }
}
