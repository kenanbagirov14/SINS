using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.TaskHistory
{
  public class TaskHistoryViewModel
  {
    public int Id { get; set; }
    public int MainTaskId { get; set; }
    public int? UpdatedUserId { get; set; }
    public System.DateTime UpdatedDate { get; set; }
    public int? Type { get; set; }
    public string Description { get; set; }
    public int? TotalCount { get; set; }

    public TaskDto MainTask { get; set; }
  }
}
