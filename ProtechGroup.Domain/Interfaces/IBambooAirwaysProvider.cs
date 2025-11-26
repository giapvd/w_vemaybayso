using ProtechGroup.Domain.DTOs;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IBambooAirwaysProvider
    {
        Task<RootLoginBamBoo> GetUserSessionsBamBoo();
        Task<RootBamBoo> GetAlinesBamBoo(string postBody, string token);
    }
}
