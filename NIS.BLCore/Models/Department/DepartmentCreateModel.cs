namespace NIS.BLCore.Models.Department
{
    public class DepartmentCreateModel
    {
        public string Name { get; set; }
        public int? DepartmentPrefix { get; set; }
        public string Description { get; set; }
        public int? DefaultUserId { get; set; }
        public string Email { get; set; } 
        public int Phone { get; set; }
        public string DepartmentType { get; set; }
    }
}
