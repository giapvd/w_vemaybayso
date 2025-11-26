using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Entities
{
    public class PriceBaggageMod
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public int TotalKg { get; set; }
        public decimal Price { get; set; }
        public decimal ServiceFee { get; set; }
        
    }
}
