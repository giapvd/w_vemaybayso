using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Infrastructure.Entities
{
    [Table("OrderRemark")]
    public class OrderRemark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int RequestId { get; set; }
        public int UserIdCreate { get; set; }
        [StringLength(1500)]
        public string Remark { get; set; }
        public DateTime DateCreate { get; set; }
        [StringLength(200)]
        public string Url { get; set; }
        public int TodolistId { get; set; }
        [StringLength(200)]
        public string Domain { get; set; }
    }
}
