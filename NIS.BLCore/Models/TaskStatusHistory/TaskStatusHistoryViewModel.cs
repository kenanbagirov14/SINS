using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.TaskStatusHistory
{
    public class TaskStatusHistoryViewModel
    {
        public int Id { get; set; }
        public int TaskStatusId { get; set; }
        public int MainTaskId { get; set; }
        public int UpdatedUserId { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

        public  TaskDto MainTask { get; set; }
        public  TaskStatusDto TaskStatus { get; set; }
    }
}
