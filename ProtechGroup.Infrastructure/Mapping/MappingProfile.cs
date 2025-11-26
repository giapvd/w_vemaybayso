using AutoMapper;
using ProtechGroup.Domain.Entities;
using ProtechGroup.Infrastructure.Entities;

namespace ProtechGroup.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain -> Infrastructure
            CreateMap<SearchInputMod, SearchInput>();
            CreateMap<AirportMod, Airport>();
            CreateMap<SearchWSHistoryMod, SearchWSHistory>();
            CreateMap<OrderBaggageMod, OrderBaggage>();
            CreateMap<OrderBlockFlightMod, OrderBlockFlight>();
            CreateMap<OrderBookingMod, OrderBooking>();
            CreateMap<OrderContactlInfoMod, OrderContactlInfo>();
            CreateMap<OrderFlightMod, OrderFlight>();
            CreateMap<OrderFlightSegmentMod, OrderFlightSegment>();
            CreateMap<OrderPaymentMod, OrderPayment>();
            CreateMap<OrderRemarkMod, OrderRemark>();
            CreateMap<OrderTravellerMod, OrderTraveller>();
            CreateMap<OrderFlightSegmentMod, OrderFlightSegment>();
            CreateMap<OrderPaymentMod, OrderPayment>();

            // Infrastructure -> Domain
            CreateMap<SearchInput, SearchInputMod>();
            CreateMap<Airport, AirportMod>();
            CreateMap<SearchWSHistory, SearchWSHistoryMod>();
            CreateMap<ServiceFee, ServiceFeeMod>();
            CreateMap<PriceBaggage, PriceBaggageMod>();
            CreateMap<OrderBaggage, OrderBaggageMod>();
            CreateMap<OrderBlockFlight, OrderBlockFlightMod>();
            CreateMap<OrderBooking, OrderBookingMod>();
            CreateMap<OrderContactlInfo, OrderContactlInfoMod>();
            CreateMap<OrderFlight, OrderFlightMod>();
            CreateMap<OrderFlightSegment, OrderFlightSegmentMod>();
            CreateMap<OrderPayment, OrderPaymentMod>();
            CreateMap<OrderRemark, OrderRemarkMod>();
            CreateMap<OrderTraveller, OrderTravellerMod>();
            CreateMap<OrderPayment, OrderPaymentMod>();
        }
    }
}
