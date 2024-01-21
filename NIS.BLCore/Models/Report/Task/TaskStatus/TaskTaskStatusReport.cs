using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Task.TaskStatus
{
    public class TaskTaskStatusReport
    {
        public List<TaskByTaskStatusViewModel> Data { get; set; }=new List<TaskByTaskStatusViewModel>();
        public int? TotalCount { get; set; }
    }
}
