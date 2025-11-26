using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Entities
{
    public class ServiceFeeMod
    {
        public int Id { get; set; }
        public bool IsDomestric {  get; set; }
        public decimal Price {  get; set; }
        public int AgencyId {  get; set; }
        public int BeforeFlightDay {  get; set; }
        public bool Status {  get; set; }
    }
}
