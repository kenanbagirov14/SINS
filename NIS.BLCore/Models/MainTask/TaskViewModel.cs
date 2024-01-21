using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.MainTask
{
  public class TaskViewModel
  {
    public int Id { get; set; }
    public int GeneratedUserId { get; set; }
    public int? CustomerRequestId { get; set; }
    public int? ExecutorUserId { get; set; }
    public int? InjuryTypeId { get; set; }
    public int? RealInjuryTypeId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? StartDate { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public int? Priority { get; set; }
    public int? DepartmentId { get; set; }
    public int? TotalCount { get; set; }
    public int? TaskStatusId { get; set; }
    public int? ProjectId { get; set; }
    public int? UpdatedUserId { get; set; }
    public string ProjectName { get; set; }
    public int? MainTaskId { get; set; }

    public virtual UserDto GeneratedUser { get; set; }
    public virtual UserDto ExecutorUser { get; set; }
    public virtual RequestDto CustomerRequest { get; set; }
    public virtual DepartmentDto Department { get; set; }
    public virtual ICollection<TagDto> Tag { get; set; }
    public virtual RatingDto Rating { get; set; }
    public virtual ProjectDto Project { get; set; }
    public virtual TaskStatusDto TaskStatus { get; set; }
    public virtual ICollection<TaskDto> ChildTasks { get; set; }
    public virtual ICollection<CommentDto> Comment { get; set; }
    public virtual ICollection<AttachmentDto> Attachment { get; set; }
    public virtual TaskDto ParentTask { get; set; }
    public virtual InjuryTypeDto InjuryType { get; set; }
    public virtual InjuryTypeDto RealInjuryType { get; set; }
    public virtual List<TaskStatusHistoryDto> TaskStatusHistory { get; set; }
  }
}
