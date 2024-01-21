using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Request.Region
{
    public class RequestRegionReport
    {
        public List<RequestByRegionViewModel> Data { get; set; }=new List<RequestByRegionViewModel>();
        public int? TotalCount { get; set; }
    }
}
