using System;
using System.ComponentModel.DataAnnotations;

namespace NIS.BLCore.DTO
{
  public class RequestDto
  {
    public int Id { get; set; }
    public int CustomerRequestTypeId { get; set; }
    public int? DepartmentId { get; set; }
    public int? RegionId { get; set; }
    public string CreatedUserId { get; set; }
    public string CustomerName { get; set; }
    public string ContractNumber { get; set; }

    public string AON { get; set; }
    public int? CustomerNumber { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? UpdatedUserId { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string Description { get; set; }
    public string ContactNumber { get; set; }
    public int RequestSourceTypeId { get; set; }
  }
}
