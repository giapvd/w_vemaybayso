using System;

namespace ProtechGroup.Infrastructure.Entities
{
    public class OrderRemarkMod
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int RequestId { get; set; }
        public int UserIdCreate { get; set; }
        public string Remark { get; set; }
        public DateTime DateCreate { get; set; }
        public string Url { get; set; }
        public int TodolistId { get; set; }
        public string Domain { get; set; }
    }
}
