using System;

namespace ProtechGroup.Domain.Entities
{
  
    public class OrderBaggageMod
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderTravellerId { get; set; }
        public string AirlineOutBound { get; set; }
        public decimal PriceOutBound { get; set; }
        public decimal BasePriceOutBound { get; set; }
        public decimal ServiceOutBound { get; set; }
        public int KgOutBound { get; set; }
        public string AirlineInBound { get; set; }
        public decimal PriceInBound { get; set; }
        public decimal BasePriceInBound { get; set; }
        public decimal ServiceInBound { get; set; }
        public int KgInBound { get; set; }
    }
}
