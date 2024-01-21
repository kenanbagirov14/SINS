namespace NIS.BLCore.Models.TaskStatusHistory
{
    public class TaskStatusHistoryUpdateModel
    {
        public int Id { get; set; }
        public int TaskStatusId { get; set; }
        public int MainTaskId { get; set; }
        public string Description { get; set; }
    }
}
