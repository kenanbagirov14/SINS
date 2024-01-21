using System;

namespace NIS.BLCore.Models.Area
{
    public class AreaFindModel
    {
        public int? Id { get; set; }
        public int? RegionId { get; set; }
        public string Name { get; set; }
        public int? AreaPrefix { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
