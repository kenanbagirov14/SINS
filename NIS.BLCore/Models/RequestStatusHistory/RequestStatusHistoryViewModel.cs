using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.RequestStatusHistory
{
    public class RequestStatusHistoryViewModel
    {
        public int Id { get; set; }
        public int CustomerRequestId { get; set; }
        public int RequestStatusId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

        public virtual RequestDto CustomerRequest { get; set; }
        public virtual RequestStatusDto RequestStatus { get; set; }
    }
}
