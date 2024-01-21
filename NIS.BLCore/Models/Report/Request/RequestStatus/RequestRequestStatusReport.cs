using System.Collections.Generic;

namespace NIS.BLCore.Models.Report.Request.RequestStatus
{
    public class RequestRequestStatusReport
    {
        public List<RequestByRequestStatusViewModel> Data { get; set; }=new List<RequestByRequestStatusViewModel>();
        public int? TotalCount { get; set; }
    }
}
