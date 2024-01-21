namespace NIS.BLCore.Models.TaskHistory
{
  public class TaskHistoryUpdateModel
  {
    public int Id { get; set; }
    public int? Type { get; set; }
    public int MainTaskId { get; set; }
    public string Description { get; set; }
  }
}
