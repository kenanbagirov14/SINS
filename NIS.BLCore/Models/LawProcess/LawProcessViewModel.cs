using NIS.BLCore.DTO;
using System;

namespace NIS.BLCore.Models.LawProcess
{
    public class LawProcessViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string OrderNo { get; set; }
        public DateTime InputDateTime { get; set; }
        public string Court { get; set; }
        public string Judge { get; set; }
        public string Description { get; set; }
        public bool Final { get; set; }
        public double? Amount { get; set; }
        public int CustomerRequestId { get; set; }

        public virtual RequestDto CustomerRequest { get; set; }
    }
}
