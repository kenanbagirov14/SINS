using System;
using System.Collections.Generic;

namespace NIS.BLCore.DTO
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DepartmentPrefix { get; set; }
        public int? DefaultUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string DepartmentType { get; set; }
        public int? Phone { get; set; }
        public int? CuratorUserId { get; set; }
        public List<DepartmentDto> ChildDepartments { get; set; }
    }
}
