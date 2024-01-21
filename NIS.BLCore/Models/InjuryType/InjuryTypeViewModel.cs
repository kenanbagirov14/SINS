using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.InjuryType
{
    public class InjuryTypeViewModel
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ExecutionDay { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? TotalCount { get; set; }

        public virtual List<TaskDto> MainTask { get; set; }
    }
}
