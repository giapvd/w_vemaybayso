using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtechGroup.Domain.Entities;

namespace ProtechGroup.Application.Interfaces
{
    public interface IVietJetsMethod
    {
        FlightResultOutput BuildFlightResultVietJets(RootVietJets[] alineVJ, int countPax, bool isDomestric);
        string GetStringRequestAlinesVietJet(SearchInputMod searchInput);
        GroupFlight GetGroupFlightVietJets(RootVietJets root, int fareId, int wattype, 
                                            int countPax, bool isDomestric);
        List<PriceBreakDownFlight> GetPriceBreakDownFlightVJ(List<FareOption> lFareot, int countPax,
                                                    decimal serviceFee, string flightKey);
        List<Segment> GetListSegmentVJ(List<Flight> flights, string ticketClass, int wayType);
        Task<string> GetBodyPostBookingVietJet(SearchInputMod searchInput, OrderContactlInfoMod orderContactInfo, OrderFlightMod orderFlight,
                                                List<OrderFlightSegmentMod> orderFlightSegment, List<OrderTravellerMod> orderTraveller,
                                                List<OrderBaggageMod> orderBaggage, string payMethod);
    }
}
