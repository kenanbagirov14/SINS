using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NIS.BLCore.Models.Attachment;

namespace NIS.BLCore.Models.CustomerRequest
{
  public class RequestUpdateModel
  {
    public int Id { get; set; }
    public int CustomerRequestTypeId { get; set; }
    public int? DepartmentId { get; set; }
    public int? RegionId { get; set; }
    public string CustomerName { get; set; }
    public int? CustomerNumber { get; set; }
    //[StringLength(50)]
    public string ContractNumber { get; set; }

    //[StringLength(50)]
    public string AON { get; set; }
    public string Text { get; set; }
    public DateTime? StartDate { get; set; }
    public string Description { get; set; }
    public string ContactNumber { get; set; }
    public int RequestStatusId { get; set; }
    public string Email { get; set; }
    public int SourceTypeId { get; set; }

    public List<AttachmentCreateModel> Attachment { get; set; }
  }
}
