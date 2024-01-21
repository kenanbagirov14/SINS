using System;

namespace NIS.BLCore.DTO
{
    public class RequestStatusHistoryDto
    {
        public int Id { get; set; }
        public int CustomerRequestId { get; set; }
        public int RequestStatusId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Description { get; set; }

        //public virtual RequestStatusDto RequestStatus { get; set; }
    }
}
