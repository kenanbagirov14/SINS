using System;

namespace NIS.BLCore.Models.SourceType
{
    public class SourceTypeFindModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
