using System;
using System.Collections.Generic;


namespace ProtechGroup.Domain
{
    public class FlightResultOutput
    {
        public bool IsError;
        public bool IsErrorException;
        public Exception Exception;
        public bool IsFirstTimeRequest;
        public string ErrorMessageOrigin;
        public string ErrorMessageCustom;
        public string SessionIdDTC;
        public List<Airline> Airlines;
        public List<BlockItem> BlockItems;
        public class Airline : IEquatable<Airline>
        {
            public string AirlineName;
            public string AirlineCode;
            public bool Equals(Airline other)
            {
                if (AirlineCode == other.AirlineCode)
                    return true;
                return false;
            }
        }
        public bool IsDisplayAvgPrice = false;
        public bool IsFlightDomestic = false;
        public bool IsShowPriceFull = false;
    }
}
