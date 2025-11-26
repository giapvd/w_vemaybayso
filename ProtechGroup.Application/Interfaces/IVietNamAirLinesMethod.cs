using ProtechGroup.Domain.DTOs;
using ProtechGroup.Domain;
using System.Collections.Generic;
using ProtechGroup.Domain.Entities;


namespace ProtechGroup.Application.Interfaces
{
    public interface IVietNamAirLinesMethod
    {
        FlightResultOutput BuildFlightResultVietNamAirLines(RootVNA alineVNA, int countPax, bool isDomestric);
        GroupFlight GetGroupFlightVietNamAirLines(ListAirOptionVNA airOption, int waytype,
                                                         string sesionId, bool isDomestric, int countPax);
        string GetBodyPostSearchFlightVietNamAirLine(SearchInputMod searchInput);
        List<PriceBreakDownFlight> GetPriceBreakDownFlightVN(List<ListFareOptionVNA> fareOpt, decimal serviceFee,
                                                                    int countPax, string sesionId, int airlineOptionId, int flightOptionId);
        List<Segment> GetListChangBayVN(List<ListSegmentVNA> listSegment, string className, int wayType);
    }
}
