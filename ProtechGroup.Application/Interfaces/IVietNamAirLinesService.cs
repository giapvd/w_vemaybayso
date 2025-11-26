using ProtechGroup.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IVietNamAirLinesService
    {
        Task<LoginVNA> GetUserSessionsVNA();
        Task<RootVNA> SearchFlightVietNamAirLines(string postBody);
    }
}
