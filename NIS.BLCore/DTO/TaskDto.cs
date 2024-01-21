using System;

namespace NIS.BLCore.DTO
{
  public class TaskDto
  {
    public int? Id { get; set; }
    public int? GeneratedUserId { get; set; }
    public int? CustomerRequestId { get; set; }
    public int? ExecutorUserId { get; set; }
    public int? InjuryTypeId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public int? DepartmentId { get; set; }
    public int? TaskStatusId { get; set; }
    public int? ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int? MainTaskId { get; set; }
    public int? UpdatedUserId { get; set; }
    public int? SourceTypeId { get; set; }
    public DateTime? StartDate { get; set; }
    public string Note { get; set; }
    public int? Priority { get; set; }
    // public TaskStatusDto TaskStatus { get; set; }
  }
}
