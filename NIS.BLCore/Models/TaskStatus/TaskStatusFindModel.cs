namespace NIS.BLCore.Models.TaskStatus
{
    public class TaskStatusFindModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
