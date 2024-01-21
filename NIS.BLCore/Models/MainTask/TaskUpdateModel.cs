using System;
using System.Collections.Generic;
using NIS.BLCore.Models.Tag;
using NIS.BLCore.Models.Rating;
namespace NIS.BLCore.Models.MainTask
{
  public class TaskUpdateModel
  {
    public int Id { get; set; }
    public int? CustomerRequestId { get; set; }
    public int? ExecutorUserId { get; set; }
    public int? InjuryTypeId { get; set; }
    public int? RealInjuryTypeId { get; set; }
    public string Description { get; set; }
    public int? DepartmentId { get; set; }
    public int? TaskStatusId { get; set; }
    public int? ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int? UpdatedUserId { get; set; }
    public int? MainTaskId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Note { get; set; }
    public int? Priority { get; set; }

    public RatingUpdateModel Rating { get; set; }

    public ICollection<TagCreateModel> Tag { get; set; }
  }
}
