using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Request.Department
{
    public class RequestDepartmentReport
    {
        public List<RequestByDepartmentViewModel> Data { get; set; }=new List<RequestByDepartmentViewModel>();
        public int? TotalCount { get; set; }
    }
}
