using ProtechGroup.Domain;
using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProtechGroup.Application.Interfaces
{
    public interface IMethodService
    {
        Task<FlightResultOutput> GetFlightDomestic(int sessionId);
        GroupFlight GetGroupFlightVNARow(int sessionId, string bookingKey, int wayType);
        GroupFlight GetGroupFlightVJRow(int sessionId, string bookingKey, int wayType);
        GroupFlight GetGroupFlightQHRow(int sessionId, string bookingKey, int wayType);
        Task<List<BookingInfoFlight>> FlightBooking(int orderId, int sessionId, string payMethod);
        Task<RootBookingVietJet> GetBookingVietJet(int orderId, int sessionId, List<OrderFlightSegmentMod> orderFlightSegment, string payMethod);
    }
}
