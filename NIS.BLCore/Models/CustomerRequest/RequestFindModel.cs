using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NIS.BLCore.Models.CustomerRequest
{
  public class RequestFindModel
  {
    public int? Id { get; set; }
    public List<int> CustomerRequestTypeId { get; set; }
    public List<int?> DepartmentId { get; set; }
    public List<int?> RegionId { get; set; }
    public int? CreatedUserId { get; set; }
    public string CustomerName { get; set; }
    public int? CustomerNumber { get; set; }
    public string Text { get; set; }
    public string ContractNumber { get; set; }

    public string AON { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public DateTime? CreatedDateFrom { get; set; }
    public DateTime? CreatedDateTo { get; set; }
    public string Description { get; set; }
    public string ContactNumber { get; set; }
    public int? SourceTypeId { get; set; }
    public List<int?> RequestStatusId { get; set; }
    public string Email { get; set; }

    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}
