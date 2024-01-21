using NIS.BLCore.Models.CustomerRequest;
using NIS.BLCore.Models.MainTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.BLCore.Models.Report.Request.CompletedRequest
{
    public class RequstByCompletedReport
    {
        public List<RquestCompletedRespons> Data = new List<RquestCompletedRespons>();
        public long? TotalCount { get; set; }
    }
}
