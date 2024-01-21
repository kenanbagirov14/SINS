using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Request.RequestType
{
    public class RequestRequestTypeReport
    {
        public List<RequestByRequestTypeViewModel> Data { get; set; }=new List<RequestByRequestTypeViewModel>();
        public int? TotalCount { get; set; }
    }
}
