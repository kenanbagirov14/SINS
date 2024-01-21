using System;

namespace NIS.BLCore.DTO
{
    public class ChildDepartmentDto
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
    }
}
