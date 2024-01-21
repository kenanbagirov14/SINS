using System;

namespace NIS.BLCore.Models.Project
{
    public class ProjectFindModel
    {
        public int? Id { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

    }
}
