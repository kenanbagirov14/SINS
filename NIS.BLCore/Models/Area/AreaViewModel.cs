using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Area
{
    public class AreaViewModel
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public int? AreaPrefix { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int? TotalCount { get; set; }

       
    }
}
