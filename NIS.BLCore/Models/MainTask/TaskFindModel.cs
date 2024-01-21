using System;
using System.Collections.Generic;

namespace NIS.BLCore.Models.MainTask
{
    public class TaskFindModel
    {
        public bool AscId { get; set; } = false;
        public int? Id { get; set; }
        public int? GeneratedUserId { get; set; }
        public int? CustomerRequestId { get; set; }
        public List<int?> ExecutorUserId { get; set; }
        public int? InjuryTypeId { get; set; }
        public int? RealInjuryTypeId { get; set; }
        public string Description { get; set; }
        public List<int?> DepartmentId { get; set; }
        public List<int> TaskStatusId { get; set; }
        public List<int?> ProjectId { get; set; }
        public List<string> ProjectName { get; set; }
        public int? RatingId { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public string Note { get; set; }
        public int? Priority { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

        public List<int> AfterHour { get; set; }
    }
}
