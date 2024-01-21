namespace NIS.BLCore.Models.TaskStatusHistory
{
    public class TaskStatusHistoryCreateModel
    {
        public int TaskStatusId { get; set; }
        public int MainTaskId { get; set; }
        public string Description { get; set; }
    }
}
