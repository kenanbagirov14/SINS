using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.CustomerRequestType
{
    public class RequestTypeViewModel
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public int ExecutionDay { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalCount { get; set; }
        public int SourceTypeId { get; set; }
        public int? ParentRequestTypeId { get; set; }
        public string Alias { get; set; }
        public bool? AutoCreateTask { get; set; }


        public virtual RequestTypeDto ParentRequestType { get; set; }
        public DepartmentDto Department { get; set; }
        public SourceTypeDto SourceType { get; set; }
        //public List<RequestDto> CustomerRequest { get; set; }
        public virtual ICollection<RequestTypeDto> ChildRequestTypes { get; set; }
    }
}
