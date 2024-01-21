using NIS.BLCore.Models.CustomerRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.BLCore.Models.Report.Request.CompletedRequest
{
   public class RquestCompletedRespons
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Value { get; set; }
        public RequestViewModel RequestView { get; set; }

    }
}
