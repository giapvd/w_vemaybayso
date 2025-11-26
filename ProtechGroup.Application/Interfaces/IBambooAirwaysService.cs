using ProtechGroup.Domain.DTOs;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IBambooAirwaysService
    {
        Task<RootLoginBamBoo> GetUserSessionsBamBoo();
        Task<RootBamBoo> GetAlinesBamBoo(string postBody);
    }
}
