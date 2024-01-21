using System;

namespace NIS.BLCore.Models.Department
{
    public class DepartmentFindModel
    {

        public string DepartmentType { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? DepartmentPrefix { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Email { get; set; } 
        public int? Phone { get; set; } 
        public string Description { get; set; }
        public int? DefaultUserId { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string Alias { get; set; }
    }
}
