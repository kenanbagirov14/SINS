using System;

namespace NIS.BLCore.DTO
{
    public class AreaDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int? AreaPrefix { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
    }
}
