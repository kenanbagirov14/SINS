using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Task.ExecutorUser
{
    public class TaskExecutorUserReport
    {
        public List<TaskByExecutorUserViewModel> Data { get; set; }=new List<TaskByExecutorUserViewModel>();
        public int? TotalCount { get; set; }
        public TaskByExecutorUserViewModel  TotalData { get; set; }
  }
}
