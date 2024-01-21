using System;

namespace NIS.BLCore.Models.RequestStatusHistory
{
    public class RequestStatusHistoryFindModel
    {
        public int? Id { get; set; }
        public int? CustomerRequestId { get; set; }
        public int? RequestStatusId { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
