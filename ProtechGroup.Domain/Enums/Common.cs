using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain
{
    public enum SegmentType
    {
        NotApply = 1,
        FirstSegmentOutBound = 2,
        FirstSegmentInBound = 3,
    }

    public enum FlightServiceSearch
    {
        Amadeus = 1,
        VietnamAirline = 2,
        Galileo = 3,
        JetStar = 4,
        BamBooAirWays = 6,
        VietjetAir = 7,
        VietjetAirInternational = 8,
        Datacom = 9,
       
    }
    public enum Supplier
    {
        Domestic = 0,
        International = 1
    }
    public enum TripType
    {
        RoundTrip = 1,
        OneWay = 0,
        Bay_Nhieu_Chang = 3,
    }

    public enum WayType
    {
        OutBound = 0,
        InBound = 1,
    }
    public enum TravellerType
    {
        Adult = 1,
        Child = 2,
        Infant = 3
    }
    public enum PaymentMethod
    {
        AtOffice = 0,
        TransferBanking = 1,
        PaymentGatewayMSB =2
    }
    public enum Title
    {
        Ong = 1,
        Ba = 2,
        Anh = 3,
        Chi = 4
    }
    public enum Gender
    {
        Nam = 1,
        Nu =0
    }
    public enum FinnalStatus
    {
        NewOrderCreated = 0,//Đơn hàng vừa tạo
        OrderProcessing = 1,//Đơn hàng đang xử lý
        OrderCompleted = 2,//Đơn hàng đã hoàn thành xử lý
        OrderCancelled = 3,//Đơn hàng đã hủy 
    }
    public enum OrderStep
    {
        PendingAcceptance =0, //Chưa tiếp nhận xử lý
        OrderReceived = 1, //Đã tiếp nhận xử lý
        InProgress = 2, //Đang xử lý
        ProcessingCompleted = 3, //Đã hoàn thành xử lý 
        OrderRefunded = 4 //Đơn hàng đã hoàn cho khách
    }
    public enum CallPaxStatus
    {
        NotYetCalled = 0, //Chưa thực hiện gọi cho khách
        ContactedSuccessfully = 1, //Đã thực hiện gọi cho khách
        CallUnsuccessful =2 //Không gọi được cho khách
    }
    public enum OrderStatus
    {
        Active =0,
        Remove=1
    }
}
