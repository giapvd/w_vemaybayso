using ProtechGroup.Domain.DTOs;
using System.Threading.Tasks;

namespace ProtechGroup.Application.Interfaces
{
    public interface IVietJetsService
    {
        Task<UserSessionVJ> GetUserSessionsVietJets();
        Task<string> GetAccessTokenVJ();
        Task<RootVietJets[]> GetAlinesVietJets(string strRequest);
        Task<RootBookingVietJet> GetBookingVietJet(string bodypost);
        Task<RootAncillaryOptions[]> GetAncillaryOptions(string bookingKey);
        Task<Companies> GetCompanies();
        Task<RootBokingIFTR> GetBookingInfomation(string pnr);
        Task<RootAgencies> GetAgenciesVJ();
        Task<RootPayMentTransaction> GetPaymentTransaction(string reservationkey, string bodyPost);
    }
}
