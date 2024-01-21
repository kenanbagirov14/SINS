using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Department
{
  public class DepartmentViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? DepartmentPrefix { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
    public string DepartmentType { get; set; }
    public int? DefaultUserId { get; set; }
    public int? TotalCount { get; set; }
    public string Email { get; set; } //TODO add to other models
    public int Phone { get; set; } //TODO add to ohter models
    public int? ParentDepartmentId { get; set; } //TODO add to ohter models
    public string Alias { get; set; } //TODO add to ohter models
    public List<AreaDto> Area { get; set; }
    public List<DepartmentDto> ChildDepartments { get; set; }
    //public List<RequestDto> CustomerRequest { get; set; }
    //public List<RequestTypeDto> CustomerRequestType { get; set; }
    //public EngineerDto Engineer { get; set; }
    //public List<TaskDto> MainTask { get; set; }
  }
}
