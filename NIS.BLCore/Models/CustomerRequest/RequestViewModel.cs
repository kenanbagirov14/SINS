using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.RequestEmail;

namespace NIS.BLCore.Models.CustomerRequest
{
  public class RequestViewModel
  {
    public int Id { get; set; }
    public int CustomerRequestTypeId { get; set; }
    public int? DepartmentId { get; set; }
    public int? RegionId { get; set; }
    public int CreatedUserId { get; set; }
    public string CustomerName { get; set; }
    public int? CustomerNumber { get; set; }
    //[StringLength(50, MinimumLength = 5)]
    public string ContractNumber { get; set; }

    public string AON { get; set; }
    public string Text { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    public int? TotalCount { get; set; }
    public int SourceTypeId { get; set; }
    public int RequestStatusId { get; set; }
    public string MailUniqueId { get; set; }

    public RequestTypeDto CustomerRequestType { get; set; }
    public DepartmentDto Department { get; set; }
    public RegionDto Region { get; set; }
    public SourceTypeDto SourceType { get; set; }
    public RequestStatusDto RequestStatus { get; set; }
    public virtual UserDto CreatedUser { get; set; }
    public virtual RatingDto Rating { get; set; }

    public List<TaskDto> MainTask { get; set; }
    public List<AttachmentDto> Attachment { get; set; }
    public List<RequestEmailViewModel> RequestEmail { get; set; }
    //public virtual List<RequestStatusHistoryDto> RequestStatusHistory { get; set; }
  }
}
