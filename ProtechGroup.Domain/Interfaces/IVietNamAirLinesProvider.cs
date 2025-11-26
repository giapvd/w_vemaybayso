using ProtechGroup.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Interfaces
{
    public interface IVietNamAirLinesProvider
    {
        Task<LoginVNA> GetUserSessionsVNA();
        Task<RootVNA> SearchFlightVietNamAirLines(string postBody, string token);
    }
}
