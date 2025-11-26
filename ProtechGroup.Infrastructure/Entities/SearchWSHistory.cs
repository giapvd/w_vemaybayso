using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("SearchWSHistory")]
    public class SearchWSHistory
    {
        [Key]
        public int Id { get; set; }
        [StringLength(3000)]
        public string AccessToken { get; set; }
        public DateTime DateTimeBlock { get; set; }
        [StringLength(10)]
        public string AirlineCode { get; set; }
    }
}
