using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public DepartmentDto Department { get; set; }
    }
}
