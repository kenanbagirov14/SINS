using System;

namespace NIS.BLCore.Models.TaskHistory
{
  public class TaskHistoryFindModel
  {
    public int? Id { get; set; }
    public int? MainTaskId { get; set; }
    public int? UpdatedUserId { get; set; }
    public int? Type { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string Description { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}
