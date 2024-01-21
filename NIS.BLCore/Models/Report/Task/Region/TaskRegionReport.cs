using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Task.Region
{
    public class TaskRegionReport
    {
        public List<TaskByRegionViewModel> Data { get; set; }=new List<TaskByRegionViewModel>();
        public int? TotalCount { get; set; }
    }
}
