using ProtechGroup.Domain.DTOs;
using System.Threading.Tasks;


namespace ProtechGroup.Domain.Interfaces
{
    public interface IVietJetsProvider
    {
        Task<UserSessionVJ> GetUserSessionsVietJets();
        Task<RootVietJets[]> GetAlinesVietJets(string strRequest, string accessToken);
        Task<RootBookingVietJet> GetBookingVietJet(string accessToken, string bodypost);
        Task<RootAncillaryOptions[]> GetAncillaryOptions(string accessToken, string bookingKey);
        Task<Companies> GetCompanies(string accessToken);
        Task<RootBokingIFTR> GetBookingInfomation(string accessToken, string pnr);
        Task<RootAgencies> GetAgenciesVJ(string accessToken);
        Task<RootPayMentTransaction> GetPaymentTransaction(string accessToken, string reservationkey, string bodyPost);

    }
}
