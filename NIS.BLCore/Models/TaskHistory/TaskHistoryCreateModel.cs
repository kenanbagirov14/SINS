namespace NIS.BLCore.Models.TaskHistory
{
  public class TaskHistoryCreateModel
  {
    public int MainTaskId { get; set; }
    public int? Type { get; set; }
    public string Description { get; set; }
  }
}
