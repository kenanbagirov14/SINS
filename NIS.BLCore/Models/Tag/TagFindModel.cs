using System;

namespace NIS.BLCore.Models.Tag
{
    public class TagFindModel
    {
        public int? Id { get; set; }
        public int? MainTaskId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

    }
}
