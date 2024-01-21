using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.TaskStatus
{
    public class TaskStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

        public  ICollection<TaskStatusHistoryDto> TaskStatusHistory { get; set; }
    }
}
