using System;
using System.Collections.Generic;
using NIS.BL.DTO;

namespace NIS.BL.Models.TaskSourceType
{
    public class TaskSourceTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

        // public virtual List<RequestStatusHistoryDto> RequestStatusHistory { get; set; }
    }
}
