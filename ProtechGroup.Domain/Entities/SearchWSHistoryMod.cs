using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Domain.Entities
{
    public class SearchWSHistoryMod
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime DateTimeBlock { get; set; }
        public string AirlineCode { get; set; }

    }
}
