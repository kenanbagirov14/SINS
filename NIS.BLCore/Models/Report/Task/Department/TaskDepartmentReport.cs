using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Task.Department
{
  public class TaskDepartmentReport
  {
    public List<TaskByDepartmentViewModel> Data { get; set; } = new List<TaskByDepartmentViewModel>();
    public int? TotalCount { get; set; }
    public TaskByDepartmentViewModel TotalData { get; set; }
  }
}
