using System;
using System.Collections.Generic;
using NIS.BLCore.Models.Attachment;
using NIS.BLCore.Models.Tag;

namespace NIS.BLCore.Models.MainTask
{
    public class TaskCreateModel
    {
        public int? CustomerRequestId { get; set; }
        public int? ExecutorUserId { get; set; }
        public int? MainTaskId { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? InjuryTypeId { get; set; }
        public int? RealInjuryTypeId { get; set; }
        public string Description { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }
        public int? Priority { get; set; }
        public int? GeneratedUserId { get; set; }

        public ICollection<TagCreateModel> Tag { get; set; }
        public ICollection<AttachmentCreateModel> Attachment { get; set; }
    }
}
