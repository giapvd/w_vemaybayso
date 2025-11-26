using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain
{
    public class BlockItem
    {
        public List<GroupFlight> FlightInBounds;
        public List<GroupFlight> FlightOutBounds;
        public bool IsRoundTrip;

        public decimal PricePerPerson;
        public decimal BasePricePerPerson = 0;
        public string RecommendationNumber = "0";
        public decimal Tax;
        public decimal TotalPrice;
        public bool IsHasThuongGia = false;
        public string Currency = "VND";

        public bool IsIntersection(BlockItem blockItem)
        {
            bool isHas = false;
            foreach (var a in FlightOutBounds)
            {
                foreach (var b in blockItem.FlightOutBounds)
                {
                    if (IsEqualGroupFlight(a, b))
                    {
                        isHas = true;
                        break;
                    }
                }
                if (isHas)
                    break;
            }
            if (!isHas)
                return false;
            if (IsRoundTrip)
            {
                isHas = false;
                foreach (var a in FlightInBounds)
                {
                    foreach (var b in blockItem.FlightInBounds)
                    {
                        if (IsEqualGroupFlight(a, b))
                        {
                            isHas = true;
                            break;
                        }
                    }
                    if (isHas)
                        break;
                }
            }
            return isHas;
        }

        private bool IsEqualGroupFlight(GroupFlight a, GroupFlight b)
        {
            if (a.MainAirlineCode.Equals(b.MainAirlineCode) && a.MainFlightNumber.Equals(b.MainFlightNumber) && a.MainDepartureDate.Equals(b.MainDepartureDate) &&
                a.MainArrivalDate.Equals(b.MainArrivalDate))
                return true;
            return false;
        }
    }
}
